using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace MvcBookStore.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        public IList<Categories> GetAllCategories()//获取所有类别
        {
            using (Models.MvcBookStoreEntities db = new MvcBookStoreEntities())
            {
                //获取书籍种类列表
                return db.Categories.ToList();
            }
        }
        public Categories GetCategoriesById(int id)
        {
            using (Models.MvcBookStoreEntities db = new MvcBookStoreEntities())
            {
                //根据id获取类别，并包含该类别全部书籍数据
                return db.Categories.Include("Books").Single(c=>c.CategoryId==id);
            }
 
        }
        public Books GetBooksById(int id)
        {
            using (Models.MvcBookStoreEntities db = new MvcBookStoreEntities())
            {
                //根据id获取书籍，并包含书籍类别书籍
                return db.Books.Include("Categories").Single(b => b.BookId== id);
            }
 
        }

    }
}