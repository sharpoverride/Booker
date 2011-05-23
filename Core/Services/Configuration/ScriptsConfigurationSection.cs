using System.Configuration;

namespace Core.Services.Configuration
{
    /// <summary>
    /// This class represents the scripts configuration
    /// </summary>
    public class ScriptsConfigurationSection : ConfigurationSection
    {
        private const string GROUP_PROPERTY = "groups";

        /// <summary>
        /// A collection of GroupElements
        /// </summary>
        [ConfigurationProperty(GROUP_PROPERTY, IsRequired = false)]
        public GroupElementCollection Groups
        {
            get
            {
                return (GroupElementCollection)this[GROUP_PROPERTY];
            }
        }
    }
}