using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bislerium_cafe.Data.Models
{
    public class Coffee
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Name is Required")]
            [Display(Name = "Name")]
            public string Name { get; set; }


            [Required(ErrorMessage = "Price is Required")]
            [Display(Name = "Price")]
            public string Price { get; set; }

        }
    }

