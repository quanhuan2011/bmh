using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;
using Common.pub;

namespace BLL.pub
{
    /// <summary>
    /// 转换类，格式转换，数据转换
    /// </summary>
    public class ConvertData
    {
        /// <summary>
        /// 将类型转换成json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public string ConvertDataToJson<T>(T t)
        {
            if (t == null)
                return "";
            string jsonData = JsonConvert.SerializeObject(t);
            return jsonData;
        }

        /// <summary>
        /// 将类型转换成json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T ConvertJsonToModel<T>(string jsonData)
        {
            if (jsonData == null)
                return default(T);
            object o = JsonConvert.DeserializeObject<T>(jsonData);
            return (T)o;
        }

        //public string ToJson(DataTable dt)
        //{
        //    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        //    javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
        //    ArrayList arrayList = new ArrayList();
        //    foreach (DataRow dataRow in dt.Rows)
        //    {
        //        Dictionary<string, object> dictionary = new Dictionary<string, object>();  //实例化一个参数集合
        //        foreach (DataColumn dataColumn in dt.Columns)
        //        {
        //            dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].ToString());
        //        }
        //        arrayList.Add(dictionary); //ArrayList集合中添加键值
        //    }

        //    return javaScriptSerializer.Serialize(arrayList);  //返回一个json字符串
        //}
        /// <summary>
        /// datarow转成实体类
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public T FillModel<T>(DataRow dr)
        {
            if (dr == null)
            {
                return default(T);
            }

            T model = (T)Activator.CreateInstance(typeof(T));
            //T model = new T();

            for (int i = 0; i < dr.Table.Columns.Count; i++)
            {
                PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName.ToLower());
                if (propertyInfo != null && dr[i] != DBNull.Value)
                    propertyInfo.SetValue(model, dr[i], null);
            }
            return model;
        }
        /// <summary>
        /// datatable转成实体类
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<T> FillModel<T>(DataTable dt)
        {
            try
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    return null;
                }
                List<T> modelList = new List<T>();
                foreach (DataRow dr in dt.Rows)
                {
                    T model = (T)Activator.CreateInstance(typeof(T));
                    //T model = new T();
                    for (int i = 0; i < dr.Table.Columns.Count; i++)
                    {
                        PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName.ToLower());
                        if (propertyInfo != null && dr[i] != DBNull.Value && dr[i].ToString() != string.Empty)
                            switch (propertyInfo.PropertyType.Name.ToLower())
                            {
                                case "int32":
                                    propertyInfo.SetValue(model, Convert.ToInt32(dr[i]), null);
                                    break;
                                case "datetime":
                                    propertyInfo.SetValue(model, Convert.ToDateTime(dr[i]), null);
                                    break;
                                default:
                                    propertyInfo.SetValue(model, dr[i], null);
                                    break;
                            }

                    }

                    modelList.Add(model);
                }
                return modelList;
            }
            catch (Exception ex)
            {
                LogApi.DebugInfo(ex);
                return null;
            }
        }

        /// <summary>
        /// datatable转成list
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<string> FillList(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            List<string> tempList = new List<string>();

            foreach (DataRow dr in dt.Rows)
            {
                tempList.Add(dr[0].ToString());
            }
            return tempList;
        }
        /// <summary>
        /// 转换列名
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dict"></param>
        /// <returns></returns>
        public DataTable TransDataTable(DataTable dt, Dictionary<string, string> dict)
        {
            foreach (var item in dict)
            {
                try
                {
                    dt.Columns[item.Key].ColumnName = item.Value;
                }
                catch
                {

                }
            }
            return dt;
        }

        public DataRow SetDataRow(DataRow dr, Dictionary<string, string> dict)
        {
            foreach (var item in dict)
            {
                try
                {
                    dr[item.Key] = item.Value;
                }
                catch
                {

                }
            }
            return dr;
        }
        /// <summary>
        /// DataTable转json
        /// </summary>
        /// <param name="jsonName"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJson(string jsonName, DataTable dt)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("{\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString().ToLower() + "\":\"" + dt.Rows[i][j].ToString() + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }
        /// <summary>
        /// DataTable转换成json数组字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string DataTableToJsonList(DataTable dt)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString().ToLower() + "\":\"" + dt.Rows[i][j].ToString() + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]");
            return Json.ToString();
        }
        /// <summary>
        /// 转成UTF-8编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        //public string EnUTF8(string str)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str);
        //    for (int i = 0; i < byStr.Length; i++)
        //    {
        //        sb.Append(@"%" + Convert.ToString(byStr[i], 16));
        //    }

        //    return (sb.ToString());
        //}

        public string EnUTF8(string unicodeString)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            Byte[] encodedBytes = utf8.GetBytes(unicodeString);
            String decodedString = utf8.GetString(encodedBytes, 0, encodedBytes.Length);
            return decodedString;
        }

        //public string GetUtf8(string sUnicode)
        //{
        //    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sUnicode);
        //    string UTF8string = System.Text.Encoding.UTF8.GetString(buffer);
        //    return UTF8string;
        //}
        /// <summary>
        /// 根据条件获取获取sql 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public string GetSqlBySqlWhere(string sql ,string sqlWhere)
        {
            StringBuilder sbsql = new StringBuilder();
            sbsql.Append(sql);
            if (!string.IsNullOrWhiteSpace(sqlWhere))
            {
                sbsql.Append(" ");
                sbsql.Append("and");
                sbsql.Append(" ");
                sbsql.Append(sqlWhere);
            }             
            return sbsql.ToString();
        }
        /// <summary>
        /// 根据键值获取字典中的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public string GetDictionaryVal(Dictionary<string, string> dict, string key, string defaultVal = "")
        {
            if (dict.ContainsKey(key))
            {
                return dict[key].ToString();
            }
            return defaultVal;        
        }

    }
}
