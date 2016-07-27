using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBookStore.Models
{

    [MetadataType(typeof(OrdersMetadata))]
    public partial class Orders
    {
        [Bind(Exclude = "OrderId")]
        private class OrdersMetadata
        {
            [ScaffoldColumn(false)]
            public int OrderId { get; set; }
            [ScaffoldColumn(false)]
            public System.DateTime OrderDate { get; set; }
            [ScaffoldColumn(false)]
            public string Username { get; set; }
            [Required(ErrorMessage = "姓不能为空！")]
            [DisplayName("姓")]
            [StringLength(160)]
            public string FirstName { get; set; }
            [Required(ErrorMessage = "名不能为空！")]
            [DisplayName("名")]
            [StringLength(160)]
            public string LastName { get; set; }
            [Required(ErrorMessage = "地址不能为空！")]
            [DisplayName("地址")]
            [StringLength(70)]
            public string Address { get; set; }
            [Required(ErrorMessage = "城市不能为空！")]
            [DisplayName("城市")]
            [StringLength(40)]
            public string City { get; set; }
            [Required(ErrorMessage = "省份不能为空！")]
            [DisplayName("省份")]
            [StringLength(40)]
            public string State { get; set; }
            [Required(ErrorMessage = "邮政编码不能为空！")]
            [DisplayName("邮政编码")]
            [StringLength(10)]
            public string PostalCode { get; set; }
            [Required(ErrorMessage = "国家不能为空！")]
            [DisplayName("国家")]
            [StringLength(40)]
            public string Country { get; set; }
            [Required(ErrorMessage = "联系电话不能为空！")]
            [DisplayName("联系电话")]
            [StringLength(24)]
            public string Phone { get; set; }
            [Required(ErrorMessage = "邮件地址不能为空！")]
            [DisplayName("邮件地址")]
            [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
                ErrorMessage = "电子邮件格式不正确！")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            [ScaffoldColumn(false)]
            public decimal Total { get; set; }

        }
    }
}