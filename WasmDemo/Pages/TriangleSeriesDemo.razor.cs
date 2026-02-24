using Microsoft.AspNetCore.Components;
using SciChartBlazor.Components;

namespace WasmDemo.Pages;

public partial class TriangleSeriesDemo : ComponentBase
{
    private XyDataSeries? _xyDataSeriesTriangle1Ref;

    // Triangle data: Every 3 points form a triangle (in List mode)
    // Triangle 1: (0,0), (2,4), (4,0)
    // Triangle 2: (5,1), (7,5), (9,1)
    private double[] xValues = [0, 2, 4, 5, 7, 9];
    private double[] yValues = [0, 4, 0, 1, 5, 1];

    // Metadata for data labels on each vertex
    private PointMetadata[] metadata = new PointMetadata[]
    {
        new() { CustomText = "0 meta" },
        new() { CustomText = "4 meta" },
        new() { CustomText = "0 meta" },
        new() { CustomText = "1 meta" },
        new() { CustomText = "5 meta" },
        new() { CustomText = "1 meta" }
    };

    private int _nextBaseX = 10;

    private static readonly double[] _peakSequence = [3.5, 4.2, 3.8, 4.7, 3.3, 4.0, 3.6, 4.5];

    private async Task AppendData()
    {
        if (_xyDataSeriesTriangle1Ref == null) return;

        double[] newX = [_nextBaseX, _nextBaseX + 2, _nextBaseX + 4];
        double[] newY = [0, _peakSequence[(_nextBaseX / 5) % _peakSequence.Length], 0];

        var newMetadata = new PointMetadata[]
        {
            new() { CustomText = $"{newY[0]:F1} meta" },
            new() { CustomText = $"{newY[1]:F1} meta" },
            new() { CustomText = $"{newY[2]:F1} meta" }
        };

        await _xyDataSeriesTriangle1Ref.AppendRange(newX, newY, newMetadata);

        _nextBaseX += 5;
    }

    private async Task AppendDataByPointer()
    {
        if (_xyDataSeriesTriangle1Ref == null) return;

        double[] newX = [_nextBaseX, _nextBaseX + 2, _nextBaseX + 4];
        double[] newY = [0, _peakSequence[(_nextBaseX / 5) % _peakSequence.Length], 0];

        var newMetadata = new PointMetadata[]
        {
            new() { CustomText = $"{newY[0]:F1} meta" },
            new() { CustomText = $"{newY[1]:F1} meta" },
            new() { CustomText = $"{newY[2]:F1} meta" }
        };

        await _xyDataSeriesTriangle1Ref.AppendRangeByPointer(newX, newY, newMetadata);

        _nextBaseX += 5;
    }
}
