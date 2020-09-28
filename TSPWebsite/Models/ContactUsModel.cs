using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TSPWebsite.Models
{
    public class ContactUsModel
    {
        [Required(ErrorMessage = "Please enter your name.")]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Please enter your email.")]
        public string Email { get; set; }
        [StringLength(5000)]
        [Required(ErrorMessage = "Please enter your message.")]
        public string Message { get; set; }
    }
}
