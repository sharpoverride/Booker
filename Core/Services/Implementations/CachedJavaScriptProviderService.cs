
using Core.Services;
using Microsoft.Practices.EnterpriseLibrary.Caching;
namespace Core.Services.Implementations
{
	public class CachedJavaScriptProviderService : IJavaScriptProviderService
	{
		#region Constants
		const string CACHE_KEY_PREFIX = "Js_";
		#endregion
		#region Fields
		private IJavaScriptProviderService realService;
		private ICacheManager cacheManager;
		private static readonly object padLock = new object();
		private ICacheItemExpiration expiration;
		#endregion

		#region Ctor
		public CachedJavaScriptProviderService( IJavaScriptProviderService realService,
			ICacheManager cacheManager, ICacheItemExpiration expiration )
		{
			this.realService = realService;
			this.cacheManager = cacheManager;
			this.expiration = expiration;
		}
		#endregion

		#region IScriptsConfigurationSectionService Members
		/// <summary>
		/// Returns the requested script
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public string GetScript( string name )
		{
			object result = null;
			// returns the value from the cache
			result = cacheManager.GetData(CACHE_KEY_PREFIX + name);
			// if no result is found
			if (result == null)
			{
				// apply a lock - critical section management
				lock (padLock)
				{
					// why!? tries to retrieve the data again
					result = cacheManager.GetData(CACHE_KEY_PREFIX + name);
					// if still it's not found
					if (result == null)
					{
						// gets the value from the real service
						result = realService.GetScript(name);
						// and it saves it to cache with the same _Expiration perios as the browser cache 

						cacheManager.Add(CACHE_KEY_PREFIX + name, result, CacheItemPriority.Normal, null, expiration);
					}
				}
			}


			return (string)result;
		}

		#endregion
	}
}
