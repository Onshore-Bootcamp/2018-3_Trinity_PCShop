using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Shop_DAL.Models
{
	public class UserDO
	{
		public long UserId { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		
		public string Email { get; set; }
		public long Role { get; set; }
		public string RoleName { get; set; }
		public string Address { get; set; }
		public string Country { get; set; }
	
		public string RoleDescription { get; set; }
	}
}
