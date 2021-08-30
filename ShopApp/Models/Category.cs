using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Models
{
    public class Category
    {

        public int ID { get; set; }

        [StringLength(20, MinimumLength = 2)]
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Please enter the name of your category.")]
        [RegularExpression("^[A-Z]+[a-zA-Z ]*$", ErrorMessage = "The name must start with at least one uppercase letter.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Image path")]
        [DataType(DataType.ImageUrl)]
        public string CategoryImage { get; set; }

        public List<Product> products { get; set; }
    }
}
