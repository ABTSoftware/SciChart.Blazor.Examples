using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SciChart.Blazor.Components;

namespace WasmDemo.Pages;

public partial class StylingDemo : ComponentBase
{
    [Inject]
    private ILogger<StylingDemo> Logger { get; set; } = null!;

    private SciChartSurface _sciChartRef;

    // Basic Settings
    private string? _id = "styling-demo-chart";
    private string? _touchAction = null;
    private bool _freezeWhenOutOfView = false;
    private bool _createSuspended = false;

    // Theme
    private string _selectedTheme = "dark";
    private ThemeProvider? _theme;

    // Title
    private string _title = "Styling Demo Chart 2";

    // Title Style
    private ETitlePosition _titlePosition = ETitlePosition.Top;
    private ETextAlignment _titleAlignment = ETextAlignment.Center;
    private bool _titlePlaceWithinChart = false;
    private double _titleFontSize = 20;
    private string _titleColor = "red";
    private TChartTitleStyle? _titleStyle = null;

    // Background
    private string _background;

    // Aspect Ratio
    private bool _disableAspect = false;
    private double _widthAspect = 3;
    private double _heightAspect = 2;

    // Padding
    private double _paddingLeft = 0;
    private double _paddingTop = 0;
    private double _paddingRight = 0;
    private double _paddingBottom = 0;
    private Thickness? _padding = null;

    // Canvas Border
    private string _canvasBorderColor = "#ff6b6b";
    private double _canvasBorderLeft = 0;
    private double _canvasBorderTop = 0;
    private double _canvasBorderRight = 0;
    private double _canvasBorderBottom = 0;
    private TBorder? _canvasBorder = null;

    // Viewport Border
    private string _viewportBorderColor = "#4dabf7";
    private double _viewportBorderLeft = 0;
    private double _viewportBorderTop = 0;
    private double _viewportBorderRight = 0;
    private double _viewportBorderBottom = 0;
    private TBorder? _viewportBorder = null;

    // Other Settings
    private bool _drawSeriesBehindAxis = false;
    private EAutoColorMode _autoColorMode = EAutoColorMode.Once;

    protected override void OnInitialized()
    {
        _theme = new ThemeProvider(EThemeProviderType.Dark);
        _theme.AxisTitleColor = "blue";

        Logger.LogInformation("StylingDemo initialized");
        UpdateTitleStyle();
    }


    // NEEDS REBULDING CHART
    private void OnThemeChanged(ChangeEventArgs e)
    {
        _selectedTheme = e.Value?.ToString() ?? "none";

        _theme = _selectedTheme switch
        {
            "dark" => new ThemeProvider(EThemeProviderType.Dark),
            "light" => new ThemeProvider(EThemeProviderType.Light),
            "dark2" => new ThemeProvider(EThemeProviderType.DarkV2),
            "navy" => new ThemeProvider(EThemeProviderType.Navy),
            _ => new ThemeProvider(EThemeProviderType.Dark)
        };

        MarkChartAsNeedingRebuild();
    }

    private void OnTitleChanged(ChangeEventArgs e)
    {
        _title = e.Value?.ToString() ?? "";
        MarkChartAsNeedingUpdate();
    }

    private void OnTitlePositionChanged(ChangeEventArgs e)
    {
        if (Enum.TryParse<ETitlePosition>(e.Value?.ToString(), out var position))
        {
            _titlePosition = position;
            UpdateTitleStyle();
        }
    }

    private void OnTitleAlignmentChanged(ChangeEventArgs e)
    {
        if (Enum.TryParse<ETextAlignment>(e.Value?.ToString(), out var alignment))
        {
            _titleAlignment = alignment;
            UpdateTitleStyle();
        }
    }

    private void OnTitlePlaceWithinChartChanged(ChangeEventArgs e)
    {
        _titlePlaceWithinChart = (bool)(e.Value ?? false);
        UpdateTitleStyle();
    }

