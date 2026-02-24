using Microsoft.AspNetCore.Components;
using SciChartBlazor.Components;

namespace ServerDemo.Pages;

public partial class AxisDemo : ComponentBase
{
    [Inject]
    private ILogger<AxisDemo> Logger { get; set; } = null!;

    private SciChartSurface _sciChartRef;
    private SciChartSurface _sciChartRef2;
    private SciChartSurface _sciChartRef3;

    // Data arrays
    private double[] xData;
    private double[] yData;

    // Category axis data
    private double[] categoryXData;
    private double[] categoryYData;
    private string[] _categoryLabels;

    // DateTime axis data
    private double[] dateTimeXData;
    private double[] dateTimeYData;

    // Second numeric axis data (for multi-axis chart)
    private double[] numericXAxis2Data;
    private double[] numericYAxis2Data;

    // Title Configuration
    private string _xAxisTitle = "X Axis";
    private string _yAxisTitle = "Y Axis";

    // Range Configuration - Chart 1
    private double _visibleRangeMin = -5;
    private double _visibleRangeMax = 5;
    private NumberRange? _xVisibleRange;
    private double _yVisibleRangeMin = -1.5;
    private double _yVisibleRangeMax = 1.5;
    private NumberRange? _yVisibleRange;
    private double _growByValue = 0.1;
    private NumberRange? _growBy;
    private EAutoRange _autoRange = EAutoRange.Once;

    // CategoryAxis Configuration - Chart 2
    private string _categoryAxisTitle = "Month";
    private double _categoryVisibleRangeMin = 0;
    private double _categoryVisibleRangeMax = 11;
    private NumberRange? _categoryVisibleRange;
    private bool _categoryDrawMajorGridLines = true;
    private bool _categoryDrawMajorBands = false;
    private string _categoryAxisBandsFill = "#e6f7ff";

    // DateTimeNumericAxis Configuration - Chart 3
    private string _dateTimeAxisTitle = "Date";
    private double _dateTimeVisibleRangeMin;
    private double _dateTimeVisibleRangeMax;
    private double _dateTimeRangeMinBound;
    private double _dateTimeRangeMaxBound;
    private NumberRange? _dateTimeVisibleRange;
    private bool _dateTimeDrawMajorGridLines = true;
    private bool _dateTimeDrawMajorBands = false;
    private string _dateTimeAxisBandsFill = "#fff4e6";

    // Second NumericAxis Configuration - Chart 3
    private string _numericXAxis2Title = "Index";
    private double _numericXAxis2VisibleRangeMin = 0;
    private double _numericXAxis2VisibleRangeMax = 30;
    private NumberRange? _numericXAxis2VisibleRange;

    // Grid & Tick Configuration
    private bool _drawMajorGridLines = true;
    private bool _drawMinorGridLines = true;
    private int _maxAutoTicks = 10;
    private int _minorsPerMajor = 5;
    private bool _autoTicks = true;

    // Visual Styling
    private bool _drawMajorBands = false;
    private string _axisBandsFill = "#e6e6fa"; // Light lavender color

    // Advanced Options
    private bool _isVisible = true;
    private bool _flippedCoordinates = false;

    protected override void OnInitialized()
    {
        Logger.LogInformation("AxisDemo initialized");

        // Initialize ranges for chart 1
        UpdateVisibleRange();
        UpdateYVisibleRange();
        UpdateGrowBy();

        // Initialize category axis data and labels for chart 2
        _categoryLabels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        categoryXData = new double[12];
        categoryYData = new double[] { 100, 150, 120, 180, 220, 190, 250, 280, 230, 200, 180, 240 };

        for (int i = 0; i < 12; i++)
        {
            categoryXData[i] = i;
        }

        UpdateCategoryVisibleRange();

        // Initialize datetime axis data for chart 3 (30 days of temperature data)
        var startDate = new DateTime(2024, 1, 1);
        dateTimeXData = new double[30];
        dateTimeYData = new double[30];
        var random = new Random(42);

        for (int i = 0; i < 30; i++)
        {
            var date = startDate.AddDays(i);
            dateTimeXData[i] = new DateTimeOffset(date).ToUnixTimeSeconds();
            dateTimeYData[i] = 15 + random.NextDouble() * 15; // Temperature between 15-30°C
        }

        _dateTimeRangeMinBound = dateTimeXData[0];
        _dateTimeRangeMaxBound = dateTimeXData[29];
        _dateTimeVisibleRangeMin = _dateTimeRangeMinBound;
        _dateTimeVisibleRangeMax = _dateTimeRangeMaxBound;
        UpdateDateTimeVisibleRange();

        // Initialize second numeric axis data for chart 3 (simple linear data)
        numericXAxis2Data = new double[30];
        numericYAxis2Data = new double[30];
        var random2 = new Random(123);

        for (int i = 0; i < 30; i++)
        {
            numericXAxis2Data[i] = i;
            numericYAxis2Data[i] = 20 + random2.NextDouble() * 10; // Values between 20-30
        }

        UpdateNumericXAxis2VisibleRange();

        // Generate sample data for line series (sine wave)
        int dataCount = 100;
        xData = new double[dataCount];
        yData = new double[dataCount];

        for (int i = 0; i < dataCount; i++)
        {
            xData[i] = (i - 50) * 0.1; // Range from -5 to 5
            yData[i] = Math.Sin(xData[i]);
        }
    }

