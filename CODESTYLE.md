> #### NOTE: This code style standard for LTW is a work in progress and much of the code does not currently conform to these rules.  This is something that will be addressed by the core team.

# Introduction
As the LTW project gains more traction and becomes more widely used, we aim to provide a more professional and consistent look to the large amount of source now in the project.  It was a broadly supported decision by the core development team to follow the Microsoft coding guidelines (the default provided in Visual Studio's C# editor).  These coding guidelines listed below are based on a [MSDN blog post](http://blogs.msdn.com/b/brada/archive/2005/01/26/361363.aspx) from 2005 by Brad Abrams describing the internal coding guidelines at Microsoft, with some changes to suit our project.
# Coding Guidelines
## Tabs & Indenting
Tab characters (\0x09) **should** be used in code. All indentation should be done with Tab Size of 4.
## Bracing
Open braces should always be at the beginning of the line after the statement that begins the block. Contents of the brace should be indented by 4 spaces. Single statements **should** have braces. For example:
```cs
if (someExpression)
{
	DoSomething();
	DoAnotherThing();
}
else
{
	DoSomethingElse();
}
```

`case` statements should be indented from the switch statement like this:
```csharp
switch (someExpression) 
{
	case 0:
		DoSomething();
		break;
	case 1:
		DoSomethingElse();
		break;
	case 2: 
	{
		int n = 1;
		DoAnotherThing(n);
	}
	break;
	case 3:
	{
		DoSomething();
		break;
	}
}
```

Braces also **should** be used for single statement blocks immediately following a `for`, `foreach`, `if`, `do`, etc. The single statement block should always be on the following line and indented by Tab. This increases code readability and maintainability.
```
for (int i = 0; i < 100; ++i)
{
	DoSomething(i);
}
```

## Single line property statements
Single line property statements can have braces that begin and end on the same line. This should only be used for simple property statements.  Add a single space before and after the braces.
```
public class Foo
{
	int bar1;

	public int Bar1
	{
		get { return bar1; }
		set { bar1 = value; }
	}

	public int Bar2 { get; set; }
}
```

## Commenting
Comments **should** be used to describe intention, algorithmic overview, and/or logical flow.  It would be ideal if, from reading the comments alone, someone other than the author could understand a function's intended behavior and general operation. While there are no minimum comment requirements (and certainly some very small routines need no commenting at all), it is best that most routines have comments reflecting the programmer's intent and approach.

Comments must provide added value or explanation to the code. Simply describing the code is not helpful or useful.
```
	// Wrong
	// Set count to 1
	count = 1;

	// Right
	// Set the initial reference count so it isn't cleaned up next frame
	count = 1;
```

### Copyright/License notice
Each file **should** start with a copyright notice. This is a short statement declaring the project name and copyright notice, and directing the reader to the license document elsewhere in the project. To avoid errors in doc comment builds, avoid using triple-slash doc comments.
```
// Last Testament of Wanderers 
// Copyright (C) 2019 - 2021 ALiwoto
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of the source code.
```

### Documentation Comments
All methods should use XML doc comments. For internal dev comments, the `<devdoc>` tag should be used.
```
public class Foo 
{
	/// <summary>Public stuff about the method</summary>
	/// <param name="bar">What a neat parameter!</param>
	/// <devdoc>Cool internal stuff!</devdoc>
	public void MyMethod(int bar)
	{
		...
	}
}
```

### Comment Style
The // (two slashes) style of comment tags should be used in most situations. Wherever possible, place comments above the code instead of beside it.  Here are some examples:
```
	// This is required for WebClient to work through the proxy
	GlobalProxySelection.Select = new WebProxy("http://itgproxy");

	// Create object to access Internet resources
	WebClient myClient = new WebClient();
```

## Spacing
Spaces improve readability by decreasing code density. Here are some guidelines for the use of space characters within code:

**Do** use a single space after a comma between function arguments.
```
Console.In.Read(myChar, 0, 1);		// Right
Console.In.Read(myChar,0,1);		// Wrong
```
**Do not** use a space after the parenthesis and function arguments.
```
CreateFoo(myChar, 0, 1);			// Right
CreateFoo( myChar, 0, 1 );			// Wrong
```
**Do not** use spaces between a function name and parentheses.
```
CreateFoo()							// Right
CreateFoo ()						// Wrong
```
**Do not** use spaces inside brackets.
```
x = dataArray[index];				// Right
x = dataArray[ index ];				// Wrong
```
**Do** use a single space before flow control statements.
```
while (x == y)						// Right
while(x==y)							// Wrong
```
**Do** use a single space before and after binary operators.
```
if (x == y)							// Right
if (x==y)							// Wrong
```
**Do not** use a space between a unary operator and the operand.
```
++i;								// Right
++ i;								// Wrong
```
**Do not** use a space before a semi-colon. 
**Do** use a space after a semi-colon if there is more on the same line.
```
for (int i = 0; i < 100; ++i)		// Right
for (int i=0 ; i<100 ; ++i)			// Wrong
```

## Naming
Follow all .NET Framework Design Guidelines for both internal and external members. Highlights of these include:
* **Do not** use Hungarian notation
* **Do** use an underscore prefix for member variables, e.g. "_foo"
* **Do** use camelCasing for member variables (first word all lowercase, subsequent words initial uppercase)
* **Do** use camelCasing for parameters
* **Do** use camelCasing for local variables
* **Do** use PascalCasing for function, property, event, and class names (all words initial uppercase)
* **Do** prefix interfaces names with "I"
* **Do not** prefix enums, classes, or delegates with any letter

The reasons to extend the public rules (no Hungarian, underscore prefix for member variables, etc.) is to produce a consistent source code appearance. 
In addition, the goal is to have clean, readable source. Code legibility should be a primary goal.

## File Organization
* Source files should contain only one public type, although multiple internal types are permitted if required.
* Source files should be given the name of the public type in the file.
* Directory names should follow the namespace for the class after `LTW`. For example, one would expect to find the public class `LTW.Constants.WotoTools` in **LTW\Constants\WotoTools.cs**.
* Class members should be grouped logically, and encapsulated into regions:
```
//-------------------------------------------------
Constant's Region
//-------------------------------------------------
static Properties Region
//-------------------------------------------------
Properties Region
//-------------------------------------------------
static field's Region
//-------------------------------------------------
field's Region
//-------------------------------------------------
static event field's Region
//-------------------------------------------------
event field's Region
//-------------------------------------------------
Constructor's Region
//-------------------------------------------------
Destructor's Region
//-------------------------------------------------
```
and as for methods:
```
//-------------------------------------------------
Initialize Method's Region
//-------------------------------------------------
Graphical Method's Region
//-------------------------------------------------
event Method's Region
//-------------------------------------------------
overrided Method's Region
//-------------------------------------------------
ordinary Method's Region
//-------------------------------------------------
Get Method's Region
//-------------------------------------------------
Set Method's Region
//-------------------------------------------------
```
* lines should be used between regions, until the 60th character of that line.
* Using statements **should** be before the namespace declaration.
```
using System;

namespace MyNamespace 
{
	public class MyClass : IFoo 
	{
		//-------------------------------------------------
		#region Constant's Region
		public const int constInt = 10;
		#endregion
		//-------------------------------------------------
		#region static Properties Region
		/// <summary>
		/// The Big Father of the <see cref="GraphicElement"/>!
		/// </summary>
		public static GameClient BigFather
		{
			get => ThereIsConstants.Forming.GameClient;
		}
		#endregion
		//-------------------------------------------------
		#region Properties Region
		public virtual WotoRes MyRes { get; set; }
		#endregion
		//-------------------------------------------------
		#region Constructor Region
		public MyClass()
		{
			...
		}
		#endregion
		//-------------------------------------------------
		#region Destructor's Region
		~MyClass()
		{
			...
		}
		#endregion
		//-------------------------------------------------
		#region ordinary Methods
		void DoSomething()
		{
			...
		}

		void FindSomething()
		{
			...
		}
		#endregion
		//-------------------------------------------------
	}
}
```

# Useful Links
[C# Coding Conventions (MSDN)](http://msdn.microsoft.com/en-us/library/ff926074.aspx)
