namespace Sonos.Api;

public enum HouseholdEventType
{
    Favorites,
    Groups,
    Playlists,
}

public enum GroupEventType
{
    GroupVolume,
    PlayBack,
    PlaybackMetadata,
}

public enum PlayerEventType
{
    AudioClip,
    PlayerVolume,
}

public enum SessionEventType
{
    PlaybackSession,
}