using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.AdminWeb.App_Start
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionAttribute : Attribute
    {
        public string Permission { get; set; }
        public PermissionAttribute(string permission)
        {
            this.Permission = permission;
        }
    }
}