using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwfDec.AVM2.Types
{
    public static class PropertyHelper
    {
        public static object GetProperty(String path, object obj)
        {
            string[] names = path.Split('.');

            Object result = obj;
            for (int i = 0; i < names.Length; i++)
            {
                String name = names[i];
                result = GetSingleProperty(name, result);
            }
            return result;
        }

        public static void SetProeprty(String path, object obj, object value)
        {
            object current = obj;
            string[] names = path.Split('.');
            for (int i = 0; i < names.Length - 1; i++)
            {
                String name = names[i];
                current = GetSingleProperty(name, current);
            }

            SetSingleProperty(names[names.Length - 1], current, value);
        }

        private static object GetSingleProperty(string name, object obj)
        {
            return obj.GetType().GetProperty(name).GetValue(obj);
        }

        private static void SetSingleProperty(string name, object obj, object value)
        {
            obj.GetType().GetProperty(name).SetValue(obj, value);
        }
    }
}
