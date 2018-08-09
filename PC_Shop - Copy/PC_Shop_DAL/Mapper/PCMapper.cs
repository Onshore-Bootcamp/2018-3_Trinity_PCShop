using PC_Shop_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Shop_DAL.Mapper
{
	class PCMapper
	{
		public PcDO MapReaderToSingle(SqlDataReader reader)
		{
			PcDO result = new PcDO();

			if (reader["PCID"] != DBNull.Value)
			{
				result.PcId = (long)reader["PCID"];
			}
			if (reader["PcName"] != DBNull.Value)
			{
				result.PcName = (string)reader["PcName"];
			}
			if (reader["Gpu"] != DBNull.Value)
			{
				result.Gpu = (string)reader["Gpu"];
			}
			if (reader["Cpu"] != DBNull.Value)
			{
				result.Cpu = (string)reader["Cpu"];
			}
			if (reader["Motherboard"] != DBNull.Value)
			{
				result.MotherBoard = (string)reader["Motherboard"];
			}
			if (reader["Ram"] != DBNull.Value)
			{
				result.Ram = (string)reader["Ram"];
			}
			if (reader["PowerSupply"] != DBNull.Value)
			{
				result.PowerSupply = (string)reader["PowerSupply"];

			}
			if (reader["Price"] != DBNull.Value)
			{
				result.Price = (int)reader["Price"];
			}


			return result;


		}
		public PcDO Shop(SqlDataReader reader)
		{

			PcDO pcDO = new PcDO();

			if (reader["PcID"] != DBNull.Value)
			{
				pcDO.PcId = (long)reader["PcID"];
			}

			if (reader["PcName"] != DBNull.Value)
			{
				pcDO.PcName = (string)reader["PcName"];
			}
			if (reader["Price"] != DBNull.Value)
			{
				pcDO.Price = (int)reader["Price"];
			}

			if (reader["UserName"] != DBNull.Value)
			{
				pcDO.Username = (string)reader["UserName"];
			}
			return pcDO;
		}
		
	}
}
