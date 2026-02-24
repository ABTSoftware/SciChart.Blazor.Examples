using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SciChartBlazor.Components;

namespace WasmDemo.Pages;

public partial class StackedMountainDemo : ComponentBase
{
    [Inject]
    private ILogger<StackedMountainDemo> Logger { get; set; } = null!;

    private SciChartSurface? _sciChartRef;
    private XyDataSeries? _xyDataSeriesSeries1Ref;
    private XyDataSeries? _xyDataSeriesSeries2Ref;
    private XyDataSeries? _xyDataSeriesSeries3Ref;
    private bool _isOneHundredPercent = false;

    // Track the current index for appending new data
    private int _currentIndex = 10; // Initial data has indices 0-9

    private double[] _xData = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
    private double[] _yData1 = [1, 2, 3, 2, 4, 3, 5, 4, 6, 5];
    private double[] _yData2 = [2, 3, 2, 4, 3, 5, 4, 6, 5, 7];
    private double[] _yData3 = [1, 2, 2, 3, 3, 4, 4, 5, 5, 6];

    protected override void OnInitialized()
    {
        Logger.LogInformation("StackedMountainChart initialized");
    }

    private void OnStackingModeChanged()
    {
        Logger?.LogInformation($"Stacking mode changed to: {(_isOneHundredPercent ? "100%" : "Standard")}");

        // Mark the chart as needing update to recreate with new stacking mode
        if (_sciChartRef != null)
        {
            _sciChartRef.RequestRebuild();
            StateHasChanged();
        }
    }

    private async Task AppendData()
    {
        if (_xyDataSeriesSeries1Ref == null) return;

        try
        {
            var newXValues = new double[3];
            var newY1Values = new double[3];
            var newY2Values = new double[3];
            var newY3Values = new double[3];

            for (int i = 0; i < 3; i++)
            {
                int index = _currentIndex + i;
                newXValues[i] = index;
                newY1Values[i] = Math.Sin(index * 0.5) * 5 + 3;
                newY2Values[i] = Math.Sin(index * 0.3) * 4 + 4;
                newY3Values[i] = Math.Cos(index * 0.4) * 3 + 3;
            }

            Logger?.LogInformation($"Appending 3 data points starting at index {_currentIndex}");

            await _xyDataSeriesSeries1Ref.AppendRange(newXValues, newY1Values);
            await _xyDataSeriesSeries2Ref!.AppendRange(newXValues, newY2Values);
            await _xyDataSeriesSeries3Ref!.AppendRange(newXValues, newY3Values);

            _currentIndex += 3;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to stacked mountain series");
        }
    }

    private async Task AppendDataByPointer()
    {
        if (_xyDataSeriesSeries1Ref == null) return;

        try
        {
            var newXValues = new double[3];
            var newY1Values = new double[3];
            var newY2Values = new double[3];
            var newY3Values = new double[3];

            for (int i = 0; i < 3; i++)
            {
                int index = _currentIndex + i;
                newXValues[i] = index;
                newY1Values[i] = Math.Sin(index * 0.5) * 5 + 3;
                newY2Values[i] = Math.Sin(index * 0.3) * 4 + 4;
                newY3Values[i] = Math.Cos(index * 0.4) * 3 + 3;
            }

            Logger?.LogInformation($"Appending 3 data points by pointer starting at index {_currentIndex}");

            await _xyDataSeriesSeries1Ref.AppendRangeByPointer(newXValues, newY1Values);
            await _xyDataSeriesSeries2Ref!.AppendRangeByPointer(newXValues, newY2Values);
            await _xyDataSeriesSeries3Ref!.AppendRangeByPointer(newXValues, newY3Values);

            _currentIndex += 3;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to stacked mountain series by pointer");
        }
    }
}
