using Microsoft.AspNetCore.Components;
using SciChart.Blazor.Components;

namespace WasmDemo.Pages;

public partial class ScatterSeriesDemo : ComponentBase
{
    private SciChartSurface? _sciChartRef;
    private XyDataSeries? _xyDataSeriesScatter1Ref;
    private XyDataSeries? _xyDataSeriesScatter2Ref;

    private double[] xData1 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    private double[] yData1 = [2.5, 3.8, 2.1, 4.5, 3.2, 5.1, 4.8, 3.5, 5.5, 4.2];

    private double[] xData2 = [1.5, 2.5, 3.5, 4.5, 5.5, 6.5, 7.5, 8.5, 9.5];
    private double[] yData2 = [1.2, 2.8, 1.5, 3.2, 2.5, 4.1, 3.8, 2.5, 4.5];

    // Metadata for data labels on scatter1
    private PointMetadata[] metadata1 = new PointMetadata[]
    {
        new() { CustomText = "2.5 meta" },
        new() { CustomText = "3.8 meta" },
        new() { CustomText = "2.1 meta" },
        new() { CustomText = "4.5 meta" },
        new() { CustomText = "3.2 meta" },
        new() { CustomText = "5.1 meta" },
        new() { CustomText = "4.8 meta" },
        new() { CustomText = "3.5 meta" },
        new() { CustomText = "5.5 meta" },
        new() { CustomText = "4.2 meta" }
    };

    private int _currentIndex = 10;

    private static readonly double[] _y1Sequence = [3.2, 4.1, 2.8, 5.0, 3.7, 4.4, 2.5, 5.3, 3.9, 4.7];
    private static readonly double[] _y2Sequence = [2.1, 3.5, 1.8, 4.2, 2.9, 3.6, 1.5, 4.8, 2.3, 3.1];

    // PointMarker state fields
    private EPointMarkerType _selectedMarkerType = EPointMarkerType.Ellipse;
    private int _markerSize = 12;
    private bool _markersEnabled = true;
    private PointMarker? _pointMarker1;
    private PointMarker? _pointMarker2;

    protected override void OnInitialized()
    {
        // Initialize point markers
        UpdatePointMarker();
    }

    private async Task AppendData()
    {
        if (_xyDataSeriesScatter1Ref == null) return;

        _currentIndex++;
        double newX = _currentIndex;
        double newY1 = _y1Sequence[_currentIndex % _y1Sequence.Length];
        double newY2 = _y2Sequence[_currentIndex % _y2Sequence.Length];

        var newMetadata1 = new PointMetadata[] { new() { CustomText = $"{newY1:F1} meta" } };

        await _xyDataSeriesScatter1Ref.AppendRange([newX], [newY1], newMetadata1);
        await _xyDataSeriesScatter2Ref!.AppendRange([newX - 0.5], [newY2]);
    }

    private async Task AppendDataByPointer()
    {
        if (_xyDataSeriesScatter1Ref == null) return;

        _currentIndex++;
        double newX = _currentIndex;
        double newY1 = _y1Sequence[_currentIndex % _y1Sequence.Length];
        double newY2 = _y2Sequence[_currentIndex % _y2Sequence.Length];

        var newMetadata1 = new PointMetadata[] { new() { CustomText = $"{newY1:F1} meta" } };

        await _xyDataSeriesScatter1Ref.AppendRangeByPointer([newX], [newY1], newMetadata1);
        await _xyDataSeriesScatter2Ref!.AppendRangeByPointer([newX - 0.5], [newY2]);
    }

    private void UpdatePointMarker()
    {
        if (_markersEnabled)
        {
            // Configurable marker for scatter1 (red)
            _pointMarker1 = new PointMarker
            {
                Type = _selectedMarkerType,
                Width = _markerSize,
                Height = _markerSize,
                Fill = "#FF6B6B",
                Stroke = "#C92A2A",
                StrokeThickness = 2,
                Opacity = 0.8,
                AntiAlias = true
            };
        }
        else
        {
            _pointMarker1 = new PointMarker
            {
                Type = _selectedMarkerType,
                Width = _markerSize,
                Height = _markerSize,
                Fill = "#FF6B6B",
                Stroke = "#C92A2A",
                StrokeThickness = 2,
                Opacity = 0,
                AntiAlias = true
            };
        }

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

    private async Task OnMarkerSizeChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out var size))
        {
            _markerSize = size;
            UpdatePointMarker();
        }
    }

    private async Task ToggleMarkers(ChangeEventArgs e)
    {
        _markersEnabled = (bool)(e.Value ?? true);
        UpdatePointMarker();
    }
}
