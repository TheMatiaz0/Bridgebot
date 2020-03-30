using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;

namespace TrualityEngine.Core
{
    public static class IniManager
    {

        private static Dictionary<Type,IIniConverter> convertes;
        [LoadOnInitialize]
        private static void Init()
        {
            convertes =
               (from type in TheReflection.GetAllType()
                where type.GetInterfaces().Where(interf => interf == typeof(IIniConverter)).FirstOrDefault()!=null
             
                let attribute = type.GetCustomAttribute<CustomIniConverterAttribute>()
                select (main: attribute.Current,converter: (IIniConverter)Activator.CreateInstance(type)))
                .ToDictionary(i => i.main, i => i.converter);
        }
        public static bool CanBeCollection(string iniCode)
        {
            return iniCode.StartsWith("{") && iniCode.EndsWith("}");
        }
        public static IEnumerable<string> SplitedFromCollection(string iniCode)
        {
            return iniCode.Replace("{", "").Replace("}", "").Split(',');
        }
       


        public static object LoadSingleObject(string iniCode, Type type)
        {

            if(convertes.TryGetValue(type,out IIniConverter converter))
            {
                return converter.Refuse(iniCode);
            }
            else if(type.IsEnum)
            {

                if (int.TryParse(iniCode, NumberStyles.Integer,CultureInfo.InvariantCulture,out int val))
                     return Enum.GetValues(type).OfType<Enum>().First(item => Convert.ChangeType(item, typeof(int)).Equals(val));
                else return Enum.GetNames(type).GetIndex(item => item == iniCode);


            }
            else if (iniCode?.StartsWith("{") ?? false)
            {


                string[] elements = SplitedFromCollection(iniCode).ToArray();
                var objElements = Array.CreateInstance(type.GetElementType(), elements.Length);
                int index = 0;
                foreach (string element in elements) objElements.SetValue(LoadSingleObject(element, type.GetElementType()), index++);
                return objElements;
            }
            else
                return Convert.ChangeType(iniCode, type);
        }
    }
}
