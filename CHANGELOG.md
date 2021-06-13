# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)

## [Unreleased]

## [1.0.0] - 2021-06-13
### Added
* DialogProvider (Open of save file, open file or browse folder dialogs)
* MessageBoxProvider (Display of message boxes)
* InvisiblePleaseWaitProvider (A default implementation for the IPleaseWaitProvider)
* WindowProvider (Create and keep of Windows instances)
* DisplayControl (Display a ViewModel with usage of the view by resources)  
  * Handling of IAsyncLoader
  * Handling of IDelayedAsyncLoader
  * Display of PleaseWait (for IDelayedAsyncLoader)
  * Optional auto call of IDisposable ViewModels
* NavigationPresenter (Display of a user control called by NavigationService)
  * Handling of IAsyncLoader
  * Handling of IDelayedAsyncLoader
  * Display of PleaseWait (for IDelayedAsyncLoader)
  * Optional auto call of IDisposable ViewModels
* NavigationService (Main service for navigate to windows and user controls out of viewmodels)
  * Show modal windows
  * Show non modal windows
  * Close windows out ot viewmodels
  * Show a user control include
  * Show message box
  * Show system dialogs
  * Handling of IAsyncLoader
  * Handling of IDelayedAsyncLoader
  * Display of PleaseWait (for IDelayedAsyncLoader)
  