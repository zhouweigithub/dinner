using Microsoft.AspNetCore.Razor.TagHelpers;
using Util.Ui.Angular.Base;
using Util.Ui.Configs;
using Util.Ui.Enums;
using Util.Ui.Material.Tabs.Renders;
using Util.Ui.Renders;
using Util.Ui.TagHelpers;

namespace Util.Ui.Material.Tabs.TagHelpers {
    /// <summary>
    /// 导航选项卡，应放在 util-nav-tabs 标签中
    /// </summary>
    [HtmlTargetElement( "util-nav-tab",ParentTag = "util-nav-tabs" )]
    public class TabLinkTagHelper : AngularTagHelperBase {
        /// <summary>
        /// 路由链接
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// 路由链接
        /// </summary>
        public string BindLink { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// Material图标
        /// </summary>
        public MaterialIcon MaterialIcon { get; set; }
        /// <summary>
        /// Font Awesome图标
        /// </summary>
        public FontAwesomeIcon FontAwesomeIcon { get; set; }
        /// <summary>
        /// 禁用
        /// </summary>
        public string Disabled { get; set; }

        /// <summary>
        /// 获取渲染器
        /// </summary>
        /// <param name="context">上下文</param>
        protected override IRender GetRender( Context context ) {
            return new TabLinkRender( new Config( context ) );
        }
    }
}