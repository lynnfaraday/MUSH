---
layout: module
title: Mail
resource: true
categories: [ core ]
description: Full-featured mail system to replace @mail.
---

## Features 

* Send mail immediately (like PennMUSH) or compose it piece by piece (like MUX).
* See and send mail to public mailing lists.
* Sort messages by date, sender, thread and more.
* Review and retract mail you've sent.
* Archive your mail into a HTML format that can be logged to a file.
* Turn on sent-mail to get a copy of mail you send.
* Use auto-routing to route messages with specific content into folders.

         ------------------------------------------------------------------------------
                                         Mail Messages                                 
                                        (sorted by date)                               
 
         ----------- 0:Inbox -----------------               2 Unread, 2 Total, ! Cleared
         1.  [-U-]  Joanna              Test Message1                       18 Apr 2010 
         2.  [-C-]  Kaiya               Hello                               18 Apr 2010
         ----------- 1:Test ------------------               0 Unread, 1 Total, 0 Cleared
         ----------- 2:Sent ------------------               0 Unread, 1 Total, 0 Cleared
         ------------------------------------------------------------------------------ 
 
         ------------------------------------------------------------------------------
         To:      Faraday
         From:    Kaiya 
         Date:    Sun Apr 18 05:35:58 2010
         Flags:   
         Subject: Hello
         ------------------------------------------------------------------------------
         Hello World.
         ------------------------------------------------------------------------------
     
## Customization 
You can set up mailing lists using +commands.  See the +shelp for details.

## Notes
Because this module stores mail in softcode and not hardcode, it can cause database bloat.  I've never personally had an issue with it in 10 years of using the system, but I've usually run it on small to mid-size games.  If your game is gigantic, or your players horde mail, you could have an issue.  Even then, you could work around the issue by forcing people to archive their mail periodically (easily done through +mail/archive).

## Functions
The following global functions are provided:

**SEND_MAIL(<sender db#>, <to list>, <thread*>, <subject>, <message>)** - Sends a mail message

Thread should be #-1 unless replying to another message.

**UNREAD_MAIL(<db#>)** - Lists a player's total unread mail.

**TOTAL_MAIL(<db#>)** - Lists a player's TOTAL mail.
