using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SciChartBlazor.Components;

namespace WasmDemo.Pages;

public partial class RectangleSeriesDemo : ComponentBase
{
    private SciChartSurface? _sciChartRef;
    private XyxyDataSeries? _xyxyDataSeriesRectangle1Ref;
    private bool useGradient = false;

    // Track the current rectangle count
    private int _currentRectangleCount = 6; // Initial data has 6 rectangles

    // Sample rectangle data (X, Y = top-left corner, X1, Y1 = bottom-right corner)
    private double[] xValues = new double[] { 1, 2, 3, 4, 5, 6 };       // X positions
    private double[] yValues = new double[] { 5, 8, 6, 9, 7, 10 };      // Y top values
    private double[] x1Values = new double[] { 1.8, 2.8, 3.8, 4.8, 5.8, 6.8 };  // X1 (right edge)
    private double[] y1Values = new double[] { 0, 0, 0, 0, 0, 0 };      // Y1 (bottom baseline)

    // Metadata for rectangles
    private PointMetadata[] metadata = new PointMetadata[]
    {
        new PointMetadata { CustomText = "Rect 1", CustomValue = 5 },
        new PointMetadata { CustomText = "Rect 2", CustomValue = 8 },
        new PointMetadata { CustomText = "Rect 3", CustomValue = 6 },
        new PointMetadata { CustomText = "Rect 4", CustomValue = 9 },
        new PointMetadata { CustomText = "Rect 5", CustomValue = 7 },
        new PointMetadata { CustomText = "Rect 6", CustomValue = 10 }
    };

    private GradientParams rectangleGradient;

    [Inject]
    private ILogger<RectangleSeriesDemo>? Logger { get; set; }

    private async Task AppendData()
    {
        if (_xyxyDataSeriesRectangle1Ref == null)
        {
            Logger?.LogInformation("XyxyDataSeries reference is null. Cannot append data.");
            return;
        }

        try
        {
            // Generate 2 new rectangles
            var random = new Random();
            var newXValues = new double[2];
            var newYValues = new double[2];
            var newX1Values = new double[2];
            var newY1Values = new double[2];
            var newMetadata = new PointMetadata[2];

            for (int i = 0; i < 2; i++)
            {
                int rectIndex = _currentRectangleCount + i;
                newXValues[i] = rectIndex + 1;
                newYValues[i] = 5 + random.Next(0, 6); // Random height between 5 and 10
                newX1Values[i] = rectIndex + 1.8;     // Width of 0.8
                newY1Values[i] = 0;                    // Baseline at 0
                newMetadata[i] = new PointMetadata
                {
                    CustomText = $"Rect {rectIndex + 1}",
                    CustomValue = newYValues[i]
                };
            }

            Logger?.LogInformation($"Appending 2 rectangles starting at index {_currentRectangleCount}");
            await _xyxyDataSeriesRectangle1Ref.AppendRange(newXValues, newYValues, newX1Values, newY1Values, newMetadata);

            _currentRectangleCount += 2;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to rectangle series");
        }
    }

    private async Task AppendDataByPointer()
    {
        if (_xyxyDataSeriesRectangle1Ref == null)
        {
            Logger?.LogInformation("XyxyDataSeries reference is null. Cannot append data.");
            return;
        }

        try
        {
            // Generate 2 new rectangles
            var random = new Random();
            var newXValues = new double[2];
            var newYValues = new double[2];
            var newX1Values = new double[2];
            var newY1Values = new double[2];
            var newMetadata = new PointMetadata[2];

            for (int i = 0; i < 2; i++)
            {
                int rectIndex = _currentRectangleCount + i;
                newXValues[i] = rectIndex + 1;
                newYValues[i] = 5 + random.Next(0, 6); // Random height between 5 and 10
                newX1Values[i] = rectIndex + 1.8;     // Width of 0.8
                newY1Values[i] = 0;                    // Baseline at 0
                newMetadata[i] = new PointMetadata
                {
                    CustomText = $"Rect {rectIndex + 1}",
                    CustomValue = newYValues[i]
                };
            }

            Logger?.LogInformation($"Appending 2 rectangles by pointer starting at index {_currentRectangleCount}");
            await _xyxyDataSeriesRectangle1Ref.AppendRangeByPointer(newXValues, newYValues, newX1Values, newY1Values, newMetadata);

            _currentRectangleCount += 2;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to rectangle series by pointer");
        }
    }

    private void ToggleGradient()
    {
        useGradient = !useGradient;
        if (_sciChartRef is not null)
        {
            if (useGradient)
            {
                rectangleGradient = new GradientParams
                {
                    StartPoint = new Point { X = 0, Y = 0 },  // Top of the chart
                    EndPoint = new Point { X = 0, Y = 1 },    // Bottom of the chart
                    GradientStops = new[]
                    {
                        new TGradientStop { Offset = 0, Color = "rgba(76, 175, 80, 0.8)" },  // More opaque at top
                        new TGradientStop { Offset = 1, Color = "rgba(76, 175, 80, 0.1)" }   // More transparent at bottom
                    }
                };
            } else
            {
                rectangleGradient = null;
            }
                _sciChartRef.RequestRebuild();
        }
    }
}
