using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace E_ticaret.DataAccess
{
    public class Category
    {
        public int Id { get; set; }

        //Data Annotations
        [DisplayName("Kategori Adı")]
        [StringLength(20,ErrorMessage ="En fazla 20 karakter girebilirsiniz.")]
        public string Name { get; set; }
        [DisplayName("Açıklama")]
        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}
