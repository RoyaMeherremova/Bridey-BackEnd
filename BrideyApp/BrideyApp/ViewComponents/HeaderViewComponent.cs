using BrideyApp.Services.Interfaces;
using BrideyApp.ViewModels.Layout;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly ILayoutService _layoutService;
        private readonly ICartService _cartService;

        public HeaderViewComponent(ILayoutService layoutService, ICartService cartService)
        {
            _layoutService = layoutService;
            _cartService = cartService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            LayoutVM model = new()
            {
                Settings = _layoutService.GetSettingDatas(),
                BasketCount = _cartService.GetDatasFromCookie().Count
            };
            return await Task.FromResult(View(model));
        }
    }
}
