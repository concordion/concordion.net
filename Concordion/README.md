# [Concordion.NET](http://www.concordion.org/dotnet/) Build Documentation

Welcome to Concordion.NET.

This project is based on the ATDD / BDD framework [Concordion](http://www.concordion.org/) written in Java. [IKVM.NET](http://www.ikvm.net/) is used to port the Java implementation to the .NET platform. The Java bytecode of Concordion is cross-compiled with [ikvmc](https://sourceforge.net/p/ikvm/wiki/Ikvmc/) to a .NET dll.
You need this project only to rebuild the Concordion.dll from the Java implementation. All other projects use the Concordion.dll from the nuget package.

## Download and Install IKVM

1. To build the Concordion.dll you have to [download IKVM](http://weblog.ikvm.net/).
2. Make sure that ikvmc.exe is on your Windows `PATH` so that it can be called in the post build steps of this project.

## Download and Insert Concordion jar files

To build the Concordion.dll for the .NET platform, you have to copy the jar files into this Visual Studio project.

1. [download Concordion](http://concordion.org/Download.html)
2. extract the jar files from the zip file, copy them into this project, and rename them to match the csproj definition (currently the following branch of Concordion is used: https://github.com/concordion/concordion/tree/IKVM-Base_1.5.1)
  * concordion-1.5.1.jar -> Concordion.jar
  * xom-1.2.5 -> xom.jar

## Build Concordion.dll

1. To build the Concordion.dll run the build.cmd command.

Concordion.dll is available at bin\Debug :-)
