using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Core.Services.Configuration;

namespace Core.Services.Implementations
{
	/// <summary>
	/// Implements a css provider based on settings found in the web.config file
	/// </summary>
    public class CssConfigurationProviderService : ICssProviderService
    {
        private readonly ICssScriptCompressionService _ScriptCompressionService;
		private readonly WebApplicationFileService _FileService;

        private const string SCRIPT_SECTION_NAME = "cssConfiguration";

        public CssConfigurationProviderService(ICssScriptCompressionService scriptCompressionService,
			WebApplicationFileService fileService)
        {
            _ScriptCompressionService = scriptCompressionService;
			_FileService = fileService;
        }

        #region ICssProviderService Members

        public string GetCss(string group)
        {
            var section = (ScriptsConfigurationSection)ConfigurationManager.GetSection(SCRIPT_SECTION_NAME);

            if (null == section) throw new ArgumentNullException("You haven't configured the script section correctly", (Exception)null);

            if (!section.Groups.Contains(new GroupElement(group)))
                throw new ConfigurationErrorsException("The specified group name " + group + " was not found in the css.config configuration");
			
            var response = new StringBuilder();
            foreach (var currentGroup in section.Groups)
            {
                // only choosing the group that was requested
                if (currentGroup.Name == group)
                {
                    foreach (var file in currentGroup.Files)
                    {
                        // getting the file path

						var fileInfo = _FileService.RetrieveFromPartialPath(file.Path);

                        if (fileInfo.Exists)
                        {
                           using(var contentReader = _FileService.GetReaderFor(fileInfo))
                            {
                                if (!currentGroup.Compress)
                                {
                                    // just add the text if compression is disabled
                                    response.Append(contentReader.ReadToEnd());
                                }
                                else
                                {

                                    string lValue = contentReader.ReadToEnd();
                                    // using Yahoo's compressor 
                                    var lCompressedForm =
                                        _ScriptCompressionService.Compress(lValue);
                                    response.Append(lCompressedForm);

                                }
                                response.Append(Environment.NewLine);

                            }
                        }
                        else
                        {
                            throw new FileNotFoundException("The specified path '" + fileInfo.FullName + "' was not found. Check the scripts.config file.");
                        }

                    }
                }
            }
            return response.ToString();
        }

        #endregion
    }
}