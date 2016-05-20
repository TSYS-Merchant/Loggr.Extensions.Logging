Loggr.Extensions.Logging
=======================

[![Build status](https://ci.appveyor.com/api/projects/status/l3snx3wl42pluf8p/branch/master?svg=true)](https://ci.appveyor.com/project/iMobile3/loggr-framework-logging/branch/master) [![NuGet Version](http://img.shields.io/nuget/v/Loggr.Extensions.Logging.svg?style=flat)](https://www.nuget.org/packages/Loggr.Extensions.Logging/)

Log to [Loggr][0] from [Microsoft.Extensions.Logging][1].

Installation
------------

Loggr.Extensions.Logging installs through [NuGet][3]:

```
PS> Install-Package Loggr.Extensions.Logging
```

Configure the Loggr provider through code:

```c#
var factory = new LoggerFactory();
var logger = factory.CreateLogger( "MyLog" );
factory.AddLoggr( "logKey", "apiKey" );
```

In the example we create a new `LoggerFactory` and add the Loggr provider using the `AddLoggr` extension method with a `logKey` and `apiKey` provided by Loggr.

Usage
-----

Log messages to Loggr, just as with every other provider:

```c#
logger.LogInformation( "This is information" );
```

This library currently uses the [loggr-dotnet][2] source directly since their NuGet package doesn't support .NET Core.

License / Support
=================

Copyright 2012-2016 iMobile3, LLC. All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, is permitted provided that adherence to the following
conditions is maintained. If you do not agree with these terms,
please do not use, install, modify or redistribute this software.

1. Redistributions of source code must retain the above copyright notice, this
list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
this list of conditions and the following disclaimer in the documentation
and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY IMOBILE3, LLC "AS IS" AND ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO
EVENT SHALL IMOBILE3, LLC OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE
OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

[0]: http://loggr.net/
[1]: https://github.com/aspnet/Logging
[2]: https://github.com/loggr/loggr-dotnet
[3]: https://www.nuget.org/packages/Loggr.Extensions.Logging
