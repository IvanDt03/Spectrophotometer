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
        };

        _yAxis = new LinearAxis
        {
            Title = "Сигнал",
            Position = AxisPosition.Left,
        };

        _model.Series.Add(_series);
        _model.Axes.Add(_xAxis);
        _model.Axes.Add(_yAxis);
    }
}
