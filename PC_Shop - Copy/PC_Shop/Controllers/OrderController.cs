using PC_Shop.CustomAttributes;
using PC_Shop.Mapper;
using PC_Shop.Models;
using PC_Shop_DAL;
using PC_Shop_DAL.DAO;
using PC_Shop_DAL.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
namespace PC_Shop.Controllers
{
	public class OrderController : Controller
	{
		//Grab DAOs for calling methods inside.
		private readonly UserDAO userDao;
		private readonly PcDAO pcDAO;
		private readonly OrdersDAO ordersDAO;
		//Grab our mapper to send data back and forth
		OrderMapper orderMapper = new OrderMapper();
		UserMapper userMapper = new UserMapper();
		PcMapper pcMapper = new PcMapper();

		//Create our connection string
		public OrderController()
		{
			string connectionString = ConfigurationManager.ConnectionStrings["DataSource"].ConnectionString;
			ordersDAO = new OrdersDAO(connectionString);
			userDao = new UserDAO(connectionString);
			pcDAO = new PcDAO(connectionString);
		}
		/// <summary>
		/// We check that the pcID is given by an attribute.
		/// We check that that all the information is give and then we call on confirmorder method to view the order before completing
		/// 
		/// </summary>
		/// <param name="pcID"></param>
		/// <returns></returns>

		[PcID("/PC/Store")]
		[HttpGet]
		public ActionResult ConfirmOrder(long pcID)
		{
			ActionResult response;
			if (!((string)Session["RoleName"] == null))
			{
				Order order = new Order();
				order.PcID = pcID;
				if (pcID > 0)
				{
					long userId = (long)Session["UserId"];
					string Username = (string)Session["Username"];
					//Map User
					UserDO userDO = userDao.ViewUserByID(userId);
					User user = userMapper.MapDOtoPO(userDO);

					PcDO pc = pcDAO.ViewDetails(pcID);
					PC mappedPc = pcMapper.MapDoToPO(pc);


					try
					{

						if (ModelState.IsValid)
						{

							OrderDO OrderObject = new OrderDO()
							{
								pcID = mappedPc.PcID,
								pcName = mappedPc.PcName,
								Address = user.Address,
								Country = user.Country,
								OrderID = order.OrderID,

								price = mappedPc.Price,
								userID = user.UserID,
								userName = user.Username



							};

							OrderDO orderDo = ordersDAO.ViewOrderByPCID(pcID);
							Order orderPo = orderMapper.MapDoToPO(orderDo);
							response = View(orderPo);


						}
						else
						{
							response = RedirectToAction("Store", "Pc");
						}
					}
					catch
					{
						response = RedirectToAction("Login", "Account");

					}
					return response;
				}
				else
				{
					response = RedirectToAction("Store", "Store");
				}
				return View();
			}
			else
			{
				response = RedirectToAction("Login", "Account");
			}
			return response;
		}
		/// <summary>
		/// If all is correct then we create the order and go back to the store.
		/// </summary>
		/// <param name="pcID"></param>
		/// <returns></returns>
		[PcID("/PC/Store")]
		[HttpPost]
		public ActionResult ConfirmOrder(Order form)
		{
			//Prepare the model to be viewed.
			//Do not call on the DAO
			ActionResult response;
			long PCID = (int)Session["PcID"];

			try
			{

				long userId = (long)Session["UserId"];
				string Username = (string)Session["Username"];
				//Map User
				UserDO userDO = userDao.ViewUserByID(userId);
				User user = userMapper.MapDOtoPO(userDO);

				PcDO pc = pcDAO.ViewDetails(form.PcID);

				PC mappedPc = pcMapper.MapDoToPO(pc);
				if (ModelState.IsValid)
				{

					OrderDO OrderObject = new OrderDO()
					{
						pcID = mappedPc.PcID,
						pcName = mappedPc.PcName,
						Address = form.Address,
						Country = form.Country,

						price = mappedPc.Price,
						userID = pc.UserID,
						userName = pc.Username



					};
					ordersDAO.CreateOrder(OrderObject);
					response = RedirectToAction("Store", "PC");


				}
				else
				{
					response = RedirectToAction("Store", "Pc");
				}
			}
			catch
			{
				response = View(form);

			}
		return response;
		}
		/// <summary>
		/// View the data from the link that it has the orderID
		/// </summary>
		/// <param name="OrderID"></param>
		/// <returns></returns>
		[HttpGet]
		public ActionResult UpdateOrder(long OrderID)
		{
			ActionResult response;
			if ((string)Session["RoleName"] == "Admin")
			{
				//check id CANT be default or less than 0
				if (OrderID > 0)
				{
					OrderDO orderDO = ordersDAO.ViewByOrderID(OrderID);
					Order orderPo = orderMapper.MapDoToPO(orderDO);

					response = View(orderPo);
				}
				else
				{
					response = RedirectToAction("Index", "Home");
				}
			}
			else
			{
				response = RedirectToAction("Index", "Home");
			}
			return response;
		}
		/// <summary>
		/// If we do then create the application
		/// </summary>
		/// <param name="form"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult UpdateOrder(Order form)
		{
			ActionResult response;
			if (ModelState.IsValid)
			{
				OrderDO orderDO = orderMapper.MappPoToDO(form);
				ordersDAO.UpdateOrder(orderDO);

				response = RedirectToAction("ViewOrders", "Order");
			}
			else
			{
				response = View(form);
			}
			return response;
		}
		/// <summary>
		/// Grab the order ID Anad delete form there and redirct back to the store
		/// </summary>
		/// <param name="OrderID"></param>
		/// <returns></returns>
		[HttpGet]
		public ActionResult DeleteOrder(long OrderID)
		{


			ActionResult response;


			if (OrderID > 0)
			{
				OrderDO orderDO = ordersDAO.ViewByOrderID(OrderID);
				Order order = orderMapper.MapDoToPO(orderDO);
				ordersDAO.DeleteOrder(OrderID);
				response = RedirectToAction("ViewOrders", "Order");
			}
			else
			{
				response = RedirectToAction("Index", "Home");
			}
			return response;



		}
		/// <summary>
		/// Read all informaton in the database about orders
		/// </summary>
		/// <returns></returns>
		public ActionResult ViewOrders()
		{
			ActionResult response;
			if ((string)Session["RoleName"] == "Admin")
			{
				List<OrderDO> allOrders = ordersDAO.ViewOrders();
				List<Order> OrdersMapped = new List<Order>();
				foreach (OrderDO orderObject in allOrders)
				{
					OrdersMapped.Add(orderMapper.MapDoToPO(orderObject));
				}

				response = View(OrdersMapped);
			}
			else
			{
				response = RedirectToAction("Index", "Home");
			}
			return response;
		}
	}
}


