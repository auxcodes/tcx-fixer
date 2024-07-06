[![Support me on ko-fi](https://pabanks.io/assets/kofi-md.svg)](https://ko-fi.com/H2H1ZZY1Q) &nbsp; [![Support me on Coindrop](https://pabanks.io/assets/coindrop-md.svg)](https://coindrop.to/auxcodes)

# Strave TCX File Fixer

C# projects for fixing TCX files exported from Strava.

## Problem They Solve

Bulk downloaded tcx files from Strava have leading spaces at the beginning of the file, which Garmin Connect deems as invalid XML with the following error message.

*"One of your files was not accepted by the system. Please contact Support for Assistance"*

![alt text](https://github.com/auxcodes/tcx-fixer/blob/master/ReadmeImages/GarminUploadError.png "Image of upload error")

Upon noticing that my Garmin Connect activity history was missing activities that existed in Strava I decided to download the missing activities from Strava.
Downloading activities individually was tedious, so I downloaded all my data.

However, whilst the individually downloaded files uploaded fine to Garmin Connect, the bulk download of activities did not, after some upload tests (To many at once? Already uploaded? Fit, gpx or tcx files?) I finally narrowed it down to the tcx files and compared them to the individually downloaded files.

Using file compare in Notepad++ the only difference was 10 leading spaces at the beginning of the first line. 

![alt text](https://github.com/auxcodes/tcx-fixer/blob/master/ReadmeImages/LeadingSpaces.png "Image of XML file with leading spaces")

After deleting the spaces the file uploaded fine to Garmin Connect.

Now I had thousands of files that need fixing, there was no way I was going to do it manually...

## TcxFixerGui

This project provides a basic gui interface for selecting .tcx files and displaying the output whilst processing the files.

![alt text](https://github.com/auxcodes/tcx-fixer/blob/master/ReadmeImages/TcxFixerGui.PNG "Image of Tcx Fixer GUI App")

## TcxFixerConsole

This was the initial proof of concept project.
Designed to be placed in the directory where your .tcx files are. 
It will find all .tcx files with in the current directory, process them and write updated versions to a new directory.

![alt text](https://github.com/auxcodes/tcx-fixer/blob/master/ReadmeImages/TcxFixerConsole.PNG "Image of Tcx Fixer Console App")
