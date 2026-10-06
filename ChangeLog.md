# SciChart Blazor Change Log

## 6.0.6-beta.1

- Updated to scichart.js 6.0.6. Examples for version 5 are on the `v5.x` branch
- WebGPU support. The renderer is chosen by the `IS_WEB_GPU` local storage key (unset = Auto, `"1"` = WebGPU, `"0"` = WebGL); Auto uses WebGPU on Apple Silicon Macs and WebGL elsewhere. Added the `ERenderMode` enum and a Render Mode dropdown (Auto / WebGL / WebGPU) on the Home page of both demos
- ES modules and tree-shaking: only the chart types the Blazor components can create are registered, so 3D, polar and pie code is left out of the bundle. `sciChartJsWrapper.js` went from 1.76 MB to 1.33 MB (409 KB to 321 KB gzipped)
- Wasm64 support. The engine is chosen by the `SCICHART_WASM_MODE` local storage key (`Wasm32` by default, `Wasm32NoSimd` or `Wasm64`); wasm64 needs Chrome / Edge 133+ or Firefox 134+. Added the `EWasmMode` enum and a Wasm Mode dropdown on the Home page of both demos
- The package now ships `scichart.wasm`, `scichart-nosimd.wasm` and `scichart-64.wasm` instead of `scichart2d.wasm`
- Added a License Debug checkbox on the Home page of both demos, which sets the `LICENSE_DEBUG` local storage key. The same key now also turns the SciChart.Blazor console logging on and off
- OHLC auto-simplify: `AutoSimplify`, `SimplifyOpenThresholdPx` and `SimplifyCloseThresholdPx` on `FastOhlcRenderableSeries` hide the open / close ticks when bars are too dense. Added an Auto Simplify On / Off button to the OHLC demo
- Per-series Y axis inside a stacked column collection: `YAxisId` on `StackedColumnRenderableSeries`. Series sharing a `StackedGroupId` must use the same Y axis. The Vertically Stacked Column demo now has a second group on its own Y axis
- Contour coloring mode: `ColorMapMode` on `UniformContoursRenderableSeries` with the new `EContourColorMapMode` enum (`SingleColor`, the default, `GradientColors` and `AlternateColors`). Added a Color Map Mode dropdown to the Uniform Contours demo
- Pixel-aligned box annotation: `IsPixelAligned` on `BoxAnnotation` keeps the border stable while dragging or panning
- Fixed a Blazor Server error when a chart was rebuilt while it was still re-rendering, for example clicking Clear in the Realtime demo

## 5.2.69-beta.28

- Updated to scichart.js 5.2.69
- Axes can now be added to or removed from a chart at runtime without recreating it; the chart update diffs the axes by id, so only the added or removed ones are built or deleted
- Added "Add Axis" / "Remove Axis" buttons to the Axis demo showing axes created and removed at runtime. 

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

