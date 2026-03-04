using Microsoft.AspNetCore.Components;
using SciChart.Blazor.Components;
using System.Text.Json;

namespace WasmDemo.Pages;

public partial class LineSeriesDemo2 : ComponentBase
{
    private SciChartSurface _sciChartRef;
    private List<SeriesData> _seriesList = new List<SeriesData>();
    private string[] _colors = new[] { "red", "blue", "green", "orange", "purple", "brown", "pink", "cyan" };

    protected override void OnInitialized()
    {
        _seriesList.Add(new SeriesData
        {
            Id = "series_0",
            Name = "Sine Wave 1",
            Color = _colors[0],
            XData = Array.Empty<double>(),
            YData = Array.Empty<double>(),
            Phase = 0
        });
    }

    private void AddSeries()
    {
        var newPhase = _seriesList.Count == 0 ? 0 : _seriesList[_seriesList.Count - 1].Phase + 0.5;
        var colorIndex = _seriesList.Count % _colors.Length;

        _seriesList.Add(new SeriesData
        {
            Id = $"series_{_seriesList.Count}",
            Name = $"Sine Wave {_seriesList.Count + 1}",
            Color = _colors[colorIndex],
            XData = Array.Empty<double>(),
            YData = Array.Empty<double>(),
            Phase = newPhase
        });
        _sciChartRef.RequestUpdate();
    }

    private void RemoveSeries()
    {
        if (_seriesList.Count > 0)
        {
            _seriesList.RemoveAt(_seriesList.Count - 1);
        }
        _sciChartRef.RequestUpdate();
    }

    private async Task AppendDataToLastSeries()
    {
        if (_seriesList.Count == 0) return;

        var lastSeries = _seriesList[_seriesList.Count - 1];
        var currentCount = lastSeries.XData.Length;
        var startIndex = currentCount;
        const double multiplier = 0.1;

        var newXValues = new double[10];
        var newYValues = new double[10];

        for (int i = 0; i < 10; i++)
        {
            double x = (startIndex + i) * multiplier;
            newXValues[i] = x;
            newYValues[i] = Math.Sin(x + lastSeries.Phase);
        }

        await lastSeries.XyDataSeriesRef!.AppendRange(newXValues, newYValues);

        var updatedXData = new double[currentCount + 10];
        var updatedYData = new double[currentCount + 10];
        Array.Copy(lastSeries.XData, updatedXData, currentCount);
        Array.Copy(lastSeries.YData, updatedYData, currentCount);
        Array.Copy(newXValues, 0, updatedXData, currentCount, 10);
        Array.Copy(newYValues, 0, updatedYData, currentCount, 10);

        lastSeries.XData = updatedXData;
        lastSeries.YData = updatedYData;
    }

    private void HandleSurfaceRendered(SciChartSurfaceDefinition chartState)
    {
        var json = JsonSerializer.Serialize(chartState);
        Console.WriteLine(json);
    }

    private class SeriesData
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public double[] XData { get; set; } = Array.Empty<double>();
        public double[] YData { get; set; } = Array.Empty<double>();
        public double Phase { get; set; }
        public XyDataSeries? XyDataSeriesRef { get; set; }
    }
}
