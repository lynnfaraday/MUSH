---
layout: module
title: Installation
resource: true
categories: [ install ]
description: Installing the system.
---

## System Requirements

This code runs only on [PennMUSH](http://www.pennmush.org).  Some folks have attempted to get it to run on MUX, but there are subtle differences between the codebases that make it a huge pain.

Each release lists what version of PennMUSH it was tested on, although theoretically they should work with any version of Penn.

## Starter DB

The easiest way to install the softcode is to use the starter database.  This contains a minimally-configured installation of every system, the necessary configuration options, and a CodeWiz character.

You can download the latest starter database package [here](https://github.com/lynnfaraday/MUSH/blob/master/farasoftcode/Releases/FaraMUSHCode%20Starter%20DB%20-%208.1%20for%201.8.5p4.zip).

To use the database:

* Install PennMUSH.
* Open the starter DB ZIP file.
* Place the mush.cnf and restrict.cnf files into the PennMUSH "game" directory.
* Edit the mush.cnf file with your game's name, port, and other options.
* Place the chatdb.gz, outdb.gz and maildb.gz files into the PennMUSH "game/data" directory.
* Start PennMUSH.
* Passwords for the wizard characters can be found in the Default Passwords.rtf file.  **Change these passwords prior to letting the general public onto your game.**
* When you log in with CodeWiz, you will see a +jobs list telling you all the remaining setup that needs to be done to customize your systems.

## Custom Installation

If you don't want to use the starter DB, here are the steps to perform a custom installation.

### Server Config 

The installers will only work right if the following items are set in your **mush.cnf** file.  Find these settings and modify them.

       null_eq_zero	yes
       tiny_booleans	no
       tiny_trim_fun	yes
       tiny_math	yes
       empty_attrs  yes
      
       function_side_effects	yes   
       player_name_spaces	no
   
       # 25000 is the minimum value.  You can set it even higher if your census or other commands hit the limit.
       function_invocation_limit	25000
    
       # Add this in the player_flags section
       player_flags    unregistered

       # In really old versions of pennmush you'll also need to increase the number of global functions
       # If this config option doesn't exist in your version don't worry about it.
      max_global_fns 200
   
       # Set these db#s after you've installed the exit and room parents
       ancestor_room	-1
       ancestor_exit	-1

In the **restrict.cnf** file you'll also want to set these options.

       # Restrict the @tel and home commands, since they mess up IC/OOC
       restrict_command @tel admin "Only staff can teleport.
       restrict_command	home admin " The home command is disabled.  Use +ic/+ooc

       # Restrict name changes, since lists go off player name not DB#.
       restrict_command	@name admin " Contact staff to change your name.

### Code Wiz
You should have a special code wizard character to own all the objects.  This ensures that nothing gets accidentally nuked if a player admin is retired, and lets multiple admin log on using that char to install and/or debug.

*Using the #1 character...*

       @pcreate CodeWiz=make_a_good_password
       @set *CodeWiz=!unregistered
       @set *CodeWiz=Wizard
       @power *CodeWiz=NO_PAY

Now you can login as CodeWiz and continue the installation.

### Installers 

You can find the installer versions on the [Releases](/releases) page.

The release package contains individual installer files for each code system.  These installers will automatically install the systems onto your game.

If you open the release package ZIP file, you'll see two folders, one labeled **FreshInstall** and one labeled **Upgrade**.  
* If you are installing a module for the first time, use the *FreshInstall* installers.
* If you already have a module installed, use the the *Upgrade* installers.  This will preserve any old data, but see the Upgrades document for more info.
* Either copy/paste the contents of the installer into your MUSH window or use your MU client's "quote/upload file" feature, if available.  

**IMPORTANT NOTES:**
* The Install Manager must always be in the inventory of whoever is doing the installations. For that reason, I recommend you always have a central "CodeWiz" character owning and installing all of your softcode objects, as explained above. 
* The install files are numbered in order.  
 * Any file with "CORE" in its name is a Core module and must be installed for the system as a whole to work.
 * Other files are optional Addons, and you can choose whether to install it or not.
 * The CORE modules MUST be installed **in the proper order** or the installation will fail.
* Use caution with copy/paste as some editors will try to put extra linebreaks into the code.  I recommend Notepad on Windows or TextEdit on Mac.
* You can choose installers from both folders if you are upgrading some modules and installing others for the first time.  Just be sure you pick the right installer for each module.