using Microsoft.AspNetCore.Components;
using SciChart.Blazor.Components;
using System.Text.Json;

namespace WasmDemo.Pages;

public enum EHitTestMode { HitTest, HitTestDataPoint }

public partial class LineSeriesHitTestDemo : ComponentBase
{
    private SciChartSurface _sciChartRef;
    private XyDataSeries? _xyDataSeriesHitScatterRef;
    private LineAnnotation _lineAnnotation = new LineAnnotation
    {
        IsHidden = true,
        Stroke = "white",
        X1 = 0,
        Y1 = 0,
        X2 = 0,
        Y2 = 0,
    };
    private readonly string _svgCirleRed = @"<svg width=""20"" height=""20"" xmlns=""http://www.w3.org/2000/svg"">
        <circle cx=""10"" cy=""10"" r=""10"" fill=""#FF1493"" stroke=""#FF1493"" stroke-width=""0""/>
    </svg>";
    private readonly string _svgCirleBlue = @"<svg width=""20"" height=""20"" xmlns=""http://www.w3.org/2000/svg"">
        <circle cx=""10"" cy=""10"" r=""10"" fill=""#00CED1"" stroke=""#00CED1"" stroke-width=""0""/>
    </svg>";
    private CustomAnnotation _customAnnotationRed = new CustomAnnotation
    {
        IsHidden = true,
        X1 = 0,
        Y1 = 0
    };
    private CustomAnnotation _customAnnotationBlue = new CustomAnnotation
    {
        IsHidden = true,
        X1 = 3,
        Y1 = 3
    };


    private readonly double[] _xValues = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
    private readonly double[] _yValues = [0, 1, 5, 1, 20, 5, 1, 8, 9, 3];

    private EHitTestMode _hitTestMode = EHitTestMode.HitTest;
    private bool _hasHitPoint = false;
    private TextAnnotation? _hitTextAnnotation;

    private void ToggleHitTestMode()
    {
        ClearVisualizations();
        _hitTestMode = _hitTestMode == EHitTestMode.HitTest ? EHitTestMode.HitTestDataPoint : EHitTestMode.HitTest;
    }

    private async Task UpdateTextAndPoint(HitTestInfo hitTest)
    {
        double x = hitTest.HitTestPointValues.X;
        double y = hitTest.HitTestPointValues.Y;
        if (_hasHitPoint)
        {
            await _sciChartRef.JSUpdateXyOnXyDataSeries("hit_scatter", 0, x, y);
        }
        else
        {
            await _xyDataSeriesHitScatterRef!.AppendRange([x], [y]);
            _hasHitPoint = true;
        }

        if (_hitTextAnnotation == null)
        {
            _hitTextAnnotation = new TextAnnotation
            {
                FontSize = 20,
                HorizontalAnchorPoint = EHorizontalAnchorPoint.Left,
                VerticalAnchorPoint = EVerticalAnchorPoint.Center,
                TextColor = "#FFFFFF"
            };
            _sciChartRef.Annotations.Add(_hitTextAnnotation);
        }

        _hitTextAnnotation.X1 = x + 0.2;
        _hitTextAnnotation.Y1 = y;
        _hitTextAnnotation.Text = hitTest.IsHit ? "Hit!" : "miss...";
    }

    private async void HandleHitTest(HitTestInfo hitTest)
    {
        //var json = JsonSerializer.Serialize(hitTest);
        //Console.WriteLine(json);
        if (_hitTestMode != EHitTestMode.HitTest) return;
        if (hitTest.IsEmpty) return;

        await UpdateTextAndPoint(hitTest);

        bool isInBounds = hitTest.IsWithinDataBounds;
        var fill = hitTest.IsHit ? "#00CED1" : "#FF1493";
        _lineAnnotation.IsHidden = false;
        _lineAnnotation.Stroke = fill;
        _lineAnnotation.X1 = hitTest.XValue;
        _lineAnnotation.Y1 = hitTest.YValue;
        _lineAnnotation.X2 = isInBounds ? hitTest.Point2XValue : hitTest.XValue;
        _lineAnnotation.Y2 = isInBounds ? hitTest.Point2YValue : hitTest.YValue;

        _sciChartRef.RequestUpdate();
    }

    private async void HandleHitTestDataPoint(HitTestInfo hitTest)
    {
        if (_hitTestMode != EHitTestMode.HitTestDataPoint) return;
        if (hitTest.IsEmpty) return;

        await UpdateTextAndPoint(hitTest);

        _customAnnotationBlue.IsHidden = true;
        _customAnnotationRed.IsHidden = true;
        CustomAnnotation currentAnnotation = hitTest.IsHit ? _customAnnotationBlue : _customAnnotationRed;

        currentAnnotation.IsHidden = false;
        currentAnnotation.X1 = hitTest.XValue;
        currentAnnotation.Y1 = hitTest.YValue;

        _sciChartRef.RequestUpdate();
    }

    private async void ClearVisualizations()
    {
        await _xyDataSeriesHitScatterRef!.Clear();
        _hasHitPoint = false;

        _lineAnnotation.IsHidden = true;

        if (_hitTextAnnotation != null)
        {
            _sciChartRef.Annotations.Remove(_hitTextAnnotation);
            _hitTextAnnotation = null;
        }

        _customAnnotationBlue.IsHidden = true;
        _customAnnotationRed.IsHidden = true;

        _sciChartRef.RequestUpdate();
    }

    private void HandleSurfaceRendered(SciChartSurfaceDefinition chartState)
    {
        var json = JsonSerializer.Serialize(chartState);
        Console.WriteLine(json);
    }
}
