# SciChart Blazor Change Log

## 5.2.62-beta.25

- Updated to scichart.js 5.2.62
- Added a `Style` parameter on `SciChartSurface` applied to the chart root div; set it to `width: 100%; height: 100%` to size the chart to fit its parent container instead of using an aspect ratio
- Fixed dynamically changing `WidthAspect` / `HeightAspect` / `DisableAspect` not taking effect on an already created chart
- Added the Resize Chart demo showing a chart that follows its parent container size, including resizing with the mouse

## 5.2.55-beta.24

- Updated to scichart.js 5.2.55 which includes fix for NonUniformHeatmap linear interpolation
- Fixed bug with project failed to start in Visual Studio with TypeScript support enabled

## 5.2.45-alpha.23

- Fixed the missing `AxisAlignment` property on `NumericAxis` and exposed additional axis label options (`LabelFormat`, `LabelPrecision`, `CursorLabelFormat`, `CursorLabelPrecision`, `LabelPrefix`, `LabelPostfix`, `Rotation`, `UseNativeText`, `UseSharedCache`, `LineSpacing`, `AlwaysShowFirstLabel`); the Axis demo now demonstrates axis alignment and label formatting (SCJS-2637).
- Added data mutation methods (`SetZValues`, `SetZValue`) on the Blazor uniform and non-uniform heatmap/contours data series, and updated the Uniform and Non-Uniform Heatmap demos with "Update all" / "Update one" buttons to showcase them (SCJS-2629).

## 5.2.45-alpha.22

- Exposed data mutation methods (`Update`, `InsertRange`, `InsertRangeByPointer`, `RemoveRange`) on the Blazor data series types, including `XyDataSeries`, `XyyDataSeries`, `XyzDataSeries`, `XyxyDataSeries`, `XyTextDataSeries`, `OhlcDataSeries`, `HlcDataSeries` and `BoxPlotDataSeries` (SCJS-2611).
- Updated the series demos (Line, Band, Bubble, BoxPlot, ErrorBars, LineSegment, Ohlc, Text) to showcase inserting, updating and removing data at runtime.

