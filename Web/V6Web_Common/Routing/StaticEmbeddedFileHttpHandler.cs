using System.IO;
using System.Net;
using System.Reflection;
using System.Web;

using V6Soft.Common.Utils.Attributes;


namespace V6Soft.Web.Common.Routing
{
    /// <summary>
    ///     Replaces IIS default static file handler to read file in an assembly.
    /// </summary>
    public class StaticEmbeddedFileHttpHandler : IHttpHandler
    {        
        private readonly string m_FilePath;

        private readonly Assembly m_Assembly;
        
        public bool IsReusable
        {
            get { return true; }
        }

        /// <summary>
        ///     Initializes new instance of StaticEmbeddedFileHttpHandler
        /// </summary>
        /// <param name="filePath">Raw file path (eg: Scripts/app.js)</param>
        /// <param name="assembly">The assembly to read resource from.</param>
        public StaticEmbeddedFileHttpHandler(string filePath, Assembly assembly)
        {
            m_FilePath = filePath;
            m_Assembly = assembly;
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpResponse response = context.Response;
            response.Clear();

            string fileNamespace = ConvertToResourceNamespace(m_FilePath),
                fileName = ExtractFileName(m_FilePath);

            Stream resourceStream = m_Assembly.GetManifestResourceStream(fileNamespace);
            using (var reader = new StreamReader(resourceStream, true))
            {
                if (reader == null)
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                else
                {
                    response.Cache.SetNoStore();
                    // `inline` to tell browser to read the file
                    // `attachment` to open download dialog in browser.
                    response.AppendHeader("Content-Disposition", "inline; filename=" + fileName);
                    response.ContentType = FigureFileExtension(fileNamespace);
                    response.Write(reader.ReadToEnd());
                }
            }

            context.Response.End();
        }

        /// <summary>
        ///     Converts from raw path (eg: Scripts/app.js) to assembly's full 
        ///     qualified name (eg: V6Soft.Web.Accounting.Modules.Customer.Scripts.app.js)
        /// </summary>
        private string ConvertToResourceNamespace(string rawPath)
        {
            string defaultNamespace;
            
            // First, tries to look for default namespace in V6 custom attribute.
            // Default namespace is not stored automatically by C#, we must store
            // it manually by adding some codes to AssemblyInfo.cs file.
            object[] attributes = m_Assembly.GetCustomAttributes(
                typeof(DefaultNamespaceAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                var namespaceAttr = (DefaultNamespaceAttribute)attributes[0];
                defaultNamespace = namespaceAttr.DefaultNamespace;
            }
            else // Then, tries to look in system attribute.
            {
                attributes = m_Assembly.GetCustomAttributes(
                    typeof(AssemblyProductAttribute), false);
                if (attributes != null && attributes.Length > 0)
                {
                    var productAttr = (AssemblyProductAttribute)attributes[0];
                    defaultNamespace = productAttr.Product;
                }
                else
                {
                    return null;
                }
            }
            return string.Format("{0}.{1}", defaultNamespace,
                rawPath.Replace('/', '.'));
        }

        private string ExtractFileName(string filePath)
        {
            return filePath.Substring(filePath.LastIndexOf('/') + 1);
        }

        private string FigureFileExtension(string filePath)
        {
            string extension = filePath.Substring(filePath.LastIndexOf('.') + 1);
            switch (extension.ToLower())
            {
                // Scripts
                case "js":
                    return "application/javascript";
                case "css":
                    return "text/css";

                // Images
                case "jpg":
                case "jpeg":
                    return "image/jpeg";
                case "png":
                    return "image/png";
                case "gif":
                    return "image/gif";
                case "svg":
                    return "image/svg+xml";

                // Text
                case "xml":
                    return "application/xml";
                case "pdf":
                    return "application/pdf";
                case "txt":
                    return "text/plain";

                // Compressed file
                case "zip":
                    return "application/zip";
                case "rar":
                    return "application/x-rar-compressed";

                // Fonts
                case "woff":
                    return "application/x-font-woff";
                case "ttf":
                    return "application/x-font-ttf";

                default:
                    return "application/octet-stream";
            }
        }
    }
}
