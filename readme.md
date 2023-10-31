# React Budget App
This project provides a web application that can be used to track spending and monthly budgets. The frontend is built using React, and the backend is built using a .NET Core REST API.

### Configuration
Various settings about this project can be controlled using the configuration file, found under the Configuration directory. This repository includes a "sample-config.json" file that demonstrates the available configuration options. To setup the application, rename the "sample-config.json" file to "config.json" and set up the different configuration settings as needed.

### Deployment
This project supports running two instances of the application with separate databases. This is helpful for testing code changes if you choose to make code changes, and the "Test" instance can be fully ignored if you plan on using the project as is.

The application can be deployed using the "Deploy-Test-Version" and "Deploy-Published-Version" command line functions found in either "Scripts.ps1" or "Scripts.sh". The "Deploy-Test-Version" command line function will deploy the test version of the application, which uses the test configuration settings. The "Deploy-Published-Version" command line function will deploy both the test and published versions of the application to ensure that both versions are in sync.