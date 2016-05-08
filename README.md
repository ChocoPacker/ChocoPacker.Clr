# ChocoPacker common libraries

## ChocoPacker.Common 

Contains all the interfaces, attributes and exceptions which should be implemented by plugins.

Shared components:
1. IGeneralConfiguration - interface with configuration, you can use it as constructor dependency in your plugins.
2. IInstallTypeDetector
    - You should decide some constant for installer type detected by your plugin. 
    - Implementation of this interface must return it's value.
    - If provided path to installer isn't installer type you support you must throw "WrongInstallerTypeException"
3. IInstallerInfoProvider
    - Implementation must be marked with SupportsInstallerAttribute, as string argument you must pass supported installer type name.
    - In this interface you have to extract as much data as possible and return it inside InstallerInfo class
    
## ChocoPacker.Loader

Used to load and instantiate plugins.