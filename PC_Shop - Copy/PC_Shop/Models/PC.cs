using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PC_Shop.Models
{
	public class PC
	{	/// <summary>
	/// Sets the properties to input and display
	/// </summary>
		public long PcID { get; set; }
		[Required]
		public string PcName { get; set; }
		[Required]
		public string Cpu { get; set; }
		[Required]
		public string Motherboard { get; set; }
		[Required]
		public string Gpu { get; set; }
		[Required]
		public string Ram { get; set; }
		[Required]
		public string Powersupply { get; set; }
		
		public string UserName { get; set; }
		
		public long UserID { get; set; }
		[Required][Range(0,10000)]
		public long Price { get; set; }
	
	}
}