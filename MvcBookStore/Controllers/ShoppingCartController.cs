using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBookStore.ViewModel;
using MvcBookStore.Models;

namespace MvcBookStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        //
        // GET: /ShoppingCart/
        IShoppingCart _shoppingCart;
        IBookRepository _bookRepository;
        public ShoppingCartController()
        {
            _shoppingCart = new ShoppingCart();
            _bookRepository = new BookRepository();
        }

        public ActionResult Index()
        {
            _shoppingCart.GetCartId(this.HttpContext);
            //初始化视图模型
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = _shoppingCart.GetCartItems(),
                CartTotal = _shoppingCart.GetTotal()
            };
            return View(viewModel);
        }
        public ActionResult AddToCart(int id)
        {
            _shoppingCart.GetCartId(this.HttpContext);
            //确定id是否有效
            Books b = _bookRepository.GetBookById(id);
            if (b != null)
            {
                _shoppingCart.AddToCart(id);
            }
            //回到购物车首页
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            _shoppingCart.GetCartId(this.HttpContext);
            //获取书籍信息返回确认消息
            Books b = _shoppingCart.GetBookByRecordId(id);
            //从购物车删除
            int itemCount = _shoppingCart.RemoveFromCart(id);
            //确认显示消息
            var results=new ShoppingCartRemoveViewModel
            {
                Message=Server.HtmlEncode("《"+b.Title+"》")+"已经从购物车中删除",
                CartTotal=_shoppingCart.GetTotal(),
                CartCount=_shoppingCart.GetCount(),
                ItemCount=itemCount,
                DeleteId=id

            };
            return Json(results);
        }
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            _shoppingCart.GetCartId(this.HttpContext);
            ViewData["CartCount"]=_shoppingCart.GetCount();
            return PartialView("CartSummary");
        }
    }
}
