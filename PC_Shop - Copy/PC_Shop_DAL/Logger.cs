using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Shop_DAL
{
	public class Logger
	{
		/// <summary>
		/// We create a method for loging the errors in the application so we know what went wrong.
		/// </summary>
		private static string _LogPath = ConfigurationManager.AppSettings["ErrorLogPath"];
		public void ErrorLog(string className, string methodName, Exception sqlEx, string level = "Error")
		{
			string stackTrace = sqlEx.StackTrace;
			//Writes to a Log Fle  for errors
			using (StreamWriter errorWriter = new StreamWriter(_LogPath, true))
			{
				errorWriter.WriteLine(new string('-', 40));
				errorWriter.WriteLine($" Class:{className} Method:{methodName}/{DateTime.Now.ToString()}/{level}\n{sqlEx.Message}\n{stackTrace}");
			}


		}
	}
}
