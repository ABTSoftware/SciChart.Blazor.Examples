using Microsoft.AspNetCore.Components;
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
            yData[i] = Math.Sin(i) * 5 + 10;      // Lower band
            y1Data[i] = Math.Sin(i) * 5 + 20;     // Upper band
            yData2[i] = Math.Cos(i * 0.75) * 4 + 12;    // Second lower band
            y1Data2[i] = Math.Cos(i * 0.75) * 4 + 22;   // Second upper band

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
                newYValues[i] = Math.Sin(index) * 5 + 10;
                newY1Values[i] = Math.Sin(index) * 5 + 20;
                newY2Values[i] = Math.Cos(index * 0.75) * 4 + 12;
                newY1_2Values[i] = Math.Cos(index * 0.75) * 4 + 22;
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
                newYValues[i] = Math.Sin(index) * 5 + 10;
                newY1Values[i] = Math.Sin(index) * 5 + 20;
                newY2Values[i] = Math.Cos(index * 0.75) * 4 + 12;
                newY1_2Values[i] = Math.Cos(index * 0.75) * 4 + 22;
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
