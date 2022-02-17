using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Permissions;
using System.Web.Services.Description;
using System.Xml.Serialization;

namespace SignTokenCore
{
	internal class WsProxy
	{
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		internal static object CallWebService(string webServiceAsmxUrl, string userName, string password, string serviceName, string methodName, object[] args)
		{
			WebClient webClient = new WebClient();
			bool flag = userName.Length > 0;
			if (flag)
			{
				webClient.Credentials = new NetworkCredential(userName, password);
			}
			Stream stream = webClient.OpenRead(webServiceAsmxUrl + "?wsdl");
			ServiceDescription serviceDescription = ServiceDescription.Read(stream);
			ServiceDescriptionImporter serviceDescriptionImporter = new ServiceDescriptionImporter();
			serviceDescriptionImporter.ProtocolName = "Soap12";
			serviceDescriptionImporter.AddServiceDescription(serviceDescription, null, null);
			serviceDescriptionImporter.Style = ServiceDescriptionImportStyle.Client;
			serviceDescriptionImporter.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties;
			CodeNamespace codeNamespace = new CodeNamespace();
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			codeCompileUnit.Namespaces.Add(codeNamespace);
			ServiceDescriptionImportWarnings serviceDescriptionImportWarnings = serviceDescriptionImporter.Import(codeNamespace, codeCompileUnit);
			bool flag2 = serviceDescriptionImportWarnings == (ServiceDescriptionImportWarnings)0;
			object result;
			if (flag2)
			{
				CodeDomProvider codeDomProvider = CodeDomProvider.CreateProvider("CSharp");
				string[] assemblyNames = new string[]
				{
					"System.dll",
					"System.Web.Services.dll",
					"System.Web.dll",
					"System.Xml.dll",
					"System.Data.dll"
				};
				CompilerParameters options = new CompilerParameters(assemblyNames);
				CompilerResults compilerResults = codeDomProvider.CompileAssemblyFromDom(options, new CodeCompileUnit[]
				{
					codeCompileUnit
				});
				bool flag3 = compilerResults.Errors.Count > 0;
				if (flag3)
				{
					throw new Exception("Compile Error Occured calling webservice. Check log for more detail.");
				}
				object obj = compilerResults.CompiledAssembly.CreateInstance(serviceName);
				MethodInfo method = obj.GetType().GetMethod(methodName);
				result = method.Invoke(obj, args);
			}
			else
			{
				result = "không gọi được hàm bên trong: " + serviceDescriptionImportWarnings;
			}
			return result;
		}
	}
}
