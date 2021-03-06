SniffCore.Navigation
===

SniffCore.Navigation follows the idea of "*It shall be async*".  
It brings the possibility to show windows, user controls and dialogs in an MVVM environment.  
Each VM can have an async ctor, cancel window opening, show loading progress and many more.

## Overview
The SniffCore.Navigation repository consists of multiple assemblies.  
The main assembly SniffCore.Navigation depends on all of them.

|Assembly|Content
|-|-
|SniffCore.Navigation.Dialogs|This assembly provides ways how to show system dialogs for files or folders.
|SniffCore.Navigation.MessageBoxes|This assembly provides ways how to show message boxes.
|SniffCore.Navigation|This is the main assembly to show windows, user controls, dialogs and work with them.
|SniffCore.Navigation.PleaseWaits|This assembly provides ways how to show please wait overlays.
|SniffCore.Navigation.Windows|This assembly provides ways how to create and show windows.

## Getting Started
First you register all needed assemblies in your IOC container like Unity
```csharp
_unityContainer.RegisterSingleton<IWindowProvider, WindowProvider>();
_unityContainer.RegisterType<IDialogProvider, DialogProvider>();
_unityContainer.RegisterType<IMessageBoxProvider, MessageBoxProvider>();
_unityContainer.RegisterType<IPleaseWaitProvider, PleaseWaitProvider>();
_unityContainer.RegisterType<INavigationService, NavigationService>();
```
Then register window and user controls with pairs in the WindowProvider
```csharp
var windowProvider = (WindowProvider) _unityContainer.Resolve<IWindowProvider>();

windowProvider.RegisterWindow<MainView>("MainView");
windowProvider.RegisterWindow<SubView>("SubView");

windowProvider.RegisterControl<DialogsView>("DialogsView");
windowProvider.RegisterControl<DisplayControlView>("DisplayControlView");
```
then you can show windows or user controls or more using the INavigationService
```csharp
var navigationService = _unityContainer.Resolve<INavigationService>();
var vm = _unityContainer.Resolve<MainViewModel>();
navigationService.ShowWindowAsync("MainView", vm);
```

## How To
* [NuGet](https://www.nuget.org/packages/SniffCore.Navigation)
* [Documentation](http://documentation.sniffcore.com/)
* [Website](http://sniffcore.com)

## License

Copyright (c) David Wendland. All rights reserved.  
Licensed under the MIT License. See LICENSE file in the project root for full license information.
