using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBookStore.Models;

namespace MvcBookStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {

        IShoppingCart _shoppingCart;
        IOrdersRepository _orderRepository;
        const string PromoCode = "FREE";
        public CheckoutController()
        {
            _shoppingCart = new ShoppingCart();
            _orderRepository = new OrdersRepository();
        }

        // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Orders();
            //用控制器的值更新模型实例
            TryUpdateModel(order);
            try
            {
                if (string.Equals(values["PromoCode"], PromoCode, StringComparison.OrdinalIgnoreCase) == false)
                {
                     return View(order);
                }
                else
                {
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;
                    //保存订单
                    _orderRepository.CreateOrder(order);
                    //添加订单商品细节信息
                    _shoppingCart.GetCartId(this.HttpContext);
                    _shoppingCart.CreateOrder(order);
                    return RedirectToAction("Complete", new { id = order.OrderId });
                }
            }
            catch
            {
                //订单错误
                return View("Error");
            }
        }
        public ActionResult Complete(int id)
        {
            //检查用户是否是订单所有者
            bool isValid = _orderRepository.IsOrderOwner(id, User.Identity.Name);
            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
 
        }
        //
        // GET: /Checkout/
        
        public ActionResult Index()
        {
            return View();
        }

    }
}
