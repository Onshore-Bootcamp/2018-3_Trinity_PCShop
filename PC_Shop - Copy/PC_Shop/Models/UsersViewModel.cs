using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PC_Shop.Models
{
	public class UsersViewModel
	{
		//We create a constuctor for our view model
		public UsersViewModel(IEnumerable<SelectListItem> selectList)
		{ 
			//make selec list = to our get and set select list
			Select = selectList;
		}
		//Create our constructor for our user to prevent null values
		public UsersViewModel()
		{
			//Create a new list using our Select parameter
			Select = new List<SelectListItem>()
			{
				//Fill our new list with values to prevent not having default values.
				new SelectListItem() { Text = "Admin", Value = "1" },
				new SelectListItem() { Text = "Seller", Value = "2" },
				new SelectListItem() { Text = "Buyer", Value = "3"}
			};


		}
		//Create an instatation of our class update users 
		public UpdateUser Update { get; set; }
		//Create an list of select to get set;
		public IEnumerable<SelectListItem> Select { get; set; }
	}
}