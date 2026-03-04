using Microsoft.AspNetCore.Components;
using SciChart.Blazor.Components;

namespace WasmDemo.Pages;
public partial class AnnotationsDemo : ComponentBase
{
    [Inject]
    private ILogger<AnnotationsDemo> Logger { get; set; } = null!;

    private SciChartSurface _sciChartRef;

    private readonly string customSvg = @"<svg width=""100"" height=""100"" xmlns=""http://www.w3.org/2000/svg"">
        <circle cx=""50"" cy=""50"" r=""50"" fill=""#FF6B6B"" stroke=""#C92A2A"" stroke-width=""0""/>
        <text x=""50"" y=""55"" font-size=""20"" fill=""white"" text-anchor=""middle"" font-weight=""bold"">SVG</text>
    </svg>";

    private int _annotationCounter = 0;

    private string _lineColor = "#0000FF";
    private string _lineArrowColor = "#FF0000";
    private string _nativeTextColor = "#008000";
    private string _arcColor = "#800080";
    private string _axisMarkerColor = "#FF5722";
    private string _boxColor = "#2196F344";
    private string _hLineColor = "#9C27B0";
    private string _vLineColor = "#FF9800";
    private string _textColor = "#FFFFFF";

    private List<LineAnnotation> _lineAnnotations = new();
    private List<LineArrowAnnotation> _lineArrowAnnotations = new();
    private List<NativeTextAnnotation> _nativeTextAnnotations = new();
    private List<ArcAnnotation> _arcAnnotations = new();
    private List<AxisMarkerAnnotation> _axisMarkerAnnotations = new();
    private List<BoxAnnotation> _boxAnnotations = new();
    private List<CustomAnnotation> _customAnnotations = new();
    private List<HorizontalLineAnnotation> _horizontalLineAnnotations = new();
    private List<VerticalLineAnnotation> _verticalLineAnnotations = new();
    private List<TextAnnotation> _textAnnotations = new();

    protected override void OnInitialized()
    {
        Logger.LogInformation("Annotations initialized");

        _lineAnnotations.Add(new LineAnnotation { Id = "line_0", X1 = 1.5, Y1 = 1, X2 = 4.4, Y2 = 3, StrokeThickness = 8, Stroke = "blue", IsEditable = true });
        _lineArrowAnnotations.Add(new LineArrowAnnotation { Id = "arrow1", X1 = 2, Y1 = 2, X2 = 5, Y2 = 4, StrokeThickness = 4, Stroke = "red", IsEditable = true, ArrowHeadPosition = EArrowHeadPosition.End });
        _nativeTextAnnotations.Add(new NativeTextAnnotation { Id = "text1", X1 = 7, Y1 = 3, Text = "Hello SciChart!", FontSize = 20, TextColor = "green", Background = "#FFFF0088", IsEditable = true, HorizontalAnchorPoint = EHorizontalAnchorPoint.Center, VerticalAnchorPoint = EVerticalAnchorPoint.Center });
        _arcAnnotations.Add(new ArcAnnotation { Id = "arc1", X1 = 1, Y1 = 5, X2 = 4, Y2 = 5, Stroke = "purple", StrokeThickness = 3, Fill = "#8000FF88", InnerRadius = 0, Height = 3, IsEditable = true });
        _axisMarkerAnnotations.Add(new AxisMarkerAnnotation { Id = "axisMarker1", Y1 = 4.5, FontSize = 16, FontWeight = "bold", FontStyle = "italic", Color = "white", Padding = new Thickness { left = 5, right = 5, top = 3, bottom = 3 }, BackgroundColor = "#FF5722", FormattedValue = "Custom Label", Image = "/CustomMarkerImage.png", ImageWidth = 32, ImageHeight = 32, IsEditable = true });
        _boxAnnotations.Add(new BoxAnnotation { Id = "box1", X1 = 5, Y1 = 6, X2 = 7, Y2 = 8, Stroke = "#2196F3", StrokeThickness = 6, Fill = "#2196F344", IsEditable = true });
        _customAnnotations.Add(new CustomAnnotation { Id = "custom1", X1 = 8, Y1 = 8, XCoordShift = 0, YCoordShift = 0, VerticalAnchorPoint = EVerticalAnchorPoint.Top, HorizontalAnchorPoint = EHorizontalAnchorPoint.Left, SvgString = customSvg, IsEditable = true });
        _horizontalLineAnnotations.Add(new HorizontalLineAnnotation { Id = "hline1", Y1 = 5.5, Stroke = "#9C27B0", StrokeThickness = 2, StrokeDashArray = new double[] { 5, 5 }, ShowLabel = true, AxisLabelStroke = "#9C27B0", AxisLabelFill = "#9C27B088", AxisFontSize = 14, LabelPlacement = ELabelPlacement.Axis, LabelValue = "Horizontal Line", DragOnLine = true, DragOnLabel = true, HorizontalAlignment = EHorizontalAlignment.Right, VerticalAlignment = EVerticalAlignment.Center, IsEditable = true });
        _verticalLineAnnotations.Add(new VerticalLineAnnotation { Id = "vline1", X1 = 4.5, Stroke = "#FF9800", StrokeThickness = 3, StrokeDashArray = new double[] { 10, 5 }, ShowLabel = true, AxisLabelStroke = "#FF9800", AxisLabelFill = "#FF980088", AxisFontSize = 14, LabelPlacement = ELabelPlacement.Axis, LabelValue = "Vertical Line", DragOnLine = true, DragOnLabel = true, HorizontalAlignment = EHorizontalAlignment.Center, VerticalAlignment = EVerticalAlignment.Bottom, IsEditable = true });
        _textAnnotations.Add(new TextAnnotation { Id = "text2", X1 = 0.5, Y1 = 9.5, Text = "SVG Text Annotation", TextColor = "#FFFFFF", FontSize = 16, FontWeight = "bold", Background = "#00BCD4", Padding = new Thickness { left = 10, right = 10, top = 5, bottom = 5 }, XCoordShift = 0, YCoordShift = 0, VerticalAnchorPoint = EVerticalAnchorPoint.Top, HorizontalAnchorPoint = EHorizontalAnchorPoint.Left, IsEditable = true });
    }

