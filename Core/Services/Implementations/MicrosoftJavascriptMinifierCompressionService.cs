using Microsoft.Ajax.Utilities;

namespace Core.Services.Implementations
{
    /// <summary>
    /// This class offers a script compression implmentation using 
    /// the Microsoft Ajax Minifier
    /// </summary>
    public class MicrosoftMinifierJavascriptCompressionService : IJavaScriptCompressionService
    {
        #region IScriptCompressionService Members
        /// <summary>
        /// Compressed the javascript content
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Compress(string content)
        {
            // The CodeSettings object specifies how the content
            // will be minified
            var codeSettings = new CodeSettings();
            codeSettings.RemoveUnneededCode = true;
            codeSettings.LocalRenaming = LocalRenaming.CrunchAll;
            codeSettings.OutputMode = OutputMode.SingleLine;
            codeSettings.StripDebugStatements = true;
            codeSettings.IndentSize = 0;

			var value = new Minifier().MinifyJavaScript(content, codeSettings);
            return value;
        }

        #endregion
    }
}