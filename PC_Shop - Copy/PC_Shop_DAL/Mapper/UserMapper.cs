
using PC_Shop_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Shop_DAL.Mapper
{
	class UserMapper
	{
		public UserDO MapReadToSingle(SqlDataReader reader)
		{
			UserDO result = new UserDO();
			if (reader["UserID"] != DBNull.Value)
			{
				result.UserId = (long)reader["UserID"];
			}
			if (reader["UserName"] != DBNull.Value)
			{
				result.Username = (string)reader["UserName"];
			}
			if (reader["Pass"] != DBNull.Value)
			{
				result.Password = (string)reader["Pass"];
			}
			if (reader["RoleID"] != DBNull.Value)
			{
				result.Role = (long)reader["RoleID"];
			}
			if (reader["Email"] != DBNull.Value)
			{
				result.Email = (string)reader["Email"];
			}
			if (reader["Address"] != DBNull.Value)
			{
				result.Address = (string)reader["Address"];
			}
			if (reader["Country"]!=DBNull.Value)
			{
				result.Country = (string)reader["Address"];
			}
			return result;
		}
		public UserDO Roles(SqlDataReader reader)
		{
			UserDO userDO = new UserDO();
			if (reader["RoleID"] != DBNull.Value)
			{
				userDO.Role = (long)reader["RoleID"];
			}

			if (reader["RoleName"] != DBNull.Value)
			{
				userDO.RoleName = (string)reader["RoleName"];
			}
			if (reader["RoleDescription"] != DBNull.Value)
			{
				userDO.RoleDescription = (string)reader["RoleDescription"];
			}
			return userDO;

		}
	}
}
