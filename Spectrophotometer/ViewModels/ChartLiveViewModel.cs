using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using Spectrophotometer.Models;
using System.Collections.ObjectModel;

namespace Spectrophotometer.ViewModels;

public class ChartLiveViewModel : Notifier
{
    private ObservableCollection<DataPoint> _data;
    private ObservableCollection<ISeries> _series;
    private ObservableCollection<ICartesianAxis> _xAxis;
    private ObservableCollection<ICartesianAxis> _yAxis;

    public ChartLiveViewModel()
    {
        _data = new ObservableCollection<DataPoint>();

        _series = new ObservableCollection<ISeries>
        {
            new LineSeries<DataPoint>
            {
                Values = _data,
                Mapping = (point, index) => new LiveChartsCore.Kernel.Coordinate(point.Lambda, point.Signal),
                Fill = null
            },
        };

        _xAxis = new ObservableCollection<ICartesianAxis>
        {
            new Axis
            {
                Name = "Длина волны, нм",
            }
        };

        _yAxis = new ObservableCollection<ICartesianAxis>
        {
            new Axis
            {
                Name = "Сигнал",
                MaxLimit = 1.0,
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

    public void PreparationChart(double minLambda, double maxLambda)
    {
        XAxis[0].MinLimit = minLambda;
        XAxis[0].MaxLimit = maxLambda + 5;
    }

    public void AddPoint(DataPoint point)
    {
        _data.Add(point);
    }

    public void ResetChart()
    {
        _data.Clear();
    }

    public bool IsEmpty() => _data.Count == 0;
}
