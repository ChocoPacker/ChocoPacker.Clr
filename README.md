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

## ChocoPacker.MSI
Tools for MSI installer interactions.

1. Support MSI/MSP packages.
2. Support uninstall detection only for MSP packages which update single MSI.
3. May fail with advanced MSP scenarios.

This software based on DTF:

https://github.com/wixtoolset/wix3/tree/develop/src/DTF

But as DTF available only for .Net Framework initially this port was used:

https://github.com/nitridan/dtfcoreclr.Linq

## ChocoPacker.SevenZip

Wrapper for 7z executable. Used inside ChocoPacker

### Usage example:

```csharp
var extractor = new SevenZipExtractor(@"C:\7zip\7za.exe");           
var archive = extractor.OpenArchive(@"C:\myarchive.zip");
using (archive)
{
   var file = archive.GetArchiveFiles().First(x => x.RelativePath == "file1.xml");
   archive.ExtractFile(file, @"C:\temp\myfile.xml"); 
}
```

Usage exmple with streams:

```csharp
var extractor = new SevenZipExtractor(@"C:\7zip\7za.exe");           
var archive = extractor.OpenArchive(@"C:\myarchive.zip");
using (archive)
{
   var file = archive.GetArchiveFiles().First(x => x.RelativePath == "file1.xml");
   archive.ExtractFile(file, @"C:\temp\myfile.xml");
   using (var stream = archive.ExtractFile(file))
   {
       var reader = new StreamReader(stream);
       var content = reader.ReadToEnd();
       Console.WriteLine(content);
   }
}
```

## ChocoPacker.Burn
Tools for Burn installer interactions.