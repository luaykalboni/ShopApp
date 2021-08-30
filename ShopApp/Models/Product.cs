using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20, MinimumLength = 5)]
        [Display(Name = "Product Name")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter the name of your product.")]
        [RegularExpression("^[A-Z]+[a-zA-Z ]*$", ErrorMessage = "The name must start with at least one uppercase letter.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Product must have a price")]
        [Range(0, 1000)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [StringLength(200, MinimumLength = 0)]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter the image url of your product.")]
        [Display(Name = "Image path")]
        [DataType(DataType.ImageUrl)]
        public string imagePath { get; set; }
        
        public bool IsinStock { get; set; }

        //one -> many : Category -> Product
        [Display(Name = "Category Id")]
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        //many <-> many : Cart <-> Product
        public List<Cart> Carts { get; set; }
    }
}
