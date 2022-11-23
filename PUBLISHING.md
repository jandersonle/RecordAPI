# Api Publishing Documentation
---
## Visual Studio Publishing
After a successful final build of the api, right click the project file within visual studio and click publish, from here you will create a publish profile. Select the option to publish to a folder and choose a preferred folder location. On the next page specify you would like a "self-contained" deployment mode, and then choose the desired framework and runtime. Then click publish. If any changes are made to the source code, the publish file will have to be updated by publishing again.
 
#### IIS Module Installation
In order to run any dotnet core applications within IIS the dotnet core hosting module is required on the server. You can download it [here](https://dotnet.microsoft.com/permalink/dotnetcore-current-windows-runtime-bundle-installer).
 
#### IIS Publishing
 
Move the publish folder generated from visual studio to the desired location on the server. Add a new site, select the default application app pool, then specify the path to the publish file. Then choose the desired port and address.  You're done ! The live site should be running and you append "/api/RecordItemController" to the url to access the endpoints.
 
#### Network Rules
 
Depending on the host machine's setup it may require you to establish inbound and outbound rules on the port you selected in order for the site to be accessible by other machines on the same network. If that's the case, open up the group policy management console and within inbound rules create a new inbound rule and then do the same for outbound rules, be sure to specify the same port on both rules.
 
---
 
## Azure Publishing
 
Similar to publishing on an IIS, once we obtain a successful release build of the API within visual studio, we can go to the public menu and add a new publish profile. This time we will select Azure as the target. When prompted, we will select Azure App Service (Windows) as the service we would like to host the application. If needed sign-in with your Azure Account and then select or create the desired app service and app resource group. From there, we will be prompted to either create or use an existing API management service. Once all of that is configured, we can select our deployment type, target framework and target runtime. We can then click on publish and the build pipeline will initiate. Once published, you can login to your Azure account and manage all services associated with this project.

