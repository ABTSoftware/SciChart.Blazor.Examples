using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SciChartBlazor.Components;

namespace WasmDemo.Pages;

public partial class NonUniformHeatmapSeriesDemo : ComponentBase
{
    private SciChartSurface? _sciChartRef;

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

    // Non-uniform X cell offsets - cells get progressively wider
    private double[] xCellOffsets = new double[] { 0, 1, 2.5, 4.5, 7, 10, 14, 19, 25, 32, 40 };

    // Non-uniform Y cell offsets - cells get progressively taller
    private double[] yCellOffsets = new double[] { 0, 1, 2.5, 4.5, 7, 10, 14, 19, 25, 32, 40 };

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
    private ILogger<NonUniformHeatmapSeriesDemo>? Logger { get; set; }
}
