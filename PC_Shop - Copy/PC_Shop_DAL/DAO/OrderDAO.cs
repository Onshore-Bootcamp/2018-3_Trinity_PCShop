using PC_Shop_DAL;
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

namespace PC_Shop_DAL
{
	public class OrdersDAO
	{

		#region Functions to grab our information
		private static Logger _Logger = new Logger();
		private static OrderMapper mapper = new OrderMapper();

		private readonly string _ConnectionString;

		public OrdersDAO(string connectionString)
		{
			_ConnectionString = connectionString;
		}

		#endregion
		#region CRUD for Orders

		/// <summary>
		/// We grab our information to 
		/// </summary>
		/// <param name="pcID"></param>
		/// <returns></returns>
		public OrderDO ViewOrderByPCID(long pcID)
		{
			OrderDO viewOrder = new OrderDO();
			try
			{
				using (SqlConnection orderConnection = new SqlConnection(_ConnectionString))
				using (SqlCommand orders = new SqlCommand("VIEW_ORDER_BY_PC", orderConnection))
				{
					orders.CommandType = CommandType.StoredProcedure;
					orders.CommandTimeout = 60;
					orders.Parameters.AddWithValue("@PcID", pcID);
					orderConnection.Open();
					orders.ExecuteNonQuery();
					using (SqlDataReader read = orders.ExecuteReader())
					{
						if (read.Read())
						{
							//Run Sql reader to find info and make equal to pc property	


							viewOrder.Country = read["Country"] != DBNull.Value ? (string)read["Country"] : null;
							viewOrder.Address = read["Address"] != DBNull.Value ? (string)read["Address"] : null;
							viewOrder.userName = read["UserName"] != DBNull.Value ? (string)read["UserName"] : null;
							viewOrder.price = read["Price"] != DBNull.Value ? (int)read["Price"] : 0;
							viewOrder.pcName = read["PcName"] != DBNull.Value ? (string)read["PcName"] : null;
							viewOrder.pcID = read["PcID"] != DBNull.Value ? (long)read["PcID"] : 0;
							viewOrder.userID = read["UserID"] != DBNull.Value ? (long)read["UserID"] : 0;


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
			return viewOrder;

		}
		/// <summary>
		/// We make a list of the orders avaiable and return the list
		/// </summary>
		/// <returns></returns>
		public List<OrderDO> ViewOrders()
		{
			List<OrderDO> orderList = new List<OrderDO>();
			try
			{
				using (SqlConnection OrderConnection = new SqlConnection(_ConnectionString))
				using (SqlCommand ViewOrders = new SqlCommand("VIEW_ORDER", OrderConnection))

				{

					ViewOrders.CommandType = CommandType.StoredProcedure;
					OrderConnection.Open();
					using (SqlDataReader sqlReader = ViewOrders.ExecuteReader())
					{
						while (sqlReader.Read())
						{
							OrderDO order = mapper.MapReadertoSingle(sqlReader);
							orderList.Add(order);
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
			return orderList;
		}
		/// <summary>
		/// We take in the orders mapped to us and add them to the order.
		/// </summary>
		/// <param name="newOrder"></param>
		public void CreateOrder(OrderDO newOrder)
		{
			try
			{

				using (SqlConnection orderconnection = new SqlConnection(_ConnectionString))
				using (SqlCommand createOrder = new SqlCommand("ADD_ORDER", orderconnection))
				{
					createOrder.CommandType = CommandType.StoredProcedure;
					createOrder.CommandTimeout = 60;
					createOrder.Parameters.AddWithValue("@UserID", newOrder.userID);
					createOrder.Parameters.AddWithValue("@Address", newOrder.Address);
					createOrder.Parameters.AddWithValue("@Country", newOrder.Country);
					createOrder.Parameters.AddWithValue("@PCID", newOrder.pcID);
					createOrder.Parameters.AddWithValue("@Username", newOrder.userName);
					createOrder.Parameters.AddWithValue("@Price", newOrder.price);
					createOrder.Parameters.AddWithValue("@PCName", newOrder.pcName);



					orderconnection.Open();
					createOrder.ExecuteNonQuery();
				}
			}
			catch (SqlException ex)
			{
				_Logger.ErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
			}


		}
		/// <summary>
		/// We update our order base on the information given by the view
		/// </summary>
		/// <param name="updatedOrder"></param>
		public void UpdateOrder(OrderDO updatedOrder)
		{
			try
			{
				using (SqlConnection orderConnection = new SqlConnection(_ConnectionString))
				using (SqlCommand updateOrder = new SqlCommand("UPDATE_ORDER", orderConnection))
				{
					updateOrder.CommandType = CommandType.StoredProcedure;
					updateOrder.CommandTimeout = 60;
					updateOrder.Parameters.AddWithValue("Address", updatedOrder.Address);
					updateOrder.Parameters.AddWithValue("Country", updatedOrder.Country);
					updateOrder.Parameters.AddWithValue("Price", updatedOrder.price);
					updateOrder.Parameters.AddWithValue("OrderID", updatedOrder.OrderID);




					orderConnection.Open();
					updateOrder.ExecuteNonQuery();

				}

			}
			catch (SqlException ex)
			{
				_Logger.ErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
			}

		}
		/// <summary>
		/// We delete the order based on the order ID
		/// </summary>
		/// <param name="OrderId"></param>
		public void DeleteOrder(long OrderId)
		{
			try
			{
				using (SqlConnection orderConnection = new SqlConnection(_ConnectionString))
				using (SqlCommand deleteOrder = new SqlCommand("DELETE_Order", orderConnection))
				{
					deleteOrder.CommandType = CommandType.StoredProcedure;
					deleteOrder.CommandTimeout = 60;
					deleteOrder.Parameters.AddWithValue("@OrderID", OrderId);
					orderConnection.Open();
					deleteOrder.ExecuteNonQuery();
				}

			}
			catch (SqlException ex)
			{
				_Logger.ErrorLog(MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, ex);
			}
		}
		/// <summary>
		/// We will get a list of all the prices and view them to send to the buissness logic.
		/// </summary>
		/// <returns></returns>
		public List<OrderDO> ViewPrices()
		{
			List<OrderDO> orderDo = new List<OrderDO>();
			try
			{
				using (SqlConnection pcConnection = new SqlConnection(_ConnectionString))
				using (SqlCommand viewPrices = new SqlCommand("VIEW_USERNAME_AND_PRICE", pcConnection))
				{
					viewPrices.CommandType = CommandType.StoredProcedure;
					viewPrices.CommandTimeout = 60;
					pcConnection.Open();
					using (SqlDataReader reader = viewPrices.ExecuteReader())
					{
						while (reader.Read())
						{
							OrderDO order = mapper.TopFive(reader);
							orderDo.Add(order);
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
			return orderDo;
		}
		/// <summary>
		/// We view to the users and price by ID
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public OrderDO ViewUsernameByID(long userID)
		{
			OrderDO top = new OrderDO();
			try
			{
				using (SqlConnection orderConnection = new SqlConnection(_ConnectionString))
				using (SqlCommand topSeller = new SqlCommand("View_Order_by_UserID", orderConnection))
				{
					topSeller.CommandType = CommandType.StoredProcedure;
					topSeller.CommandTimeout = 60;
					topSeller.Parameters.AddWithValue("@UserID", userID);
					orderConnection.Open();
					topSeller.ExecuteNonQuery();
					using (SqlDataReader read = topSeller.ExecuteReader())
					{
						if (read.Read())
						{
							//Run Sql reader to find info and make equal to pc property	


							top.userName = read["Username"] != DBNull.Value ? (string)read["UserName"] : null;



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
			return top;
		}

		/// <summary>
		/// We view the order by id recieved and
		/// </summary>
		/// <param name="orderID"></param>
		/// <returns></returns>
		public OrderDO ViewByOrderID(long orderID)
		{
			OrderDO viewOrder = new OrderDO();
			try
			{
				using (SqlConnection orderConnection = new SqlConnection(_ConnectionString))
				using (SqlCommand orders = new SqlCommand("VIEW_BY_ORDERID", orderConnection))
				{
					orders.CommandType = CommandType.StoredProcedure;
					orders.CommandTimeout = 60;
					orders.Parameters.AddWithValue("@OrderID", orderID);
					orderConnection.Open();
					orders.ExecuteNonQuery();
					using (SqlDataReader read = orders.ExecuteReader())
					{
						if (read.Read())
						{
							//Run Sql reader to find info and make equal to pc property	


							viewOrder.Country = read["Country"] != DBNull.Value ? (string)read["Country"] : null;
							viewOrder.Address = read["Address"] != DBNull.Value ? (string)read["Address"] : null;
							viewOrder.price = read["Address"] != DBNull.Value ? (int)read["Address"] : 0;

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
			return viewOrder;

		} 
		#endregion
	}
}

