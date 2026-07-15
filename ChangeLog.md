# SciChart Blazor Change Log

## 5.2.45-alpha.23

- Fixed the missing `AxisAlignment` property on `NumericAxis` and exposed additional axis label options (`LabelFormat`, `LabelPrecision`, `CursorLabelFormat`, `CursorLabelPrecision`, `LabelPrefix`, `LabelPostfix`, `Rotation`, `UseNativeText`, `UseSharedCache`, `LineSpacing`, `AlwaysShowFirstLabel`); the Axis demo now demonstrates axis alignment and label formatting (SCJS-2637).
- Added data mutation methods (`SetZValues`, `SetZValue`) on the Blazor uniform and non-uniform heatmap/contours data series, and updated the Uniform and Non-Uniform Heatmap demos with "Update all" / "Update one" buttons to showcase them (SCJS-2629).

## 5.2.45-alpha.22

- Exposed data mutation methods (`Update`, `InsertRange`, `InsertRangeByPointer`, `RemoveRange`) on the Blazor data series types, including `XyDataSeries`, `XyyDataSeries`, `XyzDataSeries`, `XyxyDataSeries`, `XyTextDataSeries`, `OhlcDataSeries`, `HlcDataSeries` and `BoxPlotDataSeries` (SCJS-2611).
- Updated the series demos (Line, Band, Bubble, BoxPlot, ErrorBars, LineSegment, Ohlc, Text) to showcase inserting, updating and removing data at runtime.

