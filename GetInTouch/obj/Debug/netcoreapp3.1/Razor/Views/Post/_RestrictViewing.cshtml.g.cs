#pragma checksum "D:\Licenta\FB\GetInTouch\GetInTouch\GetInTouch\Views\Post\_RestrictViewing.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f7380ec71208d8ff1dd7614cdf5c8d199a1bb3b2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Post__RestrictViewing), @"mvc.1.0.view", @"/Views/Post/_RestrictViewing.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Licenta\FB\GetInTouch\GetInTouch\GetInTouch\Views\_ViewImports.cshtml"
using GetInTouch;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Licenta\FB\GetInTouch\GetInTouch\GetInTouch\Views\_ViewImports.cshtml"
using GetInTouch.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Licenta\FB\GetInTouch\GetInTouch\GetInTouch\Views\Post\_RestrictViewing.cshtml"
using GetInTouch.Model;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f7380ec71208d8ff1dd7614cdf5c8d199a1bb3b2", @"/Views/Post/_RestrictViewing.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e172fbc3bdc93c0a5fa0c8a88b3dcde5d6d9c361", @"/Views/_ViewImports.cshtml")]
    public class Views_Post__RestrictViewing : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<GetInTouch.Logic.ViewModels.Post.RestrictViewingViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("restrict-viewing-type-container"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "f7380ec71208d8ff1dd7614cdf5c8d199a1bb3b23698", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("hidden", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
#nullable restore
#line 4 "D:\Licenta\FB\GetInTouch\GetInTouch\GetInTouch\Views\Post\_RestrictViewing.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.RestrictViewingType);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "f7380ec71208d8ff1dd7614cdf5c8d199a1bb3b25533", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("hidden", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
#nullable restore
#line 5 "D:\Licenta\FB\GetInTouch\GetInTouch\GetInTouch\Views\Post\_RestrictViewing.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.PostId);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n<div class=\"mt-2 mb-2 ml-4\">\r\n    <div style=\"font-size: 20px; font-weight: 500;\">Who can see this post?</div>\r\n\r\n    <div class=\"mt-3 ml-3\">\r\n        <div class=\"d-flex mb-2\">\r\n");
#nullable restore
#line 12 "D:\Licenta\FB\GetInTouch\GetInTouch\GetInTouch\Views\Post\_RestrictViewing.cshtml"
             if (Model.RestrictViewingType == RestrictViewingType.OnlyMe)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input onchange=\"setRestrictViewingType()\" id=\"restrict-viewing-only-me\" name=\"inputRadio\" type=\"radio\" checked class=\"mr-2 mt-1\"/>\r\n");
#nullable restore
#line 15 "D:\Licenta\FB\GetInTouch\GetInTouch\GetInTouch\Views\Post\_RestrictViewing.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input onchange=\"setRestrictViewingType()\" id=\"restrict-viewing-only-me\" name=\"inputRadio\" type=\"radio\" class=\"mr-2 mt-1\"/>\r\n");
#nullable restore
#line 19 "D:\Licenta\FB\GetInTouch\GetInTouch\GetInTouch\Views\Post\_RestrictViewing.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div>Only me</div>\r\n        </div>\r\n\r\n        <div class=\"d-flex mb-2\">\r\n");
#nullable restore
#line 25 "D:\Licenta\FB\GetInTouch\GetInTouch\GetInTouch\Views\Post\_RestrictViewing.cshtml"
             if (Model.RestrictViewingType == RestrictViewingType.OnlyCloseFriends)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input onchange=\"setRestrictViewingType()\" id=\"restrict-viewing-only-close-friends\" name=\"inputRadio\" type=\"radio\" checked class=\"mr-2 mt-1\" />\r\n");
#nullable restore
#line 28 "D:\Licenta\FB\GetInTouch\GetInTouch\GetInTouch\Views\Post\_RestrictViewing.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input onchange=\"setRestrictViewingType()\" id=\"restrict-viewing-only-close-friends\" name=\"inputRadio\" type=\"radio\" class=\"mr-2 mt-1\" />\r\n");
#nullable restore
#line 32 "D:\Licenta\FB\GetInTouch\GetInTouch\GetInTouch\Views\Post\_RestrictViewing.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div>Only close friends</div>\r\n        </div>\r\n\r\n        <div class=\"d-flex mb-2\">\r\n");
#nullable restore
#line 38 "D:\Licenta\FB\GetInTouch\GetInTouch\GetInTouch\Views\Post\_RestrictViewing.cshtml"
             if (Model.RestrictViewingType == RestrictViewingType.Everyone)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input onchange=\"setRestrictViewingType()\" id=\"restrict-viewing-everyone\" name=\"inputRadio\" type=\"radio\" checked class=\"mr-2 mt-1\" />\r\n");
#nullable restore
#line 41 "D:\Licenta\FB\GetInTouch\GetInTouch\GetInTouch\Views\Post\_RestrictViewing.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <input onchange=\"setRestrictViewingType()\" id=\"restrict-viewing-everyone\" name=\"inputRadio\" type=\"radio\" class=\"mr-2 mt-1\" />\r\n");
#nullable restore
#line 45 "D:\Licenta\FB\GetInTouch\GetInTouch\GetInTouch\Views\Post\_RestrictViewing.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            <div>Everyone</div>\r\n        </div>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<GetInTouch.Logic.ViewModels.Post.RestrictViewingViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
