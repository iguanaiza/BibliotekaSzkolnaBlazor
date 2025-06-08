using Microsoft.JSInterop;

namespace BibliotekaSzkolnaBlazor.Services.Extensions
{
    public static class IJSRuntimeExtensions
    {
        public static async Task ToastrSuccess(this IJSRuntime js, string message, string title)
        {
            await js.InvokeVoidAsync("ShowToastr", "success", message, title);
        }

        public static async Task ToastrError(this IJSRuntime js, string message, string title)
        {
            await js.InvokeVoidAsync("ShowToastr", "error", message, title);
        }

        public static async Task ToastrInfo(this IJSRuntime js, string message, string title)
        {
            await js.InvokeVoidAsync("ShowToastr", "info", message, title);
        }

        public static async Task ToastrWarning(this IJSRuntime js, string message, string title)
        {
            await js.InvokeVoidAsync("ShowToastr", "warning", message, title);
        }
    }

}