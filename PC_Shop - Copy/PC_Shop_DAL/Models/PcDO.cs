using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Shop_DAL.Models
{
	public class PcDO
	{
		public long PcId { get; set; }
		public long UserID { get; set; }
		public string Username { get; set; }
		public string PcName { get; set; }
		public string Cpu { get; set; }
		public string MotherBoard { get; set; }
		public string Gpu { get; set; }
		public string Ram { get; set; }
		public string PowerSupply { get; set; }
		public long  Price { get; set; }
	}
}
