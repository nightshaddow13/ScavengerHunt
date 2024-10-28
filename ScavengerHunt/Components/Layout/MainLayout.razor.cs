using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components;

namespace ScavengerHunt.Components.Layout
{
    public partial class MainLayout
    {
        [Inject] private ProtectedSessionStorage ProtectedSessionStore { get; set; } = default!;

        string? _username = null;
        bool _drawerOpen = true;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            _username = (await ProtectedSessionStore.GetAsync<string>("username")).Value;
            await base.OnParametersSetAsync();
        }

        private async Task Logout()
        {
            _username = null;
            await ProtectedSessionStore.DeleteAsync("username");
            StateHasChanged();
        }

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }
    }
}
