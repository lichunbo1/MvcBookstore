using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcBookStore.Models
{
    public interface IShoppingCart
    {
        //存放购物车ID
        string ShoppingCartId { get; set; }
        //获取临时购物车ID
        string GetCartId(HttpContextBase c);
        //添加到购物车，数量加1或删除行
        void AddToCart(int bookId);
        //从购物车删除，数量减1或删除行
        int RemoveFromCart(int bookId);
        //清空购物车
        void EmptyCart();
        //获取购物车列表
        IList<Carts> GetCartItems();
        //获取购物总数
        int GetCount();
        //获取购物总价
        decimal GetTotal();
        //创建订单
        int CreateOrder(Orders order);
        //根据RecordId获取书籍
        Books GetBookByRecordId(int id);
        //将当前购物车物品转移到用户
        void MigrateCart(string userName);

    }
}