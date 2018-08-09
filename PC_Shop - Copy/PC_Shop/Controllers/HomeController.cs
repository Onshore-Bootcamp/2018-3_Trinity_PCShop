using BLL.BO;
using BLL.Model;
using PC_Shop.Mapper;
using PC_Shop.Models;
using PC_Shop_DAL;
using PC_Shop_DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PC_Shop.Controllers
{
	public class HomeController : Controller
	{
		OrderBLO pcBO = new OrderBLO();
		OrderBO model = new OrderBO();

		OrdersDAO ordersDAO;
		//Create our connection string
		public HomeController()
		{
			string connectionString = ConfigurationManager.ConnectionStrings["DataSource"].ConnectionString;
			ordersDAO = new OrdersDAO(connectionString);

		}

		OrderMapper mapper = new OrderMapper();

		/// <summary>
		/// In here we run our bll to find our top seller.
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			ActionResult response;
			try
			{

				if (ModelState.IsValid)
				{//Create a list of of buissness logic to send to the bo.
					List<OrderBO> users = new List<OrderBO>();
					//Call from do to the dao to view the prices
					List<OrderDO> PricesMapped = ordersDAO.ViewPrices();
					//Map Do back to the BO  and add to the list until all are added
					foreach (OrderDO displayObject in PricesMapped)
					{
						users.Add(mapper.MapDoToBo(displayObject));


					}
					//take the top user in the data base based on their sales
					//we will also take the id that is passed back and put into view by username
					long userID = pcBO.ViewNumberOne(users);

					//Map DO with user to A po
					OrderDO username = ordersDAO.ViewUsernameByID(userID);
					Order topUser = mapper.MapDoToPO(username);


					response = View(topUser);
				}
				else
				{
					response = View();
				}
			}
			catch
			{
				response = View();
			}
			return response;


		}
	}
}