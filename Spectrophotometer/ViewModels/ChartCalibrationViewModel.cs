using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore;
using System.Collections.ObjectModel;
using LiveChartsCore.SkiaSharpView;
using Spectrophotometer.Models;
using System.Collections.Generic;

namespace Spectrophotometer.ViewModels;

public class ChartCalibrationViewModel : Notifier
{
    private ObservableCollection<ISeries> _series;
    private ObservableCollection<ICartesianAxis> _xAxis;
    private ObservableCollection<ICartesianAxis> _yAxis;

    public ChartCalibrationViewModel()
    {

        _series = new ObservableCollection<ISeries>
        {
            new LineSeries<DataPoint>
            {
                Mapping = (point, index) => new LiveChartsCore.Kernel.Coordinate(point.X, point.Y),
                Fill = null
            },
        };

        _xAxis = new ObservableCollection<ICartesianAxis>
        {
            new Axis
            {
                Name = "F",
            }
        };

        _yAxis = new ObservableCollection<ICartesianAxis>
        {
            new Axis
            {
                Name = "A",
            }
        };
    }

    public ObservableCollection<ISeries> Series
    {
        get { return _series; }
        set { SetValue(ref _series, value, nameof(Series)); }
    }

    public ObservableCollection<ICartesianAxis> XAxis
    {
        get { return _xAxis; }
        set { SetValue(ref _xAxis, value, nameof(XAxis)); }
    }

    public ObservableCollection<ICartesianAxis> YAxis
    {
        get { return _yAxis; }
        set { SetValue(ref _yAxis, value, nameof(YAxis)); }
    }

    public void UpdateChart(IEnumerable<DataPoint> data)
    {
        Series[0].Values = data;
    }
}
