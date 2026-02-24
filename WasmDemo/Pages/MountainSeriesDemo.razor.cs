using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SciChartBlazor.Components;

namespace WasmDemo.Pages;

public partial class MountainSeriesDemo : ComponentBase
{
    private XyDataSeries? _xyDataSeriesMountain1Ref;
    private XyDataSeries? _xyDataSeriesSplineMountain1Ref;

    // Track the current index for appending new data
    private int _currentIndex = 10; // Initial data has indices 0-9

    // Sample data for mountain chart - a sine wave pattern
    private double[] xData = new double[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private double[] yData = new double[] { 0, 2, 4, 3, 5, 7, 6, 4, 2, 1 };
    private double[] yData2 = new double[] { 1, 3, 2, 4, 3, 5, 4, 6, 3, 2 };

    // Metadata for data labels
    private PointMetadata[] metadata = new PointMetadata[]
    {
        new() { CustomText = "0 meta" },
        new() { CustomText = "2 meta" },
        new() { CustomText = "4 meta" },
        new() { CustomText = "3 meta" },
        new() { CustomText = "5 meta" },
        new() { CustomText = "7 meta" },
        new() { CustomText = "6 meta" },
        new() { CustomText = "4 meta" },
        new() { CustomText = "2 meta" },
        new() { CustomText = "1 meta" }
    };

    // Gradient definition for mountain fill
    private GradientParams mountainGradient = new GradientParams
    {
        StartPoint = new Point { X = 0, Y = 0 },  // Top of the chart
        EndPoint = new Point { X = 0, Y = 1 },    // Bottom of the chart
        GradientStops = new[]
        {
            new TGradientStop { Offset = 0, Color = "rgba(76, 175, 80, 0.8)" },  // More opaque at top
            new TGradientStop { Offset = 1, Color = "rgba(76, 175, 80, 0.1)" }   // More transparent at bottom
        }
    };

    // Gradient definition for spline mountain fill
    private GradientParams splineGradient = new GradientParams
    {
        StartPoint = new Point { X = 0, Y = 0 },
        EndPoint = new Point { X = 0, Y = 1 },
        GradientStops = new[]
        {
            new TGradientStop { Offset = 0, Color = "rgba(33, 150, 243, 0.8)" },  // Blue, more opaque at top
            new TGradientStop { Offset = 1, Color = "rgba(33, 150, 243, 0.1)" }   // Blue, more transparent at bottom
        }
    };

    [Inject]
    private ILogger<MountainSeriesDemo>? Logger { get; set; }

    private async Task AppendData()
    {
        if (_xyDataSeriesMountain1Ref == null) return;

        try
        {
            var newXValues = new double[] { _currentIndex, _currentIndex + 1 };
            var newYValues = new double[]
            {
                3 + Math.Sin(_currentIndex * 0.5) * 2,
                3 + Math.Sin((_currentIndex + 1) * 0.5) * 2
            };
            var newY2Values = new double[]
            {
                3 + Math.Cos(_currentIndex * 0.4) * 2,
                3 + Math.Cos((_currentIndex + 1) * 0.4) * 2
            };
            var newMetadata = new PointMetadata[]
            {
                new() { CustomText = $"{newYValues[0]:F1} meta" },
                new() { CustomText = $"{newYValues[1]:F1} meta" }
            };

            Logger?.LogInformation($"Appending 2 data points starting at index {_currentIndex}");
            await _xyDataSeriesMountain1Ref.AppendRange(newXValues, newYValues, newMetadata);
            await _xyDataSeriesSplineMountain1Ref!.AppendRange(newXValues, newY2Values);

            _currentIndex += 2;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to mountain series");
        }
    }

    private async Task AppendDataByPointer()
    {
        if (_xyDataSeriesMountain1Ref == null) return;

        try
        {
            var newXValues = new double[] { _currentIndex, _currentIndex + 1 };
            var newYValues = new double[]
            {
                3 + Math.Sin(_currentIndex * 0.5) * 2,
                3 + Math.Sin((_currentIndex + 1) * 0.5) * 2
            };
            var newY2Values = new double[]
            {
                3 + Math.Cos(_currentIndex * 0.4) * 2,
                3 + Math.Cos((_currentIndex + 1) * 0.4) * 2
            };
            var newMetadata = new PointMetadata[]
            {
                new() { CustomText = $"{newYValues[0]:F1} meta" },
                new() { CustomText = $"{newYValues[1]:F1} meta" }
            };

            Logger?.LogInformation($"Appending 2 data points by pointer starting at index {_currentIndex}");
            await _xyDataSeriesMountain1Ref.AppendRangeByPointer(newXValues, newYValues, newMetadata);
            await _xyDataSeriesSplineMountain1Ref!.AppendRangeByPointer(newXValues, newY2Values);

            _currentIndex += 2;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to mountain series by pointer");
        }
    }
}
