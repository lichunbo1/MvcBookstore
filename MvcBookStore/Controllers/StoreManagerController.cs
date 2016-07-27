using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBookStore.Models;
namespace MvcBookStore.Models
{
    [Authorize(Roles = "Admin")]
    public class StoreManagerController : Controller
    {
        //库模式数据库访问实列
        IBookRepository _bookRepository;
        ICategoryRepository _categoryRepository;
        public StoreManagerController()
        {
            //初始化数据库访问实列
            _bookRepository=new BookRepository();
            _categoryRepository = new CategoryRepository();

        }
        //private MvcBookStoreEntities db = new MvcBookStoreEntities();

        //
        // GET: /StoreManager/

        public ActionResult Index()
        {
            //获取全部书籍数据
            var books = _bookRepository.GetAllBooks();
            return View(books.ToList());
        }

        //
        // GET: /StoreManager/Details/5

        public ActionResult Details(int id = 0)
        {
            //根据id找到书籍
            var books = _bookRepository.GetBookById(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        //
        // GET: /StoreManager/Create

        public ActionResult Create()
        {
            //为下拉列表准备的类别数据
            ViewBag.CategoryId = new SelectList(_categoryRepository.GetAllCategories(), "CategoryId", "Name");
            return View();
        }

        //
        // POST: /StoreManager/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Books books)
        {
            if (ModelState.IsValid)
            {
                //添加新书籍
                _bookRepository.AddToBooks(books);
                
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_categoryRepository.GetAllCategories(), "CategoryId", "Name", books.CategoryId);
            return View(books);
        }

        //
        // GET: /StoreManager/Edit/5

        public ActionResult Edit(int id = 0)
        {
            //根据id获取书籍
            Books books = _bookRepository.GetBookById(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(_categoryRepository.GetAllCategories(), "CategoryId", "Name", books.CategoryId);
            return View(books);
        }

        //
        // POST: /StoreManager/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Books books)
        {
            if (ModelState.IsValid)
            {
                //更新书籍数据
                _bookRepository.UpdateBook(books);
                return RedirectToAction("Index");
            }
            //为下拉列表准备的类别数据
            ViewBag.CategoryId = new SelectList(_categoryRepository.GetAllCategories(), "CategoryId", "Name", books.CategoryId);
            return View(books);
        }

        //
        // GET: /StoreManager/Delete/5

        public ActionResult Delete(int id = 0)
        {

            //根据id获取书籍
            Books books = _bookRepository.GetBookById(id);
            if (books == null)
            {
                return HttpNotFound();
            }
            return View(books);
        }

        //
        // POST: /StoreManager/Delete/5

        //[HttpPost, ActionName("Delete")]
        ////[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    //Books books = db.Books.Single(b => b.BookId == id);
        //    //db.Books.DeleteObject(books);
        //    //db.SaveChanges();
        //    //根据id删除书籍
        //    _bookRepository.DeleteBookById(id);
        //    return RedirectToAction("Index");
        //}

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //根据ID删除书籍
            _bookRepository.DeleteBookById(id);
            return RedirectToAction("Index");
        }
    }
}