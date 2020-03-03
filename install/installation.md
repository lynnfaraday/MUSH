---
layout: module
title: Installation
resource: true
categories: [ install ]
description: Installing the system.
---

## System Requirements

This code runs only on [PennMUSH](http://www.pennmush.org).  Several folks have attempted to get it to run on MUX, but there are subtle differences between the codebases that make it infeasible.

Each release lists what version of PennMUSH it was tested on, although theoretically they should work with any version of Penn.

## Getting a Server

You can get a MU* hosting account from any number of hosting providers.  I used [Genesis MUDs](http://genesismuds.com/) for many years and was always pleased with their service.

You can also run a MU* on a virtual private server (VPS).  I use [Digital Ocean](http://lynnfaraday.github.io/MUSH//install/do-setup.html) for hosting my game.

## Install PennMUSH

First install PennMUSH.  The code has been tested with PennMUSH v1.8.6p0.  Newer versions will probably work, but try at your own risk.

If you're using Digital Ocean, detailed instructions can be found [here](/install/do-setup.html).

## Edit mush.cnf

This codebase requires several settings in `pennmush/game/mush.cnf`.  Find these settings and modify them if needed.  Some may already be set to the appropriate values; that's OK.

       null_eq_zero	yes
       tiny_booleans	no
       tiny_trim_fun	yes
       tiny_math	yes
       empty_attrs  yes
      
       function_side_effects	yes   
       player_name_spaces	no
         
       # You can set it even higher if your census or other commands hit the limit.
       function_invocation_limit 50000
    
       # Add this in the player_flags section
       player_flags    unregistered
          
       # Set these db#s after you've installed the exit and room parents
       ancestor_room	-1
       ancestor_exit	-1

## Edit restrict.cnf

This codebase requires you to restrict people from doing certain commands in `pennmush/game/restrict.cnf`.

       # Disable the mail command.  There are several things to uncomment already
       # in the restrict file.  Look for the phrase 
       #     "Remove the hardcode mail system by uncommenting these"
       restrict_command @mail nobody
       etc.
       
       # Restrict the @tel and home commands, since they mess up IC/OOC
       restrict_command @tel admin "Only staff can teleport.
       restrict_command	home admin " The home command is disabled.  Use +ic/+ooc
       
       # Restrict name changes, since lists go off player name not DB#.
       restrict_command	@name admin " Contact staff to change your name.

## Create a Game Wiz
You should have a special code wizard character to own all the objects.  This ensures that nothing gets accidentally nuked if a player admin is retired, and lets multiple admin log on using that char to install and/or debug.

*Using the #1 character...*

       @pcreate GameWiz=make_a_good_password
       @set *GameWiz=!unregistered
       @set *GameWiz=Wizard
       @power *GameWiz=NO_PAY

Now you can login as GameWiz and continue the installation.

## Installers 

> Do the installation with the **GameWiz** character, not the One character.

You can find the installer versions on the [Releases]({{site.siteroot}}/releases) page.

The release package contains individual installer files for each code system.  Since you are installing for the first time, you'll want to use the installer files in the **Fresh Install** folder.  

All you need to do is execute the commands from the install file in your MUSH client.  You can do this by copying/pasting or using your client's "file quote/upload feature." 

> Upload the installers **one by one** and **in order** according to their file numbers.  Wait for the "Installation Complete" message before proceeding with the next file. 

**IMPORTANT NOTES:**

* Use caution with copy/paste as some editors will try to put extra linebreaks into the code.  I recommend Notepad on Windows or TextEdit on Mac.
* Potato MUSH client has a limitation on copy/paste size.  You'll need to use the file upload instead. 
* You must install ALL modules labeled "CORE", but most of the AddOns are optional.  If you choose to omit certain AddOns, check the documentation just to make sure nothing depends on it.

