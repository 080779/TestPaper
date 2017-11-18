using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Chat.WebCommon
{
    public static class MVCHelper
    {
        /// <summary>
        /// mvc中model属性验证
        /// </summary>
        public static string GetValidMsg(ModelStateDictionary modelSatae)
        {
            StringBuilder builer = new StringBuilder();
            foreach (var propName in modelSatae.Keys)
            {
                if (modelSatae[propName].Errors.Count <= 0)
                {
                    continue;
                }
                builer.Append("数据验证失败：");
                foreach (ModelError modelError in modelSatae[propName].Errors)
                {
                    builer.Append(modelError.ErrorMessage+"......");
                }
            }
            return builer.ToString();
        }
    }
}