    private void UpdateVisibleRange()
    {
        _xVisibleRange = new NumberRange { Min = _visibleRangeMin, Max = _visibleRangeMax };
    }

    private void UpdateYVisibleRange()
    {
        _yVisibleRange = new NumberRange { Min = _yVisibleRangeMin, Max = _yVisibleRangeMax };
    }

    private void UpdateGrowBy()
    {
        _growBy = new NumberRange { Min = _growByValue, Max = _growByValue };
    }

    // Chart rendered callbacks to sync visible range controls
    private void OnChart1Rendered(SciChartSurfaceDefinition chartConfig)
    {
        if (chartConfig?.XAxes != null && chartConfig.XAxes.Count > 0)
        {
            var xAxisOptions = chartConfig.XAxes[0].Options;
            if (xAxisOptions?.VisibleRange != null)
            {
                _visibleRangeMin = xAxisOptions.VisibleRange.Min;
                _visibleRangeMax = xAxisOptions.VisibleRange.Max;
                StateHasChanged();
            }
        }

        if (chartConfig?.YAxes != null && chartConfig.YAxes.Count > 0)
        {
            var yAxisOptions = chartConfig.YAxes[0].Options;
            if (yAxisOptions?.VisibleRange != null)
            {
                _yVisibleRangeMin = yAxisOptions.VisibleRange.Min;
                _yVisibleRangeMax = yAxisOptions.VisibleRange.Max;
                StateHasChanged();
            }
        }
    }

    private void OnChart2Rendered(SciChartSurfaceDefinition chartConfig)
    {
        if (chartConfig?.XAxes != null && chartConfig.XAxes.Count > 0)
        {
            var xAxisOptions = chartConfig.XAxes[0].Options;
            if (xAxisOptions?.VisibleRange != null)
            {
                _categoryVisibleRangeMin = xAxisOptions.VisibleRange.Min;
                _categoryVisibleRangeMax = xAxisOptions.VisibleRange.Max;
                StateHasChanged();
            }
        }
    }

    private void OnChart3Rendered(SciChartSurfaceDefinition chartConfig)
    {
        if (chartConfig?.XAxes != null && chartConfig.XAxes.Count > 0)
        {
            // Sync DateTimeNumericAxis (first X axis)
            var xAxisOptions = chartConfig.XAxes[0].Options;
            if (xAxisOptions?.VisibleRange != null)
            {
                _dateTimeVisibleRangeMin = xAxisOptions.VisibleRange.Min;
                _dateTimeVisibleRangeMax = xAxisOptions.VisibleRange.Max;
                StateHasChanged();
            }

            // Sync second NumericAxis if present
            if (chartConfig.XAxes.Count > 1)
            {
                var xAxis2Options = chartConfig.XAxes[1].Options;
                if (xAxis2Options?.VisibleRange != null)
                {
                    _numericXAxis2VisibleRangeMin = xAxis2Options.VisibleRange.Min;
                    _numericXAxis2VisibleRangeMax = xAxis2Options.VisibleRange.Max;
                    StateHasChanged();
                }
            }
        }
    }

    // CategoryAxis range update method
    private void UpdateCategoryVisibleRange()
    {
        _categoryVisibleRange = new NumberRange { Min = _categoryVisibleRangeMin, Max = _categoryVisibleRangeMax };
    }

    // DateTimeNumericAxis range update method
    private void UpdateDateTimeVisibleRange()
    {
        _dateTimeVisibleRange = new NumberRange { Min = _dateTimeVisibleRangeMin, Max = _dateTimeVisibleRangeMax };
    }

