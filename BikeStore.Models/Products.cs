using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BikeStore.Models
{
    [Table("products", Schema = "production")]
    public class Products
    {
        [Key]
        [DisplayName("Product Id")]
        public int product_id { get; set; }
        [Required]
        [DisplayName("Product Name")]
        public string? product_name { get; set; }
        [Required]
        public int? brand_id { get; set;}
        [Required]
        public int? category_id { get; set;}
        [Required]
        public short? model_year { get; set;}
        
        [Required]
        [Display(Name = "List Price")]
        [Range(1,1000)]
        public decimal list_price{ get;set; }
        [ValidateNever]
        public string ImageUrl {  get; set; }
    }
}
