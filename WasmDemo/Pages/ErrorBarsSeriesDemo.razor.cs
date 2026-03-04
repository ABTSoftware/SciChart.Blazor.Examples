using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SciChart.Blazor.Components;

namespace WasmDemo.Pages;

public partial class ErrorBarsSeriesDemo : ComponentBase
{
    private HlcDataSeries? _hlcDataSeriesErrorbars1Ref;

    // Track the current index for appending new data
    private int _currentIndex = 5; // Initial data has indices 0-4

    // Sample error bars data representing measurements with uncertainties
    private double[] xData = new double[] { 0, 1, 2, 3, 4 };

    // Y values (measured values)
    private double[] yData = new double[] { 50, 55, 52, 58, 54 };

    // High values (measured value + error)
    private double[] highData = new double[] { 55, 60, 58, 64, 60 };

    // Low values (measured value - error)
    private double[] lowData = new double[] { 45, 50, 46, 52, 48 };

    [Inject]
    private ILogger<ErrorBarsSeriesDemo>? Logger { get; set; }

    private async Task AppendData()
    {
        if (_hlcDataSeriesErrorbars1Ref == null)
        {
            Logger?.LogInformation("HlcDataSeries reference is null. Cannot append data.");
            return;
        }

        try
        {
            // Generate 2 new error bars data points
            var newXValues = new double[] { _currentIndex, _currentIndex + 1 };
            var newYValues = new double[] { 56, 53 };
            var newHighValues = new double[] { 62, 59 };
            var newLowValues = new double[] { 50, 47 };

            Logger?.LogInformation($"Appending 2 error bars data points starting at index {_currentIndex}");
            await _hlcDataSeriesErrorbars1Ref.AppendRange(newXValues, newYValues, newHighValues, newLowValues);

            _currentIndex += 2;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to error bars series");
        }
    }

    private async Task AppendDataByPointer()
    {
        if (_hlcDataSeriesErrorbars1Ref == null)
        {
            Logger?.LogInformation("HlcDataSeries reference is null. Cannot append data.");
            return;
        }

        try
        {
            // Generate 2 new error bars data points
            var newXValues = new double[] { _currentIndex, _currentIndex + 1 };
            var newYValues = new double[] { 57, 55 };
            var newHighValues = new double[] { 63, 61 };
            var newLowValues = new double[] { 51, 49 };

            Logger?.LogInformation($"Appending 2 error bars data points by pointer starting at index {_currentIndex}");
            await _hlcDataSeriesErrorbars1Ref.AppendRangeByPointer(newXValues, newYValues, newHighValues, newLowValues);

            _currentIndex += 2;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to error bars series by pointer");
        }
    }
}
