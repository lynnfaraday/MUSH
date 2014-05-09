---
layout: module
title: Help
resource: true
categories: [ core ]
description: Help system.
---

## Features 
* A complete family of +help commands, including the basic index (+help), the detailed help file view (+help <file>), and the search facility (+help/search).
* A staff help family of commands (+shelp) for your wizard/staff/admin-only commands.
* Help files can be stored so they aren't displayed in the basic index. That way files like +help +Finger2 don't clutter up your main page. 

       ------------------------------------------------------------------------------- 
                                          +help Topics                                 
   
      +BG                       +FINGER                   +DESC
      +HELP                     +LIST                     +MAIL
   
      See +help  for more information about a topic.
       -------------------------------------------------------------------------------

## Setup 
Help files from Faraday's systems will install automatically.  If you have other commands that you want to create +help for, you'll need to set those up.  

To add a new help file, simply add an attribute to the **+help Database** object. The attribute names determine how the help file will be displayed:

* HELP_XYZ - Shows up in the main +help index.
* SHELP_XYZ - Shows up in the main +shelp index (for staff only)
* MORE_HELP_XYZ - Does not show up in the main +help index, but you can still view it. Perfect for multi-part help files, so only the first part appears in the index.
* MORE_SHELP_XYZ - Same as MORE_HELP_XYZ but for +shelp (for staff only)

Note that the top and bottom lines are automatically included by the +help/+shelp commands.

## Functions 
None
