using System.Configuration;

namespace Core.Services.Configuration
{
    /// <summary>
    /// This class represents the file element of the configuration
    /// </summary>
    public class FileElement : ConfigurationElement
    {
        private const string PATH_ATTRIBUTE = "path";
		
        internal const string KEY = PATH_ATTRIBUTE;
        /// <summary>
        /// Represents the path to the file
        /// </summary>
        [ConfigurationProperty(PATH_ATTRIBUTE,IsRequired = true)]
        public string Path
        {
            get
            {
                return (string) this[PATH_ATTRIBUTE];
            }
        }
        
    }
}