using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PC_Shop.Models
{
	public class UpdateUser
	{/// <summary>
	/// Sets the properties to read and display for updating the user
	/// </summary>
		[Required]
		[MinLength(6)]
		[MaxLength(50)]
		public string UserName { get; set; }
		[Required]
		[Range(0, 3)]
		public long RoleID { get; set; }
		[EmailAddress]
		[Required]
		public string Email { get; set; }
		[Required]
		public long UserID { get; set; }
		
		public string RoleName { get; set; }
		
		public string RoleDescription { get; set; }

	}
}