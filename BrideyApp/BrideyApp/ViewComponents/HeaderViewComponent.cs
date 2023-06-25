using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly ILayoutService _layoutService;
        public HeaderViewComponent(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            LayoutVM model = new()
            {
                Settings = _layoutService.GetSettingDatas(),

            };
            return await Task.FromResult(View(model));
        }
    }
}
