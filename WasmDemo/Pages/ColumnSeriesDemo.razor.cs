using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SciChartBlazor.Components;

namespace WasmDemo.Pages;

public partial class ColumnSeriesDemo : ComponentBase
{
    [Inject]
    private ILogger<ColumnSeriesDemo> Logger { get; set; } = null!;

    private XyDataSeries _xyDataSeriesColumnRef;

    // Track the current index for appending new data
    private int _currentIndex = 12;

    private double[] xData;
    private double[] yData;
    private PointMetadata[] metadata;

    protected override void OnInitialized()
    {
        Logger.LogInformation("ColumnSeriesChart initialized");

        // Generate sample data for column series
        double[] fixedYValues = [72, 58, 91, 65, 83, 77, 54, 88, 61, 95, 70, 79];

        xData = new double[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        yData = fixedYValues;
        metadata = xData.Select((_, i) => new PointMetadata
        {
            IsSelected = i % 3 == 0,
            CustomValue = fixedYValues[i],
            CustomText = $"Col {i}"
        }).ToArray();
    }

    private async Task AppendData()
    {
        if (_xyDataSeriesColumnRef == null) return;

        try
        {
            // Generate 3 new data points
            double[] appendYValues = [68, 84, 73];
            var newXValues = new double[] { _currentIndex, _currentIndex + 1, _currentIndex + 2 };
            var newYValues = appendYValues;
            var newMetadata = newXValues.Select((_, i) => new PointMetadata
            {
                IsSelected = (_currentIndex + i) % 3 == 0,
                CustomValue = appendYValues[i],
                CustomText = $"Col {_currentIndex + i}"
            }).ToArray();

            Logger?.LogInformation($"Appending 3 data points starting at index {_currentIndex}");
            await _xyDataSeriesColumnRef.AppendRange(newXValues, newYValues, newMetadata);

            _currentIndex += 3;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to column series");
        }
    }
}
