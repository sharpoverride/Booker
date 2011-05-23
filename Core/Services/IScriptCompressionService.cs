namespace Core.Services
{
    /// <summary>
    /// This is service is used to compress scripts
    /// </summary>
    public interface IScriptCompressionService
    {
        /// <summary>
        /// Uses a compression method on the given string
        /// </summary>
        /// <param name="content">The file's content</param>
        /// <returns></returns>
        string Compress(string content);
    }
}