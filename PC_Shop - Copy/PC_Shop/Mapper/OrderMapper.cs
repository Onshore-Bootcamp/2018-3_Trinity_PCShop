using BLL.Model;
using PC_Shop.Models;
using PC_Shop_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PC_Shop.Mapper
{
	public class OrderMapper
	{
		public Order MapDoToPO(OrderDO from)
		{

			Order to = new Order();
			to.Address = from.Address;
			to.Country = from.Country;
			to.OrderID = from.OrderID;
			to.PCName = from.pcName;
			to.UserID = from.userID;
			to.Username = from.userName;
			to.PcID = from.pcID;
			to.price = from.price;
			return to;

		}
		public OrderDO MappPoToDO(Order from)
		{
			OrderDO to = new OrderDO();
			to.pcID = from.PcID;
			to.Address = from.Address;
			to.Country = from.Country;
			to.userID = from.UserID;
			to.OrderID = from.OrderID;
			to.pcName = from.PCName;
			to.userName = from.Username;
			to.price = from.price;
			return to;
		}
		public Order MapBoToPo(OrderBO from)
		{
			Order to = new Order();
			to.UserID = from.UserID;
			to.Username = from.UserName;
			to.price = from.Price;
			return to;
		}
		public OrderBO MapDoToBo(OrderDO from)
		{
			OrderBO to =  new OrderBO();
			to.Price = from.price;
			to.UserID = from.userID;
			to.UserName = from.userName;
			return to;
		}

	}
}