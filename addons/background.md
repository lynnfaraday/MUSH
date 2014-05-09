---
layout: module
title: Backgrounds
resource: true
categories: [ addons ]
description: Write and submit character backgrounds for staff review.
---

## Features 
* No complicated entry commands - players can edit their BG attributes directly.
* Players can submit and un-submit their backgrounds for staff review.
* Integrates with the Jobs system to create jobs for background approvals and track comments and status.
* Staff can review backgrounds and approve or reject them.

## Customization 
You can set up actions to be taken when characters are approved or rejected.  For instance, on approval you probably want to teleport them to an IC location, add them to the appropriate faction channel or mailing list, etc.   There's probably not much you want to do on rejection but you never know.

You can also customize the messages that are sent when someone is approved or rejected.  This could contain a general welcome message or a form letter explaining background requirements.  The contents of their approval staff job is automatically included in the message.

The attributes that control all this are on the **BG Approval** object:

* APPROVAL_MSG
* REJECTION_MSG
* APPROVAL_ACTIONS
* REJECTION_ACTIONS

## Notes
The system assumes that characters are set with the UNREGISTERED flag when they are created. It will unset this flag when a character's background is approved. You can use this flag in coded commands to check whether a character is approved (for example, in the +ic command to keep them from going IC before they're approved). 


## Functions
None
