# Quick Start Guide

Get Blazor Attract Mode up and running in 3 simple steps!

## Step 1: Add to Program.cs

```csharp
using BlazorAttractMode;

builder.Services.AddAttractMode(options =>
{
    options.InactivityTimeoutMs = 30000; // 30 seconds
    options.VideoSource = "videos/attract.mp4"; // Your video path
    options.Loop = true;
    options.Muted = false;
});
```

## Step 2: Add to App.razor

```razor
<AttractMode @rendermode="InteractiveServer" />
```

## Step 3: Add Using Directive to _Imports.razor

```razor
@using BlazorAttractMode
```

That's it! Your Blazor app now has attract mode! 🎮

## Configuration Options

```csharp
options.InactivityTimeoutMs = 30000;  // Timeout in milliseconds
options.VideoSource = "video.mp4";     // Path or URL to video
options.Loop = true;                   // Loop video continuously
options.Muted = false;                 // Mute/unmute video
options.ZIndex = 9999;                 // CSS z-index for overlay
```

## What It Does

1. **Monitors User Activity**: Tracks mouse, keyboard, touch, and scroll
2. **Activates After Inactivity**: Shows video overlay after timeout
3. **Returns on Interaction**: Any user action brings them back
4. **Resets Timer**: Starts counting again after dismissal

## Example

Check out `samples/SampleApp` for a complete working example!

## Need Help?

- See [README.md](README.md) for detailed information
- See [USAGE.md](USAGE.md) for advanced scenarios
- Open an issue on GitHub for support

---

**Note**: Make sure your video file is in the `wwwroot` folder or accessible via URL!
