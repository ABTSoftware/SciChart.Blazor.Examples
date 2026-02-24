using Microsoft.AspNetCore.Components;
using SciChartBlazor.Components;
using SciChartBlazor.Tools;
using System.Diagnostics;
using System.Text.Json;
using System.Timers;

namespace WasmDemo.Pages;

public partial class LineSeriesRealtimeDemo : ComponentBase, IDisposable
{
    private SciChartSurface _sciChartRef;
    private XyDataSeries _xyDataSeriesLine1Ref;
    private XyDataSeries _xyDataSeriesLine2Ref;
    private XyDataSeries _xyDataSeriesLine3Ref;
    private System.Timers.Timer? _timer;
    private RandomWalkGenerator _generator1;
    private RandomWalkGenerator _generator2;
    private RandomWalkGenerator _generator3;
    private string _datapoints = "0";
    private string _fps = "0";
    private Stopwatch _fpsStopwatch = new Stopwatch();
    private long _lastRenderTime = 0;
    private int _numberOfPointsPerTimerTick = 1000; // 1,000 points every timer tick
    private EAutoRange _autoRange = EAutoRange.Always;
    private Queue<double> _fpsValues = new Queue<double>();
    private const int FpsAverageCount = 10;
    private bool _isReadyForNextUpdate = true;

    protected override void OnInitialized()
    {
        _generator1 = new RandomWalkGenerator(0);
        _generator2 = new RandomWalkGenerator(0);
        _generator3 = new RandomWalkGenerator(0);
    }

    private void StartUpdates()
    {
        _autoRange = EAutoRange.Always;
        _sciChartRef.RequestUpdate();

        if (_timer == null)
        {
            _timer = new System.Timers.Timer(10);
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = true;
        }
        _timer.Start();
    }

    private void StopUpdates()
    {
        _timer?.Stop();

        _autoRange = EAutoRange.Once;
        _sciChartRef.RequestUpdate();
    }

    private async void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        if (!_isReadyForNextUpdate)
        {
            return;
        }

        _isReadyForNextUpdate = false;

        var data1 = _generator1.GetRandomWalkSeries(_numberOfPointsPerTimerTick);
        var data2 = _generator2.GetRandomWalkSeries(_numberOfPointsPerTimerTick);
        var data3 = _generator3.GetRandomWalkSeries(_numberOfPointsPerTimerTick);

        await InvokeAsync(async () =>
        {
            await _xyDataSeriesLine1Ref.AppendRangeByPointer(data1.xValues, data1.yValues);
            await _xyDataSeriesLine2Ref.AppendRangeByPointer(data2.xValues, data2.yValues);
            await _xyDataSeriesLine3Ref.AppendRangeByPointer(data3.xValues, data3.yValues);
        });
    }

    private void HandleSurfaceRendered(SciChartSurfaceDefinition chartState)
    {
        try
        {
            _isReadyForNextUpdate = true;

            if (!_fpsStopwatch.IsRunning)
            {
                _fpsStopwatch.Start();
            }

            var totalDatapoints = 0;
            if (chartState?.Series != null)
            {
                foreach (var series in chartState.Series)
                {
                    if (series is FastLineRenderableSeriesDefinition lineSeries &&
                        lineSeries.XyData?.XValues != null)
                    {
                        totalDatapoints += lineSeries.XyData.XValues.Length; // TODO fix
                    }
                }
            }

            _datapoints = totalDatapoints.ToString("N0");

            var currentTime = _fpsStopwatch.ElapsedMilliseconds;
            if (_lastRenderTime > 0)
            {
                var deltaMs = currentTime - _lastRenderTime;
                if (deltaMs > 0)
                {
                    var currentFps = 1000.0 / deltaMs;
                    _fpsValues.Enqueue(currentFps);

                    if (_fpsValues.Count > FpsAverageCount)
                    {
                        _fpsValues.Dequeue();
                    }

                    var averageFps = _fpsValues.Average();
                    _fps = averageFps.ToString("F0");
                }
            }
            _lastRenderTime = currentTime;

            StateHasChanged();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in HandleSurfaceRendered: {ex.Message}");
        }
    }

    private void ClearData()
    {
        StopUpdates();
        _generator1.Reset();
        _generator2.Reset();
        _generator3.Reset();
        _datapoints = "0";
        _fps = "0";
        _fpsStopwatch.Reset();
        _lastRenderTime = 0;
        _fpsValues.Clear();
        _isReadyForNextUpdate = true;
        _sciChartRef.RequestRebuild();
        StateHasChanged();
    }

    public void Dispose()
    {
        if (_timer != null)
        {
            _timer.Stop();
            _timer.Dispose();
            _timer = null;
        }
    }
}
