# FakerOfData [![Build status](https://ci.appveyor.com/api/projects/status/iej2k1u2ie8gyiud/branch/master?retina=true)](https://ci.appveyor.com/project/DavidTellander/fakerofdata)

**FakerOfData** is a .Net library which lets you generate random data and put it in e.g. a database or a text file. How the data is generated is specified using a simple [internal DSL](http://martinfowler.com/bliki/InternalDslStyle.html), which takes advantage of the [dynamic features](http://msdn.microsoft.com/en-us/magazine/gg598922.aspx) of C#.

## Install

**FakerOfData** can be used from your .Net application or in a [scriptcs](http://scriptcs.net/)
script. In both cases, the simplest way to install is via [Nuget](). 

### Visual studio Package manager 

In the _Package management console_ just type:

```posh
install-package FakerOfData.Core
```

This will install the core package, which contain the DSL and some basic random value generators. 

### scriptcs

If you want to write fake data generation scripts in scriptcs just use the `-install` option:

```bat
scriptcs -install FakerOfData.Core
```

## Usage

Let's start with a complete code example of how to use FakerOfData.

**NOTE on scriptcs:** Since the current Roslyn CTP does not support `dynamic`, you must use the `--modules mono` when you run scriptcs. See the [release notes of scriptcs v0.10](https://github.com/scriptcs/scriptcs/releases/tag/v0.10)

```csharp
// Start by declaring a class representing your random data
class Employee {
    public int Id { get; set; }
    public DateTime HireDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Bio { get; set; }
}

// Tell the generator where it should put the generated data
// in this case we just write it to standard out
var consoleOutput = new CsvDestination("\t", _ => Console.Out);
Generator.Destination = consoleOutput;

// Define a counter, EmployeeId which starts at 1000
Counter.Next.EmployeeId = 1000;

// Let's generate some data
Generator
    // It is in the Of method you specify how you want 
    // your random data. It accepts a variable list of
    // Action<T> which are called in order starting with 
    // a new instance of T 
    .Of<Employee>(
        // The counter is incremented by one on each access
        row => row.Id = Counter.Next.EmployeeId,
        // The Random property of Some is dynamic, Date is 
        // a registrered member of type IRandomValue which
        // generates random dates in a specified range.
        // Random value generators may accept an options object
        row => row.HireDate  = Some.Random.Date(
            new {from = 10.Years().Ago(), to = DateTime.Now}),
        // If registered member is found on Random matching a
        // name, the special Strings directory is searched for
        // a .txt file matching the name. If found, a random
        // line from that file is returned. In this case we 
        // should have the files FirstName.txt and LastName.txt
        // in the Strings directory.
        row => row.FirstName = Some.Random.FirstName,
        row => row.LastName  = Some.Random.LastName,
        // And of course the library comes with a lorem ipsum
        // generator
        row => row.Bio       = Lorem.Ipsum(50))
    // The Of method returns an infinite sequence of objects 
    // so we better use the Linq Take method to get the number
    // of random items we want
    .Take(5)
    // Push the 5 random Employees to the registered destination,
    // in our case - the console.
    .Load();
```

You start by deciding **where** you want to store your data, e.g. a text file or a database. The `Generator` class has a public static property property - `Destination` - which must be set to an instance of a class implementing the `IDestination` interface. The example uses the `TextDestination` from the `FakerOfData.TextDestination` package.

The next step is to evalutate what **kind of data** you want FakerOfData to generate. Create a simple data class with public properties which represents the fields. It is up to the `IDestination` to handle the names. E.g. the `FakerOfData.DbDestination` expects the class' name to be the name of a table in the database and the property names to be the names of columns in that table.

Now it is time to set up what random data each field should be set to. The `Of<TType>` method on the `Generator` accepts a variable list of `Action<TType>` functions which are responsible for setting properties of the generated objects. For each new instance, the actions are called in order with the instance from the previous action as the argument.

The return value of the `Of<TType>` method is an [infinite sequence](https://www.google.se/#q=infinite+sequence+c%23) of `TType` instances, so we use the `Take` **Linq** method to grab as many as we want. 

And finally, call the `Load` method on the sequence of new random objects to send them off to the `IDestination` you set up earlier.

##Links

- [Project Huboard](https://huboard.com/derantell/FakerOfData/)
