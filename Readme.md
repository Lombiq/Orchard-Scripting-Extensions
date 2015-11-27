# Orchard Scripting Extensions Readme



## Project Description

Core module for running scripts inside Orchard.


## Documentation

**This module depends on [Helpful Libraries](https://github.com/Lombiq/Helpful-Libraries). Please install it first!**

**WARNING: This module is only compatible with Orchard 1.6 or greater!**

Scripting Extensions enhances Orchard's scripting capabilities by adding numerous new features for scripting support. Together with [PHP Scripting Extensions](https://github.com/Lombiq/Orchard-Scripting-Extensions-PHP) you can use it to run PHP scripts inside Orchard for example.

**Note:** this module only contains common services but no scripting runtime. Check out the following engines and install one or more of them:

- [PHP](https://github.com/Lombiq/Orchard-Scripting-Extensions-PHP)
- [.NET (C#/VB)](https://github.com/Lombiq/Orchard-Scripting-Extensions-DotNet)
- [JavaScript](https://github.com/Lombiq/Orchard-Scripting-Extensions-JavaScript)


## Module overview

This module consists of two features:

- Orchard Scripting Extensions: Core (OrchardHUN.Scripting): contains the core of scripting extensions
- Orchard Scripting Extensions: Rules (OrchardHUN.Scripting.Rules): adds script running rules to the Orchard Rules engine


## Core feature

This feature adds basic services for script management and also adds a scripting testbed to the dashboard.

### Generic services

Orchard Scripting Extensions include multiple generic services (described below) that automatically use every scripting engine existing and enabled in the Orchard instance. By implementing the simple IScriptingRuntime interface you can extend the set of scripting engines with your own.  
To use scripting engines from your own code, use IScriptingManager.

### Script content types

Scripts are regular content items. There are two script content types:

- Script: this one stores a script source code written in the selected language.
- Composite Script: Composite Scripts can reference other script content items. When a Composite Script is run, all the other scripts referenced by it will be run in the order which they are referenced.

Scripts' editors are syntax-highlighted thanks to the [ACE editor](http://ace.ajax.org/).

### Testbed

The testbed is a script editor that you can access from under the "Scripting" menu on the admin site. You can just write a script there or select an existing script and run it for testing purposes.  
The testbed uses dynamic pages from [Helpful Libraries](https://github.com/Lombiq/Helpful-Libraries). This makes pages like the testbed fully extensible and customizable like content items.


## Scripting Rules

This feature adds scripting-related extensions to the Rules engine.

### Script Execution Action

This action can be used to run an arbitrary script when an event fires. You can e.g. hook into the content item life cycle and run a script when a content item is published.


## Adding your own scripting engine

By implementing the simple IScriptingRuntime interface you can write your own interface that can then used with all the common scripting features. Take a look at [PHP Scripting Extensions](https://github.com/Lombiq/Orchard-Scripting-Extensions-PHP) for an example.

The module's source is available in two public source repositories, automatically mirrored in both directions with [Git-hg Mirror](https://githgmirror.com):

- [https://bitbucket.org/Lombiq/orchard-scripting-extensions](https://bitbucket.org/Lombiq/orchard-scripting-extensions) (Mercurial repository)
- [https://github.com/Lombiq/Orchard-Scripting-Extensions](https://github.com/Lombiq/Orchard-Scripting-Extensions) (Git repository)

Bug reports, feature requests and comments are warmly welcome, **please do so via GitHub**.
Feel free to send pull requests too, no matter which source repository you choose for this purpose.

This project is developed by [Lombiq Technologies Ltd](http://lombiq.com/). Commercial-grade support is available through Lombiq.