using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bdziam.DockView.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Bdziam.DockView.Demo.Pages;

public partial class Home
{
    [Inject]
    private ILogger<Home> Logger { get; set; } = default!;

    private BdziamDockView? dockView;
    private string? newPanelTitle = "New Panel";
    private string? newPanelKey = "panel";
    private string? selectedContainerId = "root";
    private DockViewContentType selectedContentType = DockViewContentType.Component;
    private string panelContent = "This is the content of the panel.";
    private string? savedLayout;
    private int panelCounter = 1;
    private int containerCounter = 1;
    private bool showDefaultLayout = true;
    private List<DynamicPanelInfo> dynamicPanels = new();
    
    // Panel options
    private bool showHeader = true;
    private bool showClose = true;
    private bool isLock = false;
    private bool showLock = true;
    private bool isFloating = false;
    private bool showFloat = true;
    private bool showMaximize = true;

    // Track containers for the dropdown list
    private List<ContainerInfo> containers = new();

    protected override void OnInitialized()
    {
        // Default layout is now declared in the Razor markup
        // We just need to add the default container to the dropdown list
        containers.Add(new ContainerInfo { Key = "container1", Title = "Row Container" });
    }

    // Model class for dynamic panels
    private class DynamicPanelInfo
    {
        public string Key { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DockViewContentType Type { get; set; } = DockViewContentType.Component;
        public bool ShowHeader { get; set; } = true;
        public bool ShowClose { get; set; } = true;
        public bool IsLock { get; set; } = false;
        public bool ShowLock { get; set; } = true;
        public bool IsFloating { get; set; } = false;
        public bool ShowFloat { get; set; } = true;
        public bool ShowMaximize { get; set; } = true;
    }

    private void AddPanel()
    {
        if (string.IsNullOrEmpty(newPanelTitle) || string.IsNullOrEmpty(newPanelKey))
        {
            return;
        }

        // Generate unique keys if not provided
        var panelKey = string.IsNullOrEmpty(newPanelKey) ? $"panel_{panelCounter++}" : newPanelKey;
        var title = string.IsNullOrEmpty(newPanelTitle) ? $"Panel {panelCounter}" : newPanelTitle;

        if (selectedContentType == DockViewContentType.Component)
        {
            // Create a regular panel component
            AddComponentPanel(panelKey, title);
        }
        else
        {
            // Create a container (Row, Column, Group)
            AddContainerPanel(panelKey, title);
        }

        // Reset input fields
        newPanelKey = $"panel_{panelCounter}";
        newPanelTitle = $"Panel {panelCounter}";
    }

    private void AddComponentPanel(string key, string title)
    {
        // Add a new panel to the dynamic panels collection
        dynamicPanels.Add(new DynamicPanelInfo
        {
            Key = key,
            Title = title,
            Content = panelContent,
            Type = DockViewContentType.Component,
            ShowHeader = showHeader,
            ShowClose = showClose,
            IsLock = isLock,
            ShowLock = showLock,
            IsFloating = isFloating,
            ShowFloat = showFloat,
            ShowMaximize = showMaximize
        });
        
        // If this is the first dynamic panel, hide the default layout
        if (showDefaultLayout && dynamicPanels.Count == 1)
        {
            showDefaultLayout = false;
        }

        StateHasChanged();
    }

    private void AddContainerPanel(string key, string title)
    {
        // Add this container to the list of available containers
        containers.Add(new ContainerInfo
        {
            Key = key,
            Title = title
        });

        // Add a new container to the dynamic panels collection
        dynamicPanels.Add(new DynamicPanelInfo
        {
            Key = key,
            Title = title,
            Type = selectedContentType,
            ShowHeader = showHeader,
            ShowClose = showClose,
            IsLock = isLock,
            ShowLock = showLock,
            IsFloating = isFloating,
            ShowFloat = showFloat,
            ShowMaximize = showMaximize
        });
        
        // If this is the first dynamic panel, hide the default layout
        if (showDefaultLayout && dynamicPanels.Count == 1)
        {
            showDefaultLayout = false;
        }

        StateHasChanged();
    }

    

    

    private async Task SaveCurrentLayout()
    {
        if (dockView != null)
        {
            savedLayout = await dockView.SaveLayout();
            StateHasChanged();
        }
    }

    private async Task LoadSavedLayout()
    {
        if (dockView != null && !string.IsNullOrEmpty(savedLayout))
        {
            await dockView.Reset(savedLayout);
        }
    }

    private async Task ResetLayout()
    {
        if (dockView != null)
        {
            await dockView.Reset();
            showDefaultLayout = true;
            dynamicPanels.Clear();
            containers.Clear();
            containers.Add(new ContainerInfo { Key = "container1", Title = "Row Container" });
            StateHasChanged();
        }
    }

    private async Task PanelVisibilityChangedAsync(string title, bool status)
    {
        Logger.LogInformation("Panel visibility changed: {Title}, Status: {Status}", title, status);
        await Task.CompletedTask;
    }

    private async Task PanelsLockChangedAsync(string[] panels, bool state)
    {
        Logger.LogInformation("Panels lock state changed: {Count} panels, State: {State}", panels.Length, state);
        await Task.CompletedTask;
    }

    private async Task SplitterAdjustedAsync()
    {
        Logger.LogInformation("Splitter was adjusted");
        await Task.CompletedTask;
    }

    private class ContainerInfo
    {
        public string Key { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
