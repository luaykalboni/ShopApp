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
        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl)]
        public string imagePath { get; set; }
        
        public bool IsinStock { get; set; }

        //one -> many : Category -> Product
        [Display(Name = "Category")]
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        //many <-> many : Cart <-> Product
        public List<CartLine> Carts { get; set; }
    }

    public class CartLine
    {
        public int id { get; set; }
        public int productId { get; set; }
        public Product product{ get; set; }

        public int cartId { get; set; }
        public Cart cart{ get; set; }
        public int qty { get; set; }
        public double total_price_line { get; set; }
    }
}