    private void HandleDragStarted() => Logger.LogInformation("LineAnnotation: Drag started");
    private void HandleDragEnded() => Logger.LogInformation("LineAnnotation: Drag ended");
    private void HandleDragDelta() => Logger.LogInformation("LineAnnotation: Drag delta");
    private void HandleClicked() { }
    private void HandleHovered() => Logger.LogInformation("LineAnnotation: Hovered");

    private string NextId(string prefix) => $"{prefix}_{++_annotationCounter}";

    private void RemoveFromChart(string? id)
    {
        var ann = _sciChartRef.Annotations.FirstOrDefault(a => a.Id == id);
        if (ann != null) _sciChartRef.Annotations.Remove(ann);
    }

    private void AddLineAnnotation()
    {
        _lineAnnotations.Add(new LineAnnotation { Id = NextId("line"), X1 = 0, Y1 = 0, X2 = 3, Y2 = 3, Stroke = _lineColor, StrokeThickness = 3 });
        _sciChartRef.RequestUpdate();
    }

    private void RemoveLineAnnotation()
    {
        var last = _lineAnnotations.LastOrDefault();
        if (last != null) { RemoveFromChart(last.Id); _lineAnnotations.Remove(last); _sciChartRef.RequestUpdate(); }
    }

    private void UpdateLineAnnotationColor()
    {
        var last = _lineAnnotations.LastOrDefault();
        Logger.LogInformation("UpdateLineAnnotationColor");
        Logger.LogInformation(last?.Id);
        if (last != null) { last.Stroke = _lineColor; _sciChartRef.RequestUpdate(); }
    }

    private void AddLineArrowAnnotation()
    {
        _lineArrowAnnotations.Add(new LineArrowAnnotation { Id = NextId("arrow"), X1 = 1, Y1 = 1, X2 = 4, Y2 = 3, Stroke = _lineArrowColor, StrokeThickness = 3, ArrowHeadPosition = EArrowHeadPosition.End });
        _sciChartRef.RequestUpdate();
    }

    private void RemoveLineArrowAnnotation()
    {
        var last = _lineArrowAnnotations.LastOrDefault();
        if (last != null) { RemoveFromChart(last.Id); _lineArrowAnnotations.Remove(last); _sciChartRef.RequestUpdate(); }
    }

    private void UpdateLineArrowAnnotationColor()
    {
        var last = _lineArrowAnnotations.LastOrDefault();
        if (last != null) { last.Stroke = _lineArrowColor; _sciChartRef.RequestUpdate(); }
    }

    private void AddNativeTextAnnotation()
    {
        _nativeTextAnnotations.Add(new NativeTextAnnotation { Id = NextId("ntext"), X1 = 5, Y1 = 5, Text = "Native Text", FontSize = 16, TextColor = _nativeTextColor });
        _sciChartRef.RequestUpdate();
    }

    private void RemoveNativeTextAnnotation()
    {
        var last = _nativeTextAnnotations.LastOrDefault();
        if (last != null) { RemoveFromChart(last.Id); _nativeTextAnnotations.Remove(last); _sciChartRef.RequestUpdate(); }
    }

    private void UpdateNativeTextAnnotationColor()
    {
        var last = _nativeTextAnnotations.LastOrDefault();
        if (last != null) { last.TextColor = _nativeTextColor; _sciChartRef.RequestUpdate(); }
    }

    private void AddArcAnnotation()
    {
        _arcAnnotations.Add(new ArcAnnotation { Id = NextId("arc"), X1 = 2, Y1 = 6, X2 = 5, Y2 = 6, Stroke = _arcColor, StrokeThickness = 2, Height = 2, InnerRadius = 0 });
        _sciChartRef.RequestUpdate();
    }

    private void RemoveArcAnnotation()
    {
        var last = _arcAnnotations.LastOrDefault();
        if (last != null) { RemoveFromChart(last.Id); _arcAnnotations.Remove(last); _sciChartRef.RequestUpdate(); }
    }

