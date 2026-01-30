# Blazor Attract Mode - Usage Guide

## Table of Contents
- [Installation](#installation)
- [Basic Setup](#basic-setup)
- [Configuration](#configuration)
- [Advanced Scenarios](#advanced-scenarios)
- [Troubleshooting](#troubleshooting)

## Installation

### From Source
1. Clone or download this repository
2. Reference the project in your Blazor application:
```bash
dotnet add reference path/to/BlazorAttractMode.csproj
```

### From NuGet (when published)
```bash
dotnet add package BlazorAttractMode
```

## Basic Setup

### Step 1: Configure Services (Program.cs)

Add the AttractMode service to your dependency injection container:

```csharp
using BlazorAttractMode;

var builder = WebApplication.CreateBuilder(args);

// Add Blazor services
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add AttractMode services with configuration
builder.Services.AddAttractMode(options =>
{
    options.InactivityTimeoutMs = 30000; // 30 seconds
    options.VideoSource = "videos/attract-mode.mp4"; // Path to your video
    options.Loop = true;
    options.Muted = false;
});

var app = builder.Build();
// ... rest of your configuration
```

### Step 2: Add Component to App.razor

Add the `<AttractMode />` component to your root App.razor file with interactive rendering:

```razor
<!DOCTYPE html>
<html lang="en">
<head>
    <!-- ... your head content ... -->
</head>
<body>
    <Routes />
    <AttractMode @rendermode="InteractiveServer" />
    <!-- ... rest of your body content ... -->
</body>
</html>
```

### Step 3: Add Using Directive (_Imports.razor)

Add the namespace to your `_Imports.razor` file:

```razor
@using BlazorAttractMode
```

## Configuration

### AttractModeOptions Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `InactivityTimeoutMs` | int | 30000 | Time in milliseconds before attract mode activates |
| `VideoSource` | string | "" | URL or path to the video file |
| `Loop` | bool | true | Whether the video should loop |
| `Muted` | bool | false | Whether the video should be muted |
| `ZIndex` | int | 9999 | CSS z-index for the overlay |

### Example Configurations

#### Short Timeout for Testing
```csharp
builder.Services.AddAttractMode(options =>
{
    options.InactivityTimeoutMs = 5000; // 5 seconds
    options.VideoSource = "test-video.mp4";
});
```

#### Production Setup with External Video
```csharp
builder.Services.AddAttractMode(options =>
{
    options.InactivityTimeoutMs = 60000; // 1 minute
    options.VideoSource = "https://yourcdn.com/attract-video.mp4";
    options.Loop = true;
    options.Muted = true; // Muted to avoid startling users
});
```

#### Environment-Specific Configuration
```csharp
builder.Services.AddAttractMode(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.InactivityTimeoutMs = 10000; // 10 seconds for dev
        options.VideoSource = "dev-video.mp4";
    }
    else
    {
        options.InactivityTimeoutMs = 45000; // 45 seconds for prod
        options.VideoSource = builder.Configuration["AttractMode:VideoUrl"] 
                              ?? "default-video.mp4";
        options.Muted = true;
    }
});
```

## Advanced Scenarios

### Listening to State Changes

You can inject the `AttractModeService` to react to attract mode state changes:

```razor
@page "/dashboard"
@inject AttractModeService AttractModeService
@implements IDisposable

<h3>Dashboard</h3>

@if (isAttractModeActive)
{
    <p>Attract mode is currently active</p>
}
else
{
    <p>User is actively using the application</p>
}

@code {
    private bool isAttractModeActive;

    protected override void OnInitialized()
    {
        AttractModeService.StateChanged += OnAttractModeStateChanged;
    }

    private void OnAttractModeStateChanged(bool isActive)
    {
        isAttractModeActive = isActive;
        StateHasChanged();
        
        // Custom logic when attract mode changes
        if (isActive)
        {
            // Pause background processes, stop timers, etc.
            Console.WriteLine("Attract mode activated");
        }
        else
        {
            // Resume normal operations
            Console.WriteLine("Attract mode deactivated - user returned");
        }
    }

    public void Dispose()
    {
        AttractModeService.StateChanged -= OnAttractModeStateChanged;
    }
}
```

### Custom Video Sources

#### Local Video File
Place your video in `wwwroot/videos/` and reference it:
```csharp
options.VideoSource = "videos/my-attract-video.mp4";
```

#### CDN-Hosted Video
```csharp
options.VideoSource = "https://cdn.example.com/videos/attract.mp4";
```

#### Multiple Video Formats
Use HTML5 video source elements by customizing the component if needed, or provide the best-supported format for your target browsers.

### Integration with Analytics

Track when attract mode activates:

```csharp
private void OnAttractModeStateChanged(bool isActive)
{
    if (isActive)
    {
        // Log to analytics
        await JSRuntime.InvokeVoidAsync("gtag", "event", "attract_mode_activated", new
        {
            event_category = "user_engagement",
            event_label = "inactivity"
        });
    }
}
```

### Conditional Rendering

Only show attract mode on certain pages:

```razor
@if (ShouldShowAttractMode())
{
    <AttractMode @rendermode="InteractiveServer" />
}

@code {
    [Inject] private NavigationManager Navigation { get; set; }
    
    private bool ShouldShowAttractMode()
    {
        // Only on homepage
        return Navigation.Uri.EndsWith("/") || Navigation.Uri.EndsWith("/home");
    }
}
```

## Troubleshooting

### Attract Mode Not Activating

1. **Check Interactive Rendering**: Ensure you're using `@rendermode="InteractiveServer"` or appropriate render mode
2. **Verify Service Registration**: Make sure `AddAttractMode()` is called in Program.cs
3. **Check Timeout**: Verify `InactivityTimeoutMs` is set to a reasonable value
4. **Browser Console**: Check for JavaScript errors in the browser console

### Video Not Playing

1. **Check Video Path**: Verify the `VideoSource` path is correct and accessible
2. **Video Format**: Ensure the video format is supported by your target browsers (MP4 with H.264 is most compatible)
3. **CORS Issues**: If using an external URL, ensure CORS headers allow video playback
4. **File Size**: Large video files may take time to load; consider optimizing your video

### Performance Issues

1. **Optimize Video**: Compress your video file to reduce size
2. **Adjust Timeout**: Increase `InactivityTimeoutMs` if activating too frequently
3. **Use Muted**: Set `Muted = true` to avoid audio processing overhead

### Component Not Dismissing on Click

1. **Check Event Handlers**: Verify JavaScript is loaded correctly
2. **Z-Index Issues**: Ensure the overlay has a high enough `ZIndex` value
3. **Browser Console**: Look for JavaScript errors

## Best Practices

1. **Video Optimization**: Keep video files under 10MB for quick loading
2. **Mute by Default**: Consider muting videos to avoid startling users
3. **Reasonable Timeouts**: 30-60 seconds is typical for most applications
4. **Test Across Browsers**: Verify video playback works in all target browsers
5. **Mobile Consideration**: Test on mobile devices; some browsers restrict autoplay
6. **Accessibility**: Consider adding a skip button for users who want to bypass attract mode

## Examples

See the `samples/SampleApp` directory for a complete working example with:
- Program.cs configuration
- App.razor integration
- Interactive rendering setup

## Support

For issues, questions, or contributions, please visit the GitHub repository.
