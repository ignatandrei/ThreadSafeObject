# ThreadSafeObject
Making any call to a function of an object thread safe
NuGet Package at :
https://www.nuget.org/packages/ThreadSafeObject/

The solution contain tests and 2 console project for .NET Core and .NET Framework 4.5.1

The usage is pretty easy

Let's say you have this

 ##### Calculation c = new Calculation();
  
#####  c.Add();

And you want 
  
 ##### .Add 
  
to be thread safe

In the Package Manager Console write:

Install-Package ThreadSafeObject

Then modify your code to:

 ##### Calculation c = new Calculation();
  
 ##### dynamic ts = new ThreadSafe(c);
  
 ##### ts.Add();


More details at 
http://msprogrammer.serviciipeweb.ro/2017/07/10/making-any-call-to-a-function-of-an-object-thread-safe/


# Support this software

This software is available for free and all of its source code is public domain.  If you want further modifications, or just to show that you appreciate this, money are always welcome.

[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://paypal.me/ignatandrei1970/25)

* $5 for a cup of coffee
* $10 for pizza 
* $25 for a lunch or two
* $100+ for upgrading my development environment


