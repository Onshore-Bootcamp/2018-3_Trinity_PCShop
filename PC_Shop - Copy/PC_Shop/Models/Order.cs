using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PC_Shop.Models
{
	public class Order
	{
		//Makes Parameters to get and set our variables bsed on requirements
		[Required]
		public string Address { get; set; }
		[Required]
		[Range(0,10000)]
		public long price { get; set; }
		[Required]
		public string Country { get; set; }
		
		public long OrderID { get; set; }
		
		public long UserID { get; set; }
		
		public long PcID { get; set; }
		
		public string PCName { get; set; }
		
		public string Username { get; set; }
		
	}
}