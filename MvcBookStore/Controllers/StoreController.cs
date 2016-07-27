using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBookStore.Models;
namespace MvcBookStore.Controllers
{
    public class StoreController : Controller
    {
        //GET:/Store/CategoryMenu
        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            return PartialView(_categoryRepository.GetAllCategories());
        }

        ICategoryRepository _categoryRepository;
        public StoreController()
        {
            _categoryRepository = new CategoryRepository();
        }

        //
        // GET: /Store/
        public ActionResult Index()
        {
            return View(_categoryRepository.GetAllCategories());
        }
        //
        // GET: /Store/Browse
        public ActionResult Browse(int id)
        {
            //根据类别id获取书籍
            return View(_categoryRepository.GetCategoriesById(id));
        }
        //
        // GET: /Store/Details
        public ActionResult Details(int id)
        {
            //根据书籍id获取获取详细书籍信息
            return View(_categoryRepository.GetBooksById(id));
        }



    }
}
