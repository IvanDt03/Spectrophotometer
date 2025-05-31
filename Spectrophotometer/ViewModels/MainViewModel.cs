using Spectrophotometer.Commands;
using Spectrophotometer.Models;
using Spectrophotometer.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Spectrophotometer.ViewModels;

public class MainViewModel : Notifier
{
    #region Fields 

    private ObservableCollection<MixtureMonomers> _mixtures;
    private MixtureMonomers _selectedMixture;
    private RatioMonomers? _loadedRatio;
    private double _minLambda = 100.0;
    private double _maxLambda = 100.0;
    private MeasuringDevice _device;

    private IDataService _dataService;
    private IDialogService _dialogService;
    private ChartLiveViewModel _chartLive;
    private ChartOxyViewModel _chartOxy;

    #endregion

    #region Initialize

    public MainViewModel()
    {
        _dataService = new ExcelDataService("Content\\Data.xlsx");
        _dialogService = new DialogService();
        _chartLive = new ChartLiveViewModel();
        _chartOxy = new ChartOxyViewModel();
        _device = new MeasuringDevice();

        _device.PropertyChanged += OnDevicePropertyChanged;

        var result = _dataService.LoadMixtures();
        if (result.IsSuccess)
            _mixtures = new ObservableCollection<MixtureMonomers>(result.Data);
        else
        {
            //_dialogService.ShowMessage(result.Message);
            _mixtures = new ObservableCollection<MixtureMonomers>();
        }
    }

    private void OnDevicePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_device.CurrentPoint):
                ChartLive.AddPoint(_device.CurrentPoint);
                ChartOxy.AddPoint(_device.CurrentPoint);
                break;
            case nameof(_device.IsRunning):
                ResetCommand.RaiseCanExecuteChanged();
                break;
        }
    }

    #endregion

    #region Properties

    public ObservableCollection<MixtureMonomers> Mixtures
    {
        get { return _mixtures; }
        set { SetValue(ref _mixtures, value, nameof(Mixtures)); }
    }

    public MixtureMonomers SelectedMixture
    {
        get { return _selectedMixture; }
        set 
        {
            var result = _dataService.LoadUnitMixture(value);
            if (result.IsSuccess)
                value.ListMonomerMixture = new ObservableCollection<RatioMonomers>(result.Data);
            else
                _dialogService.ShowMessage(result.Message);

                SetValue(ref _selectedMixture, value, nameof(SelectedMixture));
        }
    }

    public RatioMonomers? LoadedRatio
    {
        get { return _loadedRatio; }
        set { SetValue(ref _loadedRatio, value, nameof(LoadedRatio)); }
    }

    public double MinLambda
    {
        get { return _minLambda; }
        set { SetValue(ref _minLambda, value, nameof(MinLambda)); }
    }

    public double MaxLambda
    {
        get { return _maxLambda; }
        set { SetValue(ref _maxLambda, value, nameof(MaxLambda)); }
    }

    public ChartLiveViewModel ChartLive
    {
        get { return _chartLive; }
        set { SetValue(ref _chartLive, value, nameof(_chartLive)); }
    }

    public ChartOxyViewModel ChartOxy
    {
        get { return _chartOxy; }
        set { SetValue(ref _chartOxy, value, nameof(ChartOxy)); }
    }

    #endregion

    #region Commands

    private RelayCommand _loadedCommand;
    private RelayCommand _startCommand;
    private RelayCommand _resetCommand;
    private RelayCommand _printCommand;

    public RelayCommand LoadedCommand
    {
        get
        {
            return _loadedCommand ??
                (_loadedCommand = new RelayCommand(OnPrepariation, CanExecutePreparation));
        }
    }

    private void OnPrepariation(object? parametr)
    {
        var ratio = parametr as RatioMonomers;

        if (ratio != null)
        {
            LoadedRatio = ratio;
        }
    }

    private bool CanExecutePreparation(object? parametr)
    {
        return parametr is not null && LoadedRatio is null && !_device.IsRunning;
    }

    public RelayCommand StartCommand
    {
        get
        {
            return _startCommand ??
                (_startCommand = new RelayCommand(OnStart, CanExecuteStart));
        }
    }

    private void OnStart(object? parametr)
    {
        if (LoadedRatio != null && MinLambda < MaxLambda)
        {
            ChartLive.PreparationChart(MinLambda, MaxLambda);
            _device.StartMeasurement(MinLambda, MaxLambda, SelectedMixture, LoadedRatio);
        }
    }

    private bool CanExecuteStart(object? parametr)
    {
        return LoadedRatio is not null && !_device.IsRunning && ChartLive.IsEmpty() && ChartOxy.IsEmpty();
    }

    public RelayCommand ResetCommand
    {
        get
        {
            return _resetCommand ??
                (_resetCommand = new RelayCommand(OnReset, CanExecuteReset));
        }
    }

    private void OnReset(object? parametr)
    {
        LoadedRatio = null;
        ChartLive.ResetChart();
        ChartOxy.ResetChart();
    }

    private bool CanExecuteReset(object? parameter)
    {
        return !_device.IsRunning && LoadedRatio is not null;
    }

    public RelayCommand PrintCommand
    {
        get
        {
            return _printCommand ??
                (_printCommand = new RelayCommand(OnPrint, CanExecutePrint));
        }
    }

    private void OnPrint(object? parametr)
    {
        _dialogService.PrintChart(ChartOxy.Model, LoadedRatio, SelectedMixture);
    }

    private bool CanExecutePrint(object? parametr)
    {
        return !_device.IsRunning;
    }

    #endregion
}
