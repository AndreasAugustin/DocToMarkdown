DocToMarkdown
=============
With this parser you are able to parse your Visual Studio or Xamarin Studio xml outputs into markdown format.
The idea is to get a readable API documentation for your project which can be easily used for your wiki project pages. For setting up the project edit the [App.config](https://github.com/AndreasAugustin/DocToMarkdown/blob/master/src/DocToMarkdown/App.config) file.
    

### Remarks ###

- Some input came from this [project](https://gist.github.com/lontivero/593fc51f1208555112e0) .
- This project uses NLog as logger. You can find some informations about NLog at GitHub https://github.com/NLog/NLog .   Informations about the NLog.config configuration file can be found at https://github.com/nlog/nlog/wiki/Configuration-file  . If you like to use another logger, just implement the ILogger and ILoggerManager interface and edit the Programm.cs. The rest will be done through dependency injection.


### Examples ###
You are able to find examples for parsed xml files at the [DocToMarkdown wiki pages](https://github.com/AndreasAugustin/DocToMarkdown/wiki). I used the xml which have been created from this project.

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
The project depends on [NLog](https://github.com/NLog/NLog) .
Here you are able to find the [license](https://github.com/NLog/NLog/blob/master/LICENSE.txt) .
