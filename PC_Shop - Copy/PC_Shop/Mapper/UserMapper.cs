using PC_Shop.Models;
using PC_Shop_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PC_Shop.Mapper
{
	public class UserMapper
	{
		public User MapDOtoPO(UserDO from)
		{
			User to = new User();
			to.UserID = from.UserId;
			to.Username = from.Username;
			to.Password = from.Password;
			to.Email = from.Email;
			to.RoleName = from.RoleName;
			to.Address = from.Address;
			to.Country = from.Country;

			return to;
		}

		public UserDO MapPOtoDO(User from)
		{
			UserDO to = new UserDO();
			to.UserId = from.UserID;
			to.Username = from.Username;
			to.Email = from.Email;
			to.Password = from.Password;
			to.RoleName = from.RoleName;
			to.Role = from.Role;
			to.Address = from.Address;
			to.Country = from.Country;
			return to;
		}
	}
}