using BLL.Model;
using PC_Shop.Models;
using PC_Shop_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PC_Shop.Mapper
{
	public class PcMapper
	{
		public PC MapDoToPO(PcDO from)
		{

			PC to = new PC();
			
			to.PcID = from.PcId;
			to.Cpu = from.Cpu;
			to.Gpu = from.Gpu;
			to.Motherboard = from.MotherBoard;
			to.PcName = from.PcName;
			to.Powersupply = from.PowerSupply;
			to.Price = from.Price;
			to.Ram = from.Ram;
			to.UserName = from.Username;
			to.UserID = from.UserID;




			return to;
		}
		public PcDO MapPOToDo(PC from)
		{
			PcDO to = new PcDO();
		
			to.PcId = from.PcID;
			to.Cpu = from.Cpu;
			to.Gpu = from.Gpu;
			to.MotherBoard = from.Motherboard;
			to.PcName = from.PcName;
			to.PowerSupply = from.Powersupply;
			to.Price = from.Price;
			to.Ram = from.Ram;
			to.UserID = from.UserID;
			from.UserName = from.UserName;


			return to;
		}
	
	}
}