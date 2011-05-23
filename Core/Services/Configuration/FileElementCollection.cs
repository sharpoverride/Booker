namespace Core.Services.Configuration
{
    /// <summary>
    /// This class represents a collection of FileElements
    /// </summary>
    public class FileElementCollection : NamedConfigurationElementCollection<FileElement>
    {
        public FileElementCollection() 
            :base("file",FileElement.KEY)
        {
        }
    }
}