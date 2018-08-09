using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PC_Shop.Models
{
	public class User
	{
		/// <summary>
		/// Grabs the information to register the user
		/// </summary>
		[Required]
		public long UserID { get; set; }
		[Required]
		[MinLength(6)]
		[MaxLength(50)]
		public string Username { get; set; }
		/// <summary>
		/// Takes in thre parameters to make sure the pass word is what we want.
		/// First one we make required and will return an error message
		/// Second we set the length it must be
		/// Third we set the maxium it can possibly be.
		/// Fourth we make sure that they have at least one number in their password and send back an error message if they dont
		/// </summary>
		[Required]
		[MaxLength(15)]
		[MinLength(8)]
		[RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{2,})$")]
		public string Password { get; set; }
		//Get our name
		[Required]
		public string Name { get; set; }
		//Check if in register wether theyve clicked a buyer or a seller.
		[Required]
		public bool IsBuyer { get; set; }
		//Make sure their role is in between 1 and 3 if not send them the form again.
		[Range(0,3)]
		public long Role { get; set; }
	//Well give them a role name later on.
		public string RoleName { get; set; }
		//We set email adress to required and add a field to make sure it is a valid input.
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string Country { get; set; }
	}
}