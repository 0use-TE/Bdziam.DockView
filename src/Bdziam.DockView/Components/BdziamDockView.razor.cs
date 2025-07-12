// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bdziam.DockView.Data;
using Bdziam.DockView.Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bdziam.DockView.Components;

/// <summary>
/// BdziamDockView component
/// </summary>
public partial class BdziamDockView : IdComponentBase, IAsyncDisposable
{
    [Inject]
    private ILogger<BdziamDockView> Logger { get; set; } = default!;

    /// <summary>
    /// Get/set the DockView name, default is null, used for local storage identification
    /// </summary>
    [Parameter]
    [EditorRequired]
    [NotNull]
    public string? Name { get; set; }

    /// <summary>
    /// Get/set layout configuration
    /// </summary>
    [Parameter]
    public string? LayoutConfig { get; set; }

    /// <summary>
    /// Get/set whether to show the close button, default is true
    /// </summary>
    [Parameter]
    public bool ShowClose { get; set; } = true;

    /// <summary>
    /// Get/set whether it is locked, default is false
    /// </summary>
    /// <remarks>Cannot be dragged after locking</remarks>
    [Parameter]
    public bool IsLock { get; set; }

    /// <summary>
    /// Get/set whether to show the lock button, default is true
    /// </summary>
    [Parameter]
    public bool ShowLock { get; set; } = true;

    /// <summary>
    /// Get/set whether to show the maximize button, default is true
    /// </summary>
    [Parameter]
    public bool ShowMaximize { get; set; } = true;

    /// <summary>
    /// Get/set whether it is floating, default is false
    /// </summary>
    [Parameter]
    public bool IsFloating { get; set; }

    /// <summary>
    /// Get/set whether to show the float button, default is true
    /// </summary>
    [Parameter]
    public bool ShowFloat { get; set; } = true;

    /// <summary>
    /// Get/set whether to show the pin button, default is true
    /// </summary>
    [Parameter]
    public bool ShowPin { get; set; } = true;

    /// <summary>
    /// Get/set client-side rendering mode, default is null, client-side defaults to always onlyWhenVisible
    /// </summary>
    [Parameter]
    public DockViewRenderMode Renderer { get; set; }

    /// <summary>
    /// Get/set the callback for when tab visibility changes
    /// </summary>
    [Parameter]
    public Func<string[], bool, Task>? OnLockChangedCallbackAsync { get; set; }

    /// <summary>
    /// Get/set the callback for when the component is initialized
    /// </summary>
    [Parameter]
    public Func<Task>? OnInitializedCallbackAsync { get; set; }

    /// <summary>
    /// Get/set the callback for when the lock state changes
    /// </summary>
    [Parameter]
    public Func<string, bool, Task>? OnVisibleStateChangedAsync { get; set; }

    /// <summary>
    /// Get/set the callback for when the splitter is adjusted
    /// </summary>
    [Parameter]
    public Func<Task>? OnSplitterCallbackAsync { get; set; }

    /// <summary>
    /// Get/set child components
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Get/set version settings, default is null, used for local configuration, can be set globally
    /// </summary>
    [Parameter]
    public string? Version { get; set; }

    /// <summary>
    /// Get/set whether to enable local storage layout, default is null
    /// </summary>
    [Parameter]
    public bool? EnableLocalStorage { get; set; }

    /// <summary>
    /// Get/set local storage prefix, default is bb-dock
    /// </summary>
    [Parameter]
    public string? LocalStoragePrefix { get; set; }

    /// <summary>
    /// Get/set DockView component theme, default is Light
    /// </summary>
    [Parameter]
    public DockViewTheme Theme { get; set; } = DockViewTheme.Light;

    [CascadingParameter]
    private BdziamDockView? DockView { get; set; }

    [Inject]
    [NotNull]
    private IOptions<DockViewOptions>? Options { get; set; }

    [Inject]
    [NotNull]
    private IJSRuntime? JSRuntime { get; set; }

    private IJSObjectReference? _module;
    private IJSObjectReference? _interop;
    private bool _disposed;

    private string? ClassString =>
        CssBuilder.Default("b-dockview").AddClassFromAttributes(AdditionalAttributes).Build();

    private readonly List<DockViewComponentBase> _components = [];

