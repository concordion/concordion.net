# [Concordion.NET](http://www.concordion.org/dotnet/) Build Documentation

Welcome to Concordion.NET.

This project is based on the ATDD / BDD framework [Concordion](http://www.concordion.org/) written in Java. [IKVM.NET](http://www.ikvm.net/) is used to port the Java implementation to the .NET platform. The Java bytecode of Concordion is cross-compiled with [ikvmc](https://sourceforge.net/p/ikvm/wiki/Ikvmc/) to a .NET dll.

## Download and Install IKVM

1. To build the Concordion.dll you have to [download IKVM](http://weblog.ikvm.net/).
2. Make sure that ikvmc.exe is on your Windows `PATH` so that it can be called in the post build steps of this project.

## Download and Insert Concordion jar files

To build the Concordion.dll for the .NET platform, you have to copy the jar files into this Visual Studio project.

1. [download Concordion](http://concordion.org/Download.html)
2. extract the jar files from the zip file, copy them into this project, and rename them to match the csproj definition
  * concordion-1.5.1.jar -> Concordion.jar
  * xom-1.2.5 -> xom.jar

You are ready to work develop Concordion.NET with Visual Studio:-)