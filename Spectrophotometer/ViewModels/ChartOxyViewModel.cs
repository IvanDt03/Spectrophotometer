using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Spectrophotometer.ViewModels;

public class ChartOxyViewModel : Notifier
{
    private PlotModel _model;
    private LineSeries _series;
    private LinearAxis _xAxis;
    private LinearAxis _yAxis;

    public ChartOxyViewModel()
    {
        _model = new PlotModel();

        _series = new LineSeries
        {

        };

        _xAxis = new LinearAxis
        {
            Title = "Длина волны, нм",
            Position = AxisPosition.Bottom,
            FontSize = 6,
            TitleFontSize = 6,
        };

        _yAxis = new LinearAxis
        {
            Title = "Сигнал",
            Position = AxisPosition.Left,
            FontSize = 6,
            TitleFontSize = 6,
        };

        _model.Series.Add(_series);
        _model.Axes.Add(_xAxis);
        _model.Axes.Add(_yAxis);
    }

    public PlotModel Model
    {
        get { return _model; }
        set { SetValue(ref _model, value, nameof(Model)); }
    }

    public void AddPoint(Spectrophotometer.Models.DataPoint point)
    {
        _series.Points.Add(new DataPoint(point.Lambda, point.Signal));
        _model.InvalidatePlot(true);
    }

    public void ResetChart()
    {
        _series.Points.Clear();
        _model.InvalidatePlot(true);
    }

    public bool IsEmpty() => _series.Points.Count == 0;
}
