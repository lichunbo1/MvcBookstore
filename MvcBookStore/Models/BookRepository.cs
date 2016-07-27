using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace MvcBookStore.Models
{
    public class BookRepository : IBookRepository
    {
        //获取最畅销书
        public IList<Books> GetTopSellingBooks(int count)
        {
            using (Models.MvcBookStoreEntities db = new MvcBookStoreEntities())
            {
                //暂时返回全部书籍
                return db.Books.Take(count).ToList();
            }
        }
        public Books GetBookById(int id)
        {
            using (Models.MvcBookStoreEntities db = new MvcBookStoreEntities())
            {
                //根据id返回书籍
                return db.Books.Include("Categories").Single(b=>b.BookId==id);
            }
        }
        public void DeleteBookById(int id)
        {
            using (Models.MvcBookStoreEntities db = new MvcBookStoreEntities())
            {
                //根据id删除书籍
                Books books = db.Books.Single(b => b.BookId == id);
                db.Books.DeleteObject(books);
                db.SaveChanges();
            }
        }
        public void UpdateBook(Books book)
        {
            using (Models.MvcBookStoreEntities db = new MvcBookStoreEntities())
            {
                //跟新书籍
                db.Books.Attach(book);
                db.ObjectStateManager.ChangeObjectState(book,EntityState.Modified);
                db.SaveChanges();
            }
        }
        public void AddToBooks(Books book)
        {
            using (Models.MvcBookStoreEntities db = new MvcBookStoreEntities())
            {
                //添加books表
                db.Books.AddObject(book);
                db.SaveChanges();
               
            }
        }
        public IList<Books> GetAllBooks()
        {
            using (Models.MvcBookStoreEntities db = new MvcBookStoreEntities())
            {
                //暂时返回全部书籍
                return db.Books.Include("Categories").ToList();
            }
        }
    }
}