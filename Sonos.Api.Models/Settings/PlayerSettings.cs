namespace Sonos.Api.Models.Settings
{
    using System.Text.Json.Serialization;

    public class PlayerSettings
    {
        [JsonConstructor]
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