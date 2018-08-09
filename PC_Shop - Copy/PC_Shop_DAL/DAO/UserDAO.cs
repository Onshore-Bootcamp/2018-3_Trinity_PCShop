using PC_Shop_DAL.Mapper;
using PC_Shop_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PC_Shop_DAL.DAO
{
	public class UserDAO
	{
		
		#region Values needed to pass back and forth
		private readonly string _ConnectionString;
		private static UserMapper _Mapper = new UserMapper();
		private static Logger _Logger = new Logger();

		public UserDAO(string connectionString)
		{
			_ConnectionString = connectionString;
		}
		#endregion

		#region Login and Register

		/// <summary>
		/// We view our users by their user name to log them in
		/// <param name="username"></param>
		/// <returns></returns>
		public UserDO ViewUserByUserName(string username)
		{
			UserDO user = new UserDO();
			try
			{   //Getting together our sql Connections to run a view by users statement.
				using (SqlConnection connection = new SqlConnection(_ConnectionString))
				using (SqlCommand command = new SqlCommand("VIEW_BY_USERNAME", connection))
				{//runs our sql commands to find our username
					command.CommandType = CommandType.StoredProcedure;
					command.CommandTimeout = 60;
					command.Parameters.AddWithValue("@UserName", username);
					connection.Open();
					//A sql reader runs until its done and is gone.
					//It goes through a series of checks to make sure it will not break.
					//While reading each piece of the database.
					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							user.UserId = reader["UserID"] != DBNull.Value ? (long)reader["UserID"] : 0;
							user.Username = reader["UserName"] != DBNull.Value ? (string)reader["UserName"] : null;
							user.Password = reader["Pass"] != DBNull.Value ? (string)reader["Pass"] : null;
							user.RoleName = reader["RoleName"] != DBNull.Value ? (string)reader["RoleName"] : null;
							user.Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : null;
							user.Address = reader["Address"] != DBNull.Value ? (string)reader["Address"] : null;
							user.Country = reader["Country"] != DBNull.Value ? (string)reader["Country"] : null;
						}
					}
				}
			}
			catch (SqlException ex)
			{
				_Logger.ErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
			}
			return user;

		}

		/// <summary>
		/// We take in the user do model 
		/// given to us by a 
		/// mapper to create the
		/// inputs given in the database
		/// </summary>
		/// <param name="userToRegister"></param>
		public void RegisterUser(UserDO userToRegister)
		{
			try
			{//get our command to register a user 
				using (SqlConnection RegisterConnection = new SqlConnection(_ConnectionString))
				using (SqlCommand Registercommand = new SqlCommand("ADD_USER", RegisterConnection))
				{
					Registercommand.CommandType = CommandType.StoredProcedure;
					Registercommand.CommandTimeout = 60;
					Registercommand.Parameters.AddWithValue("@UserName", userToRegister.Username);
					Registercommand.Parameters.AddWithValue("@Pass", userToRegister.Password);
					Registercommand.Parameters.AddWithValue("@Name", userToRegister.Name);
					Registercommand.Parameters.AddWithValue("@Email", userToRegister.Email);
					Registercommand.Parameters.AddWithValue("@RoleID", userToRegister.Role);
					Registercommand.Parameters.AddWithValue("@Address", userToRegister.Address);
					Registercommand.Parameters.AddWithValue("@Country", userToRegister.Country);

					RegisterConnection.Open();
					Registercommand.ExecuteNonQuery();
					RegisterConnection.Close();
				}
			}
			catch (SqlException ex)
			{
				_Logger.ErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);

			}

		}
		#endregion

		#region View and delete CRUD

		
		/// <summary>
		/// We delete a user based on the user ID we were given
		/// </summary>
		/// <param name="userID"></param>
		public void DeleteUser(long userID)
		{
			try
			{
				using (SqlConnection DataSource = new SqlConnection(_ConnectionString))
				using (SqlCommand deleteUser = new SqlCommand("DELETE_USER", DataSource))
				{
					deleteUser.CommandType = CommandType.StoredProcedure;
					deleteUser.CommandTimeout = 60;
					deleteUser.Parameters.AddWithValue("@UserID", userID);

					DataSource.Open();
					deleteUser.ExecuteNonQuery();


				}
			}
			catch (SqlException sqlex)
			{
				_Logger.ErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, sqlex);
			}
		}

		/// <summary>
		/// We view a list of all the users in the database using a stored procedure and commmand.
		/// </summary>
		/// <returns></returns>
		public List<UserDO> ViewUsers()
		{
			List<UserDO> displayUsers = new List<UserDO>();

			try
			{
				using (SqlConnection viewConnection = new SqlConnection(_ConnectionString))
				using (SqlCommand viewUsers = new SqlCommand("VIEW_USERS", viewConnection))
				{
					viewUsers.CommandType = CommandType.StoredProcedure;
					viewConnection.Open();
					using (SqlDataReader sqlDataReader = viewUsers.ExecuteReader())
					{

						//read entries from the database
						while (sqlDataReader.Read())
						{
							UserDO user = _Mapper.MapReadToSingle(sqlDataReader);
							displayUsers.Add(user);
						}
					}
				}
			}
			catch (SqlException sqlex)
			{
				_Logger.ErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, sqlex);

			}
			catch (Exception ex)
			{
				_Logger.ErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
			}
			return displayUsers;
		}
		/// <summary>
		/// We view the user by their ID to get all of their details.
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public UserDO ViewUserByID(long userID)
		{
			UserDO user = new UserDO();
			try
			{   //Getting together our sql Connections to run a view by users statement.
				using (SqlConnection connection = new SqlConnection(_ConnectionString))
				using (SqlCommand command = new SqlCommand("VIEW_BY_USER_ID", connection))
				{//runs our sql commands to find our username
					command.CommandType = CommandType.StoredProcedure;
					command.CommandTimeout = 60;
					command.Parameters.AddWithValue("@UserID", userID);
					connection.Open();
					//A sql reader runs until its done and is gone.
					//It goes through a series of checks to make sure it will not break.
					//While reading each piece of the database.
					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{

							user.Username = reader["UserName"] != DBNull.Value ? (string)reader["UserName"] : null;
							user.Password = reader["Pass"] != DBNull.Value ? (string)reader["Pass"] : null;
							user.Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : null;
							user.Address = reader["Address"] != DBNull.Value ? (string)reader["Address"] : null;
							user.Country = reader["Country"] != DBNull.Value ? (string)reader["Country"] : null;
							user.RoleName = reader["RoleName"] != DBNull.Value ? (string)reader["Country"] : null;
							user.UserId = reader["UserID"] != DBNull.Value ? (long)reader["UserID"] : 0;
						}
					}
				}
			}
			catch (SqlException ex)
			{
				_Logger.ErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
			}
			return user;

		}
		#endregion

		#region CRUD for Update.
		/// <summary>
				/// We view all the information for the three field of the view to update a user.
				/// </summary>
				/// <param name="user"></param>
		public void UpdateUser(UserDO user)
		{
			try
			{
				using (SqlConnection DataAccessConnection = new SqlConnection(_ConnectionString))
				using (SqlCommand UpdateUser = new SqlCommand("UPDATE_USER", DataAccessConnection))
				{
					UpdateUser.CommandType = CommandType.StoredProcedure;
					UpdateUser.CommandTimeout = 60;
					UpdateUser.Parameters.AddWithValue("@UserId", user.UserId);
					UpdateUser.Parameters.AddWithValue("@UserName", user.Username);
					UpdateUser.Parameters.AddWithValue("@RoleID", user.Role);
					UpdateUser.Parameters.AddWithValue("@Email", user.Email);

					DataAccessConnection.Open();
					UpdateUser.ExecuteNonQuery();
				}
			}
			catch (SqlException sqlex)
			{
				_Logger.ErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, sqlex);
			}

		}
		
		/// <summary>
		/// We View the user we want to update by a username and then give the details.
		/// </summary>
		/// <param name="userName"></param>
		/// <returns></returns>
		public UserDO View_By_Username_For_Update(string userName) { 
		UserDO user = new UserDO();
			try
			{   //Getting together our sql Connections to run a view by users statement.
				using (SqlConnection connection = new SqlConnection(_ConnectionString))
				using (SqlCommand command = new SqlCommand("VIEW_USERNAME_FOR_UPDATE", connection))
				{//runs our sql commands to find our username
					command.CommandType = CommandType.StoredProcedure;
					command.CommandTimeout = 60;
					command.Parameters.AddWithValue("@UserName", userName);
					connection.Open();
					//A sql reader runs until its done and is gone.
					//It goes through a series of checks to make sure it will not break.
					//While reading each piece of the database.
					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							user.UserId = reader["UserID"] != DBNull.Value? (long) reader["UserID"] : 0;
							user.Username = reader["UserName"] != DBNull.Value? (string) reader["UserName"] : null;
							user.Role = reader["RoleID"] != DBNull.Value? (long) reader["RoleID"] : 0;
							user.Email = reader["Email"] != DBNull.Value? (string) reader["Email"] : null;
							
						}
					}
				}
			}
			catch (SqlException ex)
			{
				_Logger.ErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
			}
			return user;

		}

		/// <summary>
		/// We view a list of the roles to send to the database and view as a drop down list.
		/// </summary>
		/// <returns></returns>
		public List<UserDO> ViewRoles()
		{
			List<UserDO> roles = new List<UserDO>();
			try
			{
				using (SqlConnection rolesConnection = new SqlConnection(_ConnectionString))
				using (SqlCommand rolesCommand = new SqlCommand("VIEW_ROLES", rolesConnection))
				{
					rolesCommand.CommandType = CommandType.StoredProcedure;
					rolesConnection.Open();
					using (SqlDataReader sqlReader = rolesCommand.ExecuteReader())
					{
						while (sqlReader.Read())
						{
							UserDO userDO = _Mapper.Roles(sqlReader);
							roles.Add(userDO);
						}
					}
				}
			}
			catch (SqlException sqlex)
			{
				_Logger.ErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, sqlex);

			}
			catch (Exception ex)
			{
				_Logger.ErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
			}
			return roles;

		}
		#endregion
	}

}
