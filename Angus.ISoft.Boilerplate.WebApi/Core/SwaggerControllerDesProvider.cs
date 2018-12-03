using Swashbuckle.Swagger;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace Angus.ISoft.Boilerplate.WebApi.Core
{
    public class SwaggerControllerDesProvider : ISwaggerProvider
    {
        private readonly ISwaggerProvider swaggerProvider;
        private ConcurrentDictionary<string, SwaggerDocument> cache = new ConcurrentDictionary<string, SwaggerDocument>();
        private readonly string xml;

        public SwaggerControllerDesProvider(ISwaggerProvider _swaggerProvider, string _xml)
        {
            swaggerProvider = _swaggerProvider;
            xml = _xml;
        }

        public SwaggerDocument GetSwagger(string rootUrl, string apiVersion)
        {
            var cacheKey = string.Format("{0}_{1}", rootUrl, apiVersion);
            SwaggerDocument srcDoc = null;
            if (!cache.TryGetValue(cacheKey, out srcDoc))
            {
                srcDoc = swaggerProvider.GetSwagger(rootUrl, apiVersion);
                srcDoc.vendorExtensions = new Dictionary<string, object> { { "ControllerDesc", GetControllerDesc() } };
                cache.TryAdd(cacheKey, srcDoc);
            }
            return srcDoc;
        }

        public ConcurrentDictionary<string, string> GetControllerDesc()
        {
            string xmlpath = xml;
            ConcurrentDictionary<string, string> controllerDescDict = new ConcurrentDictionary<string, string>();
            if (File.Exists(xmlpath))
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(xmlpath);
                string type = string.Empty, path = string.Empty, controllerName = string.Empty;

                string[] arrPath;
                int length = -1, cCount = "Controller".Length;
                XmlNode summaryNode = null;
                foreach (XmlNode node in xmldoc.SelectNodes("//member"))
                {
                    type = node.Attributes["name"].Value;
                    if (type.StartsWith("T:"))
                    {
                        arrPath = type.Split('.');
                        length = arrPath.Length;
                        controllerName = arrPath[length - 1];
                        if (controllerName.EndsWith("Controller"))
                        {
                            summaryNode = node.SelectSingleNode("summary");
                            string key = controllerName.Remove(controllerName.Length - cCount, cCount);
                            if (summaryNode != null && !string.IsNullOrEmpty(summaryNode.InnerText) && !controllerDescDict.ContainsKey(key))
                            {
                                controllerDescDict.TryAdd(key, summaryNode.InnerText.Trim());
                            }
                        }
                    }
                }
            }
            return controllerDescDict;
        }
    }
}