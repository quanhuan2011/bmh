using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Common.pub
{
    /// <summary>
    /// 全局变量通用类
    /// by wangjc 20161017
    /// </summary>
    public static class CommonVariables
    {
        #region FTP配置
        #region FtpPt 图片
        /// <summary>
        /// 图片ftp用户名
        /// </summary>
        public static string FtpUserPt
        {
            get
            {
                return ConfigurationManager.AppSettings["FtpUserPt"].ToString();
            }
        }
        /// <summary>
        /// 图片ftp密码
        /// </summary>
        public static string FtpPwdPt
        {
            get
            {
                return ConfigurationManager.AppSettings["FtpPwdPt"].ToString();
            }
        }
        /// <summary>
        /// 图片ftpurl地址
        /// </summary>
        public static string FtpUrlPt
        {
            get
            {
                return ConfigurationManager.AppSettings["FtpUrlPt"].ToString();
            }
        }

        /// <summary>
        /// 图片访问地址
        /// </summary>
        public static string PtUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["PtUrl"].ToString();
            }
        }

        #endregion

        #region FtpApp 安装包
        /// <summary>
        /// 安装包ftp用户名
        /// </summary>
        public static string FtpUserApp
        {
            get
            {
                return ConfigurationManager.AppSettings["FtpUserApp"].ToString();
            }
        }
        /// <summary>
        /// 图片ftp密码
        /// </summary>
        public static string FtpPwdApp
        {
            get
            {
                return ConfigurationManager.AppSettings["FtpPwdApp"].ToString();
            }
        }
        /// <summary>
        /// 安装包ftpurl地址
        /// </summary>
        public static string FtpUrlApp
        {
            get
            {
                return ConfigurationManager.AppSettings["FtpUrlApp"].ToString();
            }
        }
        /// <summary>
        /// App访问地址
        /// </summary>
        public static string AppUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["AppUrl"].ToString();
            }
        }
        #endregion

        /// <summary>
        /// FTP字符编码
        /// </summary>
        public static string EncodingType
        {
            get
            {
                return ConfigurationManager.AppSettings["EncodingType"].ToString();
            }
        }
        #endregion

        /// <summary>
        /// 获取列表页默认显示行数
        /// </summary>
        public static int PageSize
        {
            get
            {
                if (ConfigurationManager.AppSettings["DefaultPageSize"] != null)
                {
                    return int.Parse(ConfigurationManager.AppSettings["DefaultPageSize"]);
                }
                else
                {
                    return 10;
                }
            }
        }

        /// <summary>
        /// 获取页面显示个数
        /// </summary>
        public static int PageNumber
        {
            get
            {
                if (ConfigurationManager.AppSettings["DefaultPageNumber"] != null)
                {
                    return int.Parse(ConfigurationManager.AppSettings["DefaultPageNumber"]);
                }
                else
                {
                    return 6;
                }
            }
        }


        #region 表名、视图、序列

        #region 视图
        /// <summary>
        /// 物料管理-视图
        /// </summary>
        public const string V_BEE_MATERIALINFO = "V_BEE_MATERIALINFO";

        #endregion

        #region 序列
        /// <summary>
        /// 物料表-序列
        /// </summary>
        public const string SEQ_BEE_MATERIALINFO_ID = "SEQ_BEE_MATERIALINFO_ID";
        /// <summary>
        /// 日志表-序列
        /// </summary>
        public const string SEQ_BEE_LOGINFO_ID = "SEQ_BEE_LOGINFO_ID";
        #endregion

        #region 表名

        #endregion

        #endregion

    }
}
