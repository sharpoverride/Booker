using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace Core.Services.Implementations
{
	/// <summary>
	/// This is a decorator over the real ICssProviderService implementation
	/// </summary>
	public class CachedCssProviderSerive : ICssProviderService
	{
		#region Constants
		const string CACHE_KEY_PREFIX = "Css_";
		#endregion

		#region Fields
		private ICssProviderService realService;
		private ICacheManager cacheManager;
		private static readonly object padlock = new object();
		private ICacheItemExpiration expiration;
		#endregion

		#region Ctor
		public CachedCssProviderSerive( ICssProviderService realService,
			ICacheManager cacheManager, ICacheItemExpiration expiration )
		{
			this.realService = realService;
			this.cacheManager = cacheManager;
			this.expiration = expiration;
		}
		#endregion

		#region ICssProviderService Members
		/// <summary>
		/// Returns the requested script
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public string GetCss( string group )
		{

			object result = null;
			// returns the value from the cache
			result = cacheManager.GetData(CACHE_KEY_PREFIX + group);
			// if no result is found
			if (result == null)
			{
				// apply a lock - critical section management
				lock (padlock)
				{
					// why!? tries to retrieve the data again
					result = cacheManager.GetData(CACHE_KEY_PREFIX + group);
					// if still it's not found
					if (result == null)
					{
						// gets the value from the real service
						result = realService.GetCss(group);
						// and it saves it to cache with the same _Expiration perios as the browser cache 

						cacheManager.Add(CACHE_KEY_PREFIX + group, result, CacheItemPriority.Normal, null, expiration);
					}
				}
			}


			return (string)result;
		}

		#endregion
	}
}
