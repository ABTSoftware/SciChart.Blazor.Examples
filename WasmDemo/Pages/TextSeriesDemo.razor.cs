using Microsoft.AspNetCore.Components;
using SciChart.Blazor.Components;

namespace WasmDemo.Pages;

public partial class TextSeriesDemo : ComponentBase
{
    private XyTextDataSeries? _xyTextDataSeriesText1Ref;

    private double[] xData = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    private double[] yData = [2.5, 3.8, 2.1, 4.5, 3.2, 5.1, 4.8, 3.5, 5.5, 4.2];
    private string[] textData = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J"];

    private int _currentIndex = 10;
    private readonly string[] _labels = ["K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"];
    private int _labelIndex = 0;

    private async Task AppendData()
    {
        if (_xyTextDataSeriesText1Ref == null) return;

        _currentIndex++;
        double newX = 4;
        double newY = 4;
        string newText = _labels[_labelIndex % _labels.Length];
        _labelIndex++;

        await _xyTextDataSeriesText1Ref.AppendRange([newX], [newY], [newText]);
    }

    private async Task UpdateData()
    {
        if (_xyTextDataSeriesText1Ref == null) return;
        await _xyTextDataSeriesText1Ref.UpdateXyText(0, 1, 4, "Z");
    }

    private async Task InsertData()
    {
        if (_xyTextDataSeriesText1Ref == null) return;
        await _xyTextDataSeriesText1Ref.InsertRange(0, [0.3, 0.6], [3, 4], ["X", "Y"]);
    }

    private async Task RemoveData()
    {
        if (_xyTextDataSeriesText1Ref == null) return;
        await _xyTextDataSeriesText1Ref.RemoveRange(0, 2);
    }
}
