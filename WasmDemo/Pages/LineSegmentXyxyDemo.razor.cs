using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SciChart.Blazor.Components;

namespace WasmDemo.Pages;

public partial class LineSegmentXyxyDemo : ComponentBase
{
    private XyxyDataSeries? _xyxyDataSeriesLinesegment1Ref;

    // Track the current segment index
    private int _currentSegmentCount = 5; // Initial data has 5 segments

    // Sample line segment data representing disconnected lines (start x,y -> end x1,y1)
    private double[] xValues = new double[] { 0, 2, 4, 6, 8 };       // Start X
    private double[] yValues = new double[] { 1, 3, 2, 4, 3 };       // Start Y
    private double[] x1Values = new double[] { 1, 3, 5, 7, 9 };      // End X
    private double[] y1Values = new double[] { 3, 1, 4, 2, 5 };      // End Y

    [Inject]
    private ILogger<LineSegmentXyxyDemo>? Logger { get; set; }

    private async Task AppendData()
    {
        if (_xyxyDataSeriesLinesegment1Ref == null)
        {
            Logger?.LogInformation("XyxyDataSeries reference is null. Cannot append data.");
            return;
        }

        try
        {
            // Generate 2 new line segments
            var newXValues = new double[2];
            var newYValues = new double[2];
            var newX1Values = new double[2];
            var newY1Values = new double[2];

            for (int i = 0; i < 2; i++)
            {
                int baseX = _currentSegmentCount * 2 + i * 2;
                newXValues[i] = baseX;
                newYValues[i] = 3;
                newX1Values[i] = baseX + 1;
                newY1Values[i] = 4;
            }

            Logger?.LogInformation($"Appending 2 line segments starting at segment {_currentSegmentCount}");
            await _xyxyDataSeriesLinesegment1Ref.AppendRange(newXValues, newYValues, newX1Values, newY1Values);

            _currentSegmentCount += 2;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to line segment series");
        }
    }

    private async Task UpdateData()
    {
        if (_xyxyDataSeriesLinesegment1Ref == null) return;
        try
        {
            await _xyxyDataSeriesLinesegment1Ref.UpdateXyz(0, 0, 3, 1, 4);
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to update line segment series");
        }
    }

    private async Task InsertData()
    {
        if (_xyxyDataSeriesLinesegment1Ref == null) return;
        try
        {
            var newXValues = new double[] { 1.3, 1.6 };
            var newYValues = new double[] { 2, 3 };
            var newX1Values = new double[] { 1.4, 1.7 };
            var newY1Values = new double[] { 4, 5 };
            await _xyxyDataSeriesLinesegment1Ref.InsertRange(1, newXValues, newYValues, newX1Values, newY1Values);
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to insert data into line segment series");
        }
    }

    private async Task InsertDataByPointer()
    {
        if (_xyxyDataSeriesLinesegment1Ref == null) return;
        try
        {
            var newXValues = new double[] { 1.3, 1.6 };
            var newYValues = new double[] { 2, 3 };
            var newX1Values = new double[] { 1.4, 1.7 };
            var newY1Values = new double[] { 4, 5 };
            await _xyxyDataSeriesLinesegment1Ref.InsertRangeByPointer(1, newXValues, newYValues, newX1Values, newY1Values);
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to insert data into line segment series by pointer");
        }
    }

    private async Task RemoveData()
    {
        if (_xyxyDataSeriesLinesegment1Ref == null) return;
        try
        {
            await _xyxyDataSeriesLinesegment1Ref.RemoveRange(1, 2);
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to remove data from line segment series");
        }
    }

    private async Task AppendDataByPointer()
    {
        if (_xyxyDataSeriesLinesegment1Ref == null)
        {
            Logger?.LogInformation("XyxyDataSeries reference is null. Cannot append data.");
            return;
        }

        try
        {
            // Generate 2 new line segments
            var newXValues = new double[2];
            var newYValues = new double[2];
            var newX1Values = new double[2];
            var newY1Values = new double[2];

            for (int i = 0; i < 2; i++)
            {
                int baseX = _currentSegmentCount * 2 + i * 2;
                newXValues[i] = baseX;
                newYValues[i] = 3;
                newX1Values[i] = baseX + 1;
                newY1Values[i] = 4;
            }

            Logger?.LogInformation($"Appending 2 line segments by pointer starting at segment {_currentSegmentCount}");
            await _xyxyDataSeriesLinesegment1Ref.AppendRangeByPointer(newXValues, newYValues, newX1Values, newY1Values);

            _currentSegmentCount += 2;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to line segment series by pointer");
        }
    }
}
