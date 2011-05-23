using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using Core.Services.Configuration;

namespace Core.Services.Implementations
{
	/// <summary>
	/// Implements a javascript provider based on settings found in the web.config file
	/// </summary>
    public class JavaScriptConfigurationProviderService:IJavaScriptProviderService
    {
		private readonly WebApplicationFileService webApplicationFileService;
        private readonly IJavaScriptCompressionService _scriptCompressionService;
        private const string SCRIPT_SECTION_NAME = "scriptsConfiguration";

        public JavaScriptConfigurationProviderService(
            IJavaScriptCompressionService scriptCompressionService,
			WebApplicationFileService webApplicationFileService)
        {
            _scriptCompressionService = scriptCompressionService;
			this.webApplicationFileService = webApplicationFileService;
        }
        #region IScriptsConfigurationSectionService Members

        /// <summary>
        /// Gets the script based on it's name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetScript(string name)
        {
            var groupName = name;
            var section = (ScriptsConfigurationSection) ConfigurationManager.GetSection(SCRIPT_SECTION_NAME);

            if(null == section) throw new ArgumentNullException("You haven't configured the script section correctly",(Exception)null);

            if(!section.Groups.Contains(new GroupElement(groupName)))
                throw new ConfigurationErrorsException("The specified group name "+ groupName +" was not found in the scripts.config configuration");

            var response = new StringBuilder();
            foreach (var theGroup in section.Groups)
            {
                // only choosing the group that was requested
                if (theGroup.Name == groupName)
                {
                    foreach (var file in theGroup.Files)
                    {
                        // getting the file path

						var fileInfo = this.webApplicationFileService.RetrieveFromPartialPath(file.Path);


                        if (fileInfo.Exists)
                        {
                            using (StreamReader contentReader = this.webApplicationFileService.GetReaderFor(fileInfo))
                            {
                                if (!theGroup.Compress)
                                {
                                    // just add the text if compression is disabled
                                    response.Append(contentReader.ReadToEnd());
                                }
                                else
                                {

                                    string contentValue = contentReader.ReadToEnd();
                                    var compressedContent =
                                        _scriptCompressionService.Compress(contentValue);
                                    response.Append(compressedContent);

                                }
                                response.Append(Environment.NewLine);

                            }
                        }
                        else
                        {
                            throw new FileNotFoundException("The specified path '"+fileInfo.FullName+"' was not found. Check the scripts.config file.");
                        }

                    }
                }
            }
            return response.ToString();

        }

        #endregion
    }
}