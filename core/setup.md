---
layout: module
title: Player Setup
resource: true
categories: [ core ]
description: Performs initial player setup required by the other systems.
---

## Features

The main feature of this module is to let you set up default player attributes as either visible/unlocked or hidden/locked with default values.   For example, the +cookies system sets up a hidden/locked player attribute called COOKIES with a default value of 0.

## Setup

You'll want to configure the ADDITIONAL_SETUP attribute on the **Player Setup Data** object.  This contains actions that need to be done on all new players, such as adding them to default channels and such.

All of my softcode systems will set up their own player attributes.  You can set up extra ones using the global setup_attr() function, described below.

The setup object defaults to being in room #0, as this is the standard player start room in PennMUSH.  If you've changed your game configuration to start players in a different room, put the object there and make sure to put it back in its proper location after any upgrades.

## Functions
**SETUP_ATTR(<h or v>,<name>,<default value>)** - Sets up a player attribute for all current and future players.

where "h" is for a hidden/locked attribute and "v" is for a visible/unlocked attribute.

For example:  setup_attr(h,icloc,#66) would set up a hidden/locked attribute named ICLOC on all players with a default value of #66.  This will update existing players as well, unless they already have an ICLOC attribute set.

Note: The player's DB# is passed as %0 when an attribute is set up, so you can use that to set up the default values if you need to do advanced processing.