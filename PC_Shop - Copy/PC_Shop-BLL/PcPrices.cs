using PC_Shop_BLL.Mapper;
using PC_Shop_BLL.Models;
using PC_Shop_DAL.DAO;
using PC_Shop_DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Shop_BLL
{
	public class PcPrices
	{

private readonly PcDAO pcDao;

		public PcPrices()
		{
			string connectionString = ConfigurationManager.ConnectionStrings["DataSource"].ConnectionString;
			pcDao = new PcDAO(connectionString);
		}
		BLLMapper mapper = new BLLMapper();
		BLL_Model model = new BLL_Model();
		public List<BLL_Model> Grab()
		{
			List<PcDO> pcdo= pcDao.ViewPrices();
			List<BLL_Model> model = new List<BLL_Model>();
			foreach (PcDO sumObject in pcdo)
			{

				model.Add(mapper.MapperBLLtoPCDO(sumObject));
			}
			return model;
		}

	}
}
