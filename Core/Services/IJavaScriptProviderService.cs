namespace Core.Services
{
    public interface IJavaScriptProviderService
    {
        /// <summary>
        /// Gets the script by it's name and returns it to the client
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The script's content</returns>
        string GetScript(string name);
    }
}