    // Second NumericAxis range update method
    private void UpdateNumericXAxis2VisibleRange()
    {
        _numericXAxis2VisibleRange = new NumberRange { Min = _numericXAxis2VisibleRangeMin, Max = _numericXAxis2VisibleRangeMax };
    }

    private void OnAutoRangeChanged(ChangeEventArgs e)
    {
        if (Enum.TryParse<EAutoRange>(e.Value?.ToString(), out var autoRange))
        {
            _autoRange = autoRange;
            if (_sciChartRef != null)
            {
                _sciChartRef.RequestUpdate();
            }
        }
    }

    private void OnVisibleRangeMinChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var min))
        {
            _visibleRangeMin = min;
            UpdateVisibleRange();
            if (_sciChartRef != null)
            {
                _sciChartRef.RequestUpdate();
            }
        }
    }

    private void OnVisibleRangeMaxChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var max))
        {
            _visibleRangeMax = max;
            UpdateVisibleRange();
            if (_sciChartRef != null)
            {
                _sciChartRef.RequestUpdate();
            }
        }
    }

    private void OnYVisibleRangeMinChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var min))
        {
            _yVisibleRangeMin = min;
            UpdateYVisibleRange();
            if (_sciChartRef != null)
            {
                _sciChartRef.RequestUpdate();
            }
        }
    }

    private void OnYVisibleRangeMaxChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var max))
        {
            _yVisibleRangeMax = max;
            UpdateYVisibleRange();
            if (_sciChartRef != null)
            {
                _sciChartRef.RequestUpdate();
            }
        }
    }

    private void OnGrowByChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var growBy))
        {
            _growByValue = growBy;
            UpdateGrowBy();
            if (_sciChartRef != null)
            {
                _sciChartRef.RequestUpdate();
            }
        }
    }

    private void OnXAxisTitleChanged(ChangeEventArgs e)
    {
        _xAxisTitle = e.Value?.ToString() ?? "X Axis";
        if (_sciChartRef != null)
        {
            _sciChartRef.RequestUpdate();
        }
    }

    private void OnYAxisTitleChanged(ChangeEventArgs e)
    {
        _yAxisTitle = e.Value?.ToString() ?? "Y Axis";
        if (_sciChartRef != null)
        {
            _sciChartRef.RequestUpdate();
        }
    }

    private void OnDrawMajorGridLinesChanged(ChangeEventArgs e)
    {
        _drawMajorGridLines = (bool)(e.Value ?? true);
        if (_sciChartRef != null)
        {
            _sciChartRef.RequestUpdate();
        }
    }

    private void OnDrawMinorGridLinesChanged(ChangeEventArgs e)
    {
        _drawMinorGridLines = (bool)(e.Value ?? true);
        if (_sciChartRef != null)
        {
            _sciChartRef.RequestUpdate();
        }
    }

    private void OnMaxAutoTicksChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out var ticks))
        {
            _maxAutoTicks = ticks;
            if (_sciChartRef != null)
            {
                _sciChartRef.RequestUpdate();
            }
        }
    }

    private void OnMinorsPerMajorChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out var minors))
        {
            _minorsPerMajor = minors;
            if (_sciChartRef != null)
            {
                _sciChartRef.RequestUpdate();
            }
        }
    }

    private void OnAutoTicksChanged(ChangeEventArgs e)
    {
        _autoTicks = (bool)(e.Value ?? true);
        if (_sciChartRef != null)
        {
            _sciChartRef.RequestUpdate();
        }
    }

    private void OnDrawMajorBandsChanged(ChangeEventArgs e)
    {
        _drawMajorBands = (bool)(e.Value ?? false);
        if (_sciChartRef != null)
        {
            _sciChartRef.RequestUpdate();
        }
    }

    private void OnAxisBandsFillChanged(ChangeEventArgs e)
    {
        _axisBandsFill = e.Value?.ToString() ?? "#e6e6fa";
        if (_sciChartRef != null)
        {
            _sciChartRef.RequestUpdate();
        }
    }

    private void OnIsVisibleChanged(ChangeEventArgs e)
    {
        _isVisible = (bool)(e.Value ?? true);
        if (_sciChartRef != null)
        {
            _sciChartRef.RequestUpdate();
        }
    }

    private void OnFlippedCoordinatesChanged(ChangeEventArgs e)
    {
        _flippedCoordinates = (bool)(e.Value ?? false);
        if (_sciChartRef != null)
        {
            _sciChartRef.RequestUpdate();
        }
    }

    // CategoryAxis event handlers
    private void OnCategoryAxisTitleChanged(ChangeEventArgs e)
    {
        _categoryAxisTitle = e.Value?.ToString() ?? "Month";
        if (_sciChartRef2 != null)
        {
            _sciChartRef2.RequestUpdate();
        }
    }

    private void OnCategoryVisibleRangeMinChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var min))
        {
            _categoryVisibleRangeMin = min;
            UpdateCategoryVisibleRange();
            if (_sciChartRef2 != null)
            {
                _sciChartRef2.RequestUpdate();
            }
        }
    }

    private void OnCategoryVisibleRangeMaxChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var max))
        {
            _categoryVisibleRangeMax = max;
            UpdateCategoryVisibleRange();
            if (_sciChartRef2 != null)
            {
                _sciChartRef2.RequestUpdate();
            }
        }
    }

    private void OnCategoryDrawMajorGridLinesChanged(ChangeEventArgs e)
    {
        _categoryDrawMajorGridLines = (bool)(e.Value ?? true);
        if (_sciChartRef2 != null)
        {
            _sciChartRef2.RequestUpdate();
        }
    }

    private void OnCategoryDrawMajorBandsChanged(ChangeEventArgs e)
    {
        _categoryDrawMajorBands = (bool)(e.Value ?? false);
        if (_sciChartRef2 != null)
        {
            _sciChartRef2.RequestUpdate();
        }
    }

    private void OnCategoryAxisBandsFillChanged(ChangeEventArgs e)
    {
        _categoryAxisBandsFill = e.Value?.ToString() ?? "#e6f7ff";
        if (_sciChartRef2 != null)
        {
            _sciChartRef2.RequestUpdate();
        }
    }

    // DateTimeNumericAxis event handlers
    private void OnDateTimeAxisTitleChanged(ChangeEventArgs e)
    {
        _dateTimeAxisTitle = e.Value?.ToString() ?? "Date";
        if (_sciChartRef3 != null)
        {
            _sciChartRef3.RequestUpdate();
        }
    }

    private void OnDateTimeVisibleRangeMinChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var min))
        {
            _dateTimeVisibleRangeMin = min;
            UpdateDateTimeVisibleRange();
            if (_sciChartRef3 != null)
            {
                _sciChartRef3.RequestUpdate();
            }
        }
    }

    private void OnDateTimeVisibleRangeMaxChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var max))
        {
            _dateTimeVisibleRangeMax = max;
            UpdateDateTimeVisibleRange();
            if (_sciChartRef3 != null)
            {
                _sciChartRef3.RequestUpdate();
            }
        }
    }

    private void OnDateTimeDrawMajorGridLinesChanged(ChangeEventArgs e)
    {
        _dateTimeDrawMajorGridLines = (bool)(e.Value ?? true);
        if (_sciChartRef3 != null)
        {
            _sciChartRef3.RequestUpdate();
        }
    }

    private void OnDateTimeDrawMajorBandsChanged(ChangeEventArgs e)
    {
        _dateTimeDrawMajorBands = (bool)(e.Value ?? false);
        if (_sciChartRef3 != null)
        {
            _sciChartRef3.RequestUpdate();
        }
    }

    private void OnDateTimeAxisBandsFillChanged(ChangeEventArgs e)
    {
        _dateTimeAxisBandsFill = e.Value?.ToString() ?? "#fff4e6";
        if (_sciChartRef3 != null)
        {
            _sciChartRef3.RequestUpdate();
        }
    }

    // Second NumericAxis event handlers
    private void OnNumericXAxis2TitleChanged(ChangeEventArgs e)
    {
        _numericXAxis2Title = e.Value?.ToString() ?? "Index";
        if (_sciChartRef3 != null)
        {
            _sciChartRef3.RequestUpdate();
        }
    }

    private void OnNumericXAxis2VisibleRangeMinChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var min))
        {
            _numericXAxis2VisibleRangeMin = min;
            UpdateNumericXAxis2VisibleRange();
            if (_sciChartRef3 != null)
            {
                _sciChartRef3.RequestUpdate();
            }
        }
    }

    private void OnNumericXAxis2VisibleRangeMaxChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var max))
        {
            _numericXAxis2VisibleRangeMax = max;
            UpdateNumericXAxis2VisibleRange();
            if (_sciChartRef3 != null)
            {
                _sciChartRef3.RequestUpdate();
            }
        }
    }
}
