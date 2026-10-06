using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SciChart.Blazor.Components;

namespace ServerDemo.Pages;

public partial class Index : ComponentBase
{
    [Inject]
    private IJSRuntime JS { get; set; } = null!;
    [Inject]
    private NavigationManager Navigation { get; set; } = null!;

    // scichart.js reads these when it loads, so a change is applied by reloading the page
    private const string WebGpuStorageKey = "IS_WEB_GPU";
    private const string LicenseDebugStorageKey = "LICENSE_DEBUG";
    private const string WasmModeStorageKey = "SCICHART_WASM_MODE";
    private ERenderMode _renderMode = ERenderMode.Auto;
    private EWasmMode _wasmMode = EWasmMode.Wasm32;
    private bool _licenseDebug;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _renderMode = await JS.InvokeAsync<string?>("localStorage.getItem", WebGpuStorageKey) switch
            {
                "1" => ERenderMode.WebGPU,
                "0" => ERenderMode.WebGL,
                _ => ERenderMode.Auto
            };
            _wasmMode = Enum.TryParse<EWasmMode>(await JS.InvokeAsync<string?>("localStorage.getItem", WasmModeStorageKey), out var wasmMode)
                ? wasmMode
                : EWasmMode.Wasm32;
            _licenseDebug = await JS.InvokeAsync<string?>("localStorage.getItem", LicenseDebugStorageKey) == "1";
            StateHasChanged();
        }
    }

    private async Task OnRenderModeChanged(ChangeEventArgs e)
    {
        _renderMode = Enum.Parse<ERenderMode>(e.Value?.ToString() ?? nameof(ERenderMode.Auto));
        switch (_renderMode)
        {
            case ERenderMode.WebGL:
                await JS.InvokeVoidAsync("localStorage.setItem", WebGpuStorageKey, "0");
                break;
            case ERenderMode.WebGPU:
                await JS.InvokeVoidAsync("localStorage.setItem", WebGpuStorageKey, "1");
                break;
            default:
                await JS.InvokeVoidAsync("localStorage.removeItem", WebGpuStorageKey);
                break;
        }
        Navigation.Refresh(forceReload: true);
    }

    private async Task OnWasmModeChanged(ChangeEventArgs e)
    {
        _wasmMode = Enum.Parse<EWasmMode>(e.Value?.ToString() ?? nameof(EWasmMode.Wasm32));
        await JS.InvokeVoidAsync("localStorage.setItem", WasmModeStorageKey, _wasmMode.ToString());
        Navigation.Refresh(forceReload: true);
    }

    private async Task OnLicenseDebugChanged(ChangeEventArgs e)
    {
        _licenseDebug = e.Value is true;
        await JS.InvokeVoidAsync("localStorage.setItem", LicenseDebugStorageKey, _licenseDebug ? "1" : "0");
        Navigation.Refresh(forceReload: true);
    }
}
