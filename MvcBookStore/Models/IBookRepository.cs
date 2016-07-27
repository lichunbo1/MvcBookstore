using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcBookStore.Models
{
    public interface IBookRepository
    {
        IList<Books> GetTopSellingBooks(int count);
        Books GetBookById(int id);//根据id获取书籍
        void DeleteBookById(int id);//根据id删除书籍
        void UpdateBook(Books book);
        void AddToBooks(Books book);
        IList<Books> GetAllBooks();//获取全部书籍

    }
}