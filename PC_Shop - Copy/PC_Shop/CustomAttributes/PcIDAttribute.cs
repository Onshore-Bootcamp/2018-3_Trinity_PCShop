using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PC_Shop.CustomAttributes
{
	public class PcIDAttribute: ActionFilterAttribute
	{
		private long _PcID;
		private string _Route;
		public PcIDAttribute(string Route)
		{
			_Route = Route;
			
		}
		//Checks to pcID to make sure it is valid
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var pcID = filterContext.HttpContext.Session["PcID"];
			if (pcID ==null)
			{
				filterContext.Result = new RedirectResult(_Route, false);
			}
		}
	}
}