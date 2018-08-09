using PC_Shop_DAL.Models;
using PC_Shop_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PC_Shop_DAL.DAO;
using PC_Shop.Models;
using PC_Shop.Mapper;
using PC_Shop.CustomAttributes;

namespace PC_Shop.Controllers
{
	public class PCController : Controller
	{
		private readonly PcDAO pcDAO;
		PcMapper _mapper = new PcMapper();

		public PCController()
		{
			string connectionString = ConfigurationManager.ConnectionStrings["DataSource"].ConnectionString;
			pcDAO = new PcDAO(connectionString);

		}

		// GET: Store
		/// <summary>
		/// Grabs a list of the items in the data base to display as a store
		/// </summary>
		/// <returns></returns>
		public ActionResult Store()
		{
			ActionResult response;
			//Checks to see if the user has any role if they do then let them in if they dont then send back to login
			if (!(Session["RoleName"] == null) || (string)Session["RoleName"] == "Seller" || (string)Session["RoleName"] == "Admin" || (string)Session["RoleName"] == "Buyer")
			{
				List<PcDO> allPCs = pcDAO.ViewShop();
				List<PC> PCsMapped = new List<PC>();
				foreach (PcDO dataObject in allPCs)
				{
					PCsMapped.Add(_mapper.MapDoToPO(dataObject));
				}
				response = View(PCsMapped);
			}
			else
			{
				response = RedirectToAction("Login", "Account");
			}
			return response;


		}
		/// <summary>
		/// Create a computer when the Rolename is equal to session        
		/// </summary>
		/// <returns></returns>

		[HttpGet]
		public ActionResult CreatePC()
		{
			ActionResult result;
			if ((string)Session["RoleName"] == "Seller" || (string)Session["RoleName"] == "Admin")
			{
				result = View();
			}
			else
			{
				result = RedirectToAction("Login", "Account");
			}
			return result;
		}
		/// <summary>
		/// Create a pc with the information given in the above view. 
		/// </summary>
		/// <param name="form"></param>
		/// <returns actionresult> </returns>
		[HttpPost]
		public ActionResult CreatePC(PC form)
		{
			ActionResult response;


			try
			{
				//if all the information is here we can let them create their pc
				if (ModelState.IsValid)
				{
					PcDO SellerObject = new PcDO()
					{
						PcName = form.PcName,
						Cpu = form.Cpu,
						Gpu = form.Gpu,
						MotherBoard = form.Motherboard,
						Ram = form.Ram,
						PowerSupply = form.Powersupply,
						Price = form.Price,
						Username = Session["UserName"].ToString(),
						UserID = Convert.ToInt64(Session["UserID"])

					};
					//We create the pc here using the seller object we just made up above with all the information from the view
					pcDAO.CreatePC(SellerObject);
					response = RedirectToAction("Store", "Pc");

				}
				//Return the form if the information wasnt provided
				else
				{
					response = View(form);
				}
			}
			//If the try fails then we return to the login page 
			catch
			{
				response = RedirectToAction("Login", "Account");

			}
			return response;
		}
		/// <summary>
		/// We check to make sure a pcid was supplied and if it was then we can run the applicaton
		/// </summary>
		/// <param name="pcID"></param>
		/// <returns></returns>
		[HttpGet]
		[PcIDAttribute("/PC/Store")]
		public ActionResult Delete(long pcID)
		{
			ActionResult response;

			if ((string)Session["RoleName"] == "Admin")
			{
				if ((int)Session["PcID"] > 0)
				{
					PcDO pc = pcDAO.ViewDetails((int)Session["PcID"]);
					PC deletedPC = _mapper.MapDoToPO(pc);
					pcDAO.DeletePC(deletedPC.PcID);
					response = RedirectToAction("Store", "PC");
				}
				else
				{
					response = RedirectToAction("Store", "Store");
				}
				Session.Remove("PcID");
				return response;
			}
			else
			{
				response = RedirectToAction("Store", "PC");
			}
			return response;

		}

		/// <summary>
		/// We view all the details of the pc by the pcID
		/// </summary>
		/// <param name="pcID"></param>
		/// <returns></returns>
		public ActionResult PCDetails(int pcID)
		{

			ActionResult response;

			//check id CANT be default or less than 0
			if (pcID > 0)
			{
				Session["PcID"] = pcID;
				PcDO pcDO = pcDAO.ViewDetails((int)Session["PcID"]);
				PC pcPO = _mapper.MapDoToPO(pcDO);

				response = View(pcPO);
			}
			else
			{
				response = RedirectToAction("Store", "Store");
			}
			return response;
		}

		/// <summary>
		/// We check to make sure an ID was supplied and if it is we can view thee form to update it.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[PcIDAttribute("/PC/Store")]
		public ActionResult Update()
		{
			ActionResult response;
			if ((string)Session["RoleName"] == "Admin")
			{
				//check id CANT be default or less than 0
				if ((int)Session["PcID"] > 0)
				{
					PcDO pcDo = pcDAO.ViewDetails((int)Session["PcID"]);
					PC pcPO = _mapper.MapDoToPO(pcDo);

					response = View(pcPO);
				}
				else
				{
					response = RedirectToAction("Index", "Home");
				}
			}
			else
			{
				response = RedirectToAction("Store", "PC");
			}
			return response;

			//Request.UrlReferrer

		}
		/// <summary>
		/// We can now edit the form to update a pc entirely.
		/// </summary>
		/// <param name="form"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Update(PC form)
		{
			ActionResult response;
			if (ModelState.IsValid)
			{
				PcDO pcDO = _mapper.MapPOToDo(form);
				pcDAO.UpdatePc(pcDO);

				response = RedirectToAction("Store", "PC");
			}
			else
			{
				response = View(form);
			}
			Session.Remove("PcID");
			return response;
		}
	}
}