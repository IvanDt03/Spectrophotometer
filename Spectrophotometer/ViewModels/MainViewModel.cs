using Spectrophotometer.Commands;
using Spectrophotometer.Models;
using Spectrophotometer.Service;
using System.Collections.ObjectModel;

namespace Spectrophotometer.ViewModels;

public class MainViewModel : Notifier
{
    #region Fields 

    private ObservableCollection<MixtureMonomers> _mixtures;
    private MixtureMonomers _selectedMixture;
    private RatioMonomers _loadedRatio;
    private double _minLambda = 100.0;
    private double _maxLambda = 100.0;

    private IDataService _dataService;
    private IDialogService _dialogService;
    private ChartLiveViewModel _chart;

    #endregion

    #region Initialize

    public MainViewModel()
    {
        _dataService = new ExcelDataService("Content\\Data.xlsx");
        _dialogService = new MessageBoxDialogService();
        _chart = new ChartLiveViewModel();

        var result = _dataService.LoadMixtures();
        if (result.IsSuccess)
            _mixtures = new ObservableCollection<MixtureMonomers>(result.Data);
        else
        {
            //_dialogService.ShowMessage(result.Message);
            _mixtures = new ObservableCollection<MixtureMonomers>();
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

    public RatioMonomers LoadedRatio
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

    public ChartLiveViewModel Chart
    {
        get { return _chart; }
        set { SetValue(ref _chart, value, nameof(_chart)); }
    }

    #endregion

    #region Commands

    private RelayCommand _preparationCommand;
    private RelayCommand _startCommand;
    private RelayCommand _resetCommand;
    private RelayCommand _printCommand;

    public RelayCommand PreparationCommand
    {
        get { return _preparationCommand; }
    }

    public RelayCommand StartCommand
    {
        get { return _startCommand; }
    }

    public RelayCommand ResetCommand
    {
        get { return _resetCommand; }
    }


    #endregion
}
