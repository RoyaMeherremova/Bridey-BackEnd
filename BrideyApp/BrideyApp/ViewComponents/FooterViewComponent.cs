using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels.Layout;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly ILayoutService _layoutService;
        private readonly ISocialService _socialService;

        public FooterViewComponent(ILayoutService layoutService, ISocialService socialService)
        {
            _layoutService = layoutService;
            _socialService = socialService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            FooterVM model = new FooterVM()
            {
                Settings = _layoutService.GetSettingDatas(),
                Socials = await _socialService.GetAll()

            };
            return await Task.FromResult(View(model));
        }
    }
}
