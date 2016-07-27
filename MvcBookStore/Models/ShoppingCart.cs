using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcBookStore.Models
{
    public class ShoppingCart:IShoppingCart
    {
        public string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        //获取购物车唯一id
        public string GetCartId(HttpContextBase c)
        {
            if (c.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(c.User.Identity.Name))
                {
                    c.Session[CartSessionKey] = c.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    c.Session[CartSessionKey] = tempCartId.ToString();
                }   
            }
            ShoppingCartId = c.Session[CartSessionKey].ToString();
            return c.Session[CartSessionKey].ToString();
        }
        public void AddToCart(int bookId)
        {
            using (var db = new MvcBookStoreEntities())
            {
                //获取购物车中的条目
                var cartItem = db.Carts.SingleOrDefault(c=>c.CartId==ShoppingCartId&&c.BookId==bookId);
                if (cartItem == null)
                {
                    //添加新条目
                    cartItem = new Carts
                    {
                        BookId = bookId,
                        CartId = ShoppingCartId,
                        Count = 1,
                        DateCreated = DateTime.Now
                    };
                    db.AddToCarts(cartItem);
                }
                else
                {
                    //现有条目数量加一
                    cartItem.Count++;
                }
                db.SaveChanges();
            }
        }
        public int RemoveFromCart(int id)
        {
            using (var db = new MvcBookStoreEntities())
            {
                //获取购物车中的条目
                var cartItem = db.Carts.SingleOrDefault(cart => cart.RecordId==id );
                int itemCount = 0;
                if (cartItem != null)
                {
                   
                   if( cartItem.Count>1)
                    {
                        cartItem.Count--;
                        itemCount = cartItem.Count;
                       
                    }
                    else
                   {
                       db.DeleteObject(cartItem);
                   } 
                    db.SaveChanges(); 
                }
                return itemCount;
            }   
        }
        //清空购物车
        public void EmptyCart()
        {
            using (var db = new MvcBookStoreEntities())
            {
                var cartItems = db.Carts.Where(cart=>cart.CartId==ShoppingCartId);
                foreach (var cartItem in cartItems)
                {
                    db.DeleteObject(cartItem);
                }
                db.SaveChanges();
             }
 
        }
        //获取购物车列表
        public IList<Carts> GetCartItems()
        {
            using (var db = new MvcBookStoreEntities())
            {
                return db.Carts.Include("Books").Where(cart => cart.CartId == ShoppingCartId).ToList();
            }
        }
        //获取购物总数
        public int GetCount()
        {
            using (var db = new MvcBookStoreEntities())
            {
                //查询每件物品的数量并求和
                int? count = (
                    from cartItems in db.Carts
                    where cartItems.CartId == ShoppingCartId
                    select (int?)cartItems.Count
                    ).Sum();
                return count ?? 0;//如果空值返回0
            }
 
        }
        //获取购物总价
        public decimal GetTotal() 
        {
            using (var db = new MvcBookStoreEntities())
            {
                //查询购物车中德每件物品乘以数量价格再求和
                decimal? total = (from cartItems in db.Carts
                                  where cartItems.CartId == ShoppingCartId
                                  select (int?)cartItems.Count
                                  * cartItems.Books.Price
                                    ).Sum();
                return total ?? decimal.Zero;
 
            }
        }
        //创建订单
        public int CreateOrder(Orders order)
        {
            decimal orderTotal = 0;
            var cartItems = GetCartItems();
            using (var db = new MvcBookStoreEntities())
            {
                //将全部购物车商品加入到表
                foreach (var item in cartItems)
                {
                    var orderDetail = new OrderDetails
                    {
                        BookId = item.BookId,
                        OrderId = order.OrderId,
                        UnitPrice = item.Books.Price,
                        Quantity = item.Count
                    };
                    //计算商品总价
                    orderTotal+=(item.Count*item.Books.Price);
                    db.AddToOrderDetails(orderDetail);
                }
                //设置订单总价
                order.Total = orderTotal;
                db.SaveChanges();
            }
            //清空购物车
            EmptyCart();
            //返回订单id
            return order.OrderId;
        }
        //根据RecordId获取书籍
        public Books GetBookByRecordId(int id)
        {
            using (var db = new MvcBookStoreEntities())
            {
                var r = db.Carts.Include("Books").Single(c => c.RecordId == id);
                return r.Books;
            }
        }
        //将当前购物车物品转移到用户
        public void MigrateCart(string userName)
        {
            using (var db = new MvcBookStoreEntities())
            {
                var shoppingCart = db.Carts.Where(c => c.CartId == ShoppingCartId);
                foreach (Carts item in shoppingCart)
                {
                    item.CartId = userName;
                }
                db.SaveChanges();
            }
        }
    }
}