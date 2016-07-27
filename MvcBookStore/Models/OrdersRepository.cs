using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcBookStore.Models
{
    public class OrdersRepository:IOrdersRepository
    {
        //添加新订单
        public void CreateOrder(Orders order)
        {
            using (MvcBookStoreEntities db = new MvcBookStoreEntities())
            {
                db.AddToOrders(order);
                db.SaveChanges();
            }
        }
        //检查订单所有者
        public bool IsOrderOwner(int orderId, string userName)
        {
            using (MvcBookStoreEntities db = new MvcBookStoreEntities())
            {
                return db.Orders.Any(o=>o.OrderId==orderId&&o.Username==userName);
            }
 
        }
     
    }
}