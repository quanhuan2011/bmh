using System.Linq;
using System.Web;
using System.Text;
using System.Data.OracleClient;
using System.IO;
using System.Data;
using DAL.database;
using CVBUtility;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BLL.pub
{
    public class UploadHelper
    {
        DBOperate dbOperate = new DBOperate();
        public static bool SaveAgentImg(byte[] bytes,ref string fileno)
        {

            System.Configuration.AppSettingsReader appReader = new System.Configuration.AppSettingsReader();
            string strCn = DatabasePool.GetDatabaseConnectStr(appReader.GetValue("DBClient", typeof(string)).ToString(), "chinavb234123489");
            StringBuilder sbSQL = new StringBuilder("insert into bee_uploadimg(fileid,fileno,filecontent) values(S_uploadimg.Nextval,:fileno,:filecontent)");
            string[] tempCn = strCn.Split(';');
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < tempCn.Length; i++)
            {
                if (tempCn[i].Contains("Incr"))
                    break;
                else
                    sb.Append(tempCn[i]);
                sb.Append(";");
            }
            strCn = sb.ToString();

            OracleConnection cn = new OracleConnection(strCn);
            OracleCommand cmd = cn.CreateCommand();
            cmd.CommandText = sbSQL.ToString();
            fileno= Guid.NewGuid().ToString();
            //cmd.Parameters.Add(":id", OracleType.Int32, 36).Value = 1;
            cmd.Parameters.Add(":fileno", OracleType.VarChar, 50).Value = fileno;
            cmd.Parameters.Add(":filecontent", OracleType.Blob).Value = bytes;
            try
            {
                cn.Open();
                return (cmd.ExecuteNonQuery()==1?true:false);
            }
            catch (Exception ex)
            {
                
                return false;
                
            }
            finally
            {
                cn.Close();
                cmd.Dispose();
            }
          
        }
        public static Byte[] GetImgBytes(string fileNo)
        {
            System.Configuration.AppSettingsReader appReader = new System.Configuration.AppSettingsReader();
            string strCn = DatabasePool.GetDatabaseConnectStr(appReader.GetValue("DBClient", typeof(string)).ToString(), "chinavb234123489");          
            string[] tempCn = strCn.Split(';');
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < tempCn.Length; i++)
            {
                if (tempCn[i].Contains("Incr"))
                    break;
                else
                    sb.Append(tempCn[i]);
                sb.Append(";");
            }
            strCn = sb.ToString();
            OracleConnection cn = new OracleConnection(strCn);
            OracleCommand cmd = cn.CreateCommand();
            cmd.CommandText ="select filecontent from bee_uploadimg where fileno=:fileno";
            cmd.Parameters.Add(":fileno", OracleType.VarChar, 50).Value = fileNo;
            byte[] pic = null;
            try
            {
                cn.Open();
                MemoryStream stream = new MemoryStream();
                IDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    pic = (byte[])reader[0];                    
                }
            }
            catch
            {

            }
            return pic;
        }


    }


}
