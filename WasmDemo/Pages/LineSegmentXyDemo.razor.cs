using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SciChart.Blazor.Components;

namespace WasmDemo.Pages;

public partial class LineSegmentXyDemo : ComponentBase
{
    private XyDataSeries? _xyDataSeriesLineSegmentRef;

    // Track the current index for appending new data
    private int _currentIndex = 10; // Initial data has indices 0-9 (5 segments)

    // Sample line segment data using XY pairs (every 2 points form a segment)
    // Points: (0,1)-(1,3), (2,3)-(3,1), (4,2)-(5,4), (6,4)-(7,2), (8,3)-(9,5)
    private double[] xData = new double[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private double[] yData = new double[] { 1, 3, 3, 1, 2, 4, 4, 2, 3, 5 };

    [Inject]
    private ILogger<LineSegmentXyDemo>? Logger { get; set; }

    private async Task AppendData()
    {
        if (_xyDataSeriesLineSegmentRef == null) return;

        try
        {
            // Generate 2 new line segments (4 points)
            var random = new Random();
            var newXValues = new double[4];
            var newYValues = new double[4];

            for (int i = 0; i < 2; i++)
            {
                // Each segment has 2 points
                int idx = i * 2;
                newXValues[idx] = _currentIndex + idx;
                newYValues[idx] = random.Next(1, 6);
                newXValues[idx + 1] = _currentIndex + idx + 1;
                newYValues[idx + 1] = random.Next(1, 6);
            }

            Logger?.LogInformation($"Appending 2 line segments (4 points) starting at index {_currentIndex}");
            await _xyDataSeriesLineSegmentRef.AppendRange(newXValues, newYValues);

            _currentIndex += 4;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to line segment series");
        }
    }

    private async Task AppendDataByPointer()
    {
        if (_xyDataSeriesLineSegmentRef == null) return;

        try
        {
            // Generate 2 new line segments (4 points)
            var random = new Random();
            var newXValues = new double[4];
            var newYValues = new double[4];

            for (int i = 0; i < 2; i++)
            {
                // Each segment has 2 points
                int idx = i * 2;
                newXValues[idx] = _currentIndex + idx;
                newYValues[idx] = random.Next(1, 6);
                newXValues[idx + 1] = _currentIndex + idx + 1;
                newYValues[idx + 1] = random.Next(1, 6);
            }

            Logger?.LogInformation($"Appending 2 line segments (4 points) by pointer starting at index {_currentIndex}");
            await _xyDataSeriesLineSegmentRef.AppendRangeByPointer(newXValues, newYValues);

            _currentIndex += 4;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to line segment series by pointer");
        }
    }
}
