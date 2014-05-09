---
layout: module
title: Install Manager
resource: true
categories: [ core ]
description: Helps to install and manage all the other systems.
---

The install manager is the lynchpin for installing all of the softcode modules.

**IMPORTANT:**  The Install Manager must always be in the inventory of whoever is doing the installations.  For that reason, I recommend you always have a central "CodeWiz" character owning and installing all of your softcode objects, as explained in Installation.

## Features 
* Allows you to install new modules and upgrade existing ones to new versions without affecting your existing data.  See Upgrades.
* Shows you which module are installed and their versions.
* Allows you to uninstall a module.
* When a module is installed, it also sets up help files (via +help), cron jobs (via Myrddin's Cron), and player attributes (via Player Setup).

         ------------------------------------------------------------------------------
         Help System Install Information
 
         Object                   DB#       Version
         HELP_CMD                 #5        5.0  
         HELP_DB                  #4        5.0
         ------------------------------------------------------------------------------

## Setup 
This data can be configured on the **Install Manager** object:

* MASTER_ROOM - Set this to be the DB# of your master room (default is #2).

## Functions
The install manager registers a few global functions for use by the installers, but you should never call them directly.