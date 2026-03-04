using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SciChart.Blazor.Components;

namespace WasmDemo.Pages;

public partial class ImpulseSeriesDemo : ComponentBase
{
    private XyDataSeries? _xyDataSeriesImpulse1Ref;

    // Track the current index for appending new data
    private int _currentIndex = 10; // Initial data has indices 0-9

    private static readonly int[] _ySequence = [-2, 4, -1, 5, -3, 6, 2, -4, 3, 1, -5, 5];

    // Sample impulse data representing discrete signal values
    private double[] xData = new double[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private double[] yData = new double[] { 3, -2, 5, -1, 4, -3, 6, 2, -4, 1 };

    // Metadata for data labels
    private PointMetadata[] metadata = new PointMetadata[]
    {
        new() { CustomText = "3.0 meta" },
        new() { CustomText = "-2.0 meta" },
        new() { CustomText = "5.0 meta" },
        new() { CustomText = "-1.0" },
        new() { CustomText = "4.0" },
        new() { CustomText = "-3.0" },
        new() { CustomText = "6.0" },
        new() { CustomText = "2.0" },
        new() { CustomText = "-4.0" },
        new() { CustomText = "1.0" }
    };

    [Inject]
    private ILogger<ImpulseSeriesDemo>? Logger { get; set; }

    private async Task AppendData()
    {
        if (_xyDataSeriesImpulse1Ref == null) return;

        try
        {
            var newXValues = new double[] { _currentIndex, _currentIndex + 1, _currentIndex + 2 };
            var newYValues = new double[]
            {
                _ySequence[_currentIndex % _ySequence.Length],
                _ySequence[(_currentIndex + 1) % _ySequence.Length],
                _ySequence[(_currentIndex + 2) % _ySequence.Length]
            };
            var newMetadata = new PointMetadata[]
            {
                new() { CustomText = $"{newYValues[0]:F1}" },
                new() { CustomText = $"{newYValues[1]:F1}" },
                new() { CustomText = $"{newYValues[2]:F1}" }
            };

            Logger?.LogInformation($"Appending 3 impulse data points starting at index {_currentIndex}");
            await _xyDataSeriesImpulse1Ref.AppendRange(newXValues, newYValues, newMetadata);

            _currentIndex += 3;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to impulse series");
        }
    }

    private async Task AppendDataByPointer()
    {
        if (_xyDataSeriesImpulse1Ref == null) return;

        try
        {
            var newXValues = new double[] { _currentIndex, _currentIndex + 1, _currentIndex + 2 };
            var newYValues = new double[]
            {
                _ySequence[_currentIndex % _ySequence.Length],
                _ySequence[(_currentIndex + 1) % _ySequence.Length],
                _ySequence[(_currentIndex + 2) % _ySequence.Length]
            };
            var newMetadata = new PointMetadata[]
            {
                new() { CustomText = $"{newYValues[0]:F1}" },
                new() { CustomText = $"{newYValues[1]:F1}" },
                new() { CustomText = $"{newYValues[2]:F1}" }
            };

            Logger?.LogInformation($"Appending 3 impulse data points by pointer starting at index {_currentIndex}");
            await _xyDataSeriesImpulse1Ref.AppendRangeByPointer(newXValues, newYValues, newMetadata);

            _currentIndex += 3;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "Failed to append data to impulse series by pointer");
        }
    }
}
