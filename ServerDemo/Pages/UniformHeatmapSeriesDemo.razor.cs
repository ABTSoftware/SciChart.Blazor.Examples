using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SciChart.Blazor.Components;

namespace ServerDemo.Pages;

public partial class UniformHeatmapSeriesDemo : ComponentBase
{
    private SciChartSurface? _sciChartRef;
    private UniformHeatmapDataSeries? _dataSeriesRef;

    // Sample heatmap data - 10x10 grid with values from 0 to 100
    private double[][] zValues = new double[][]
    {
        new double[] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90 },
        new double[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 },
        new double[] { 20, 30, 40, 50, 60, 70, 80, 90, 80, 70 },
        new double[] { 30, 40, 50, 60, 70, 80, 90, 80, 70, 60 },
        new double[] { 40, 50, 60, 70, 80, 90, 80, 70, 60, 50 },
        new double[] { 50, 60, 70, 80, 90, 80, 70, 60, 50, 40 },
        new double[] { 60, 70, 80, 90, 80, 70, 60, 50, 40, 30 },
        new double[] { 70, 80, 90, 80, 70, 60, 50, 40, 30, 20 },
        new double[] { 80, 90, 80, 70, 60, 50, 40, 30, 20, 10 },
        new double[] { 90, 80, 70, 60, 50, 40, 30, 20, 10, 0 }
    };

    // Color map for the heatmap - blue to red gradient
    private HeatmapColorMap colorMap = new HeatmapColorMap
    {
        Minimum = 0,
        Maximum = 100,
        GradientStops = new[]
        {
            new TGradientStop { Offset = 0.0, Color = "#0000FF" },  // Blue (cold)
            new TGradientStop { Offset = 0.5, Color = "#00FF00" },  // Green (medium)
            new TGradientStop { Offset = 1.0, Color = "#FF0000" }   // Red (hot)
        }
    };

    [Inject]
    private ILogger<UniformHeatmapSeriesDemo>? Logger { get; set; }

    private async Task UpdateAll()
    {
        if (_dataSeriesRef is null) return;
        var newZValues = new double[10][];
        for (int y = 0; y < 10; y++)
        {
            newZValues[y] = new double[10];
            for (int x = 0; x < 10; x++)
            {
                var v = (Math.Sin(x * 0.6) + Math.Cos(y * 0.6)) * 25 + 50;
                newZValues[y][x] = Math.Round(v);
            }
        }
        await _dataSeriesRef.SetZValues(newZValues);
    }

    private async Task UpdateOne()
    {
        if (_dataSeriesRef is null) return;
        await _dataSeriesRef.SetZValue(0, 0, 99);
    }
}
