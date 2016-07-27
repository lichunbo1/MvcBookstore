using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBookStore.Filters;
using MvcBookStore.Models;
namespace MvcBookStore.Controllers
{
    public class HomeController : Controller
    {
         IBookRepository _bookRepository;
         public HomeController()
            {
                _bookRepository = new BookRepository();
            }
        public ActionResult Index()
        {
            
           //获取最畅销的书
            var books = _bookRepository.GetTopSellingBooks(5);
            return View(books);
        }



    }
}
