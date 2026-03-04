using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SciChart.Blazor.Components;

namespace WasmDemo.Pages;

public partial class LineSeriesDemo : ComponentBase
{
    [Inject]
    private ILogger<LineSeriesDemo> Logger { get; set; } = null!;

    private SciChartSurface _sciChartRef;
    private XyDataSeries _xyDataSeriesLine1Ref;
    private XyDataSeries _xyDataSeriesLine2Ref;
    private XyDataSeries _xyDataSeriesSplineLine1Ref;

    // Track the current index for appending new data
    private int _currentIndex = 10;

    private double[] xData;
    private double[] yData;
    private double[] yData2;
    private double[] yData3;
    private PointMetadata[] metadata;

    // Series style fields
    private string _stroke = "#ff0000";  // red in hex
    private double _strokeThickness = 4;

    // PointMarker state fields
    private EPointMarkerType _selectedMarkerType = EPointMarkerType.Ellipse;
    private bool _lastPointOnly = false;
    private int _markerSize = 10;
    private PointMarker? _pointMarker;

    // JavaScript function string for custom tooltip template
    private string tooltipTemplate = @"seriesInfo => [
        `X: ${seriesInfo.formattedXValue}`,
        `Y: ${seriesInfo.formattedYValue}`,
        `Series: ${seriesInfo.seriesName}`,
        `Metadata.CustomText: ${seriesInfo.pointMetadata?.customText ?? 'null'}`,
        `Metadata.CustomValue: ${seriesInfo.pointMetadata?.customValue ?? 'null'}`
    ]";

    protected override void OnInitialized()
    {
        Logger.LogInformation("LineSeriesDemo initialized");

        // Initialize point marker
        UpdatePointMarker();

        // Generate sample data for line series
        int dataCount = 10;
        xData = new double[dataCount];
        yData = new double[dataCount];
        yData2 = new double[dataCount];
        yData3 = new double[dataCount];
        metadata = new PointMetadata[dataCount];

        for (int i = 0; i < dataCount; i++)
        {
            xData[i] = i;
            yData[i] = Math.Sin(i *1.0) * 10;      // Sine wave
            yData2[i] = Math.Cos(i *1.0) * 8;      // Cosine wave
            yData3[i] = Math.Sin(i *1.05) * 6 + 2; // Spline wave (different frequency, offset)

            // Create metadata for each point
            metadata[i] = new PointMetadata
            {
                IsSelected = i % 3 == 0,  // Every 3rd point is selected
                CustomValue = i * 10.5,   // Sample numeric metadata
                CustomText = $"Point {i}" // Sample string metadata
            };
        }
    }

    private Task AppendData() =>
        AppendDataCore((ds, x, y, m) => ds.AppendRange(x, y, m));

    private Task AppendDataByPointer() =>
        AppendDataCore((ds, x, y, m) => ds.AppendRangeByPointer(x, y, m));

    private async Task AppendDataCore(Func<XyDataSeries, double[], double[], PointMetadata[], Task> appendFn)
    {
        if (_xyDataSeriesLine1Ref == null)
        {
            Logger?.LogInformation("XyDataSeries reference is null. Cannot append data.");
            return;
        }

        try
        {
            var (newXValues, newYValues, newY2Values, newY3Values, newMetadata) = GenerateNextDataPoints();
            Logger?.LogInformation($"Appending 5 data points starting at index {_currentIndex}");

            await appendFn(_xyDataSeriesLine1Ref, newXValues, newYValues, newMetadata);
            await appendFn(_xyDataSeriesLine2Ref, newXValues, newY2Values, newMetadata);
            await appendFn(_xyDataSeriesSplineLine1Ref, newXValues, newY3Values, newMetadata);

            _currentIndex += 5;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to line series");
        }
    }

    private (double[] xValues, double[] yValues, double[] y2Values, double[] y3Values, PointMetadata[] metadata) GenerateNextDataPoints()
    {
        var xValues = new double[5];
        var yValues = new double[5];
        var y2Values = new double[5];
        var y3Values = new double[5];
        var metadata = new PointMetadata[5];

        for (int i = 0; i < 5; i++)
        {
            int index = _currentIndex + i;
            xValues[i] = index;
            yValues[i] = Math.Sin(index * 1.0) * 10;
            y2Values[i] = Math.Cos(index * 1.0) * 8;
            y3Values[i] = Math.Sin(index * 1.05) * 6 + 2;
            metadata[i] = new PointMetadata
            {
                IsSelected = index % 3 == 0,
                CustomValue = index * 10.5,
                CustomText = $"Point {index}"
            };
        }

        return (xValues, yValues, y2Values, y3Values, metadata);
    }

    private void UpdatePointMarker()
    {

        _pointMarker = new PointMarker
        {
            Type = _selectedMarkerType,
            Width = _markerSize,
            Height = _markerSize,
            Fill = "#FF6B6B",
            Stroke = "#C92A2A",
            StrokeThickness = 2,
            Opacity = 0.8,
            LastPointOnly = _lastPointOnly,
            AntiAlias = true
        };

        if (_sciChartRef != null)
        {
            _sciChartRef.RequestUpdate();
        }
    }

    private async Task OnMarkerTypeChanged(ChangeEventArgs e)
    {
        if (Enum.TryParse<EPointMarkerType>(e.Value?.ToString(), out var markerType))
        {
            _selectedMarkerType = markerType;
            UpdatePointMarker();
        }
    }

    private async Task ToggleLastPointOnly(ChangeEventArgs e)
    {
        _lastPointOnly = (bool)(e.Value ?? false);
        UpdatePointMarker();
    }

    private async Task OnMarkerSizeChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out var size))
        {
            _markerSize = size;
            UpdatePointMarker();
        }
    }

    private void OnStrokeColorChanged(ChangeEventArgs e)
    {
        _stroke = e.Value?.ToString() ?? "#ff0000";
        if (_sciChartRef != null)
        {
            _sciChartRef.RequestUpdate();
        }
    }

    private void OnStrokeThicknessChanged(ChangeEventArgs e)
    {
        if (double.TryParse(e.Value?.ToString(), out var thickness))
        {
            _strokeThickness = thickness;
            if (_sciChartRef != null)
            {
                _sciChartRef.RequestUpdate();
            }
        }
    }
}
