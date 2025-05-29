using Spectrophotometer.ViewModels;

namespace Spectrophotometer.Models;

public class UnitMonomerMixture : Notifier
{
    private double _volumeFirstMonomer;
    private double _volumeSecondMonomer;
    private double _signalFactor;

    public UnitMonomerMixture(double volumeFirst, double volumesecond, double signalFactor)
    {
        _volumeFirstMonomer = volumeFirst;
        _volumeSecondMonomer = volumesecond;
        _signalFactor = signalFactor;
    }

    public double VolumeFirstMonomer
    {
        get { return _volumeFirstMonomer; }
        private set { SetValue(ref _volumeFirstMonomer, value, nameof(VolumeFirstMonomer)); }
    }

    public double VolumeSecondMonomer
    {
        get { return _volumeSecondMonomer; }
        private set { SetValue(ref _volumeSecondMonomer, value, nameof(VolumeSecondMonomer)); }
    }

    public double SignalFactor
    {
        get { return _signalFactor; }
        private set { SetValue(ref _signalFactor, value, nameof(_signalFactor)); }
    }
}
