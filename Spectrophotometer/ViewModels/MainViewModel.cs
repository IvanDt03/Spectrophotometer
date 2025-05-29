using Spectrophotometer.Models;
using Spectrophotometer.Service;
using System.Collections.ObjectModel;

namespace Spectrophotometer.ViewModels;

public class MainViewModel : Notifier
{
    private ObservableCollection<MonomerMixtures> _mixtures;
    private MonomerMixtures _selectedMixture;
    private UnitMonomerMixture _selectedUnitMixture;

    private IDataService _dataService;
    private IDialogService _dialogService;

    public MainViewModel()
    {
        _dataService = new ExcelDataService("Content\\Data.xlsx");
        _dialogService = new MessageBoxDialogService();

        var result = _dataService.LoadMixtures();
        if (result.IsSuccess)
            _mixtures = new ObservableCollection<MonomerMixtures>(result.Data);
        else
        {
            //_dialogService.ShowMessage(result.Message);
            _mixtures = new ObservableCollection<MonomerMixtures>();
        }
    }

    public ObservableCollection<MonomerMixtures> Mixtures
    {
        get { return _mixtures; }
        set { SetValue(ref _mixtures, value, nameof(Mixtures)); }
    }

    public MonomerMixtures SelectedMixture
    {
        get { return _selectedMixture; }
        set 
        {
            var result = _dataService.LoadUnitMixture(value.Title);
            if (result.IsSuccess)
                value.ListMonomerMixture = new ObservableCollection<UnitMonomerMixture>(result.Data);
            else
                _dialogService.ShowMessage(result.Message);

                SetValue(ref _selectedMixture, value, nameof(SelectedMixture));
        }
    }

    public UnitMonomerMixture SelectedUnitMixture
    {
        get { return _selectedUnitMixture; }
        set { SetValue(ref _selectedUnitMixture, value, nameof(SelectedUnitMixture)); }
    }


}
