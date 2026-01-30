# Blazor Attract Mode

Lightweight UI plugin that displays a video when there hasn't been interaction with the Blazor site. Just like in video games.

## Features

- ⚡ **Extra Lightweight**: Minimal JavaScript and CSS footprint
- 🎮 **Video Game Inspired**: Automatically triggers after inactivity
- 🔧 **Easy Setup**: Just add to App.razor and configure in Program.cs
- 🎯 **Highly Configurable**: Customize timeout, video source, and behavior
- 📱 **Universal Input Detection**: Monitors mouse, keyboard, touch, and scroll events
- 🎨 **Responsive**: Full-screen video overlay with optimal scaling

## Installation

Add the package to your Blazor project:

```bash
dotnet add package BlazorAttractMode
```

Or reference the project directly:

```bash
dotnet add reference path/to/BlazorAttractMode.csproj
```

## Quick Start

### 1. Configure Services in Program.cs

```csharp
using BlazorAttractMode;

var builder = WebApplication.CreateBuilder(args);

// Add AttractMode services
builder.Services.AddAttractMode(options =>
{
    options.InactivityTimeoutMs = 30000; // 30 seconds
    options.VideoSource = "path/to/your/video.mp4";
    options.Loop = true;
    options.Muted = false;
});

// ... rest of your configuration
```

### 2. Add Component to App.razor

Add the `<AttractMode />` component to your App.razor file:

```razor
<!DOCTYPE html>
<html lang="en">
<head>
    <!-- ... your head content ... -->
</head>
<body>
    <Routes />
    <AttractMode />
    <!-- ... rest of your body content ... -->
</body>
</html>
```

### 3. Add Using Directive

Add the namespace to your `_Imports.razor`:

```razor
@using BlazorAttractMode
```

That's it! Your Blazor app now has attract mode enabled. 🎉

## Configuration Options

The `AttractModeOptions` class provides the following configuration options:

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `InactivityTimeoutMs` | int | 30000 | Inactivity timeout in milliseconds before attract mode triggers |
| `VideoSource` | string | "" | URL or path to the video file to display |
| `Loop` | bool | true | Whether the video should loop continuously |
| `Muted` | bool | false | Whether the video should be muted |
| `ZIndex` | int | 9999 | Z-index for the attract mode overlay |

## Advanced Usage

### Dynamic Configuration

You can configure different settings based on environment:

```csharp
builder.Services.AddAttractMode(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.InactivityTimeoutMs = 5000; // 5 seconds for testing
    }
    else
    {
        options.InactivityTimeoutMs = 60000; // 1 minute for production
    }
    
    options.VideoSource = builder.Configuration["AttractMode:VideoUrl"] 
                          ?? "default-video.mp4";
});
```

### Listening to State Changes

You can inject the `AttractModeService` to react to attract mode state changes:

```csharp
@inject AttractModeService AttractModeService

@code {
    protected override void OnInitialized()
    {
        AttractModeService.StateChanged += OnAttractModeStateChanged;
    }

    private void OnAttractModeStateChanged(bool isActive)
    {
        // React to attract mode activation/deactivation
        Console.WriteLine($"Attract mode is now: {(isActive ? "Active" : "Inactive")}");
    }

    public void Dispose()
    {
        AttractModeService.StateChanged -= OnAttractModeStateChanged;
    }
}
```

## How It Works

The AttractMode component:

1. **Monitors User Activity**: Listens for mouse, keyboard, touch, and scroll events
2. **Tracks Inactivity**: Starts a timer when the page loads
3. **Triggers Attract Mode**: When inactivity exceeds the configured timeout, displays the video overlay
4. **Resets on Interaction**: Any user interaction immediately hides the video and restarts the timer
5. **Optimized Performance**: Uses passive event listeners and efficient timer management

## Browser Compatibility

- ✅ Chrome/Edge (Chromium)
- ✅ Firefox
- ✅ Safari
- ✅ Mobile browsers (iOS Safari, Chrome Mobile)

## Examples

Check the `samples/SampleApp` directory for a complete working example.

## License

This project is open source. Feel free to use it in your projects!

## Contributing

Contributions are welcome! Please feel free to submit issues or pull requests.

