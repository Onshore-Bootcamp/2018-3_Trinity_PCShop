using PC_Shop.Models;
using PC_Shop_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PC_Shop.Mapper
{
	public class UpdateUserMapper
	{
		public UpdateUser MapDOtoPO(UserDO from)
		{
			UpdateUser to = new UpdateUser();
			to.UserID = from.UserId;
			to.Email = from.Email;
			to.UserName = from.Username;
			to.RoleID = from.Role;
			to.RoleName = from.RoleName;
			to.RoleDescription = from.RoleDescription;
			return to;
		}
		public UserDO MapPOtoDO(UpdateUser from)
		{
			UserDO to = new UserDO();
			to.UserId = from.UserID;
			to.Username = from.UserName;
			to.Email = from.Email;
			to.Role = from.RoleID;
			to.RoleName = from.RoleName;
			to.RoleDescription = from.RoleDescription;
			return to;
		}
	}
}