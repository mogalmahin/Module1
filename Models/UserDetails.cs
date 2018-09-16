using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CarPool.Models
{
    public class UserDetails
    {
		[Required]
		[StringLength(4)]
		public String VAMID { get; set; }
		[Required]
		[StringLength(20,MinimumLength = 4)]
		public String Name { get; set; }
		[Required]
	
        public int Phone_Number { get; set; }
		[Required]
		[RegularExpression ("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Password should contain 1letter,1captial and 1 small ,length 4-8")]
		public string Password { get; set; }
		[Required(ErrorMessage = "Confirmation Password is required.")]
		[Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
		public string ConfirmPassword { get; set; }
	}
}