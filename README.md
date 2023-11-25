# TFL Road Status console application

## About The Solution

This solution has been created based on the specification word document provided by TFL.

The overall idea is to provide a console application that when given a valid Road Id will return its current state of disruption.

## Getting Started

The following section makes the assumption that you have either downloaded the solution from [the github repo (i-scott/tfl)](https://github.com/i-scott/tfl) or have been provided with a zip file that you have extracted to your local file system.

<!-- GETTING STARTED -->
### Prerequisites

In order to build and run this console application you will need;

* .Net7 (minimum)  
  https://dotnet.microsoft.com/en-us/download/dotnet/7.0

* TFL Account Subscription Information added to `appsettings.json`
  _Available from the [API-Portal](https://api-portal.tfl.gov.uk/)_
  
  ```sh
  "security": {
      "appId": "<subscription name>",
      "primaryAppKey": "<primary key>",
    }
  ```
  
## Doing stuff

This section makes the assumption you have opened a terminal window and have navigated into the solution folder `RoadStatus`. In this folder you should see the `RoadStatus.sln` file

### Running The Tests

This solution does have a test library to run the tests using

```sh
dotnet test
```

or

```sh
dotnet test .\TFLRoadStatus.Tests\
```

### Building the solution

Rather than build the solution we are going to publish the solution to a folder where we can run the program, to do this we type the following

```sh
dotnet publish RoadStatus -c Release -o ..\bin
```

This will build the code and then publish the executable we need into a bin folder.

### Running the application

Assuming you are still in the RoadStatus solution folder, navigate into the bin folder created when publishing

```sh
cd ..\bin
```

#### Adding security information

Before you can run the console application you will need to create a subscription to the Road API on the API-Portal.  You should make a note of the Subscription Name (`appId`) and the Primary key (`primaryAppKey`) as these will need to be added into `appsettings.json` file located in the bin folder.

Using a text editor of your choice open the `appsettings.json` file and update the following

```sh
    "security": {      
      "appId": null,
      "primaryAppKey": null
    }
```

Replace the null values with required values

Now you can run the application as follows

```sh
.\RoadStatus _roadid_
or 
.\RoadStatus,exe _roadid_
```

e.g.

```sh
.\RoadStatus A2
```

## Assumptions

The following assumptions where made

1. Only one Road ID would be provided or retrieved.  _Although the API does allow for multiple, the brief implies only one_
2. There is no need to Retries, this of course could be achieved using something like `Polly`
3. Security information is actually required.  Found the application does still work without subscription information.  However if they do not exist in the `appsettings.json` the program will through an expected exception

## Improvements

Some maybe improvements

1. Improve the logging so that whilst debugging output is more verbose
2. Introduce Retries (maybe overkill, as this can be run over and over easily)
3. Provide an option to get all the known road ID's so they can be fed into the app
4. Provide a more robust commandline parser, rather than the really basic thing thats there



