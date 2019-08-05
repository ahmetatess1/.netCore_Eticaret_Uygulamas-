using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace E_ticaret.DataAccess
{
    public class Product
    {
        public int id { get; set; }
        [DisplayName("Ürün Adı")]
        public string Name { get; set; }
        [DisplayName("Açıklama")]
        public string Description { get; set; }
        [DisplayName("Fiyatı")]
        public double Price { get; set; }
        [DisplayName("Stok")]
        public int Stock { get; set; }
        public bool IsHome { get; set; }

        public string Image{ get; set; }
        public bool IsApproved { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
