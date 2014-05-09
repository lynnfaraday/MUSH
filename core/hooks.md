---
layout: module
title: Hooks
resource: true
categories: [ core ]
description: Manages 'hooks' that are triggered by other commands.
---

## Features 
PennMUSH only allows one @hook for a given command.  My code requires at least two pose @hooks - one for the autospacer and one for combat.  This manager lets you have one hook registered with the server, and call multiple internally.

## Customization 
You can add your own pose hooks by creating attributes on the *Hook Manager* object.

     HOOK_POSE_<name> Hook Manager=pemit(%#,Hook!)

The Install Manager will auto-install @hooks from the other Fara systems.

## Functions

None
