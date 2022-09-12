namespace Sonos.Api.Models.Settings
{
    public class PlayerSettings
    {
        public PlayerSettings(VolumeMode volumeMode, float volumeScalingFactor, bool monoMode, bool wifiDisable)
        {
            this.VolumeMode = volumeMode;
            this.VolumeScalingFactor = volumeScalingFactor;
            this.MonoMode = monoMode;
            this.WifiDisable = wifiDisable;
        }

        public VolumeMode VolumeMode { get; }

        public float VolumeScalingFactor { get; }

        public bool MonoMode { get; }

        public bool WifiDisable { get; }
    }
}