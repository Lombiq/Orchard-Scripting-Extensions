using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Orchard;
using Orchard.Localization;
using Orchard.UI.Admin;
using Orchard.UI.Notify;
using OrchardHUN.Scripting.Exceptions;
using OrchardHUN.Scripting.Services;
using Piedone.HelpfulLibraries.Contents.DynamicPages;
using Orchard.ContentManagement;
using Orchard.Mvc;
using Orchard.Security;

namespace OrchardHUN.Scripting.Controllers
{
    [Admin]
    public class AdminController : Controller, IUpdateModel
    {
        private readonly IScriptingManager _scriptingManager;
        private readonly IOrchardServices _orchardServices;
        private readonly IContentManager _contentManager;
        private readonly IAuthorizer _authorizer;
        private readonly IPageEventHandler _pageEventHandler;

        public Localizer T { get; set; }


        public AdminController(
            IScriptingManager scriptingManager,
            IOrchardServices orchardServices,
            IPageEventHandler pageEventHandler)
        {
            _scriptingManager = scriptingManager;
            _orchardServices = orchardServices;
            _contentManager = orchardServices.ContentManager;
            _authorizer = orchardServices.Authorizer;
            _pageEventHandler = pageEventHandler;

            T = NullLocalizer.Instance;
        }


        public ActionResult Testbed()
        {
            if (!_authorizer.Authorize(StandardPermissions.SiteOwner, T("You're not allowed to use the scripting testbed.")))
                return new HttpUnauthorizedResult();

            var page = NewPage("Testbed");
            _pageEventHandler.OnPageBuilt(new PageContext(page, PageConfigs.AdminGroup));

            return PageResult(page);
        }

        [HttpPost, ActionName("Testbed")]
        public ActionResult TestbedPost()
        {
            if (!_authorizer.Authorize(StandardPermissions.SiteOwner, T("You're not allowed to use the scripting testbed.")))
                return new HttpUnauthorizedResult();

            var page = NewPage("Testbed");
            _pageEventHandler.OnPageBuilt(new PageContext(page, PageConfigs.AdminGroup));
            _contentManager.UpdateEditor(page, this);

            return PageResult(page);
        }

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }


        private IContent NewPage(string pageName)
        {
            return _orchardServices.ContentManager.NewPage(pageName, PageConfigs.AdminGroup, _pageEventHandler);
        }

        private ShapeResult PageResult(IContent page)
        {
            return new ShapeResult(this, _orchardServices.ContentManager.BuildPageDisplay(page));
        }
    }
}