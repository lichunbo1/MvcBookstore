using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcBookStore.Models;

namespace MvcBookStore.ViewModel
{
    public class ShoppingCartViewModel
    {
        public IList<Carts> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}