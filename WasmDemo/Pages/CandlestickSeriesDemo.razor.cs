using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SciChart.Blazor.Components;

namespace WasmDemo.Pages;

public partial class CandlestickSeriesDemo : ComponentBase
{
    [Inject]
    private ILogger<CandlestickSeriesDemo> Logger { get; set; } = null!;

    private SciChartSurface _sciChartRef;

    private double[] xData;
    private double[] openData;
    private double[] highData;
    private double[] lowData;
    private double[] closeData;

    protected override void OnInitialized()
    {
        Logger.LogInformation("CandlestickSeriesChart initialized");

        // Generate sample OHLC data
        int dataCount = 50;
        xData = new double[dataCount];
        openData = new double[dataCount];
        highData = new double[dataCount];
        lowData = new double[dataCount];
        closeData = new double[dataCount];

        Random random = new Random(42);
        double price = 100.0;

        for (int i = 0; i < dataCount; i++)
        {
            xData[i] = i;

            double open = price;
            double change = (random.NextDouble() - 0.5) * 10;
            double close = open + change;
            double high = Math.Max(open, close) + random.NextDouble() * 5;
            double low = Math.Min(open, close) - random.NextDouble() * 5;

            openData[i] = open;
            highData[i] = high;
            lowData[i] = low;
            closeData[i] = close;

            price = close;
        }
    }
}
