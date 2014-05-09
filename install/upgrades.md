---
layout: module
title: Upgrades
resource: true
categories: [ install ]
description: Upgrading to a new version.
---

Beginning with version 5.0, all of the modules support (hopefully) seamless upgrades to new versions.

## Upgrade Steps

*  Do a backup.  All release packages are tested before they go out the door, but you never know!
*  Obtain both the Upgrade and Fresh Install release packages for the version you're upgrading to.   //Note:  You must upgrade to each version in order; you cannot skip straight from 6.0 to 8.0.//
*  See any special upgrade instructions mentioned on the release notes page for your particular version.
*  Log into the game using your GameWiz char (or whoever has the Install Manager objects in their inventory.)
*  Go through the Upgrade installers one by one in sequential order.  //Note: Installers must be done in the right order or they may fail.//
*  If it's a system you're using, copy/paste or upload the file to your game.   //Note:  If using cut/paste, be careful about text editors that automatically word-wrap or insert extra linebreaks.//
* If you get an error about a system not being found, it's probably a new system, or one you had never installed before.  In that case, use the 'Fresh Install' installer for **that system only**.


## Preserving Data
The difference between an upgrade and a full install is that the upgrade will preserve existing data.

* Data created by the system will be safe when doing an upgrade.  For example: bbposts and +jobs.
* Data that I intend for you to configure will also be safe.  For example:  +finger fields and skill lists.  If the help files tell you to configure something, it's going to be safe from upgrades.
* Anything else will be replaced and the DB#'s automatically updated.


## Code Mods
Modifying the softcode will make it very difficult for you to upgrade the systems in the future. See the Modifying Code document for some advice.