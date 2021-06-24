using Codebiz.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Codebiz.Domain.ERP.Context
{
    public static class Helpers
    {
        public static void SeedEnumData<TData, TEnum>(IDbSet<TData> items)
           where TData : EnumBase<TEnum>
           where TEnum : struct
        {
            var etype = typeof(TEnum);

            if (!etype.IsEnum)
                throw new Exception(string.Format("Type '{0}' must be enum", etype.AssemblyQualifiedName));

            var ntype = Enum.GetUnderlyingType(etype);

            if (ntype == typeof(long) || ntype == typeof(ulong) || ntype == typeof(uint))
                throw new Exception();

            foreach (TEnum evalue in Enum.GetValues(etype))
            {
                var item = Activator.CreateInstance<TData>();

                item.Id = (int)Convert.ChangeType(evalue, typeof(int));

                if (item.Id <= 0)
                    throw new Exception("Enum underlying value must be positive");

                item.Name = Enum.GetName(etype, evalue);

                item.Description = GetEnumDescription(evalue);

                var existingItem = items.FirstOrDefault(a => a.Id == item.Id);
                if (existingItem == null)
                {
                    items.Add(item);
                }
                else
                {
                    existingItem.Name = item.Name;
                    existingItem.Description = item.Description;
                }
            }
        }

        public static string GetEnumDescription<TEnum>(TEnum item)
        {
            Type type = item.GetType();

            var attribute = type.GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false).Cast<DescriptionAttribute>().FirstOrDefault();
            return attribute == null ? string.Empty : attribute.Description;
        }

        //account creator function
        public static string GenerateRandomString(int length)
        {
            System.Threading.Thread.Sleep(20); //add a little delay to improve randomness
            char[] charpool = "ABCDEFGHJKLMNPQRSTUVWXYZ1234567890".ToCharArray();
            var rdm = new Random();
            var result = "";

            for (var cntr = 0; cntr < length; cntr++)
            {
                var rndmnmbr = rdm.Next(charpool.Length - 1);
                result += charpool[rndmnmbr];
            }

            return result;
        }

        public static List<TEntity> ReadAndMapFile<TEntity, TId>(string filePath, char separator = '\t')
        {
            var entity_list = new List<TEntity>();
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream s = assembly.GetManifestResourceStream(filePath))
            {
                using (var sr = new StreamReader(s))
                {

                    string line_data = string.Empty;
                    var line_data_fieldName = sr.ReadLine();
                    var line_data_fieldNameAlias = sr.ReadLine();
                    var field_names = line_data_fieldName.Split(separator);

                    while ((line_data = sr.ReadLine()) != null)
                    {
                        var myEntity = Activator.CreateInstance<TEntity>();
                        if (line_data != string.Empty)
                        {
                            var line_data_array = line_data.Split(separator);
                            Type _type = typeof(TEntity);

                            TId Id = default(TId);
                            if (!string.IsNullOrEmpty(line_data_array[0]))
                                Id = (TId)Convert.ChangeType(line_data_array[0], typeof(TId));


                            for (var x = 0; x < line_data_array.Length; x++)
                            {
                                try
                                {
                                    PropertyInfo propInfo = _type.GetProperty(field_names[x]);
                                    if (propInfo != null)
                                    {
                                        Type fieldType = propInfo.PropertyType;
                                        //bool
                                        if ((fieldType == typeof(bool) || fieldType == typeof(bool?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            switch (line_data_array[x])
                                            {
                                                case "1":
                                                    propInfo.SetValue(myEntity, true);
                                                    break;
                                                case "0":
                                                    propInfo.SetValue(myEntity, false);
                                                    break;
                                                case "True":
                                                    propInfo.SetValue(myEntity, bool.Parse(line_data_array[x]));
                                                    break;
                                                case "False":
                                                    propInfo.SetValue(myEntity, bool.Parse(line_data_array[x]));
                                                    break;
                                                case "Y":
                                                    propInfo.SetValue(myEntity, true);
                                                    break;
                                                case "N":
                                                    propInfo.SetValue(myEntity, false);
                                                    break;
                                            }
                                        }

                                        if ((fieldType == typeof(byte) || fieldType == typeof(byte?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            byte b = byte.TryParse(line_data_array[x], out b) ? b : (byte)0;
                                            propInfo.SetValue(myEntity, b);
                                        }

                                        if ((fieldType == typeof(sbyte) || fieldType == typeof(sbyte?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            sbyte b = sbyte.TryParse(line_data_array[x], out b) ? b : (sbyte)0;
                                            propInfo.SetValue(myEntity, b);
                                        }

                                        if ((fieldType == typeof(int) || fieldType == typeof(int?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            int i = int.TryParse(line_data_array[x], out i) ? i : 0;
                                            propInfo.SetValue(myEntity, i);
                                        }

                                        if ((fieldType == typeof(long) || fieldType == typeof(long?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            long i = long.TryParse(line_data_array[x], out i) ? i : 0;
                                            propInfo.SetValue(myEntity, i);
                                        }

                                        if ((fieldType == typeof(float) || fieldType == typeof(float?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            var str_data = line_data_array[x].Replace("\"", "").Replace(",", "").Replace("\'", "").Replace(" ", "");
                                            double tmp_val = double.TryParse(str_data, out tmp_val) ? tmp_val : 0;
                                            propInfo.SetValue(myEntity, tmp_val);
                                        }

                                        if ((fieldType == typeof(double) || fieldType == typeof(double?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            var str_data = line_data_array[x].Replace("\"", "").Replace(",", "").Replace("\'", "").Replace(" ", "");
                                            double tmp_val = double.TryParse(str_data, out tmp_val) ? tmp_val : 0;
                                            propInfo.SetValue(myEntity, tmp_val);
                                        }

                                        if ((fieldType == typeof(decimal) || fieldType == typeof(decimal?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            var str_data = line_data_array[x].Replace("\"", "").Replace(",", "").Replace("\'", "").Replace(" ", "");

                                            decimal tmp_val = decimal.TryParse(str_data, out tmp_val) ? tmp_val : 0;

                                            propInfo.SetValue(myEntity, tmp_val);
                                        }

                                        if (fieldType == typeof(string))
                                            propInfo.SetValue(myEntity, line_data_array[x]);

                                        if (fieldType == typeof(char))
                                            propInfo.SetValue(myEntity, line_data_array[x]);

                                        if ((fieldType == typeof(DateTime) || fieldType == typeof(DateTime?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            DateTime tmp_val = DateTime.TryParse(line_data_array[x], out tmp_val) ? tmp_val : DateTime.Parse("1900-01-01");
                                            propInfo.SetValue(myEntity, tmp_val);
                                        }

                                        if ((fieldType == typeof(TimeSpan) || fieldType == typeof(TimeSpan?)) && !string.IsNullOrEmpty(line_data_array[x]))
                                        {
                                            TimeSpan tmp_val = TimeSpan.TryParse(line_data_array[x], out tmp_val) ? tmp_val : TimeSpan.Parse("1900-01-01");
                                            propInfo.SetValue(myEntity, tmp_val);
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    //Errors.Add($"{e.Data}");
                                }
                            }
                        }
                        entity_list.Add(myEntity);
                    } 
                }
            }

            return entity_list;
        }

        public static string ToSentenceCase(this string str)
        {
            return System.Text.RegularExpressions.Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
        }
    }
}
