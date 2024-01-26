using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bislerium_cafe.Data.Models
{
    public class Addins
    {

        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "The Name is Required")]  // Required attribute ensures that this Name field is mandatory.
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")] // Similar annotations for Last Name.
        public string Price { get; set; }

    }
}
