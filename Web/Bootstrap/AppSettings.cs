using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Web.Bootstrap
{
	public class AppSettings
	{
		public static TimeSpan DefaultCacheAbsoluteTimeExpiration = TimeSpan.Parse(ConfigurationManager.AppSettings["DefaultCacheAbsoluteTimeExpiration"]);
		public static TimeSpan StaticFileHttpMaxAge = TimeSpan.Parse(ConfigurationManager.AppSettings["StaticFileHttpMaxAge"]);

		public static string ScriptsVersion = ConfigurationManager.AppSettings["ScriptsVersion"];
	}
}