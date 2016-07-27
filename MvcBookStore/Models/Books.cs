using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcBookStore.Models
{
    [MetadataType(typeof(BooksMetadata))]
    public partial class Books
    {
        private class BooksMetadata
        {
            [Required(ErrorMessage = "必须填写书名！")]
            public string Title { get; set; }
            [ScaffoldColumn(false)]
            [Required(ErrorMessage = "必须填写价格！")]
            [RegularExpression(@"\d+(\.){0,1}\d{0,2}", ErrorMessage = "请输入正确的格式！！")]
            public decimal Price { get; set; }
            [Required(ErrorMessage = "必须填写作者！")]
            public string Authors { get; set; }
        }
    }
}