    private void OnTitleFontSizeChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var size))
        {
            _titleFontSize = size;
            UpdateTitleStyle();
        }
    }

    private void OnTitleColorChanged(ChangeEventArgs e)
    {
        _titleColor = e.Value?.ToString() ?? "#c8c7c3";
        UpdateTitleStyle();
    }

    private void UpdateTitleStyle()
    {
        _titleStyle = new TChartTitleStyle
        {
            Position = _titlePosition,
            Alignment = _titleAlignment,
            PlaceWithinChart = _titlePlaceWithinChart,
            FontSize = _titleFontSize,
            Color = _titleColor
        };
        MarkChartAsNeedingUpdate();
    }

    private void OnBackgroundChanged(ChangeEventArgs e)
    {
        _background = e.Value?.ToString() ?? "#1e1e1e";
        MarkChartAsNeedingUpdate();
    }

    // NEEDS REBULDING CHART
    private void OnDisableAspectChanged(ChangeEventArgs e)
    {
        _disableAspect = (bool)(e.Value ?? false);
        MarkChartAsNeedingRebuild();
    }

    // NEEDS REBULDING CHART
    private void OnWidthAspectChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var value))
        {
            _widthAspect = value;
            MarkChartAsNeedingRebuild();
        }
    }

    // NEEDS REBULDING CHART
    private void OnHeightAspectChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var value))
        {
            _heightAspect = value;
            MarkChartAsNeedingRebuild();
        }
    }

    // Padding Handlers
    private void OnPaddingLeftChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var value))
        {
            _paddingLeft = value;
            UpdatePadding();
        }
    }

    private void OnPaddingTopChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var value))
        {
            _paddingTop = value;
            UpdatePadding();
        }
    }

    private void OnPaddingRightChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var value))
        {
            _paddingRight = value;
            UpdatePadding();
        }
    }

    private void OnPaddingBottomChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var value))
        {
            _paddingBottom = value;
            UpdatePadding();
        }
    }

    private void UpdatePadding()
    {
        _padding = new Thickness
        {
            left = _paddingLeft,
            top = _paddingTop,
            right = _paddingRight,
            bottom = _paddingBottom
        };
        MarkChartAsNeedingUpdate();
    }

    // Canvas Border Handlers
    private void OnCanvasBorderColorChanged(ChangeEventArgs e)
    {
        _canvasBorderColor = e.Value?.ToString() ?? "#ff6b6b";
        UpdateCanvasBorder();
    }

    private void OnCanvasBorderLeftChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var value))
        {
            _canvasBorderLeft = value;
            UpdateCanvasBorder();
        }
    }

    private void OnCanvasBorderTopChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var value))
        {
            _canvasBorderTop = value;
            UpdateCanvasBorder();
        }
    }

    private void OnCanvasBorderRightChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var value))
        {
            _canvasBorderRight = value;
            UpdateCanvasBorder();
        }
    }

    private void OnCanvasBorderBottomChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var value))
        {
            _canvasBorderBottom = value;
            UpdateCanvasBorder();
        }
    }

    private void UpdateCanvasBorder()
    {
        _canvasBorder = new TBorder
        {
            Color = _canvasBorderColor,
            BorderLeft = (int?)_canvasBorderLeft,
            BorderTop = (int?)_canvasBorderTop,
            BorderRight = (int?)_canvasBorderRight,
            BorderBottom = (int?)_canvasBorderBottom
        };
        MarkChartAsNeedingUpdate();
    }

    // Viewport Border Handlers
    private void OnViewportBorderColorChanged(ChangeEventArgs e)
    {
        _viewportBorderColor = e.Value?.ToString() ?? "#4dabf7";
        UpdateViewportBorder();
    }

    private void OnViewportBorderLeftChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var value))
        {
            _viewportBorderLeft = value;
            UpdateViewportBorder();
        }
    }

    private void OnViewportBorderTopChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var value))
        {
            _viewportBorderTop = value;
            UpdateViewportBorder();
        }
    }

    private void OnViewportBorderRightChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var value))
        {
            _viewportBorderRight = value;
            UpdateViewportBorder();
        }
    }

    private void OnViewportBorderBottomChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var value))
        {
            _viewportBorderBottom = value;
            UpdateViewportBorder();
        }
    }

    private void UpdateViewportBorder()
    {
        _viewportBorder = new TBorder
        {
            Color = _viewportBorderColor,
            BorderLeft = (int?)_viewportBorderLeft,
            BorderTop = (int?)_viewportBorderTop,
            BorderRight = (int?)_viewportBorderRight,
            BorderBottom = (int?)_viewportBorderBottom
        };
        MarkChartAsNeedingUpdate();
    }

    // Other Settings Handlers
    private void OnDrawSeriesBehindAxisChanged(ChangeEventArgs e)
    {
        _drawSeriesBehindAxis = (bool)(e.Value ?? false);
        MarkChartAsNeedingUpdate();
    }

    private void OnAutoColorModeChanged(ChangeEventArgs e)
    {
        if (Enum.TryParse<EAutoColorMode>(e.Value?.ToString(), out var mode))
        {
            _autoColorMode = mode;
            MarkChartAsNeedingUpdate();
        }
    }

    private void MarkChartAsNeedingUpdate()
    {
        if (_sciChartRef != null)
        {
            _sciChartRef.RequestUpdate();
        }
    }

    private void MarkChartAsNeedingRebuild()
    {
        if (_sciChartRef != null)
        {
            _sciChartRef.RequestRebuild();
        }
    }
}
