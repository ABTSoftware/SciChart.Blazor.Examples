using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SciChart.Blazor.Components;

namespace ServerDemo.Pages;

public partial class VerticallyStackedColumnDemo : ComponentBase
{
    [Inject]
    private ILogger<VerticallyStackedColumnDemo> Logger { get; set; } = null!;

    private SciChartSurface? _sciChartRef;
    private XyDataSeries? _xyDataSeriesSeries1Ref;
    private XyDataSeries? _xyDataSeriesSeries2Ref;
    private XyDataSeries? _xyDataSeriesSeries3Ref;
    private XyDataSeries? _xyDataSeriesSeries4Ref;
    private XyDataSeries? _xyDataSeriesSeries5Ref;
    private bool _isOneHundredPercent = false;

    // Track the current index for appending new data
    private int _currentIndex = 6; // Initial data has indices 0-5

    private double[] _xData = [0, 1, 2, 3, 4, 5];
    private double[] _yData1 = [10, 15, 13, 17, 14, 16];
    private double[] _yData2 = [12, 18, 15, 19, 16, 20];
    private double[] _yData3 = [8, 11, 9, 12, 10, 13];
    private double[] _yData4 = [120, 150, 135, 170, 160, 180];
    private double[] _yData5 = [80, 95, 90, 110, 100, 120];

    protected override void OnInitialized()
    {
        Logger.LogInformation("VerticallyStackedColumnChart initialized");
    }

    private void OnStackingModeChanged()
    {
        Logger?.LogInformation($"Stacking mode changed to: {(_isOneHundredPercent ? "100%" : "Standard")}");

        // Mark the chart as needing rebuild to recreate with new stacking mode
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
            var newXValues = new double[2];
            var newY1Values = new double[2];
            var newY2Values = new double[2];
            var newY3Values = new double[2];
            var newY4Values = new double[2];
            var newY5Values = new double[2];

            for (int i = 0; i < 2; i++)
            {
                int index = _currentIndex + i;
                newXValues[i] = index;
                newY1Values[i] = 10 + (index % 8) + (index * 0.5);
                newY2Values[i] = 12 + (index % 10) + (index * 0.6);
                newY3Values[i] = 8 + (index % 6) + (index * 0.4);
                newY4Values[i] = 120 + (index % 10) * 5 + (index * 4);
                newY5Values[i] = 80 + (index % 7) * 4 + (index * 3);
            }

            Logger?.LogInformation($"Appending 2 data points starting at index {_currentIndex}");

            await Task.WhenAll(
                _xyDataSeriesSeries1Ref.AppendRange(newXValues, newY1Values),
                _xyDataSeriesSeries2Ref!.AppendRange(newXValues, newY2Values),
                _xyDataSeriesSeries3Ref!.AppendRange(newXValues, newY3Values),
                _xyDataSeriesSeries4Ref!.AppendRange(newXValues, newY4Values),
                _xyDataSeriesSeries5Ref!.AppendRange(newXValues, newY5Values));

            _currentIndex += 2;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to stacked column series");
        }
    }

}
