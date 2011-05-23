using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.Win32;

namespace Core.Services
{
	/// <summary>
	/// This service provides the mime type of a file based on the file extension provided
	/// </summary>
    public class MimeTypeResolverService
	{
		#region Private Members

		private const string REGISTRY_KEY = "Content Type";

		#endregion

		#region Public Members

		/// <summary>
		/// Tries to get the file MIME type from the Registry
		/// </summary>
		public  bool TryGetMIMETypeFromRegistry( string filePath, out string mimeType )
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentNullException("filePath");
			}

			mimeType = null;

			string fileExtension = Path.GetExtension(filePath).ToLowerInvariant();
			RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(fileExtension);

			if (registryKey != null && registryKey.GetValue(REGISTRY_KEY) != null)
			{
				mimeType = registryKey.GetValue(REGISTRY_KEY).ToString();
				return true;
			}

			return false;
		}

		/// <summary>
		/// Gets the MIME type for a file based on an extension-MIME type association. 
		/// If no association exists for the supplied file extension the default returned value is 'application/octet-stream'.
		/// </summary>
		public  string GetMIMEType( string filePath )
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentNullException("filePath");
			}

			string fileExtension = Path.GetExtension(filePath).ToLowerInvariant();

			string resolvedMimeType;
			switch (fileExtension)
			{
				case ".3dm":
					resolvedMimeType = "x-world/x-3dmf";
					break;
				case ".3dmf":
					resolvedMimeType = "x-world/x-3dmf";
					break;
				case ".a":
					resolvedMimeType = "application/octet-stream";
					break;
				case ".aab":
					resolvedMimeType = "application/x-authorware-bin";
					break;
				case ".aam":
					resolvedMimeType = "application/x-authorware-map";
					break;
				case ".aas":
					resolvedMimeType = "application/x-authorware-seg";
					break;
				case ".abc":
					resolvedMimeType = "text/vnd.abc";
					break;
				case ".acgi":
					resolvedMimeType = "text/html";
					break;
				case ".afl":
					resolvedMimeType = "video/animaflex";
					break;
				case ".ai":
					resolvedMimeType = "application/postscript";
					break;
				case ".aif":
					resolvedMimeType = "audio/aiff";
					break;
				case ".aifc":
					resolvedMimeType = "audio/aiff";
					break;
				case ".aiff":
					resolvedMimeType = "audio/aiff";
					break;
				case ".aim":
					resolvedMimeType = "application/x-aim";
					break;
				case ".aip":
					resolvedMimeType = "text/x-audiosoft-intra";
					break;
				case ".ani":
					resolvedMimeType = "application/x-navi-animation";
					break;
				case ".aos":
					resolvedMimeType = "application/x-nokia-9000-communicator-add-on-software";
					break;
				case ".aps":
					resolvedMimeType = "application/mime";
					break;
				case ".arc":
					resolvedMimeType = "application/octet-stream";
					break;
				case ".arj":
					resolvedMimeType = "application/arj";
					break;
				case ".art":
					resolvedMimeType = "image/x-jg";
					break;
				case ".asf":
					resolvedMimeType = "video/x-ms-asf";
					break;
				case ".asm":
					resolvedMimeType = "text/x-asm";
					break;
				case ".asp":
					resolvedMimeType = "text/asp";
					break;
				case ".asx":
					resolvedMimeType = "video/x-ms-asf";
					break;
				case ".au":
					resolvedMimeType = "audio/basic";
					break;
				case ".avi":
					resolvedMimeType = "video/avi";
					break;
				case ".avs":
					resolvedMimeType = "video/avs-video";
					break;
				case ".bcpio":
					resolvedMimeType = "application/x-bcpio";
					break;
				case ".bin":
					resolvedMimeType = "application/octet-stream";
					break;
				case ".bm":
					resolvedMimeType = "image/bmp";
					break;
				case ".bmp":
					resolvedMimeType = "image/bmp";
					break;
				case ".boo":
					resolvedMimeType = "application/book";
					break;
				case ".book":
					resolvedMimeType = "application/book";
					break;
				case ".boz":
					resolvedMimeType = "application/x-bzip2";
					break;
				case ".bsh":
					resolvedMimeType = "application/x-bsh";
					break;
				case ".bz":
					resolvedMimeType = "application/x-bzip";
					break;
				case ".bz2":
					resolvedMimeType = "application/x-bzip2";
					break;
				case ".c":
					resolvedMimeType = "text/plain";
					break;
				case ".c++":
					resolvedMimeType = "text/plain";
					break;
				case ".cat":
					resolvedMimeType = "application/vnd.ms-pki.seccat";
					break;
				case ".cc":
					resolvedMimeType = "text/plain";
					break;
				case ".ccad":
					resolvedMimeType = "application/clariscad";
					break;
				case ".cco":
					resolvedMimeType = "application/x-cocoa";
					break;
				case ".cdf":
					resolvedMimeType = "application/cdf";
					break;
				case ".cer":
					resolvedMimeType = "application/pkix-cert";
					break;
				case ".cha":
					resolvedMimeType = "application/x-chat";
					break;
				case ".chat":
					resolvedMimeType = "application/x-chat";
					break;
				case ".class":
					resolvedMimeType = "application/java";
					break;
				case ".com":
					resolvedMimeType = "application/octet-stream";
					break;
				case ".conf":
					resolvedMimeType = "text/plain";
					break;
				case ".cpio":
					resolvedMimeType = "application/x-cpio";
					break;
				case ".cpp":
					resolvedMimeType = "text/x-c";
					break;
				case ".cpt":
					resolvedMimeType = "application/x-cpt";
					break;
				case ".crl":
					resolvedMimeType = "application/pkcs-crl";
					break;
				case ".crt":
					resolvedMimeType = "application/pkix-cert";
					break;
				case ".csh":
					resolvedMimeType = "application/x-csh";
					break;
				case ".css":
					resolvedMimeType = "text/css";
					break;
				case ".cxx":
					resolvedMimeType = "text/plain";
					break;
				case ".dcr":
					resolvedMimeType = "application/x-director";
					break;
				case ".deepv":
					resolvedMimeType = "application/x-deepv";
					break;
				case ".def":
					resolvedMimeType = "text/plain";
					break;
				case ".der":
					resolvedMimeType = "application/x-x509-ca-cert";
					break;
				case ".dif":
					resolvedMimeType = "video/x-dv";
					break;
				case ".dir":
					resolvedMimeType = "application/x-director";
					break;
				case ".dl":
					resolvedMimeType = "video/dl";
					break;
				case ".doc":
					resolvedMimeType = "application/msword";
					break;
				case ".dot":
					resolvedMimeType = "application/msword";
					break;
				case ".dp":
					resolvedMimeType = "application/commonground";
					break;
				case ".drw":
					resolvedMimeType = "application/drafting";
					break;
				case ".dump":
					resolvedMimeType = "application/octet-stream";
					break;
				case ".dv":
					resolvedMimeType = "video/x-dv";
					break;
				case ".dvi":
					resolvedMimeType = "application/x-dvi";
					break;
				case ".dwf":
					resolvedMimeType = "model/vnd.dwf";
					break;
				case ".dwg":
					resolvedMimeType = "image/vnd.dwg";
					break;
				case ".dxf":
					resolvedMimeType = "image/vnd.dwg";
					break;
				case ".dxr":
					resolvedMimeType = "application/x-director";
					break;
				case ".el":
					resolvedMimeType = "text/x-script.elisp";
					break;
				case ".elc":
					resolvedMimeType = "application/x-elc";
					break;
				case ".env":
					resolvedMimeType = "application/x-envoy";
					break;
				case ".eps":
					resolvedMimeType = "application/postscript";
					break;
				case ".es":
					resolvedMimeType = "application/x-esrehber";
					break;
				case ".etx":
					resolvedMimeType = "text/x-setext";
					break;
				case ".evy":
					resolvedMimeType = "application/envoy";
					break;
				case ".exe":
					resolvedMimeType = "application/octet-stream";
					break;
				case ".f":
					resolvedMimeType = "text/plain";
					break;
				case ".f77":
					resolvedMimeType = "text/x-fortran";
					break;
				case ".f90":
					resolvedMimeType = "text/plain";
					break;
				case ".fdf":
					resolvedMimeType = "application/vnd.fdf";
					break;
				case ".fif":
					resolvedMimeType = "image/fif";
					break;
				case ".fli":
					resolvedMimeType = "video/fli";
					break;
				case ".flo":
					resolvedMimeType = "image/florian";
					break;
				case ".flx":
					resolvedMimeType = "text/vnd.fmi.flexstor";
					break;
				case ".fmf":
					resolvedMimeType = "video/x-atomic3d-feature";
					break;
				case ".for":
					resolvedMimeType = "text/x-fortran";
					break;
				case ".fpx":
					resolvedMimeType = "image/vnd.fpx";
					break;
				case ".frl":
					resolvedMimeType = "application/freeloader";
					break;
				case ".funk":
					resolvedMimeType = "audio/make";
					break;
				case ".g":
					resolvedMimeType = "text/plain";
					break;
				case ".g3":
					resolvedMimeType = "image/g3fax";
					break;
				case ".gif":
					resolvedMimeType = "image/gif";
					break;
				case ".gl":
					resolvedMimeType = "video/gl";
					break;
				case ".gsd":
					resolvedMimeType = "audio/x-gsm";
					break;
				case ".gsm":
					resolvedMimeType = "audio/x-gsm";
					break;
				case ".gsp":
					resolvedMimeType = "application/x-gsp";
					break;
				case ".gss":
					resolvedMimeType = "application/x-gss";
					break;
				case ".gtar":
					resolvedMimeType = "application/x-gtar";
					break;
				case ".gz":
					resolvedMimeType = "application/x-gzip";
					break;
				case ".gzip":
					resolvedMimeType = "application/x-gzip";
					break;
				case ".h":
					resolvedMimeType = "text/plain";
					break;
				case ".hdf":
					resolvedMimeType = "application/x-hdf";
					break;
				case ".help":
					resolvedMimeType = "application/x-helpfile";
					break;
				case ".hgl":
					resolvedMimeType = "application/vnd.hp-hpgl";
					break;
				case ".hh":
					resolvedMimeType = "text/plain";
					break;
				case ".hlb":
					resolvedMimeType = "text/x-script";
					break;
				case ".hlp":
					resolvedMimeType = "application/hlp";
					break;
				case ".hpg":
					resolvedMimeType = "application/vnd.hp-hpgl";
					break;
				case ".hpgl":
					resolvedMimeType = "application/vnd.hp-hpgl";
					break;
				case ".hqx":
					resolvedMimeType = "application/binhex";
					break;
				case ".hta":
					resolvedMimeType = "application/hta";
					break;
				case ".htc":
					resolvedMimeType = "text/x-component";
					break;
				case ".htm":
					resolvedMimeType = "text/html";
					break;
				case ".html":
					resolvedMimeType = "text/html";
					break;
				case ".htmls":
					resolvedMimeType = "text/html";
					break;
				case ".htt":
					resolvedMimeType = "text/webviewhtml";
					break;
				case ".htx":
					resolvedMimeType = "text/html";
					break;
				case ".ice":
					resolvedMimeType = "x-conference/x-cooltalk";
					break;
				case ".ico":
					resolvedMimeType = "image/x-icon";
					break;
				case ".idc":
					resolvedMimeType = "text/plain";
					break;
				case ".ief":
					resolvedMimeType = "image/ief";
					break;
				case ".iefs":
					resolvedMimeType = "image/ief";
					break;
				case ".iges":
					resolvedMimeType = "application/iges";
					break;
				case ".igs":
					resolvedMimeType = "application/iges";
					break;
				case ".ima":
					resolvedMimeType = "application/x-ima";
					break;
				case ".imap":
					resolvedMimeType = "application/x-httpd-imap";
					break;
				case ".inf":
					resolvedMimeType = "application/inf";
					break;
				case ".ins":
					resolvedMimeType = "application/x-internett-signup";
					break;
				case ".ip":
					resolvedMimeType = "application/x-ip2";
					break;
				case ".isu":
					resolvedMimeType = "video/x-isvideo";
					break;
				case ".it":
					resolvedMimeType = "audio/it";
					break;
				case ".iv":
					resolvedMimeType = "application/x-inventor";
					break;
				case ".ivr":
					resolvedMimeType = "i-world/i-vrml";
					break;
				case ".ivy":
					resolvedMimeType = "application/x-livescreen";
					break;
				case ".jam":
					resolvedMimeType = "audio/x-jam";
					break;
				case ".jav":
					resolvedMimeType = "text/plain";
					break;
				case ".java":
					resolvedMimeType = "text/plain";
					break;
				case ".jcm":
					resolvedMimeType = "application/x-java-commerce";
					break;
				case ".jfif":
					resolvedMimeType = "image/jpeg";
					break;
				case ".jfif-tbnl":
					resolvedMimeType = "image/jpeg";
					break;
				case ".jpe":
					resolvedMimeType = "image/jpeg";
					break;
				case ".jpeg":
					resolvedMimeType = "image/jpeg";
					break;
				case ".jpg":
					resolvedMimeType = "image/jpeg";
					break;
				case ".jps":
					resolvedMimeType = "image/x-jps";
					break;
				case ".js":
					resolvedMimeType = "text/javascript";
					break;
				case ".jut":
					resolvedMimeType = "image/jutvision";
					break;
				case ".kar":
					resolvedMimeType = "audio/midi";
					break;
				case ".ksh":
					resolvedMimeType = "application/x-ksh";
					break;
				case ".la":
					resolvedMimeType = "audio/nspaudio";
					break;
				case ".lam":
					resolvedMimeType = "audio/x-liveaudio";
					break;
				case ".latex":
					resolvedMimeType = "application/x-latex";
					break;
				case ".lha":
					resolvedMimeType = "application/octet-stream";
					break;
				case ".lhx":
					resolvedMimeType = "application/octet-stream";
					break;
				case ".list":
					resolvedMimeType = "text/plain";
					break;
				case ".lma":
					resolvedMimeType = "audio/nspaudio";
					break;
				case ".log":
					resolvedMimeType = "text/plain";
					break;
				case ".lsp":
					resolvedMimeType = "application/x-lisp";
					break;
				case ".lst":
					resolvedMimeType = "text/plain";
					break;
				case ".lsx":
					resolvedMimeType = "text/x-la-asf";
					break;
				case ".ltx":
					resolvedMimeType = "application/x-latex";
					break;
				case ".lzh":
					resolvedMimeType = "application/octet-stream";
					break;
				case ".lzx":
					resolvedMimeType = "application/octet-stream";
					break;
				case ".m":
					resolvedMimeType = "text/plain";
					break;
				case ".m1v":
					resolvedMimeType = "video/mpeg";
					break;
				case ".m2a":
					resolvedMimeType = "audio/mpeg";
					break;
				case ".m2v":
					resolvedMimeType = "video/mpeg";
					break;
				case ".m3u":
					resolvedMimeType = "audio/x-mpequrl";
					break;
				case ".man":
					resolvedMimeType = "application/x-troff-man";
					break;
				case ".map":
					resolvedMimeType = "application/x-navimap";
					break;
				case ".mar":
					resolvedMimeType = "text/plain";
					break;
				case ".mbd":
					resolvedMimeType = "application/mbedlet";
					break;
				case ".mc$":
					resolvedMimeType = "application/x-magic-cap-package-1.0";
					break;
				case ".mcd":
					resolvedMimeType = "application/mcad";
					break;
				case ".mcf":
					resolvedMimeType = "text/mcf";
					break;
				case ".mcp":
					resolvedMimeType = "application/netmc";
					break;
				case ".me":
					resolvedMimeType = "application/x-troff-me";
					break;
				case ".mht":
					resolvedMimeType = "message/rfc822";
					break;
				case ".mhtml":
					resolvedMimeType = "message/rfc822";
					break;
				case ".mid":
					resolvedMimeType = "audio/midi";
					break;
				case ".midi":
					resolvedMimeType = "audio/midi";
					break;
				case ".mif":
					resolvedMimeType = "application/x-mif";
					break;
				case ".mime":
					resolvedMimeType = "message/rfc822";
					break;
				case ".mjf":
					resolvedMimeType = "audio/x-vnd.audioexplosion.mjuicemediafile";
					break;
				case ".mjpg":
					resolvedMimeType = "video/x-motion-jpeg";
					break;
				case ".mm":
					resolvedMimeType = "application/base64";
					break;
				case ".mme":
					resolvedMimeType = "application/base64";
					break;
				case ".mod":
					resolvedMimeType = "audio/mod";
					break;
				case ".moov":
					resolvedMimeType = "video/quicktime";
					break;
				case ".mov":
					resolvedMimeType = "video/quicktime";
					break;
				case ".movie":
					resolvedMimeType = "video/x-sgi-movie";
					break;
				case ".mp2":
					resolvedMimeType = "audio/mpeg";
					break;
				case ".mp3":
					resolvedMimeType = "audio/mpeg";
					break;
				case ".mpa":
					resolvedMimeType = "audio/mpeg";
					break;
				case ".mpc":
					resolvedMimeType = "application/x-project";
					break;
				case ".mpe":
					resolvedMimeType = "video/mpeg";
					break;
				case ".mpeg":
					resolvedMimeType = "video/mpeg";
					break;
				case ".mpg":
					resolvedMimeType = "video/mpeg";
					break;
				case ".mpga":
					resolvedMimeType = "audio/mpeg";
					break;
				case ".mpp":
					resolvedMimeType = "application/vnd.ms-project";
					break;
				case ".mpt":
					resolvedMimeType = "application/vnd.ms-project";
					break;
				case ".mpv":
					resolvedMimeType = "application/vnd.ms-project";
					break;
				case ".mpx":
					resolvedMimeType = "application/vnd.ms-project";
					break;
				case ".mrc":
					resolvedMimeType = "application/marc";
					break;
				case ".ms":
					resolvedMimeType = "application/x-troff-ms";
					break;
				case ".mv":
					resolvedMimeType = "video/x-sgi-movie";
					break;
				case ".my":
					resolvedMimeType = "audio/make";
					break;
				case ".mzz":
					resolvedMimeType = "application/x-vnd.audioexplosion.mzz";
					break;
				case ".nap":
					resolvedMimeType = "image/naplps";
					break;
				case ".naplps":
					resolvedMimeType = "image/naplps";
					break;
				case ".nc":
					resolvedMimeType = "application/x-netcdf";
					break;
				case ".ncm":
					resolvedMimeType = "application/vnd.nokia.configuration-message";
					break;
				case ".nif":
					resolvedMimeType = "image/x-niff";
					break;
				case ".niff":
					resolvedMimeType = "image/x-niff";
					break;
				case ".nix":
					resolvedMimeType = "application/x-mix-transfer";
					break;
				case ".nsc":
					resolvedMimeType = "application/x-conference";
					break;
				case ".nvd":
					resolvedMimeType = "application/x-navidoc";
					break;
				case ".o":
					resolvedMimeType = "application/octet-stream";
					break;
				case ".oda":
					resolvedMimeType = "application/oda";
					break;
				case ".omc":
					resolvedMimeType = "application/x-omc";
					break;
				case ".omcd":
					resolvedMimeType = "application/x-omcdatamaker";
					break;
				case ".omcr":
					resolvedMimeType = "application/x-omcregerator";
					break;
				case ".p":
					resolvedMimeType = "text/x-pascal";
					break;
				case ".p10":
					resolvedMimeType = "application/pkcs10";
					break;
				case ".p12":
					resolvedMimeType = "application/pkcs-12";
					break;
				case ".p7a":
					resolvedMimeType = "application/x-pkcs7-signature";
					break;
				case ".p7c":
					resolvedMimeType = "application/pkcs7-mime";
					break;
				case ".p7m":
					resolvedMimeType = "application/pkcs7-mime";
					break;
				case ".p7r":
					resolvedMimeType = "application/x-pkcs7-certreqresp";
					break;
				case ".p7s":
					resolvedMimeType = "application/pkcs7-signature";
					break;
				case ".part":
					resolvedMimeType = "application/pro_eng";
					break;
				case ".pas":
					resolvedMimeType = "text/pascal";
					break;
				case ".pbm":
					resolvedMimeType = "image/x-portable-bitmap";
					break;
				case ".pcl":
					resolvedMimeType = "application/vnd.hp-pcl";
					break;
				case ".pct":
					resolvedMimeType = "image/x-pict";
					break;
				case ".pcx":
					resolvedMimeType = "image/x-pcx";
					break;
				case ".pdb":
					resolvedMimeType = "chemical/x-pdb";
					break;
				case ".pdf":
					resolvedMimeType = "application/pdf";
					break;
				case ".pfunk":
					resolvedMimeType = "audio/make";
					break;
				case ".pgm":
					resolvedMimeType = "image/x-portable-greymap";
					break;
				case ".pic":
					resolvedMimeType = "image/pict";
					break;
				case ".pict":
					resolvedMimeType = "image/pict";
					break;
				case ".pkg":
					resolvedMimeType = "application/x-newton-compatible-pkg";
					break;
				case ".pko":
					resolvedMimeType = "application/vnd.ms-pki.pko";
					break;
				case ".pl":
					resolvedMimeType = "text/plain";
					break;
				case ".plx":
					resolvedMimeType = "application/x-pixclscript";
					break;
				case ".pm":
					resolvedMimeType = "image/x-xpixmap";
					break;
				case ".pm4":
					resolvedMimeType = "application/x-pagemaker";
					break;
				case ".pm5":
					resolvedMimeType = "application/x-pagemaker";
					break;
				case ".png":
					resolvedMimeType = "image/png";
					break;
				case ".pnm":
					resolvedMimeType = "application/x-portable-anymap";
					break;
				case ".pot":
					resolvedMimeType = "application/vnd.ms-powerpoint";
					break;
				case ".pov":
					resolvedMimeType = "model/x-pov";
					break;
				case ".ppa":
					resolvedMimeType = "application/vnd.ms-powerpoint";
					break;
				case ".ppm":
					resolvedMimeType = "image/x-portable-pixmap";
					break;
				case ".pps":
					resolvedMimeType = "application/vnd.ms-powerpoint";
					break;
				case ".ppt":
					resolvedMimeType = "application/vnd.ms-powerpoint";
					break;
				case ".ppz":
					resolvedMimeType = "application/vnd.ms-powerpoint";
					break;
				case ".pre":
					resolvedMimeType = "application/x-freelance";
					break;
				case ".prt":
					resolvedMimeType = "application/pro_eng";
					break;
				case ".ps":
					resolvedMimeType = "application/postscript";
					break;
				case ".psd":
					resolvedMimeType = "application/octet-stream";
					break;
				case ".pvu":
					resolvedMimeType = "paleovu/x-pv";
					break;
				case ".pwz":
					resolvedMimeType = "application/vnd.ms-powerpoint";
					break;
				case ".py":
					resolvedMimeType = "text/x-script.phyton";
					break;
				case ".pyc":
					resolvedMimeType = "applicaiton/x-bytecode.python";
					break;
				case ".qcp":
					resolvedMimeType = "audio/vnd.qcelp";
					break;
				case ".qd3":
					resolvedMimeType = "x-world/x-3dmf";
					break;
				case ".qd3d":
					resolvedMimeType = "x-world/x-3dmf";
					break;
				case ".qif":
					resolvedMimeType = "image/x-quicktime";
					break;
				case ".qt":
					resolvedMimeType = "video/quicktime";
					break;
				case ".qtc":
					resolvedMimeType = "video/x-qtc";
					break;
				case ".qti":
					resolvedMimeType = "image/x-quicktime";
					break;
				case ".qtif":
					resolvedMimeType = "image/x-quicktime";
					break;
				case ".ra":
					resolvedMimeType = "audio/x-pn-realaudio";
					break;
				case ".ram":
					resolvedMimeType = "audio/x-pn-realaudio";
					break;
				case ".ras":
					resolvedMimeType = "application/x-cmu-raster";
					break;
				case ".rast":
					resolvedMimeType = "image/cmu-raster";
					break;
				case ".rexx":
					resolvedMimeType = "text/x-script.rexx";
					break;
				case ".rf":
					resolvedMimeType = "image/vnd.rn-realflash";
					break;
				case ".rgb":
					resolvedMimeType = "image/x-rgb";
					break;
				case ".rm":
					resolvedMimeType = "application/vnd.rn-realmedia";
					break;
				case ".rmi":
					resolvedMimeType = "audio/mid";
					break;
				case ".rmm":
					resolvedMimeType = "audio/x-pn-realaudio";
					break;
				case ".rmp":
					resolvedMimeType = "audio/x-pn-realaudio";
					break;
				case ".rng":
					resolvedMimeType = "application/ringing-tones";
					break;
				case ".rnx":
					resolvedMimeType = "application/vnd.rn-realplayer";
					break;
				case ".roff":
					resolvedMimeType = "application/x-troff";
					break;
				case ".rp":
					resolvedMimeType = "image/vnd.rn-realpix";
					break;
				case ".rpm":
					resolvedMimeType = "audio/x-pn-realaudio-plugin";
					break;
				case ".rt":
					resolvedMimeType = "text/richtext";
					break;
				case ".rtf":
					resolvedMimeType = "text/richtext";
					break;
				case ".rtx":
					resolvedMimeType = "text/richtext";
					break;
				case ".rv":
					resolvedMimeType = "video/vnd.rn-realvideo";
					break;
				case ".s":
					resolvedMimeType = "text/x-asm";
					break;
				case ".s3m":
					resolvedMimeType = "audio/s3m";
					break;
				case ".saveme":
					resolvedMimeType = "application/octet-stream";
					break;
				case ".sbk":
					resolvedMimeType = "application/x-tbook";
					break;
				case ".scm":
					resolvedMimeType = "application/x-lotusscreencam";
					break;
				case ".sdml":
					resolvedMimeType = "text/plain";
					break;
				case ".sdp":
					resolvedMimeType = "application/sdp";
					break;
				case ".sdr":
					resolvedMimeType = "application/sounder";
					break;
				case ".sea":
					resolvedMimeType = "application/sea";
					break;
				case ".set":
					resolvedMimeType = "application/set";
					break;
				case ".sgm":
					resolvedMimeType = "text/sgml";
					break;
				case ".sgml":
					resolvedMimeType = "text/sgml";
					break;
				case ".sh":
					resolvedMimeType = "application/x-sh";
					break;
				case ".shar":
					resolvedMimeType = "application/x-shar";
					break;
				case ".shtml":
					resolvedMimeType = "text/html";
					break;
				case ".sid":
					resolvedMimeType = "audio/x-psid";
					break;
				case ".sit":
					resolvedMimeType = "application/x-sit";
					break;
				case ".skd":
					resolvedMimeType = "application/x-koan";
					break;
				case ".skm":
					resolvedMimeType = "application/x-koan";
					break;
				case ".skp":
					resolvedMimeType = "application/x-koan";
					break;
				case ".skt":
					resolvedMimeType = "application/x-koan";
					break;
				case ".sl":
					resolvedMimeType = "application/x-seelogo";
					break;
				case ".smi":
					resolvedMimeType = "application/smil";
					break;
				case ".smil":
					resolvedMimeType = "application/smil";
					break;
				case ".snd":
					resolvedMimeType = "audio/basic";
					break;
				case ".sol":
					resolvedMimeType = "application/solids";
					break;
				case ".spc":
					resolvedMimeType = "text/x-speech";
					break;
				case ".spl":
					resolvedMimeType = "application/futuresplash";
					break;
				case ".spr":
					resolvedMimeType = "application/x-sprite";
					break;
				case ".sprite":
					resolvedMimeType = "application/x-sprite";
					break;
				case ".src":
					resolvedMimeType = "application/x-wais-source";
					break;
				case ".ssi":
					resolvedMimeType = "text/x-server-parsed-html";
					break;
				case ".ssm":
					resolvedMimeType = "application/streamingmedia";
					break;
				case ".sst":
					resolvedMimeType = "application/vnd.ms-pki.certstore";
					break;
				case ".step":
					resolvedMimeType = "application/step";
					break;
				case ".stl":
					resolvedMimeType = "application/sla";
					break;
				case ".stp":
					resolvedMimeType = "application/step";
					break;
				case ".sv4cpio":
					resolvedMimeType = "application/x-sv4cpio";
					break;
				case ".sv4crc":
					resolvedMimeType = "application/x-sv4crc";
					break;
				case ".svf":
					resolvedMimeType = "image/vnd.dwg";
					break;
				case ".svr":
					resolvedMimeType = "application/x-world";
					break;
				case ".swf":
					resolvedMimeType = "application/x-shockwave-flash";
					break;
				case ".t":
					resolvedMimeType = "application/x-troff";
					break;
				case ".talk":
					resolvedMimeType = "text/x-speech";
					break;
				case ".tar":
					resolvedMimeType = "application/x-tar";
					break;
				case ".tbk":
					resolvedMimeType = "application/toolbook";
					break;
				case ".tcl":
					resolvedMimeType = "application/x-tcl";
					break;
				case ".tcsh":
					resolvedMimeType = "text/x-script.tcsh";
					break;
				case ".tex":
					resolvedMimeType = "application/x-tex";
					break;
				case ".texi":
					resolvedMimeType = "application/x-texinfo";
					break;
				case ".texinfo":
					resolvedMimeType = "application/x-texinfo";
					break;
				case ".text":
					resolvedMimeType = "text/plain";
					break;
				case ".tgz":
					resolvedMimeType = "application/x-compressed";
					break;
				case ".tif":
					resolvedMimeType = "image/tiff";
					break;
				case ".tiff":
					resolvedMimeType = "image/tiff";
					break;
				case ".tr":
					resolvedMimeType = "application/x-troff";
					break;
				case ".tsi":
					resolvedMimeType = "audio/tsp-audio";
					break;
				case ".tsp":
					resolvedMimeType = "application/dsptype";
					break;
				case ".tsv":
					resolvedMimeType = "text/tab-separated-values";
					break;
				case ".turbot":
					resolvedMimeType = "image/florian";
					break;
				case ".txt":
					resolvedMimeType = "text/plain";
					break;
				case ".uil":
					resolvedMimeType = "text/x-uil";
					break;
				case ".uni":
					resolvedMimeType = "text/uri-list";
					break;
				case ".unis":
					resolvedMimeType = "text/uri-list";
					break;
				case ".unv":
					resolvedMimeType = "application/i-deas";
					break;
				case ".uri":
					resolvedMimeType = "text/uri-list";
					break;
				case ".uris":
					resolvedMimeType = "text/uri-list";
					break;
				case ".ustar":
					resolvedMimeType = "application/x-ustar";
					break;
				case ".uu":
					resolvedMimeType = "application/octet-stream";
					break;
				case ".uue":
					resolvedMimeType = "text/x-uuencode";
					break;
				case ".vcd":
					resolvedMimeType = "application/x-cdlink";
					break;
				case ".vcs":
					resolvedMimeType = "text/x-vcalendar";
					break;
				case ".vda":
					resolvedMimeType = "application/vda";
					break;
				case ".vdo":
					resolvedMimeType = "video/vdo";
					break;
				case ".vew":
					resolvedMimeType = "application/groupwise";
					break;
				case ".viv":
					resolvedMimeType = "video/vivo";
					break;
				case ".vivo":
					resolvedMimeType = "video/vivo";
					break;
				case ".vmd":
					resolvedMimeType = "application/vocaltec-media-desc";
					break;
				case ".vmf":
					resolvedMimeType = "application/vocaltec-media-file";
					break;
				case ".voc":
					resolvedMimeType = "audio/voc";
					break;
				case ".vos":
					resolvedMimeType = "video/vosaic";
					break;
				case ".vox":
					resolvedMimeType = "audio/voxware";
					break;
				case ".vqe":
					resolvedMimeType = "audio/x-twinvq-plugin";
					break;
				case ".vqf":
					resolvedMimeType = "audio/x-twinvq";
					break;
				case ".vql":
					resolvedMimeType = "audio/x-twinvq-plugin";
					break;
				case ".vrml":
					resolvedMimeType = "application/x-vrml";
					break;
				case ".vrt":
					resolvedMimeType = "x-world/x-vrt";
					break;
				case ".vsd":
					resolvedMimeType = "application/x-visio";
					break;
				case ".vst":
					resolvedMimeType = "application/x-visio";
					break;
				case ".vsw":
					resolvedMimeType = "application/x-visio";
					break;
				case ".w60":
					resolvedMimeType = "application/wordperfect6.0";
					break;
				case ".w61":
					resolvedMimeType = "application/wordperfect6.1";
					break;
				case ".w6w":
					resolvedMimeType = "application/msword";
					break;
				case ".wav":
					resolvedMimeType = "audio/wav";
					break;
				case ".wb1":
					resolvedMimeType = "application/x-qpro";
					break;
				case ".wbmp":
					resolvedMimeType = "image/vnd.wap.wbmp";
					break;
				case ".web":
					resolvedMimeType = "application/vnd.xara";
					break;
				case ".wiz":
					resolvedMimeType = "application/msword";
					break;
				case ".wk1":
					resolvedMimeType = "application/x-123";
					break;
				case ".wmf":
					resolvedMimeType = "windows/metafile";
					break;
				case ".wml":
					resolvedMimeType = "text/vnd.wap.wml";
					break;
				case ".wmlc":
					resolvedMimeType = "application/vnd.wap.wmlc";
					break;
				case ".wmls":
					resolvedMimeType = "text/vnd.wap.wmlscript";
					break;
				case ".wmlsc":
					resolvedMimeType = "application/vnd.wap.wmlscriptc";
					break;
				case ".word":
					resolvedMimeType = "application/msword";
					break;
				case ".wp":
					resolvedMimeType = "application/wordperfect";
					break;
				case ".wp5":
					resolvedMimeType = "application/wordperfect";
					break;
				case ".wp6":
					resolvedMimeType = "application/wordperfect";
					break;
				case ".wpd":
					resolvedMimeType = "application/wordperfect";
					break;
				case ".wq1":
					resolvedMimeType = "application/x-lotus";
					break;
				case ".wri":
					resolvedMimeType = "application/mswrite";
					break;
				case ".wrl":
					resolvedMimeType = "application/x-world";
					break;
				case ".wrz":
					resolvedMimeType = "x-world/x-vrml";
					break;
				case ".wsc":
					resolvedMimeType = "text/scriplet";
					break;
				case ".wsrc":
					resolvedMimeType = "application/x-wais-source";
					break;
				case ".wtk":
					resolvedMimeType = "application/x-wintalk";
					break;
				case ".xbm":
					resolvedMimeType = "image/x-xbitmap";
					break;
				case ".xdr":
					resolvedMimeType = "video/x-amt-demorun";
					break;
				case ".xgz":
					resolvedMimeType = "xgl/drawing";
					break;
				case ".xif":
					resolvedMimeType = "image/vnd.xiff";
					break;
				case ".xl":
					resolvedMimeType = "application/excel";
					break;
				case ".xla":
					resolvedMimeType = "application/vnd.ms-excel";
					break;
				case ".xlb":
					resolvedMimeType = "application/vnd.ms-excel";
					break;
				case ".xlc":
					resolvedMimeType = "application/vnd.ms-excel";
					break;
				case ".xld":
					resolvedMimeType = "application/vnd.ms-excel";
					break;
				case ".xlk":
					resolvedMimeType = "application/vnd.ms-excel";
					break;
				case ".xll":
					resolvedMimeType = "application/vnd.ms-excel";
					break;
				case ".xlm":
					resolvedMimeType = "application/vnd.ms-excel";
					break;
				case ".xls":
					resolvedMimeType = "application/vnd.ms-excel";
					break;
				case ".xlt":
					resolvedMimeType = "application/vnd.ms-excel";
					break;
				case ".xlv":
					resolvedMimeType = "application/vnd.ms-excel";
					break;
				case ".xlw":
					resolvedMimeType = "application/vnd.ms-excel";
					break;
				case ".xm":
					resolvedMimeType = "audio/xm";
					break;
				case ".xml":
					resolvedMimeType = "application/xml";
					break;
				case ".xmz":
					resolvedMimeType = "xgl/movie";
					break;
				case ".xpix":
					resolvedMimeType = "application/x-vnd.ls-xpix";
					break;
				case ".xpm":
					resolvedMimeType = "image/xpm";
					break;
				case ".x-png":
					resolvedMimeType = "image/png";
					break;
				case ".xsr":
					resolvedMimeType = "video/x-amt-showrun";
					break;
				case ".xwd":
					resolvedMimeType = "image/x-xwd";
					break;
				case ".xyz":
					resolvedMimeType = "chemical/x-pdb";
					break;
				case ".z":
					resolvedMimeType = "application/x-compressed";
					break;
				case ".zip":
					resolvedMimeType = "application/zip";
					break;
				case ".zoo":
					resolvedMimeType = "application/octet-stream";
					break;
				case ".zsh":
					resolvedMimeType = "text/x-script.zsh";
					break;

				default:
					resolvedMimeType = "application/octet-stream";
					break;
			}
			return resolvedMimeType;
		}

		#endregion 

    }
}
