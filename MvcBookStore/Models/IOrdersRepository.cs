using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcBookStore.Models
{
     public interface IOrdersRepository
    {
        //添加新订单
        void CreateOrder(Orders order);
        //检查订单所有者
        bool IsOrderOwner(int orderId,string userName);
    }
}
