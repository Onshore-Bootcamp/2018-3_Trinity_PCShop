
using PC_Shop.Mapper;
using PC_Shop.Models;
using PC_Shop_DAL.DAO;
using PC_Shop_DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pc_Shop
{
	public class AccountController : Controller
	{
		UserMapper _mapper = new UserMapper();
		UpdateUserMapper mapper = new UpdateUserMapper();



		private readonly UserDAO _UserDataAccess;
		private readonly PcDAO _PcDAO;
		public AccountController()
		{
			string connectionString = ConfigurationManager.ConnectionStrings["DataSource"].ConnectionString;


			_UserDataAccess = new UserDAO(connectionString);
			_PcDAO = new PcDAO(connectionString);
		}
		[HttpGet]
		public ActionResult Register()
		{
			return View();

		}
		//check if form is correct if is send to database.
		[HttpPost]
		public ActionResult Register(User form)
		{
			ActionResult response;
			if (ModelState.IsValid)
			{

				if (form.IsBuyer == false)
				{
					UserDO SellerObject = new UserDO()
					{
						Username = form.Username,

						Password = form.Password,
						Email = form.Email,
						Name = form.Name,
						Role = 2,
						RoleName = "Supplier",
						Address = form.Address,
						Country = form.Country
					};

					_UserDataAccess.RegisterUser(SellerObject);
					response = RedirectToAction("Login", "Account");
				}
				else if (form.IsBuyer == true)
				{

					UserDO BuyerObject = new UserDO()
					{
						Username = form.Username,
						Password = form.Password,
						Email = form.Email,
						Name = form.Name,
						Role = 3,
						RoleName = "Buyer",
						Address = form.Address,
						Country = form.Country

					};
					_UserDataAccess.RegisterUser(BuyerObject);
					response = RedirectToAction("Login", "Account");
				}
				else
				{
					response = View(form);
				}
			}
			else
			{
				response = View(form);

			}
			return response;
		}

		///Return the view
		[HttpGet]
		public ActionResult Login()
		{
			Login login = new Login();
			if (TempData.ContainsKey("Username"))
			{
				login.UserName = (string)TempData["Username"];
			}
			return View(login);
		}
		[HttpPost]
		//View by the username to see if form is valid if it is then let them view the store else return form.
		public ActionResult Login(Login form)
		{
			ActionResult response;
			try
			{

				if (ModelState.IsValid)
				{
					UserDO userDataObject = _UserDataAccess.ViewUserByUserName(form.UserName);

					if (userDataObject.Username.Equals(form.UserName) && userDataObject.Email.Equals(form.Email) && userDataObject.Password.Equals(form.Password))
					{
						Session["Username"] = userDataObject.Username;
						Session["UserID"] = userDataObject.UserId;
						Session["RoleName"] = userDataObject.RoleName;
						userDataObject.Email = form.Email;
						response = RedirectToAction("Store", "PC");

					}
					else
					{
						response = View(form);
					}
				}
				else
				{
					response = View(form);
				}
			}
			catch
			{
				response = View(form);
			}
			return response;

		}



		//Find the username and viewby it to delete all information on that account.
		[HttpGet]
		public ActionResult Delete(string username)
		{
			ActionResult response;
			string userName = (string)Session["UserName"];
			if (username != userName)
			{
				UserDO user = _UserDataAccess.ViewUserByUserName(username);
				User deletedUser = _mapper.MapDOtoPO(user);
				long userID = deletedUser.UserID;
				//Make sure that ID isnt 0
				if (userID > 0)
				{
					_UserDataAccess.DeleteUser(userID);
					response = RedirectToAction("ViewUsers", "Account");
				}
				else
				{
					response = RedirectToAction("ViewUsers", "Account");
				}
			}
			else
			{
				response = RedirectToAction("Index", "Home");
			}
			return response;

		}
		//View by all users by their ID
		public ActionResult ViewUsers()
		{
			ActionResult response;
			if ((string)Session["RoleName"] == "Admin")
			{//Get the list of users available and map  to return a view of them.
				List<UserDO> allUsers = _UserDataAccess.ViewUsers();

				List<User> mappedUsers = new List<User>();

				foreach (UserDO dataObject in allUsers)
				{
					mappedUsers.Add(_mapper.MapDOtoPO(dataObject));
				}

				response = View(mappedUsers);
			}
			else
			{
				response = RedirectToAction("Index", "Home");
			}
			return response;
		}
		//View users by their details through username
		public ActionResult UserDetails(string username)
		{

			ActionResult response;

			UserDO user = _UserDataAccess.ViewUserByUserName(username);
			User detailUser = _mapper.MapDOtoPO(user);
			long userID = detailUser.UserID;
			if (userID > 0)
			{
				response = View(detailUser);
			}
			else
			{
				response = RedirectToAction("ViewUsers", "Account");

			}


			return response;
		}

		//Grab username to update the user
		[HttpGet]
		public ActionResult Update(string username)
		{
			ActionResult result;
			string UserName = (string)Session["UserName"];
			if (!(Session["RoleName"] == null) && ((string)Session["RoleName"] == "Admin"))
			{
				if (username != UserName)
				{UserDO user = _UserDataAccess.View_By_Username_For_Update(username);
					UpdateUser detailsUser = mapper.MapDOtoPO(user);
					long userID = detailsUser.UserID;
					//Check to make sure there is an id and not zero
					if (userID > 0)
					{
						//View the roles avaliable
						List<UserDO> userDO = _UserDataAccess.ViewRoles();
						//Create a new list with those roles
						List<UpdateUser> roles = new List<UpdateUser>();
						//Create a new selectListItem 
						List<SelectListItem> items = new List<SelectListItem>();
						//For each user role in the database map to Update User
						foreach (UserDO roleObject in userDO)
						{
							roles.Add(mapper.MapDOtoPO(roleObject));
						}
						foreach (UpdateUser role in roles)
						{
							
							items.Add(new SelectListItem { Text = role.RoleName, Value = role.RoleID.ToString() });
						

						}

						UsersViewModel vm = new UsersViewModel(items);
						vm.Update = detailsUser;

						
						result = View(vm);
					}
					else
					{
						result = RedirectToAction("Store", "PC");
					}
					return result;

				}
				else
				{
					result = RedirectToAction("ViewUsers", "Account");
				}
			}
			else
			{
				result = RedirectToAction("ViewUsers", "Account");
			}
			return result;
		}
		//grab form and check if its accurate if it is then update
		[HttpPost]
		public ActionResult Update(UsersViewModel form)
		{
			ActionResult response;
			if (ModelState.IsValid)
			{
				UserDO userDO = mapper.MapPOtoDO(form.Update);
				_UserDataAccess.UpdateUser(userDO);
				response = RedirectToAction("ViewUsers", "Account");
			}
			else
			{
				response = View(form);
			}
			return response;
		}
		/// <summary>
		/// We clear our session if link is clicked to send them to login
		/// </summary>
		/// <returns></returns>
		public ActionResult LogOut()
		{
			ActionResult response;
			Session.Clear();

			response = RedirectToAction("Login", "Account");
			return response;
		}
	}
}