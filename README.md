[![Gitter](https://badges.gitter.im/AndreasAugustin/DocToMarkdown.svg)](https://gitter.im/AndreasAugustin/DocToMarkdown?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)

[![Linux build Status](https://travis-ci.org/AndreasAugustin/DocToMarkdown.svg?branch=master)](https://travis-ci.org/AndreasAugustin/DocToMarkdown)

[![Windows status](https://ci.appveyor.com/api/projects/status/8xi2i2h5twfj051w?svg=true)](https://ci.appveyor.com/project/AndreasAugustin/doctomarkdown)

DocToMarkdown
=============

[![Join the chat at https://gitter.im/AndreasAugustin/DocToMarkdown](https://badges.gitter.im/AndreasAugustin/DocToMarkdown.svg)](https://gitter.im/AndreasAugustin/DocToMarkdown?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
With this parser you are able to parse your Visual Studio or Xamarin Studio xml outputs into markdown format.
The idea is to get a readable API documentation for your project which can be easily used for your wiki project pages. For setting up the project edit the [App.config](https://github.com/AndreasAugustin/DocToMarkdown/blob/master/src/DocToMarkdown/App.config) file.

---
### Requirements ###
- Target framework **.Net/Mono 4.5**
- This project uses **NLog** as logger. You are able to find some informations about NLog at GitHub https://github.com/NLog/NLog . Informations about the NLog.config configuration file can be found at https://github.com/nlog/nlog/wiki/Configuration-file . If you like to use another logger, just implement the ILogger and ILoggerManager interface and edit the Programm.cs. The rest will be done through dependency injection.

### Start project ###
- Edit the **App.config** file
- If you use the library out of the build folder, just start the **DocToMarkdown.exe** in your console.  

---
### Remarks ###

- Some input came from this [project](https://gist.github.com/lontivero/593fc51f1208555112e0) .
- This project uses NLog as logger. You can find some informations about NLog at GitHub https://github.com/NLog/NLog .   Informations about the NLog.config configuration file can be found at https://github.com/nlog/nlog/wiki/Configuration-file  . If you like to use another logger, just implement the ILogger and ILoggerManager interface and edit the Programm.cs. The rest will be done through dependency injection.


### Examples ###
You are able to find examples for parsed xml files at the [DocToMarkdown wiki pages](https://github.com/AndreasAugustin/DocToMarkdown/wiki). I used the xml which have been created from this project.

---
### Testing ###
- This project uses **NUnit** *(2.6.3 License: http://www.nunit.org/nuget/license.html)* as a testing framework. You are able to find some informations about NUnit at http://www.nunit.org
- As a mocking framework it uses **NSubstitute** *(1.7.2.0 BSD-Licence: http://opensource.org/licenses/bsd-license.php)*. You are able to find some informations about NSubstitute at https://nsubstitute.github.io
- I tested the output of this project with documentation files created with **Visual Studio** (2012 and 2013) and **Xamarin Studio** (5.5.3).

---
### Change log ###
**0.1**
- First released version (pre-alpha)
- Every [recommended tag](http://msdn.microsoft.com/en-us/library/5ast78ax.aspx) of [MSDN](http://msdn.microsoft.com/en-us) is supported (exception is the [*include*](http://msdn.microsoft.com/en-us/library/9h8dy30z.aspx) tag, but this one is not part of the xml documentation)
- It is possible to log messages.
- GitHubFlavored markdown (gfm) and markdown are supported
- It is possible to parse the href attribute of all tags to get a link to external pages
- For markdown it is possible to parse the cref attribute of all tags to get a link to an anchor at the same page (Not possible yet for gfm)
- It is possible to set the App.config file before starting the application.


---
## Some legal stuff ##

Copyright (c) 2014, Andreas Augustin. All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

### External resources ###
- [NLog](https://github.com/NLog/NLog) .
 - Here you are able to find the [license](https://github.com/NLog/NLog/blob/master/LICENSE.txt) .
- [NUnit](http://www.nunit.org) 2.6.3
 - Here you are able to find the [license](http://www.nunit.org/nuget/license.html) .
- [NSubstitute](https://nsubstitute.github.io)
 - Here you are able to find the [license](http://opensource.org/licenses/bsd-license.php) *(BSD)*.
