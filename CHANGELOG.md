# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)

## [Ideas/Upcoming]
* Switch Animations

## [Unreleased]

## [1.6.0] - 2021-08-01
* Added Owner overloads for the system dialogs
* Register a window as main window for optional fallback for modal windows, dialogs and messageboxes

## [1.5.0] - 2021-07-21
### Added
* UIDispatcher in Utils to have an easy access to the UIDispatcher in ViewModels
* ColorPicker
* FontPicker
* Support control caching in the NavigationPresenter
* Support user control switch abort by pending changes and other reason

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
  