using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bislerium_cafe.Data.Models
{
   public class OrderModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();


        [Required(ErrorMessage = "Quantity is required")] // Similar annotations for Last Name.
        public string Quantity { get; set; }

        public string Price { get; set; }
        public string TotalPrice { get; set; }
        public string Username { get; set; }
        [Required(ErrorMessage = "Phonenumber is required")] // Similar annotations for Last Name.

        public string Phonenumber { get; set; }

        public bool IsPaid {  get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime PaidDate { get; set; }
        public List<Addins> Addins { get; set; }
        public Coffee Coffee{ get; set; }

        public string Discount  { get; set; }
        public string FinalPrice { get; set; }




    }
}
