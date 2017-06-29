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


