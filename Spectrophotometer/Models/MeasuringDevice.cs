using Spectrophotometer.ViewModels;
using System;
using System.Windows.Threading;

namespace Spectrophotometer.Models;

public class MeasuringDevice : Notifier
{
    private DispatcherTimer _timer;
    private DataPoint _currentPoint;
    private bool _isRunning;
    private double _currentMinLambdaUI;
    private double _currentMaxLambdaUI;
    private MonomerMixtures? _monomerMixturesSelected;
    private int _indexSelectedUnit;

    public MeasuringDevice()
    {
        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromMicroseconds(50);
        _timer.Tick += OnStartMeasurement;
        _isRunning = false;
        _monomerMixturesSelected = null;
    }

    private void OnStartMeasurement(object? sender, EventArgs e)
    {
        if (_currentMinLambdaUI > _currentMaxLambdaUI)
        {
            IsRunning = false;
            _timer.Stop();
        }

        var lambda = ++_currentMinLambdaUI;
        var signal = A_Factor * Math.Pow(1 + Math.Pow(lambda - _monomerMixturesSelected.LambdaA, 2) / Math.Pow(_monomerMixturesSelected.WFactor, 2), -1);

        CurrentPoint = new DataPoint(lambda, signal);
    }

    public void StartMeasurement(double minLambda, double maxLambda,  MonomerMixtures selected, int indexSelectedUnit)
    {
        if (IsRunning || selected == null)
            return;

        _currentMinLambdaUI = minLambda;
        _currentMaxLambdaUI = maxLambda;
        _monomerMixturesSelected = selected;
        _indexSelectedUnit = indexSelectedUnit;

        IsRunning = true;
        _timer.Start();
    }

    public DataPoint CurrentPoint
    {
        get { return _currentPoint; }
        private set { SetValue(ref _currentPoint, value, nameof(CurrentPoint)); }
    }

    public bool IsRunning
    {
        get { return _isRunning; }
        private set { SetValue(ref _isRunning, value, nameof(_isRunning)); }
    }

    private double ValueBack_1
    {
        get 
        { 
            return Math.Pow(Math.Pow(_monomerMixturesSelected.LambdaA - _monomerMixturesSelected.LambdaMin, 2) / Math.Pow(_monomerMixturesSelected.WFactor, 2) + 1, -1); 
        }
    }
    private double ValueBack_2
    {
        get 
        { 
            return Math.Pow(Math.Pow(_monomerMixturesSelected.LambdaA - _monomerMixturesSelected.LambdaMax, 2) / Math.Pow(_monomerMixturesSelected.WFactor, 2) + 1, -1); 
        }
    }
    private double Shift
    {
        get { return Math.Abs(ValueBack_1 - ValueBack_2) / 2; }
    }
    private double A_Factor
    {
        get { return _monomerMixturesSelected[_indexSelectedUnit].SignalFactor / (1 - Shift); }
    }
}
