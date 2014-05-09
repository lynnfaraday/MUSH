---
layout: module
title: Events
resource: true
categories: [ addons ]
description: Tracks upcoming MUSH events.
---

## Features 
* Lets players submit and view upcoming events.
* Supports a reminder that you can add to an @aconnect. 


      ------------------------------ Events System ------------------------------
      #   Event                                        Date/Time
      ------------------------------------------------------------------------------
      1.  Premier                                      Tue Feb 09 2010 @ 9pm EST 
      2.  Home                                         Sat Mar 13 2010 @ 7pm EST
      ------------------------------------------------------------------------------

## Customization 
If you are not using my +motd module, you probably want to add the +events/rem command to a global @aconnect so people will be notified of upcoming events.

## Functions 
The module provides the following global functions:

**EVENT_REMINDERS()** - Gets the text to display upcoming events.
