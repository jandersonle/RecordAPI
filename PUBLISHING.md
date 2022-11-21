# Api Publishing Documentation
---
## Visual Studio Publishing
After a successful final build of the api, right click the project file within visual studio and click publish, from here you will create a publish profile. Select the option to publish to a folder and choose a preferred folder location. On the next page specify you would like a "self-contained" deployment mode, and then choose the desired framework and runtime. Then click publish. If any changes are made to the source code, the publish file will have be be updated by publishing again.

## IIS Module Installation
In order to run any dotnet core applications within IIS the dotnet core hosting module is required on the server. You can download it [here](https://dotnet.microsoft.com/permalink/dotnetcore-current-windows-runtime-bundle-installer).

## IIS Publishing

Move the publish file generated from visual studio to the desired location on the server. Add a new site, select the default application app pool, then specify the path to the publish file. Then choose the desired port and address.  You're done ! The live site should be running and you append "/api/RecordItemController" to the url to access the endpoints.

