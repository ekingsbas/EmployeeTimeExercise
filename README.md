# EmployeeTimeExercise
EmployeeTimeExercise is a .Net C# Console application exercise for a job application in IOET Inc.

## Overview
### The exercise is:
The company ACME offers their employees the flexibility to work the hours they want. But due to some external circumstances they need to know what employees have been at the office within the same time frame. The goal of this exercise is to output a table containing pairs of employees and how often they have coincided in the office.

Input: the name of an employee and the schedule they worked, indicating the time and hours. This should be a .txt file with at least five sets of data. You can include the data from our examples below:

Example 1:

INPUT:
RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00- 21:00
ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00
ANDRES=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00


OUTPUT:
ASTRID-RENE: 2
ASTRID-ANDRES: 3
RENE-ANDRES: 2

### Highlights
This small application as a monolithical architecture because its size, and its contained in one "Main" class.

## Installation
This applitacion is build for Net Framework 4.5 as target. You need a Visual Studio and .Net Framework 4.5 previously installed in your computer in order to build the application.

## Usage
You need to include in the app.config file the path for the .txt file where the source data is:
```bash
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
    </startup>
<appSettings>
  <add key="sourceFileLocation" value="source-to-my-file.txt"/>
</appSettings>
</configuration>
```
## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License
THere is no special license needed in order to use or share this code. Just gimme some credit.
