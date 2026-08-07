using Microsoft.AspNetCore.Components;

namespace ServerDemo.Pages;

public partial class ResizeChartDemo : ComponentBase
{
    private int _containerWidth = 600;
    private int _containerHeight = 400;

    private double[] xData = new double[101];
    private double[] yData = new double[101];

    protected override void OnInitialized()
    {
        for (int i = 0; i <= 100; i++)
        {
            xData[i] = i * 0.1;
            yData[i] = Math.Sin(i * 0.1 * Math.PI);
        }
    }

    private void ResizeContainer()
    {
        _containerWidth = 900;
        _containerHeight = 300;
    }

    private void ResetContainer()
    {
        _containerWidth = 600;
        _containerHeight = 400;
    }
}
