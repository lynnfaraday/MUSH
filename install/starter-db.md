---
layout: module
title: Starter Database Installation
resource: true
categories: [ install ]
description: Installing with the starter database.
---

The easiest way to install the softcode is to use the starter database and a compatible version of PennMUSH.  This contains a minimally-configured installation of every system, the necessary configuration options, and a CodeWiz character.

The latest release (8.2 for PennMUSH 1.8.6) can be found [here](https://github.com/lynnfaraday/MUSH/blob/master/farasoftcode/Releases/FaraMUSHCode%20Starter%20DB%20-%208.2%20for%201.8.6%20p0.zip)

If you're looking for older versions, you can find them [here](https://github.com/lynnfaraday/MUSH/tree/master/farasoftcode/Installers/DBs).


To use the database:

* Install the appropriate version of PennMUSH.
* Place the mush.cnf and restrict.cnf files into the PennMUSH "game" directory.
* Edit the mush.cnf file with your game's name, port, and other custom options.
* Place the chatdb.gz, outdb.gz and maildb.gz files into the PennMUSH "game/data" directory.
* Start PennMUSH.
* Passwords for the wizard characters can be found in the Default Passwords.rtf file.  **Change these passwords prior to letting the general public onto your game.**
* When you log in with GameWiz, you will see a +jobs list telling you all the remaining setup that needs to be done to customize your systems.
