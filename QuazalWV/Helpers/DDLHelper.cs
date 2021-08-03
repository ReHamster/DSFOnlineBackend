﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV.Helpers
{
	class DDLHelper
	{
        // Function to get property values
        public static object[] ReadPropertyValues(Type[] typeList, Stream str)
        {
            var paramsInstances = new List<object>();

            foreach (var type in typeList)
            {
                if (type == null)
				{
                    paramsInstances.Add(null);
                    continue;
                }

                paramsInstances.Add(ReadObject(type, str));
            }

            return paramsInstances.ToArray();
        }

        // Complex objects

        // reads type and returns new object instance
        public static T ReadObject<T>(Stream s) where T : class
		{
            return (T)ReadObject(typeof(T), s);
		}

        public static object ReadObject(Type currentType, Stream str)
		{
            object instance;

            // handle types
            if (currentType == typeof(string))
            {
                instance = Helper.ReadString(str);
            }
            else if (currentType == typeof(bool))
            {
                instance = Helper.ReadBool(str);
            }
            else if (currentType == typeof(float))
            {
                instance = Helper.ReadFloat(str);
            }
            else if (currentType == typeof(double))
            {
                instance = Helper.ReadDouble(str);
            }
            else if (currentType == typeof(byte))
            {
                instance = Helper.ReadU8(str);
            }
            else if (currentType == typeof(uint) ||
                    currentType == typeof(int))
            {
                instance = Convert.ChangeType(Helper.ReadU32(str), currentType);
            }
            else if (currentType == typeof(ushort) ||
                     currentType == typeof(short))
            {
                instance = Convert.ChangeType(Helper.ReadU16(str), currentType);
            }
            else if (typeof(IEnumerable).IsAssignableFrom(currentType))
            {
                var arrayItemType = currentType.GetGenericArguments().SingleOrDefault();

                // get array size
                uint size = Helper.ReadU32(str);

                var arrayValues = new List<object>();

                // new array of objects 
                for (int i = 0; i < size; i++)
                {
                    var itemInstance = ReadObject(arrayItemType, str);
                    arrayValues.Add(itemInstance);
                }
                instance = arrayValues;
            }
            else if (typeof(Stream).IsAssignableFrom(currentType))
			{
                instance = str;
            }
            else if(typeof(RMCPRequest).IsAssignableFrom(currentType))
			{
                instance = Activator.CreateInstance(currentType, new object[] { str });
            }
            else // read complex object
			{
                // collect all properties even from base types
                var allProperties = new List<PropertyInfo>();

                var nType = currentType;
                do
                {
                    // FIXME: prepend or append?
                    allProperties.AddRange(nType.GetProperties());
                    nType = nType.BaseType;
                } while (nType != null);

                // make creation lambda and use default constructor
                var newObjectFunc = Expression.Lambda<Func<object>>(
                    Expression.New(currentType.GetConstructor(Type.EmptyTypes))
                ).Compile();

                // get types and skip read-only
                var allPropertyTypes = allProperties.Select(x => x.CanWrite ? x.PropertyType : null);

                // this will recurse
                var allPropertyValues = ReadPropertyValues(allPropertyTypes.ToArray(), str);

                // create instance
                instance = newObjectFunc();

                // assign all values to new instance
                for (int i = 0; i < allProperties.Count; i++)
                {
                    allProperties[i].SetValue(instance, allPropertyValues[i]);
                }
            }

            return instance;
		}

        //------------------------------------------------------------------------

        // writes object to buffer
		public static void WriteObject<T>(T obj, Stream s) where T : class
		{
            WriteObject(typeof(T), obj, s);
        }

        public static void WriteObject(Type currentType, object obj, Stream str)
        {
            // handle types
            if (currentType == typeof(string))
            {
                Helper.WriteString(str, (string)Convert.ChangeType(obj, currentType));
            }
            else if (currentType == typeof(bool))
            {
                Helper.WriteBool(str, (bool)Convert.ChangeType(obj, currentType));
            }
            else if (currentType == typeof(float))
            {
                Helper.WriteFloat(str, (float)Convert.ChangeType(obj, currentType));
            }
            else if (currentType == typeof(double))
            {
                Helper.WriteDouble(str, (double)Convert.ChangeType(obj, currentType));
            }
            else if (currentType == typeof(byte) ||
                     currentType == typeof(sbyte))
            {
                Helper.WriteU8(str, (byte)Convert.ChangeType(obj, currentType));
            }
            else if (currentType == typeof(uint) ||
                    currentType == typeof(int))
            {
                Helper.WriteU32(str, (uint)Convert.ChangeType(obj, currentType));
            }
            else if (currentType == typeof(ushort) ||
                     currentType == typeof(short))
            {
                Helper.WriteU16(str, (ushort)Convert.ChangeType(obj, currentType));
            }
            else if (typeof(IEnumerable).IsAssignableFrom(currentType))
            {
                IEnumerable<object> arrayValues = (IEnumerable<object>)obj;

                var arrayItemType = currentType.GetGenericArguments().SingleOrDefault();

                var size = arrayValues.Count();

                // store array size
                Helper.WriteU32(str, (uint)size);

                // write items
                for (int i = 0; i < size; i++)
                {
                    WriteObject(arrayItemType, arrayValues.ElementAt(i), str);
                }
            }
			else
			{
                // assume it's a nested complex type
                // collect all properties even from base types
                var allProperties = new List<PropertyInfo>();

                var nType = currentType;
                do
                {
                    // FIXME: prepend or append?
                    allProperties.AddRange(nType.GetProperties());
                    nType = nType.BaseType;
                } while (nType != null);

                // get types and skip read-only
                var allPropertyTypes = allProperties.Select(x => x.CanRead ? x.PropertyType : null);

                // assign all values to new instance
                for (int i = 0; i < allProperties.Count; i++)
                {
                    var value = allProperties[i].GetValue(obj);
                    WriteObject(allProperties[i].PropertyType, value, str);
                }
            }
        }
    }
}
