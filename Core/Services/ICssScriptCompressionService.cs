namespace Core.Services
{
	/// <summary>
	/// This interface adds a more meaningfull name to the 
	/// <c ref="Core.Services.IScriptCompressionService">IScriptCompressionService</c>
	/// Implementors of this interface will offer ways
	/// of minifying Stylesheets.
	/// </summary>
    public interface ICssScriptCompressionService : IScriptCompressionService
    {
    }
}