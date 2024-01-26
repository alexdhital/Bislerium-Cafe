﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bislerium_cafe.Data.Models
{
    public class MemberModel
    {

        public Guid Id { get; set; } = Guid.NewGuid();
   
        public string Name { get; set; }

        [Required(ErrorMessage = "Phonenumber is Required")]
        [Display(Name = "Phonenumber"),UIHint("Phonenumber")]

        public string Phonenumber { get; set; }
        public string Count { get; set; }

    }
}

