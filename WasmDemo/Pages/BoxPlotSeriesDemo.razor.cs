using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SciChartBlazor.Components;

namespace WasmDemo.Pages;

public partial class BoxPlotSeriesDemo : ComponentBase
{
    private BoxPlotDataSeries? _boxPlotDataSeriesBoxplot1Ref;

    // Track the current index for appending new data
    private int _currentIndex = 5; // Initial data has indices 0-4

    // Sample box plot data representing statistical distributions for 5 categories
    private double[] xData = new double[] { 0, 1, 2, 3, 4 };

    // Maximum values (upper whisker)
    private double[] maximumData = new double[] { 95, 88, 92, 97, 90 };

    // Upper quartile (75th percentile)
    private double[] upperQuartileData = new double[] { 80, 75, 78, 82, 77 };

    // Median (50th percentile)
    private double[] medianData = new double[] { 65, 60, 63, 68, 62 };

    // Lower quartile (25th percentile)
    private double[] lowerQuartileData = new double[] { 50, 45, 48, 53, 47 };

    // Minimum values (lower whisker)
    private double[] minimumData = new double[] { 30, 25, 28, 35, 27 };

    [Inject]
    private ILogger<BoxPlotSeriesDemo>? Logger { get; set; }

    private async Task AppendData()
    {
        if (_boxPlotDataSeriesBoxplot1Ref == null)
        {
            Logger?.LogInformation("BoxPlotDataSeries reference is null. Cannot append data.");
            return;
        }

        try
        {
            // Generate 2 new box plot data points
            var newXValues = new double[] { _currentIndex, _currentIndex + 1 };
            var newMaxValues = new double[] { 93, 89 };
            var newUpperQValues = new double[] { 79, 76 };
            var newMedianValues = new double[] { 64, 61 };
            var newLowerQValues = new double[] { 49, 46 };
            var newMinValues = new double[] { 29, 26 };

            Logger?.LogInformation($"Appending 2 box plot data points starting at index {_currentIndex}");
            await _boxPlotDataSeriesBoxplot1Ref.AppendRange(newXValues, newMaxValues, newUpperQValues, newMedianValues, newLowerQValues, newMinValues);

            _currentIndex += 2;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to box plot series");
        }
    }

    private async Task AppendDataByPointer()
    {
        if (_boxPlotDataSeriesBoxplot1Ref == null)
        {
            Logger?.LogInformation("BoxPlotDataSeries reference is null. Cannot append data.");
            return;
        }

        try
        {
            // Generate 2 new box plot data points
            var newXValues = new double[] { _currentIndex, _currentIndex + 1 };
            var newMaxValues = new double[] { 94, 91 };
            var newUpperQValues = new double[] { 81, 78 };
            var newMedianValues = new double[] { 66, 63 };
            var newLowerQValues = new double[] { 51, 48 };
            var newMinValues = new double[] { 31, 28 };

            Logger?.LogInformation($"Appending 2 box plot data points by pointer starting at index {_currentIndex}");
            await _boxPlotDataSeriesBoxplot1Ref.AppendRangeByPointer(newXValues, newMaxValues, newUpperQValues, newMedianValues, newLowerQValues, newMinValues);

            _currentIndex += 2;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to box plot series by pointer");
        }
    }
}
