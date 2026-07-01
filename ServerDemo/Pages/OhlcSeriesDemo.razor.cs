using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SciChart.Blazor.Components;

namespace ServerDemo.Pages;

public partial class OhlcSeriesDemo : ComponentBase
{
    [Inject]
    private ILogger<OhlcSeriesDemo> Logger { get; set; } = null!;

    private SciChartSurface _sciChartRef;
    private OhlcDataSeries? _ohlcDataSeriesRef;

    private double[] xData;
    private double[] openData;
    private double[] highData;
    private double[] lowData;
    private double[] closeData;

    protected override void OnInitialized()
    {
        Logger.LogInformation("OhlcSeriesChart initialized");

        // Generate sample OHLC data
        int dataCount = 50;
        xData = new double[dataCount];
        openData = new double[dataCount];
        highData = new double[dataCount];
        lowData = new double[dataCount];
        closeData = new double[dataCount];

        Random random = new Random(123);
        double price = 100.0;

        for (int i = 0; i < dataCount; i++)
        {
            xData[i] = i;

            double open = price;
            double change = (random.NextDouble() - 0.5) * 8;
            double close = open + change;
            double high = Math.Max(open, close) + random.NextDouble() * 4;
            double low = Math.Min(open, close) - random.NextDouble() * 4;

            openData[i] = open;
            highData[i] = high;
            lowData[i] = low;
            closeData[i] = close;

            price = close;
        }
    }

    private async Task AppendData()
    {
        if (_ohlcDataSeriesRef == null) return;
        try
        {
            var newXValues = new double[] { 50, 51 };
            var newOpenValues = new double[] { 100, 102 };
            var newHighValues = new double[] { 108, 110 };
            var newLowValues = new double[] { 95, 97 };
            var newCloseValues = new double[] { 104, 106 };
            await _ohlcDataSeriesRef.AppendRange(newXValues, newOpenValues, newHighValues, newLowValues, newCloseValues);
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to OHLC series");
        }
    }

    private async Task UpdateData()
    {
        if (_ohlcDataSeriesRef == null) return;
        try
        {
            await _ohlcDataSeriesRef.UpdateXohlc(0, 0, 100, 112, 88, 106);
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to update OHLC series");
        }
    }

    private async Task InsertData()
    {
        if (_ohlcDataSeriesRef == null) return;
        try
        {
            var newXValues = new double[] { 0.3, 0.6 };
            var newOpenValues = new double[] { 100, 102 };
            var newHighValues = new double[] { 108, 110 };
            var newLowValues = new double[] { 95, 97 };
            var newCloseValues = new double[] { 104, 106 };
            await _ohlcDataSeriesRef.InsertRange(1, newXValues, newOpenValues, newHighValues, newLowValues, newCloseValues);
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to insert data into OHLC series");
        }
    }

    private async Task RemoveData()
    {
        if (_ohlcDataSeriesRef == null) return;
        try
        {
            await _ohlcDataSeriesRef.RemoveRange(1, 2);
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to remove data from OHLC series");
        }
    }
}
