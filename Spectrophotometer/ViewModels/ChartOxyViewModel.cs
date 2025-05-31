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
            TrackerFormatString = "Длина волны: {2:0.}\nСигнал: {4:0.000}",
        };

        _xAxis = new LinearAxis
        {
            Position = AxisPosition.Bottom,
            FontSize = 6,
            IsPanEnabled = false,
        };

        _yAxis = new LinearAxis
        {
            Position = AxisPosition.Left,
            FontSize = 6,
            Minimum = 0.0,
            Maximum = 1.5,
            IsPanEnabled = false,
        };

        _model.Series.Add(_series);
        _model.Axes.Add(_xAxis);
        _model.Axes.Add(_yAxis);
        _model.Padding = new OxyThickness(5);
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
