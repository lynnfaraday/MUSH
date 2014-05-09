---
layout: module
title: Alts
resource: true
categories: [ addons ]
description: Lets players track their alternate characters.
---

## Features 
* Allows players to see if their alts have new mail.
* Alts are hidden to staff only.
* (Optionally) Notifies staff when alts are registered or unregistered.
* The BBS will automatically mark bbs messages as read on all your alts.


       ---------------------------------------------------------------------
       Alt Mail Report
       Lynn           0 unread messages 
       Faraday        2 unread messages
       ---------------------------------------------------------------------

## Customization 
Set the NOTIFY_STAFF attribute on the **Alts DB** object to 1 if you want a +job created whenever someone registers or unregisters an alt.  (default is off)

## Functions 
The alts module exposes the following global functions:

**ALTS()** - Gets someone's alts (list of names)
Usage:  alts(<db# or name>)
