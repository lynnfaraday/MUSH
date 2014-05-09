---
layout: module
title: Idle Purger
resource: true
categories: [ addons ]
description: A system for idle-nuking players.
---

## Features 
* Allows you to configure the idle timeout.
* Provides the list of idle players for review before nukage.
* Allows you to specify a list of 'protected' players who will never be nuked.  (guests, staffers, builders, FCs, etc.)
* Performs necessary cleanup activities when someone's nuked, like removing them from mailing lists or housing.

This system is not mine; it was originally Coded by Shofari@B5MUSH while I was headwiz there.  I have expanded it a bit and integrated it to play nice with my other systems.

## Customization 
This data can be customized on the **Idle Purger** object:

Idle Timeout -  Someone will appear in the list for idle purging when they haven't logged in for this many days.

     &IDLETIME Idle Purger=5184000

Protected List - This is a list of special characters (staff NPCs, feature chars, game wiz characters, builders, guests, etc.) who should never appear on the idle purge list no matter how long they've been idle.  (You can also set this via softcoded commands.  See +shelp +idle.)
  
     &IDLEPROTECT Idle Purger=#1

**ALWAYS RE-RUN THE +idle/start COMMAND IF YOU CHANGE THE PROTECTED LIST.**

List Purge Actions - Triggered once for the entire list of purged characters, which is passed in as \%0.  By default, this will post the list of idle-nuked players to the Announcements bbs.
   
       &LIST_PURGE_ACTIONS Idle Purger+ +bbpost Announcements/Idle PurgeThe following characters have been idle-nuked for inactivity.%R[iter(%0,%R[name(##)])]

Player Purge Actions - Triggered once per character who's nuked.  The character DB# is passed in as \%0 and the person doing the purging is passed as \%1.  You can use it to clean up housing, gear, or whatever.

      &PLAYER_PURGE_ACTIONS Idle Purger+ @tr me/TR_CLEAR_MAIL%0;@tr me/TR_REMOVE_ALT+ %0;@tr me/TR_REMOVE_FRIENDS%0; @fo %1=@nuke %0

Mailing List Object - If you're using FaraMail, set this attribute to point to your mailing list object, and the Idle Purger will remove nuked players from mailing lists.

     &MAILING_LIST_OBJECT Idle Purger=#1234

## Functions
None
