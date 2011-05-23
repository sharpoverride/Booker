using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Core;
using Core.Services;
using Core.Services.Implementations;

namespace Web.Bootstrap.Container
{
	/// <summary>
	/// This class registers the implementations of minification services
	/// </summary>
	public static class MinificationRegistrar
	{
		private const string DEFAULT_CACHE_MANAGER = "DefaultCacheManager";
		private const string CSS_COMPRESSION_SERVICE_NAME = "CssCompressionService";
		private const string JS_COMPRESSION_SERVICE_NAME = "JsCompressionService";

		public static void RegisterWith( IUnityContainer container )
		{
			container.RegisterType<ICssScriptCompressionService, MicrosoftMinifierCssCompresionService>();
			container.RegisterType<IJavaScriptCompressionService, MicrosoftMinifierJavascriptCompressionService>();

			container.RegisterType<ICssProviderService, CssConfigurationProviderService>();//(CSS_COMPRESSION_SERVICE_NAME);
			container.RegisterType<IJavaScriptProviderService, JavaScriptConfigurationProviderService>();//(JS_COMPRESSION_SERVICE_NAME);

			var scriptCacheExpiration = new AbsoluteTime(DateTime.Now.Add(AppSettings.DefaultCacheAbsoluteTimeExpiration));
			/*
			container.RegisterType<Services.ICssProviderService, Services.Implementations.CachedCssProviderSerive>(
				new InjectionConstructor(
				 new ResolvedParameter<Services.ICssProviderService>(CSS_COMPRESSION_SERVICE_NAME),
				 new ResolvedParameter<ICacheManager>(DEFAULT_CACHE_MANAGER),
				 scriptCacheExpiration));

			container.RegisterType<Services.IJavaScriptProviderService, Services.Implementations.CachedJavaScriptProviderService>(
				new InjectionConstructor(
				 new ResolvedParameter<Services.IJavaScriptProviderService>(JS_COMPRESSION_SERVICE_NAME),
				 new ResolvedParameter<ICacheManager>(DEFAULT_CACHE_MANAGER),
				 scriptCacheExpiration));
			*/
		}
	}
}