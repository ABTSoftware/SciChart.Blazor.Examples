using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SciChart.Blazor.Components;

namespace WasmDemo.Pages;

public partial class BubbleSeriesDemo : ComponentBase
{
    private XyzDataSeries? _xyzDataSeriesBubble1Ref;

    // Track the current index for appending new data
    private int _currentIndex = 10; // Initial data has indices 0-9

    // Sample bubble data with X, Y positions and Z sizes
    private double[] xData = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    private double[] yData = new double[] { 2, 4, 3, 5, 4, 6, 5, 7, 6, 8 };
    private double[] zData = new double[] { 10, 20, 15, 25, 18, 30, 22, 35, 28, 40 };
    private PointMetadata[] metadata = new PointMetadata[]
    {
        new() { IsSelected = true, CustomValue = 0, CustomText = "Bubble 0" },
        new() { IsSelected = false, CustomValue = 10.5, CustomText = "Bubble 1" },
        new() { IsSelected = false, CustomValue = 21, CustomText = "Bubble 2" },
        new() { IsSelected = true, CustomValue = 31.5, CustomText = "Bubble 3" },
        new() { IsSelected = false, CustomValue = 42, CustomText = "Bubble 4" },
        new() { IsSelected = false, CustomValue = 52.5, CustomText = "Bubble 5" },
        new() { IsSelected = true, CustomValue = 63, CustomText = "Bubble 6" },
        new() { IsSelected = false, CustomValue = 73.5, CustomText = "Bubble 7" },
        new() { IsSelected = false, CustomValue = 84, CustomText = "Bubble 8" },
        new() { IsSelected = true, CustomValue = 94.5, CustomText = "Bubble 9" }
    };

    [Inject]
    private ILogger<BubbleSeriesDemo>? Logger { get; set; }

    private async Task AppendData()
    {
        if (_xyzDataSeriesBubble1Ref == null)
        {
            Logger?.LogInformation("XyzDataSeries reference is null. Cannot append data.");
            return;
        }

        try
        {
            // Generate 2 new bubble data points
            var newXValues = new double[] { _currentIndex + 1, _currentIndex + 2 };
            var newYValues = new double[] { 7 + (_currentIndex % 3), 8 + (_currentIndex % 3) };
            var newZValues = new double[] { 15 + (_currentIndex % 10) * 2, 20 + (_currentIndex % 10) * 2 };
            var newMetadata = new PointMetadata[]
            {
                new() { CustomText = $"Bubble {_currentIndex + 1}", CustomValue = (_currentIndex + 1) * 10.5 },
                new() { CustomText = $"Bubble {_currentIndex + 2}", CustomValue = (_currentIndex + 2) * 10.5 }
            };

            Logger?.LogInformation($"Appending 2 bubble data points starting at index {_currentIndex + 1}");
            await _xyzDataSeriesBubble1Ref.AppendRange(newXValues, newYValues, newZValues, newMetadata);

            _currentIndex += 2;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to bubble series");
        }
    }

    private async Task AppendDataByPointer()
    {
        if (_xyzDataSeriesBubble1Ref == null)
        {
            Logger?.LogInformation("XyzDataSeries reference is null. Cannot append data.");
            return;
        }

        try
        {
            // Generate 2 new bubble data points
            var newXValues = new double[] { _currentIndex + 1, _currentIndex + 2 };
            var newYValues = new double[] { 6 + (_currentIndex % 4), 7 + (_currentIndex % 4) };
            var newZValues = new double[] { 18 + (_currentIndex % 12) * 2, 24 + (_currentIndex % 12) * 2 };
            var newMetadata = new PointMetadata[]
            {
                new() { CustomText = $"Bubble {_currentIndex + 1}", CustomValue = (_currentIndex + 1) * 10.5 },
                new() { CustomText = $"Bubble {_currentIndex + 2}", CustomValue = (_currentIndex + 2) * 10.5 }
            };

            Logger?.LogInformation($"Appending 2 bubble data points by pointer starting at index {_currentIndex + 1}");
            await _xyzDataSeriesBubble1Ref.AppendRangeByPointer(newXValues, newYValues, newZValues, newMetadata);

            _currentIndex += 2;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to bubble series by pointer");
        }
    }
}
