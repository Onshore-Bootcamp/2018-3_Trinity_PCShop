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
	public class PcDAO
	{
		#region Functions to create our data.

		/// <summary>
		/// We grab our mapper from pc and our logger to log all errors
		/// </summary>
		private static Logger _Logger = new Logger();
		private static PCMapper mapper = new PCMapper();

		//We create a connection string to hold the value in the constructor.
		private readonly string _ConnectionString;

		//We create a constructor to take in the connection string from the view.
		public PcDAO(string connectionString)
		{
			_ConnectionString = connectionString;
		} 
		#endregion
		#region CRUD For PC 

		/// <summary>
		/// We go and grab the details of the pc based on the PC ID we were given.
		/// </summary>
		/// <param name="pcID"></param>
		/// <returns></returns>
		public PcDO ViewDetails(long pcID)
		{
			PcDO viewPcs = new PcDO();
			try
			{
				//Getting Things ready to find our specific pc
				using (SqlConnection pcConnection = new SqlConnection(_ConnectionString))
				using (SqlCommand details = new SqlCommand("VIEW_PC", pcConnection))
				{
					details.CommandType = CommandType.StoredProcedure;
					details.CommandTimeout = 60;
					details.Parameters.AddWithValue("@PCID", pcID);
					pcConnection.Open();


					using (SqlDataReader readDetails = details.ExecuteReader())
					{
						if (readDetails.Read())
						{
							//Run Sql reader to find info and make equal to pc property	
							viewPcs.PcId = readDetails["PcID"] != DBNull.Value ? (long)readDetails["PcID"] : 0;
							viewPcs.UserID = readDetails["UserID"] != DBNull.Value ? (long)readDetails["UserID"] : 0;
							viewPcs.PcName = readDetails["PcName"] != DBNull.Value ? (string)readDetails["PcName"] : null;
							viewPcs.Cpu = readDetails["Cpu"] != DBNull.Value ? (string)readDetails["Cpu"] : null;
							viewPcs.MotherBoard = readDetails["Motherboard"] != DBNull.Value ? (string)readDetails["Motherboard"] : null;
							viewPcs.Gpu = readDetails["Gpu"] != DBNull.Value ? (string)readDetails["GPu"] : null;
							viewPcs.Ram = readDetails["Ram"] != DBNull.Value ? (string)readDetails["Ram"] : null;
							viewPcs.PowerSupply = readDetails["PowerSupply"] != DBNull.Value ? (string)readDetails["PowerSupply"] : null;
							viewPcs.Username = readDetails["Username"] != DBNull.Value ? (string)readDetails["Username"] : null;
							viewPcs.Price = readDetails["Price"] != DBNull.Value ? (int)readDetails["Price"] : 0;





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
			return viewPcs;
		}

		/// <summary>
		/// We view three items in the store to display before we give PC Details
		/// </summary>
		/// <returns></returns>
		public List<PcDO> ViewShop()
		{   //We set a list to hold all the information in the database.
			List<PcDO> shopList = new List<PcDO>();
			try
			{
				//We gather our connection and command string tied to the the sql procedure
				using (SqlConnection pcConnection = new SqlConnection(_ConnectionString))
				using (SqlCommand viewShop = new SqlCommand("VIEW_Store", pcConnection))

				{
					//We open our connection and set the command as a stored procedure
					viewShop.CommandType = CommandType.StoredProcedure;
					pcConnection.Open();
					//While its reading we execute our reader and then map every thing in the those three columns to the list above
					using (SqlDataReader sqlReader = viewShop.ExecuteReader())
					{
						while (sqlReader.Read())
						{
							PcDO pc = mapper.Shop(sqlReader);
							shopList.Add(pc);
						}
					}
				}

			}
			//We catch any exceptions if there is any of them if not then we will return the list to the view.
			catch (SqlException sqlex)
			{
				_Logger.ErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, sqlex);

			}
			catch (Exception ex)
			{
				_Logger.ErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
			}
			return shopList;
		}

		/// <summary>
		/// We Create a new pc using the values mapped to the pcDO
		/// </summary>
		/// <param name="newPC"></param>
		public void CreatePC(PcDO newPC)
		{
			try
			{
				// We create our connecton and command and we set our commmand limits.
				//We then add the pcDO values mapped  to the sql parameter 
				using (SqlConnection pcShopperConnection = new SqlConnection(_ConnectionString))
				using (SqlCommand createPC = new SqlCommand("ADD_PC", pcShopperConnection))
				{
					createPC.CommandType = CommandType.StoredProcedure;
					createPC.CommandTimeout = 60;
					createPC.Parameters.AddWithValue("@PcName", newPC.PcName);
					createPC.Parameters.AddWithValue("@Cpu", newPC.Cpu);
					createPC.Parameters.AddWithValue("@Motherboard", newPC.MotherBoard);
					createPC.Parameters.AddWithValue("@Gpu", newPC.Gpu);
					createPC.Parameters.AddWithValue("@Ram", newPC.Ram);
					createPC.Parameters.AddWithValue("@PowerSupply", newPC.PowerSupply);
					createPC.Parameters.AddWithValue("@UserID", newPC.UserID);
					createPC.Parameters.AddWithValue("@Price", newPC.Price);
					//We then open the connection and execute the query
					pcShopperConnection.Open();
					createPC.ExecuteNonQuery();

				}
			}
			catch (SqlException ex)
			{
				_Logger.ErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
			}
		}

		/// <summary>
		/// We go and update the pc based on the information we were given inside the view when we mapped to a Data layer.
		/// </summary>
		/// <param name="pc"></param>
		public void UpdatePc(PcDO pc)
		{
			try
			{
				using (SqlConnection pcConnection = new SqlConnection(_ConnectionString))
				using (SqlCommand updatePC = new SqlCommand("UPDATE_PC", pcConnection))
				{
					updatePC.CommandType = CommandType.StoredProcedure;
					updatePC.CommandTimeout = 60;
					updatePC.Parameters.AddWithValue("@PCID", pc.PcId);

					updatePC.Parameters.AddWithValue("@PcName", pc.PcName);
					updatePC.Parameters.AddWithValue("@Price", pc.Price);
					updatePC.Parameters.AddWithValue("@CPU", pc.Cpu);
					updatePC.Parameters.AddWithValue("@MotherBoard", pc.Price);
					updatePC.Parameters.AddWithValue("@GPU", pc.Gpu);
					updatePC.Parameters.AddWithValue("@Ram", pc.Ram);
					updatePC.Parameters.AddWithValue("@PowerSupply", pc.PowerSupply);
					updatePC.Parameters.AddWithValue("@UserID", pc.UserID);







					pcConnection.Open();
					updatePC.ExecuteNonQuery();

				}

			}
			catch (SqlException ex)
			{
				_Logger.ErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
			}
			try
			{
				using (SqlConnection pcConnection = new SqlConnection(_ConnectionString))
				using (SqlCommand updatePC = new SqlCommand("UPDATE_PC", pcConnection))
				{
					updatePC.CommandType = CommandType.StoredProcedure;
					updatePC.CommandTimeout = 60;
					updatePC.Parameters.AddWithValue("@PCID", pc.PcId);

					updatePC.Parameters.AddWithValue("@PcName", pc.PcName);
					updatePC.Parameters.AddWithValue("@Price", pc.Price);
					pcConnection.Open();
					updatePC.ExecuteNonQuery();

				}

			}
			catch (SqlException ex)
			{
				_Logger.ErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
			}

		}

		/// <summary>
		/// We delete a pc based on the information we were given in the view
		/// </summary>
		/// <param name="pcID"></param>
		public void DeletePC(long pcID)
		{
			try
			{
				using (SqlConnection pcConnection = new SqlConnection(_ConnectionString))
				using (SqlCommand deletePC = new SqlCommand("DELETE_PC", pcConnection))
				{
					deletePC.CommandType = CommandType.StoredProcedure;
					deletePC.CommandTimeout = 60;
					deletePC.Parameters.AddWithValue("@PCID", pcID);
					pcConnection.Open();
					deletePC.ExecuteNonQuery();
				}

			}
			catch (SqlException ex)
			{
				_Logger.ErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
			}
		}

		#endregion

	}
}