    private void UpdateArcAnnotationColor()
    {
        var last = _arcAnnotations.LastOrDefault();
        if (last != null) { last.Stroke = _arcColor; _sciChartRef.RequestUpdate(); }
    }

    private void AddAxisMarkerAnnotation()
    {
        _axisMarkerAnnotations.Add(new AxisMarkerAnnotation { Id = NextId("axm"), Y1 = 3, BackgroundColor = _axisMarkerColor, FormattedValue = "Marker", Color = "white" });
        _sciChartRef.RequestUpdate();
    }

    private void RemoveAxisMarkerAnnotation()
    {
        var last = _axisMarkerAnnotations.LastOrDefault();
        if (last != null) { RemoveFromChart(last.Id); _axisMarkerAnnotations.Remove(last); _sciChartRef.RequestUpdate(); }
    }

    private void UpdateAxisMarkerAnnotationColor()
    {
        var last = _axisMarkerAnnotations.LastOrDefault();
        if (last != null) { last.BackgroundColor = _axisMarkerColor; _sciChartRef.RequestUpdate(); }
    }

    private void AddBoxAnnotation()
    {
        _boxAnnotations.Add(new BoxAnnotation { Id = NextId("box"), X1 = 6, Y1 = 2, X2 = 8, Y2 = 4, Fill = _boxColor, Stroke = "#2196F3", StrokeThickness = 2 });
        _sciChartRef.RequestUpdate();
    }

    private void RemoveBoxAnnotation()
    {
        var last = _boxAnnotations.LastOrDefault();
        if (last != null) { RemoveFromChart(last.Id); _boxAnnotations.Remove(last); _sciChartRef.RequestUpdate(); }
    }

    private void UpdateBoxAnnotationColor()
    {
        var last = _boxAnnotations.LastOrDefault();
        if (last != null) { last.Fill = _boxColor; _sciChartRef.RequestUpdate(); }
    }

    private void AddCustomAnnotation()
    {
        _customAnnotations.Add(new CustomAnnotation { Id = NextId("custom"), X1 = 7, Y1 = 6, SvgString = customSvg, VerticalAnchorPoint = EVerticalAnchorPoint.Top, HorizontalAnchorPoint = EHorizontalAnchorPoint.Left });
        _sciChartRef.RequestUpdate();
    }

    private void RemoveCustomAnnotation()
    {
        var last = _customAnnotations.LastOrDefault();
        if (last != null) { RemoveFromChart(last.Id); _customAnnotations.Remove(last); _sciChartRef.RequestUpdate(); }
    }

    private void AddHorizontalLineAnnotation()
    {
        _horizontalLineAnnotations.Add(new HorizontalLineAnnotation { Id = NextId("hline"), Y1 = 7, Stroke = _hLineColor, StrokeThickness = 2, ShowLabel = true, LabelValue = "H Line" });
        _sciChartRef.RequestUpdate();
    }

    private void RemoveHorizontalLineAnnotation()
    {
        var last = _horizontalLineAnnotations.LastOrDefault();
        if (last != null) { RemoveFromChart(last.Id); _horizontalLineAnnotations.Remove(last); _sciChartRef.RequestUpdate(); }
    }

    private void UpdateHorizontalLineAnnotationColor()
    {
        var last = _horizontalLineAnnotations.LastOrDefault();
        if (last != null) { last.Stroke = _hLineColor; _sciChartRef.RequestUpdate(); }
    }

    private void AddVerticalLineAnnotation()
    {
        _verticalLineAnnotations.Add(new VerticalLineAnnotation { Id = NextId("vline"), X1 = 6, Stroke = _vLineColor, StrokeThickness = 2, ShowLabel = true, LabelValue = "V Line" });
        _sciChartRef.RequestUpdate();
    }

    private void RemoveVerticalLineAnnotation()
    {
        var last = _verticalLineAnnotations.LastOrDefault();
        if (last != null) { RemoveFromChart(last.Id); _verticalLineAnnotations.Remove(last); _sciChartRef.RequestUpdate(); }
    }

    private void UpdateVerticalLineAnnotationColor()
    {
        var last = _verticalLineAnnotations.LastOrDefault();
        if (last != null) { last.Stroke = _vLineColor; _sciChartRef.RequestUpdate(); }
    }

    private void AddTextAnnotation()
    {
        _textAnnotations.Add(new TextAnnotation { Id = NextId("text"), X1 = 0.5, Y1 = 7, Text = "SVG Text", TextColor = _textColor, FontSize = 14 });
        _sciChartRef.RequestUpdate();
    }

    private void RemoveTextAnnotation()
    {
        var last = _textAnnotations.LastOrDefault();
        if (last != null) { RemoveFromChart(last.Id); _textAnnotations.Remove(last); _sciChartRef.RequestUpdate(); }
    }

    private void UpdateTextAnnotationColor()
    {
        var last = _textAnnotations.LastOrDefault();
        if (last != null) { last.TextColor = _textColor; _sciChartRef.RequestUpdate(); }
    }
}
