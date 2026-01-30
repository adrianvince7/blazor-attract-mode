using Microsoft.Extensions.Options;

namespace BlazorAttractMode;

/// <summary>
/// Service for managing AttractMode state.
/// </summary>
public class AttractModeService
{
    private readonly AttractModeOptions _options;

    public AttractModeService(IOptions<AttractModeOptions> options)
    {
        _options = options.Value;
    }

    /// <summary>
    /// Gets the configured options.
    /// </summary>
    public AttractModeOptions Options => _options;

    /// <summary>
    /// Event raised when attract mode state changes.
    /// </summary>
    public event Action<bool>? StateChanged;

    /// <summary>
    /// Notifies listeners that attract mode state has changed.
    /// </summary>
    public void NotifyStateChanged(bool isActive)
    {
        StateChanged?.Invoke(isActive);
    }
}
