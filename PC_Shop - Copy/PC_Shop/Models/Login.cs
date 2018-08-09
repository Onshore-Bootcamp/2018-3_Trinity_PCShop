using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PC_Shop.Models
{
	public class Login
	{
		/// <summary>
		/// Grabs the information to log the register user into the application 
		/// </summary>
		/// We set te Username to have a min and max value possible and make it required
		[Required]
		[MaxLength(50)]
		[MinLength(6)]
		public string UserName { get; set; }
		/// <summary>
		/// Takes in thre parameters to make sure the pass word is what we want.
		/// First one we make required and will return an error message
		/// Second we set the length it must be
		/// Third we set the maxium it can possibly be.
		/// Fourth we make sure that they have at least one number in their password and send back an error message if they dont
		/// </summary>
		[Required]
		[MinLength(8)]
		[MaxLength(15)]
		[RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{2,})$",ErrorMessage ="Please input your password with a number.")]
		public string Password { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}