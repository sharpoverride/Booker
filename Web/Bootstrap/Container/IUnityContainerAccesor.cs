using Microsoft.Practices.Unity;

namespace Web.Bootstrap.Container
{
	public interface IUnityContainerAccessor
	{
		IUnityContainer Container { get; }
	}
}