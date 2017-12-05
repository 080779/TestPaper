using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.WebCommon
{
    /// <summary>
    /// 分页方法
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// 每页数据条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总数据条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 当前页码（从 1 开始）
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 点击提交转向地址(ajax提交为一个js方法)
        /// </summary>
        public string UrlPattern { get; set; }
        /// <summary>
        /// 最多的页码数
        /// </summary>
        public int MaxPagerCount { get; set; }
        /// <summary>
        /// 当前页标的〈a〉标签样式名
        /// </summary>
        public string CurrentLinkClassName { get; set; }

        public Pagination()
        {
            this.PageSize = 10;
            this.MaxPagerCount = 10;
        }
        public string GetPagerHtml()
        {
            StringBuilder sb = new StringBuilder();
            //算出来的页数
            int pageCount = (int)Math.Ceiling(TotalCount * 1.0f / PageSize);
            int startPageIndex = Math.Max(1, PageIndex - MaxPagerCount / 2);//第一个页码
            int endPageIndex = Math.Min(pageCount, startPageIndex + MaxPagerCount - 1);//最后一个页码
            sb.AppendLine("<ul><li>第</li>");
            for (int i = startPageIndex; i <= endPageIndex; i++)
            {
                if (i == PageIndex)
                {
                    sb.Append("<li class='").Append(CurrentLinkClassName).Append("'>").Append(i).Append("</li>").AppendLine();
                }
                else
                {
                    sb.Append("<li><a href='").Append(UrlPattern.Replace("{pn}", i.ToString())).Append("'>").Append(i).Append("</a></li>").AppendLine();
                }
            }
            sb.AppendLine("<li>页</li></ul>");
            return sb.ToString();
        }
    }
}
