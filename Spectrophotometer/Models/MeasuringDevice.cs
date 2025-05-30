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
    private MixtureMonomers? _currentMixture;
    private RatioMonomers? _currentRatio;

    public MeasuringDevice()
    {
        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(70);
        _timer.Tick += OnStartMeasurement;
        _isRunning = false;
        _currentMixture = null;
    }

    private void OnStartMeasurement(object? sender, EventArgs e)
    {
        if (_currentMinLambdaUI > _currentMaxLambdaUI)
        {
            IsRunning = false;
            _timer.Stop();
        }

        var lambda = ++_currentMinLambdaUI;
        var signal = A_Factor * Math.Pow(1 + Math.Pow(lambda - _currentMixture.LambdaA, 2) / Math.Pow(_currentMixture.WFactor, 2), -1);

        CurrentPoint = new DataPoint(lambda, signal);
    }

    public void StartMeasurement(double minLambda, double maxLambda,  MixtureMonomers selected, RatioMonomers ratio)
    {
        if (IsRunning)
            return;

        if (selected is null || ratio is null)
            return;

        _currentMinLambdaUI = minLambda;
        _currentMaxLambdaUI = maxLambda;
        _currentMixture = selected;
        _currentRatio = ratio;

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
            return Math.Pow(Math.Pow(_currentMixture.LambdaA - _currentMixture.LambdaMin, 2) / Math.Pow(_currentMixture.WFactor, 2) + 1, -1); 
        }
    }
    private double ValueBack_2
    {
        get 
        { 
            return Math.Pow(Math.Pow(_currentMixture.LambdaA - _currentMixture.LambdaMax, 2) / Math.Pow(_currentMixture.WFactor, 2) + 1, -1); 
        }
    }
    private double Shift
    {
        get { return Math.Abs(ValueBack_1 - ValueBack_2) / 2; }
    }
    private double A_Factor
    {
        get { return _currentRatio.SignalFactor / (1 - Shift); }
    }
}
