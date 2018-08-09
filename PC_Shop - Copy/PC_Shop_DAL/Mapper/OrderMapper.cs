using PC_Shop_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Shop_DAL.Mapper
{
	public class OrderMapper
	{

		public OrderDO MapReadertoSingle(SqlDataReader read)
		{
			OrderDO result = new OrderDO();

			if (read["Address"] != DBNull.Value)
			{
				result.Address = (string)read["Address"];
			}
			if (read["Country"] != DBNull.Value)
			{
				result.Country = (string)read["Country"];
			}
			if (read["Username"] != DBNull.Value)
			{
				result.userName = (string)read["Username"];
			}
			if (read["Price"] != DBNull.Value)
			{
				result.price = (long)read["Price"];
			}
			if (read["PcName"] != DBNull.Value)
			{
				result.pcName = (string)read["PcName"];
			}
			if (read["PcID"] != DBNull.Value)
			{
				result.pcID = (long)read["PcID"];
			}
			if (read["OrderID"] != DBNull.Value)
			{
				result.OrderID = (long)read["OrderID"];
			}
			if (read["UserID"] != DBNull.Value)
			{
				result.userID = (long)read["UserID"];
			}
			return result;
		}
		public OrderDO TopFive(SqlDataReader reader)
		{
			OrderDO	orderDO = new OrderDO();

			if (reader["UserName"] != DBNull.Value)
			{
				orderDO.userName = (string)reader["UserName"];
			}
			if (reader["Price"] != DBNull.Value)
			{
				orderDO.price = (long)reader["Price"];
			}
			if (reader["UserID"] != DBNull.Value)
			{
				orderDO.userID = (long)reader["UserID"];
			}
			return orderDO;
		}
	}
}
