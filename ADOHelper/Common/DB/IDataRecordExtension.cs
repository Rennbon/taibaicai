using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace ADOHelper.Common.DB
{
    public static class IDataRecordExtension
    {
        public static bool GetBoolean(this IDataRecord record, string name)
        {
            return GetBoolean(record, name, default(bool));
        }

        public static bool GetBoolean(this IDataRecord record, string name, bool defaultValue)
        {
            return GetValue(record, name, record.GetBoolean, defaultValue);
        }

        public static byte GetByte(this IDataRecord record, string name)
        {
            return GetByte(record, name, default(byte));
        }

        public static byte GetByte(this IDataRecord record, string name, byte defaultValue)
        {
            return GetValue(record, name, record.GetByte, defaultValue);
        }

        public static char GetChar(this IDataRecord record, string name)
        {
            return GetChar(record, name, default(char));
        }

        public static char GetChar(this IDataRecord record, string name, char defaultValue)
        {
            return GetValue(record, name, record.GetChar, defaultValue);
        }

        public static Int16 GetInt16(this IDataRecord record, string name)
        {
            return GetInt16(record, name, default(Int16));
        }
        public static Int16 GetInt16(this IDataRecord record, string name, Int16 defaultValue)
        {
            return GetValue(record, name, record.GetInt16, defaultValue);
        }

        public static Int32 GetInt32(this IDataRecord record, string name)
        {
            return GetInt32(record, name, default(Int32));
        }
        public static Int32 GetInt32(this IDataRecord record, string name, Int32 defaultValue)
        {
            return GetValue(record, name, record.GetInt32, defaultValue);
        }

        public static Int64 GetInt64(this IDataRecord record, string name)
        {
            return GetInt64(record, name, default(Int64));
        }
        public static Int64 GetInt64(this IDataRecord record, string name, Int64 defaultValue)
        {
            return GetValue(record, name, record.GetInt64, defaultValue);
        }
        public static String GetString(this IDataRecord record, string name)
        {
            return GetString(record, name, default(String));
        }
        public static String GetString(this IDataRecord record, string name, string defaultValue)
        {
            return GetValue(record, name, record.GetString, defaultValue);
        }

        public static Decimal GetDecimal(this IDataRecord record, string name)
        {
            return GetDecimal(record, name, default(Decimal));
        }
        public static Decimal GetDecimal(this IDataRecord record, string name, Decimal defaultValue)
        {
            return GetValue(record, name, record.GetDecimal, defaultValue);
        }
        public static DateTime GetDateTime(this IDataRecord record, string name)
        {
            return GetDateTime(record, name, default(DateTime));
        }
        public static DateTime GetDateTime(this IDataRecord record, string name, DateTime defaultValue)
        {
            return GetValue(record, name, record.GetDateTime, defaultValue);
        }
        public static Guid GetGuid(this IDataRecord record, string name)
        {
            return GetGuid(record, name, default(Guid));
        }
        public static Guid GetGuid(this IDataRecord record, string name,Guid defaultValue)
        {
            return GetValue(record, name, record.GetGuid, defaultValue);
        }
        private static T GetValue<T>(IDataRecord record, string name, Func<int, T> get, T defaultValue)
        {
            int i = record.GetOrdinal(name);
            if (record.IsDBNull(i))
            {
                return defaultValue;
            }
            else
            {
                return get(i);
            }
        }
    }
}
