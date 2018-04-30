using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnushkaSales.Model.Models
{
    public class Branch
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Required]
        [Display(Name = "Branch Address")]
        public string BranchAddress { get; set; }

        [Required]
        [Display(Name = "Branch City")]
        public string BranchCity { get; set; }

        [Required]
        [Display(Name = "Branch PinCode")]
        public int BranchPinCode { get; set; }
    }
}