    /// <summary>
    /// OnInitialized method
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Logger.LogInformation("BdziamDockView OnInitialized: Id = {Id}, _components count = {Count}", Id, _components.Count);
    }

    /// <summary>
    /// OnAfterRender method
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            Logger.LogInformation("BdziamDockView OnAfterRenderAsync: firstRender = true, _components count = {Count}", _components.Count);
            await LoadModuleAsync();
        }
        else
        {
            Logger.LogInformation("BdziamDockView OnAfterRenderAsync: update, _components count = {Count}", _components.Count);
            await InvokeVoidAsync("update", Id, GetOptions());
        }
    }

    /// <summary>
    /// Loads the JavaScript module
    /// </summary>
    /// <returns>A Task representing the asynchronous operation</returns>
    private async Task LoadModuleAsync()
    {
        try
        {
            Logger.LogInformation("LoadModuleAsync: Before importing JS module, _components count = {Count}", _components.Count);
            var options = GetOptions();
            Logger.LogInformation("LoadModuleAsync: Options created, Contents count = {Count}", options.Contents?.Count ?? 0);
            
            _module = await JSRuntime!.InvokeAsync<IJSObjectReference>("import", "./_content/Bdziam.DockView/Components/BdziamDockView.razor.js");
            _interop = await _module.InvokeAsync<IJSObjectReference>("init", Id, DotNetObjectReference.Create(this), options);
        }
        catch (JSDisconnectedException ex)
        {
            Logger.LogError(ex, "JSDisconnectedException occurred while loading the JavaScript module.");
        }
        catch (TaskCanceledException ex)
        {
            Logger.LogError(ex, "TaskCanceledException occurred while loading the JavaScript module.");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "An error occurred while loading the JavaScript module.");
        }
    }

    /// <summary>
    /// InvokeInit method
    /// </summary>
    /// <returns></returns>
    private Task InvokeInitAsync() => InvokeVoidAsync("init", Id, _interop, GetOptions());

    private DockViewConfig GetOptions()
    {
        Logger.LogInformation("GetOptions: _components count = {Count}", _components.Count);
        foreach (var component in _components)
        {
            Logger.LogInformation("Component: {Type}, Id: {Id}", 
                component.GetType().Name, component.Id);
        }
        
        return new DockViewConfig
        {
            EnableLocalStorage = EnableLocalStorage ?? false,
            LocalStorageKey = $"{GetPrefixKey()}-{Name}-{GetVersion()}",
            IsLock = IsLock,
            ShowLock = ShowLock,
            IsFloating = IsFloating,
            ShowFloat = ShowFloat,
            ShowClose = ShowClose,
            ShowPin = ShowPin,
            ShowMaximize = ShowMaximize,
            Renderer = Renderer,
            LayoutConfig = LayoutConfig,
            Theme = Theme.ToDescriptionString(),
            InitializedCallback = nameof(InitializedCallbackAsync),
            PanelVisibleChangedCallback = nameof(PanelVisibleChangedCallbackAsync),
            LockChangedCallback = nameof(LockChangedCallbackAsync),
            SplitterCallback = nameof(SplitterCallbackAsync),
            Contents = _components,
        };
    }

    private string GetVersion() => Version ?? Options.Value.Version ?? "v1";

    private string GetPrefixKey() =>
        LocalStoragePrefix ?? Options.Value.LocalStoragePrefix ?? "b-dockview";

    /// Reset layout method
    /// </summary>
    /// <returns></returns>
    public async Task Reset(string? layoutConfig = null)
    {
        var options = GetOptions();
        if (layoutConfig != null)
        {
            options.LayoutConfig = layoutConfig;
        }
        await InvokeVoidAsync("reset", Id, options);
    }

    /// <summary>
    /// Save layout method
    /// </summary>
    /// <returns></returns>
    public Task<string?> SaveLayout() => InvokeAsync<string?>("save", Id);

    /// <summary>
    /// Invokes a JavaScript function using the module reference
    /// </summary>
    /// <param name="identifier">The JavaScript function identifier</param>
    /// <param name="args">Arguments to pass to the JavaScript function</param>
    /// <returns>A Task representing the asynchronous operation</returns>
    private async Task InvokeVoidAsync(string identifier, params object?[] args)
    {
        if (_module != null)
        {
            try
            {
                await _module.InvokeVoidAsync(identifier, args);
            }
            catch (JSDisconnectedException)
            {
                // Component was disposed or connection lost
            }
            catch (TaskCanceledException)
            {
                // Operation was cancelled
            }
        }
    }

    /// <summary>
    /// Invokes a JavaScript function that returns a value using the module reference
    /// </summary>
    /// <typeparam name="T">The return type</typeparam>
    /// <param name="identifier">The JavaScript function identifier</param>
    /// <param name="args">Arguments to pass to the JavaScript function</param>
    /// <returns>A Task containing the result</returns>
    private async Task<T> InvokeAsync<T>(string identifier, params object?[] args)
    {
        if (_module != null)
        {
            try
            {
                return await _module.InvokeAsync<T>(identifier, args);
            }
            catch (JSDisconnectedException)
            {
                return default(T)!;
            }
            catch (TaskCanceledException)
            {
                return default(T)!;
            }
        }
        return default(T)!;
    }

    /// <summary>
    /// Initialized callback method
    /// </summary>
    [JSInvokable]
    public async Task InitializedCallbackAsync()
    {
        if (OnInitializedCallbackAsync != null)
        {
            await OnInitializedCallbackAsync();
        }
    }

    /// <summary>
    /// Panel visible changed callback method
    /// </summary>
    [JSInvokable]
    public async Task PanelVisibleChangedCallbackAsync(string title, bool status)
    {
        if (OnVisibleStateChangedAsync != null)
        {
            await OnVisibleStateChangedAsync(title, status);
        }
    }

    /// <summary>
    /// Lock changed callback method
    /// </summary>
    [JSInvokable]
    public async Task LockChangedCallbackAsync(string[] panels, bool state)
    {
        if (OnLockChangedCallbackAsync != null)
        {
            await OnLockChangedCallbackAsync(panels, state);
        }
    }

    /// <summary>
    /// Splitter callback method
    /// </summary>
    [JSInvokable]
    public async Task SplitterCallbackAsync()
    {
        if (OnSplitterCallbackAsync != null)
        {
            await OnSplitterCallbackAsync();
        }
    }

    /// <summary>
    /// Disposes the component and cleans up JavaScript resources
    /// </summary>
    /// <returns>A ValueTask representing the asynchronous operation</returns>
    public async ValueTask DisposeAsync()
    {
        if (!_disposed)
        {
            if (_module != null)
            {
                try
                {
                    await _module.InvokeVoidAsync("dispose", Id);
                }
                catch (JSDisconnectedException)
                {
                    // Expected when the circuit is disconnected
                }
                catch (TaskCanceledException)
                {
                    // Expected when the operation is cancelled
                }

                await _module.DisposeAsync();
                _module = null;
            }

            if (_interop != null)
            {
                await _interop.DisposeAsync();
                _interop = null;
            }

            _disposed = true;
        }

        GC.SuppressFinalize(this);
    }
}
