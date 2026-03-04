using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SciChart.Blazor.Components;

namespace WasmDemo.Pages;

public partial class OhlcSeriesDemo : ComponentBase
{
    [Inject]
    private ILogger<OhlcSeriesDemo> Logger { get; set; } = null!;

    private SciChartSurface _sciChartRef;

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
}
