using PC_Shop_BLL.Models;
using PC_Shop_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Shop_BLL.Mapper
{
	class BLLMapper
	{
		public BLL_Model MapperBLLtoPCDO(PcDO from)
		{
			BLL_Model to = new BLL_Model();
			to.price = from.Price;
			return to;
		}
		public PcDO pcDOtoBLL(BLL_Model form)
		{
			PcDO to = new PcDO();
			to.Price = form.price;
			return to;
		}
	}
}
