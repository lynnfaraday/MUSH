---
layout: module
title: MOTD
resource: true
categories: [ addons ]
description: Shows players useful info when they log in.
---

## Features 
* Optional "special" Message of the Day for special announcements.
* Shows unread mail and BBS.
* Shows upcoming events.

 
       ############################################################################+ 
                     Global BBS System - Unread Messages (+bbread)                 
                    Announcements (#1): 6 unread (1, 2, 3, 4, 5, 6)                
 
                               Upcoming Events (+events)                           
                          "PLOT" on Sat Apr 14 2007 @ 6pm EST                      
 
                                  Mail Report (@mail)                              
                                  0 Unread -- 40 Total                             
                          You have more than 25 mail messages.                     
                  Please archive and erase ones that you do not need!              
       ############################################################################+ 
 


## Requirements 
* +events - To show events listings.

## Customization 
You can customize a few things on the **MOTD DB** object, including:

* BBPOCKET - The DB# of the bbpocket object, to allow integration with the BBS.
* MAILSYS - Which mail system you're using, builtin or faramail
* COLOR - The ansi color of the MOTD

The daily important message can be set via softcode.  See +shelp.

## Functions
None
