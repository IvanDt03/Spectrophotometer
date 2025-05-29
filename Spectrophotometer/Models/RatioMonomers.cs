using Spectrophotometer.ViewModels;

namespace Spectrophotometer.Models;

public class RatioMonomers : Notifier
{
    private double _volumeFirstMonomer;
    private double _volumeSecondMonomer;
    private double _signalFactor;
    private MixtureMonomers _mixture;

    public RatioMonomers(MixtureMonomers mixture, double volumeFirst, double volumesecond, double signalFactor)
    {
        _mixture = mixture;
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

    public MixtureMonomers Mixture
    {
        get { return _mixture; }
    }
}
