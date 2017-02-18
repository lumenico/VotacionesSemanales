
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Reflection;
using Newtonsoft;
using Newtonsoft.Json;


namespace Votaciones.Core
{

    public class TypeNameSerializationBinder : SerializationBinder
    {
        public string TypeFormat
        {
            get { return m_TypeFormat; }
            private set { m_TypeFormat = value; }
        }

        private string m_TypeFormat;
        public TypeNameSerializationBinder(string typeFormat__1)
        {
            TypeFormat = typeFormat__1;
        }

        public  void BindToName(Type serializedType, ref string assemblyName, ref string typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;
        }

        public override Type BindToType(string assemblyName, string typeName)
        {
            string resolvedTypeName = string.Format(TypeFormat, typeName);
            return Type.GetType(resolvedTypeName, true);
        }
    }


    public class JsonSettings
    {
        public static JsonSerializerSettings settings
        {
            get
            {
                JsonSerializerSettings settingsAux = new JsonSerializerSettings();
                settingsAux.TypeNameHandling = TypeNameHandling.Objects;
                settingsAux.Binder = new TypeNameSerializationBinder("Votaciones.Commands.{0}");
                settingsAux.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                return settingsAux;
            }
        }
    }


}