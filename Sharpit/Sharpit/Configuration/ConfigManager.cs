using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sharpit.Configuration
{
    class ConfigManager
    {

        public static void Save<T>(T cfg) where T : Configuration
        {
            var annotations = Attribute.GetCustomAttributes(cfg.GetType());
            FilePath filePath = (FilePath) annotations.ToList().FirstOrDefault(x => x.GetType() == typeof (FilePath));

            if (filePath == null)
            {
                throw new ConfigurationException("Could not find path to the configuration file of " + typeof(T).Name + "! Did you forget the FilePath attribute?");
            }

            var properties = typeof (T).GetProperties();

            if (!File.Exists(filePath.Path))
            {
                File.Create(filePath.Path).Dispose();
            }

            XElement rootElement = new XElement(typeof(T).Name);

            foreach (var propertyInfo in properties)
            {
                Type type = propertyInfo.DeclaringType;
                if (!(type.GetInterfaces().Contains(typeof (IEnumerable))))
                {
                    rootElement.Add(new XElement(propertyInfo.Name,propertyInfo.GetValue(cfg)));
                }
            }

            rootElement.Save(filePath.Path);

        }

        public static T Load<T>() where T : Configuration, new()
        {
            var annotations = Attribute.GetCustomAttributes(typeof(T));
            T obj = new T();

            FilePath filePath = (FilePath)annotations.ToList().FirstOrDefault(x => x.GetType() == typeof(FilePath));

            if (filePath == null)
            {
                throw new ConfigurationException("Could not find path to the configuration file of " + typeof(T).Name + "! Did you forget the FilePath attribute?");
            }

            var properties = typeof(T).GetProperties();

            if (!File.Exists(filePath.Path))
            {
                Save(obj);
            }

            XElement rootElement = XElement.Load(filePath.Path);

            foreach (var propertyInfo in properties)
            {
                Type type = propertyInfo.PropertyType;
                if (!(type.GetInterfaces().Contains(typeof(IEnumerable))))
                {
                    dynamic newValue = Convert.ChangeType(propertyInfo.GetValue(obj), type);
                    propertyInfo.SetValue(obj,newValue);
                }
            }

            return obj;
        }
    }

    


}
