﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace ExtendedXmlSerialization
{
    internal static class PrimitiveValueTools
    {
        public static string SetPrimitiveValue(object value, Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    return value.ToString();
                case TypeCode.Char:
                    return XmlConvert.ToString((UInt16)(char)value);
                case TypeCode.SByte:
                    return XmlConvert.ToString((sbyte)value);
                case TypeCode.Byte:
                    return XmlConvert.ToString((byte)value);
                case TypeCode.Int16:
                    return XmlConvert.ToString((short)value);
                case TypeCode.UInt16:
                    return XmlConvert.ToString((ushort)value);
                case TypeCode.Int32:
                    return XmlConvert.ToString((int)value);
                case TypeCode.UInt32:
                    return XmlConvert.ToString((uint)value);
                case TypeCode.Int64:
                    return XmlConvert.ToString((long)value);
                case TypeCode.UInt64:
                    return XmlConvert.ToString((ulong)value);
                case TypeCode.Single:
                    return XmlConvert.ToString((float)value);
                case TypeCode.Double:
                    return XmlConvert.ToString((double)value);
                case TypeCode.Decimal:
                    return XmlConvert.ToString((decimal)value);
                case TypeCode.DateTime:
                    return XmlConvert.ToString((DateTime)value, XmlDateTimeSerializationMode.RoundtripKind);
                case TypeCode.String:
                    return (string)value;
                default:
                    if (type == typeof(Guid))
                    {
                        return XmlConvert.ToString((Guid)value);
                    }
                    if (type == typeof(TimeSpan))
                    {
                        return XmlConvert.ToString((TimeSpan)value);
                    }
                    return value.ToString();
            }
        }
        public static object GetPrimitiveValue(string value, Type type, string nodeName)
        {
            try
            {
                if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    type = type.GetGenericArguments()[0];
                }
                if (type.GetTypeInfo().IsEnum)
                {
                    return Enum.Parse(type, value);
                }
                switch (Type.GetTypeCode(type))
                {
                    case TypeCode.Boolean:
                        return Convert.ToBoolean(value);
                    case TypeCode.Char:
                        return (char)XmlConvert.ToUInt16(value);
                    case TypeCode.SByte:
                        return XmlConvert.ToSByte(value);
                    case TypeCode.Byte:
                        return XmlConvert.ToByte(value);
                    case TypeCode.Int16:
                        return XmlConvert.ToInt16(value);
                    case TypeCode.UInt16:
                        return XmlConvert.ToUInt16(value);
                    case TypeCode.Int32:
                        return XmlConvert.ToInt32(value);
                    case TypeCode.UInt32:
                        return XmlConvert.ToUInt32(value);
                    case TypeCode.Int64:
                        return XmlConvert.ToInt64(value);
                    case TypeCode.UInt64:
                        return XmlConvert.ToUInt64(value);
                    case TypeCode.Single:
                        return XmlConvert.ToSingle(DecimalSeparator(value));
                    case TypeCode.Double:
                        return XmlConvert.ToDouble(DecimalSeparator(value));
                    case TypeCode.Decimal:
                        return XmlConvert.ToDecimal(DecimalSeparator(value));
                    case TypeCode.DateTime:
                        return XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
                    case TypeCode.String:
                        return value;
                    default:
                        if (type == typeof(Guid))
                        {
                            return XmlConvert.ToGuid(value);
                        }
                        if (type == typeof(TimeSpan))
                        {
                            return XmlConvert.ToTimeSpan(value);
                        }
                        throw new NotSupportedException("Unknown primitive type " + type.Name + " - value: " + value);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unsuccessful conversion node {nodeName} for type {type.Name} - value {value}", ex);
            }
        }
        private static string DecimalSeparator(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            return value.Replace(",", ".");
        }
    }
}
