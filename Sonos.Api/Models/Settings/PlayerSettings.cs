namespace Sonos.Api.Models.Settings;

public record PlayerSettings(VolumeMode VolumeMode, float VolumeScalingFactor, bool MonoMode, bool WifiDisable);