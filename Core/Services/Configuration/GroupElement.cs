using System.Configuration;

namespace Core.Services.Configuration
{
    /// <summary>
    /// This class represents a group element of the configuration
    /// </summary>
    public class GroupElement : ConfigurationElement
    {
        private const string NAME_ATTRIBUTE = "name";
        private const string COMPRESS_ATTRIBUTE = "compress";
        private const string FILES_PROPERTY = "files";

        internal const string KEY = NAME_ATTRIBUTE;

        public GroupElement()
        {
			
        }

        public GroupElement(string name)
        {
            this[NAME_ATTRIBUTE] = name;
        }
        /// <summary>
        /// The name of the group
        /// </summary>
        [ConfigurationProperty(NAME_ATTRIBUTE,IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)this[NAME_ATTRIBUTE];
            }
        }
        /// <summary>
        /// Specifies that compression should be used for the files in this group
        /// </summary>
        [ConfigurationProperty(COMPRESS_ATTRIBUTE, IsRequired = false)]
        public bool Compress
        {
            get
            {
                if (this[COMPRESS_ATTRIBUTE] == null) return false;
                return (bool)this[COMPRESS_ATTRIBUTE];
            }
        }

        /// <summary>
        /// A collection of file attributes
        /// </summary>
        [ConfigurationProperty(FILES_PROPERTY, IsRequired = false)]
        public FileElementCollection Files
        {
            get
            {
                return (FileElementCollection)this[FILES_PROPERTY];
            }
        }

        public override bool Equals(object compareTo)
        {
            if(null == compareTo) return false;
            if(typeof(GroupElement) != compareTo.GetType()) return false;

            return Name == ((GroupElement) compareTo).Name;
        }

		public override int GetHashCode()
		{
			return base.GetHashCode()^3+3;
		}

    }
}