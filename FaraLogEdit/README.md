FaraLogEdit
===========

RP Log Editor for MUSH

This script is designed to clean up roleplay logs from MUSHes and MUXes.

Features:
--------------
- Accepts a configuration file telling it what things shold be stripped from the log - pages, channel spam, system messages, etc.  You can customize this to your heart'\
s content, and even have multiple config files for different MUSHes.
- The edited file is named with the current date/time, like:  "2007-06-09 -- Title".  This allows you to easily tell edited logs from raw logs.
- The raw log file is not touched at all, so you always have it as a backup.
- The script will put blank lines between poses (unless they already have one) to space them out and make them pretty.
- It adds a timestamp to the top of the file so you know when the log was from.

See the sample configuration file for instructions on how to edit it or set up your own.

Installation
--------------
This is a perl script, and thus requires Perl.
-  Mac OSX and Unix should have Perl already installed.
-  On Windows, you will probably have to download and install a Perl package such as ActivePerl (available from http://www.activestate.com/Products/activeperl/).

Usage
--------------
The general syntax for the script is:
       faralogedit.pl <config filename> <raw filename> <edited filename>

       Example:  faralogedit.pl mush.cfg "My Log" "Cool Title"
       *** if any of the filenames have spaces in them, you need to put them in double quotes, as shown

How you run a Perl script depends on your OS and your perl package.  In general, put the script in the same directory as your logs, and then:

- On Windows, open up a Windows Command Prompt, go to the directory with your logs, and type:
       perl faralogedit.pl <config filename> <raw filename> <edited filename>
- On Mac OSX or Unix, open up a Terminal window, go to the directory with your logs, and type:
       ./faralogedit.pl <config filename> <raw filename> <edited filename>

   If you get permission denied errors, type:
       chmod +x faralogedit.pl
   This will give it permission to run.
   
Disclaimers
--------------
This code is provided as-is without warranty.

Credits
--------------
Inspired by the original logedit.pl script written by Javelin (available at http://ftp.pennmush.org/Accessories/)




