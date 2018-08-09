
using BLL.Model;
using PC_Shop_DAL.DAO;
using PC_Shop_DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BO
{
	public class OrderBLO

	{


		public long ViewNumberOne(List<OrderBO> boModel)
		{
			
				var query =
					(from x in boModel
					 group x.UserID by x.UserID into g
					 orderby g.Sum() descending
					 select g.Key).Take(1);
					 return query.FirstOrDefault();

			
			
			
		}






	}
}
