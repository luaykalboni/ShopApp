using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double TotalPrice { get; set; } = 0;

        //many <-> many : Cart <-> Product 
        public List<CartLine> lines { get; set; }

        public int getProductQty()
        {
            int count = 0;
            this.lines.ForEach(l =>
            {
                count += l.qty;
            });
            return count;
        }

        public double getTotalPrice()
        {
            double sum = 0;
            this.lines.ForEach(l =>
            {
                sum += l.total_price_line;
            });
            return sum;
        }
    }
}
