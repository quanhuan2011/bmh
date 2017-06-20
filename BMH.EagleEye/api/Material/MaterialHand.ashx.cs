using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Material;
using BLL.pub;
using Common.pub;
using BLL.manager;

namespace BMH.EagleEye.api.Material
{
    /// <summary>
    /// MaterialHand 的摘要说明
    /// </summary>
    public class MaterialHand : IHttpHandler
    {
        protected string strBindAdUserId = "";
        MaterialBll materialBll = null;
        ToolClass toolClass = null;

        string _strAccountId = string.Empty;
        string _strAccountName = string.Empty;
        string _strAccountUserName = string.Empty;
        string _strAccountType = string.Empty;
        string _strHeadImageUrl = string.Empty;
        string _strAdUserId = string.Empty;
        public void ProcessRequest(HttpContext context)
        {

            #region 登录cookie信息
            BaseClass.POMOHOCookie cookies = new BaseClass.POMOHOCookie();
            _strAccountId = cookies.BeeAccountId;
            _strAccountName = cookies.BeeAccountName;
            _strAccountUserName = cookies.BeeAccountUserName;
            _strAccountType = cookies.BeeAccountType;
            _strHeadImageUrl = cookies.BeeHeadImageUrl;
            _strAdUserId = cookies.BeeAdUserId;
            #endregion

            var action = context.Request["action"];
            string state = "0";
            switch (action)
            {
                #region 新增物料获取ID
                case "GetMaterialId":
                    toolClass = new ToolClass();
                    int maid = toolClass.GetSeqValue(CommonVariables.SEQ_BEE_MATERIALINFO_ID);
                    context.Response.Write(BLL.pub.Result.GetResult(maid > 0 ? "1" : state, "数据获取成功！", maid));
                    break;
                #endregion

                #region 修改查询数据
                case "EditMaterial"://修改查询数据
                    int id = int.Parse(context.Request["maid"]);
                    materialBll = new MaterialBll();
                    Model.Material model = materialBll.GetMaterialList(id);
                    if (model != null)
                    {
                        state = "1";
                        context.Response.Write(BLL.pub.Result.GetResult(state, "数据获取成功！", model));
                    }
                    break;
                #endregion

                #region 保存 根据【context.Request["datatype"]】 新增【AddMaterial】 或 修改【EditMaterial】
                case "SaveMaterial"://保存 根据【context.Request["datatype"]】 新增【AddMaterial】 或 修改【EditMaterial】
                    string sql = string.Empty;
                    toolClass = new ToolClass();
                    string data = context.Request["data"].ToString();
                    LogModify("SaveMaterial", string.Format("datatype:{0},data:{1}", context.Request["datatype"].ToString(),data));
                    Model.Material material = ConvertData.ConvertJsonToModel<Model.Material>(context.Request["data"].ToString().Replace("\r\n", ""));
                    if (context.Request["datatype"].ToString() == "EditMaterial")//update
                    {                       
                        sql = string.Format("update bee_materialinfo t set t.name='{0}',t.imageurl='{1}',t.linkurl='{2}',t.title='{3}',t.width='{4}',t.height='{5}',t.sizes='{6}',t.display='{7}',t.ismark='{8}',t.materialtype='{9}',t.operationid='{10}',t.format='{11}',t.statustime=sysdate,t.remark='{13}',t.showtime='{14}',t.confirmtext='{15}',t.canceltext='{16}' where materialid='{12}'", material.name, material.imageurl.Replace("&", "'||'&'||'"), material.linkurl.Replace("&", "'||'&'||'"), material.title, material.width, material.height, material.sizes, material.display, material.ismark, material.materialtype, material.operationid, material.format, material.materialid, material.remark, material.showtime, material.confirmtext, material.canceltext);
                        int count = toolClass.ExecuteStatement(sql);
                        context.Response.Write(BLL.pub.Result.GetResult(count > 0 ? "1" : state, "更新失败！", "更新成功！"));
                    }
                    else
                    {
                        MaterialManager materialManager = new MaterialManager();
                        context.Response.Write(materialManager.InsertMaterialData(material));                      
                    }
                    break;
                #endregion

            }
        }
        /// <summary>
        /// 日志记录操作方法
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="data"></param>
        private void LogModify(string funcName, string data)
        {
            YYLog.ClassLibrary.Log.WriteLog(string.Format("YYLog.Modify:/api/MaterialHand/{0}", funcName), "账户信息:账户id{0},账户名{1},ip{2};操作信息:{3}", _strAccountId, _strAccountName, pageclass.IP.GetIP(), data);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}