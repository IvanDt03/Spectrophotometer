using Spectrophotometer.ViewModels;

namespace Spectrophotometer.Models;

public class UnitMonomerMixture : Notifier
{
    private string _nameFirstMonomer;
    private string _nameSecondMonomer;
    private double _volumeFirstMonomer;
    private double _volumeSecondMonomer;
    private double _signalFactor;

    public UnitMonomerMixture(string nameFirstMonomer, string nameSecondMonomer, double volumeFirst, double volumesecond, double signalFactor)
    {
        _nameFirstMonomer = nameFirstMonomer;
        _nameSecondMonomer = nameSecondMonomer;
        _volumeFirstMonomer = volumeFirst;
        _volumeSecondMonomer = volumesecond;
        _signalFactor = signalFactor;
    }

    public double VolumeFirstMonomer
    {
        get { return _volumeFirstMonomer; }
        set { SetValue(ref _volumeFirstMonomer, value, nameof(VolumeFirstMonomer)); }
    }

    public double VolumeSecondMonomer
    {
        get { return _volumeSecondMonomer; }
        set { SetValue(ref _volumeSecondMonomer, value, nameof(VolumeSecondMonomer)); }
    }

    public double SignalFactor
    {
        get { return _signalFactor; }
        set { SetValue(ref _signalFactor, value, nameof(_signalFactor)); }
    }

    public string NameFirstMonomer
    {
        get { return _nameFirstMonomer; }
        set { SetValue(ref _nameFirstMonomer, value, nameof(_nameFirstMonomer)); }
    }

    public string NameSecondMonomer
    {
        get { return _nameSecondMonomer; }
        set { SetValue(ref _nameSecondMonomer, value, nameof(NameSecondMonomer)); }
    }
}
