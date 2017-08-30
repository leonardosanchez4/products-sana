using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Configuration;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Text;
using System.Web.UI.WebControls;

namespace ProductsDLL
{
    public abstract class ConnectionRepository
    {

        private readonly ObjectCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policyCache;
        private readonly string _connectionString;
        private readonly object ConfigurationManager;

        private string XMLPath;
        protected SqlConnection Connection { get; private set; }

        /// <summary>
        /// Agrego un objeto al cache
        /// </summary>
        /// <param name="_object"></param>
        protected internal void CacheAdd(string key, object _object)
        {
            if (_object != null)
            {
                _cache.Add(key, _object, _policyCache);

            }
        }

        protected internal void CacheClear(string key)
        {
            _cache.Remove(key);
        }

        protected internal List<T> CacheContent<T>(string key)
        {
            var list = (List<T>)_cache[key];

            return list;
        }

        protected internal T CacheContentOne<T>(string key)
        {
            var _object = (T)_cache[key];

            if (_object != null)
            {
                return _object;
            }
            else
            {
                return default(T);
            }

        }

        protected ConnectionRepository(string appSettingKey)
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[appSettingKey].ConnectionString;
            Connection = new SqlConnection(_connectionString);
            _policyCache = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(24) };
            XMLPath = System.Web.HttpContext.Current.Server.MapPath("/Xml/");
        }



        public bool NewLineOnAttributes { get; set; }
        /// <summary>
        /// Serializes an object to an XML string, using the specified namespaces.
        /// </summary>
        public string ToXml(object obj, XmlSerializerNamespaces ns)
        {
            Type T = obj.GetType();

            var xs = new XmlSerializer(T);
            var ws = new XmlWriterSettings { Indent = true, NewLineOnAttributes = NewLineOnAttributes, OmitXmlDeclaration = true };

            var sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb, ws))
            {
                xs.Serialize(writer, obj, ns);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Serializes an object to an XML string.
        /// </summary>
        public string ToXml(object obj)
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            return ToXml(obj, ns);
        }

        /// <summary>
        /// Deserializes an object from an XML string.
        /// </summary>
        public T FromXml<T>(string xml)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StringReader sr = new StringReader(xml))
            {
                return (T)xs.Deserialize(sr);
            }
        }

        /// <summary>
        /// Deserializes an object from an XML string, using the specified type name.
        /// </summary>
        public object FromXml(string xml, string typeName)
        {
            Type T = Type.GetType(typeName);
            XmlSerializer xs = new XmlSerializer(T);
            using (StringReader sr = new StringReader(xml))
            {
                return xs.Deserialize(sr);
            }
        }

        /// <summary>
        /// Serializes an object to an XML file.
        /// </summary>
        public void ToXmlFile(Object obj, string ObjectName)
        {
            var xs = new XmlSerializer(obj.GetType());
            var ns = new XmlSerializerNamespaces();
            var ws = new XmlWriterSettings { Indent = true, NewLineOnAttributes = NewLineOnAttributes, OmitXmlDeclaration = true };
            ns.Add("", "");

  

            using (XmlWriter writer = XmlWriter.Create(XMLPath + ObjectName + ".xml", ws))
            {
                xs.Serialize(writer, obj);
            }
        }

        /// <summary>
        /// Deserializes an object from an XML file.
        /// </summary>
        public T FromXmlFile<T>(string ObjectName)
        {
        
            if (!File.Exists(XMLPath + ObjectName + ".xml"))
            {
                var a = File.Create(XMLPath + ObjectName + ".xml");
                a.Close();
                return default(T);
            }
            else
            {
                StreamReader sr = new StreamReader(XMLPath + ObjectName + ".xml");
                try
                {
                    var result = FromXml<T>(sr.ReadToEnd());
                    return result;
                }
                catch(Exception ex)
                {
                    sr.Close();
                    return default(T);
                }
                finally
                {
                    sr.Close();
                }                                
            }
        }

    }
}
