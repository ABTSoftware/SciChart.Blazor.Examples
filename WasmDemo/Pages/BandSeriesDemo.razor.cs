using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SciChart.Blazor.Components;

namespace WasmDemo.Pages;

public partial class BandSeriesDemo : ComponentBase
{
    [Inject]
    private ILogger<BandSeriesDemo> Logger { get; set; } = null!;

    private XyyDataSeries? _xyyDataSeriesBand1Ref;
    private XyyDataSeries? _xyyDataSeriesSplineBand1Ref;

    // Track the current index for appending new data
    private int _currentIndex = 10; // Initial data has indices 0-49

    private double[] xData;
    private double[] yData;
    private double[] y1Data;
    private double[] yData2;
    private double[] y1Data2;
    private PointMetadata[] metadata;

    protected override void OnInitialized()
    {
        Logger.LogInformation("BandSeriesChart initialized");

        // Generate sample data for band series
        int dataCount = 10;
        xData = new double[dataCount];
        yData = new double[dataCount];
        y1Data = new double[dataCount];
        yData2 = new double[dataCount];
        y1Data2 = new double[dataCount];
        metadata = new PointMetadata[dataCount];

        for (int i = 0; i < dataCount; i++)
        {
            xData[i] = i;
            yData[i] = 18 + i * 1.2 + Math.Sin(i) * 3;       // Regular band Y — rising wave, crosses above Y1 after index 5
            y1Data[i] = 30 - i * 1.2 + Math.Sin(i) * 3;      // Regular band Y1 — falling wave (Y < Y1 at 0, Y > Y1 by index 5)
            yData2[i] = 2 + i * 1.2 + Math.Sin(i * 0.9) * 3;    // Spline band Y — rising wave, crosses above Y1 after index 5
            y1Data2[i] = 14 - i * 1.2 + Math.Sin(i * 0.9) * 3;  // Spline band Y1 — falling wave

            // Create metadata for each point
            metadata[i] = new PointMetadata
            {
                IsSelected = i % 3 == 0,  // Every 3rd point is selected
                CustomValue = i * 10.5,   // Sample numeric metadata
                CustomText = $"Band {i}"  // Sample string metadata
            };
        }
    }

    private async Task AppendData()
    {
        if (_xyyDataSeriesBand1Ref == null)
        {
            Logger?.LogInformation("XyyDataSeries reference is null. Cannot append data.");
            return;
        }

        try
        {
            // Generate 5 new data points
            var newXValues = new double[5];
            var newYValues = new double[5];
            var newY1Values = new double[5];
            var newY2Values = new double[5];
            var newY1_2Values = new double[5];

            for (int i = 0; i < 5; i++)
            {
                int index = _currentIndex + i;
                newXValues[i] = index;
                newYValues[i] = 18 + index * 1.2 + Math.Sin(index) * 3;
                newY1Values[i] = 30 - index * 1.2 + Math.Sin(index) * 3;
                newY2Values[i] = 2 + index * 1.2 + Math.Sin(index * 0.9) * 3;
                newY1_2Values[i] = 14 - index * 1.2 + Math.Sin(index * 0.9) * 3;
            }

            Logger?.LogInformation($"Appending 5 data points starting at index {_currentIndex}");

            await _xyyDataSeriesBand1Ref.AppendRange(newXValues, newYValues, newY1Values);
            await _xyyDataSeriesSplineBand1Ref!.AppendRange(newXValues, newY2Values, newY1_2Values);

            _currentIndex += 5;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to band series");
        }
    }

    private async Task UpdateData()
    {
        if (_xyyDataSeriesBand1Ref == null) return;
        try
        {
            await _xyyDataSeriesBand1Ref.UpdateXyy1(0, 0, 15, 25);
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to update band series");
        }
    }

    private async Task InsertData()
    {
        if (_xyyDataSeriesBand1Ref == null) return;
        try
        {
            var newXValues = new double[] { 0.3, 0.6 };
            var newYValues = new double[] { 12, 14 };
            var newY1Values = new double[] { 22, 24 };
            var newMetadata = new PointMetadata[]
            {
                new() { CustomText = "Inserted A" },
                new() { CustomText = "Inserted B" }
            };
            await _xyyDataSeriesBand1Ref.InsertRange(1, newXValues, newYValues, newY1Values, newMetadata);
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to insert data into band series");
        }
    }

    private async Task InsertDataByPointer()
    {
        if (_xyyDataSeriesBand1Ref == null) return;
        try
        {
            var newXValues = new double[] { 0.3, 0.6 };
            var newYValues = new double[] { 12, 14 };
            var newY1Values = new double[] { 22, 24 };
            var newMetadata = new PointMetadata[]
            {
                new() { CustomText = "Inserted A" },
                new() { CustomText = "Inserted B" }
            };
            await _xyyDataSeriesBand1Ref.InsertRangeByPointer(1, newXValues, newYValues, newY1Values, newMetadata);
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to insert data into band series by pointer");
        }
    }

    private async Task RemoveData()
    {
        if (_xyyDataSeriesBand1Ref == null) return;
        try
        {
            await _xyyDataSeriesBand1Ref.RemoveRange(1, 2);
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to remove data from band series");
        }
    }

    private async Task AppendDataByPointer()
    {
        if (_xyyDataSeriesBand1Ref == null)
        {
            Logger?.LogInformation("XyyDataSeries reference is null. Cannot append data.");
            return;
        }

        try
        {
            // Generate 5 new data points
            var newXValues = new double[5];
            var newYValues = new double[5];
            var newY1Values = new double[5];
            var newY2Values = new double[5];
            var newY1_2Values = new double[5];

            for (int i = 0; i < 5; i++)
            {
                int index = _currentIndex + i;
                newXValues[i] = index;
                newYValues[i] = 18 + index * 1.2 + Math.Sin(index) * 3;
                newY1Values[i] = 30 - index * 1.2 + Math.Sin(index) * 3;
                newY2Values[i] = 2 + index * 1.2 + Math.Sin(index * 0.9) * 3;
                newY1_2Values[i] = 14 - index * 1.2 + Math.Sin(index * 0.9) * 3;
            }

            Logger?.LogInformation($"Appending 5 data points by pointer starting at index {_currentIndex}");

            await _xyyDataSeriesBand1Ref.AppendRangeByPointer(newXValues, newYValues, newY1Values);
            await _xyyDataSeriesSplineBand1Ref!.AppendRangeByPointer(newXValues, newY2Values, newY1_2Values);

            _currentIndex += 5;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to band series by pointer");
        }
    }
}
