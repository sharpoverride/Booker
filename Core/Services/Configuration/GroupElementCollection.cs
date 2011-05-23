namespace Core.Services.Configuration
{
    /// <summary>
    /// This class represents a collection of group elements
    /// </summary>
    public class GroupElementCollection : NamedConfigurationElementCollection<GroupElement>
    {
        public GroupElementCollection()
            :base("group",GroupElement.KEY)
        {
			
        }
    }
}