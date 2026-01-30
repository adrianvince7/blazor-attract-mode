namespace BlazorAttractMode;

/// <summary>
/// Configuration options for the AttractMode component.
/// </summary>
public class AttractModeOptions
{
    /// <summary>
    /// Gets or sets the inactivity timeout in milliseconds before the attract mode is triggered.
    /// Default is 30000 (30 seconds).
    /// </summary>
    public int InactivityTimeoutMs { get; set; } = 30000;

    /// <summary>
    /// Gets or sets the video source URL to display during attract mode.
    /// </summary>
    public string VideoSource { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the video should loop.
    /// Default is true.
    /// </summary>
    public bool Loop { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the video should be muted.
    /// Default is false.
    /// </summary>
    public bool Muted { get; set; } = false;

    /// <summary>
    /// Gets or sets the z-index for the attract mode overlay.
    /// Default is 9999.
    /// </summary>
    public int ZIndex { get; set; } = 9999;
}
