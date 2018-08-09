using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Shop_DAL.Models
{
	public class OrderDO
	{	/// <summary>
		/// Mapped by the Po to the DO
		/// </summary>
		public string Address { get; set; }
		public DateTime ShipDate { get; set; }
		public string Country { get; set; }
		public long OrderID { get; set; }
		public long pcID { get; set; }
		public string pcName { get; set; }
		public long userID { get; set; }
		public string userName { get; set; }
		public long price { get; set; }
	}
}
