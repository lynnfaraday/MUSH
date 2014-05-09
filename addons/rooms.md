---
layout: module
title: Room Parents
resource: true
categories: [ addons ]
description: Various useful room parents.
---

## Features 
* Basic Exit Parent that emits automatic messages for succ/osucc/etc.
* Basic Room Parent that looks pretty (IMHO) and displays things like the IC and RL time in the desc.
* Tinyplot Room Parent for rooms with changeable descs stored in a central database.
* Room Parent includes built-in support for places (if configured).
* Room Parent includes support for local help files (+lhelp).

        -------------------------------------------------------------------------------
        Room Zero (#0RL)                                                               
                           5 Capricorn 2209  -- Sat Apr 07 21:25:22 2007             
        ------------------------------------------------------------------------------
        You are in Room Zero.
        ------------------------------------------------------------------------------
        Contents:     Lynn   
      
        Exits:        <F>  Test2                       <T>  Test Room                  
    
        Special:      +lhelp - Local Help Available 
                      places - Special Places Available
        ------------------------------------------------------------------------------- 
 
 
         -------------------------------------------------------------------------------
         TP Room (#77Rn)                                                                
                      Sat Apr 07 21:28:09 2007 -- Sat Apr 07 21:28:09 2007             
         ------------------------------------------------------------------------------
         The walls of the room shimmer. They are shapeless, malleable, almost waiting to
         be given form. With a little imagination, the room can become anything, from an 
         abandoned mining tunnel to an ornate ballroom.
 
         This is a TinyPlot Room which you can use to simulate any room not actually coded 
         on the MUSH. The room's commands allow you to change the description to suit your 
         needs, even storing multiple descs if you so desire. You can also lock the room for
         privacy, and request that it be linked to the IC world for special TPs.
 
         This room is currently UNLOCKED.
 
         See +lhelp for local help files.
         ------------------------------------------------------------------------------
         Contents:     Lynn   
 
         Exits:        <Out> Room Zero                  
 
         Special:      +lhelp - Local Help Available
         ------------------------------------------------------------------------------- 

## Customization 
The only customizable options are the room color and the default format for the 'date' section of the room desc.  

  &ROOM_DATE Room Parent=[ictime()] -- [time()]
  &ROOM_COLOR Room Parent=r

The room description itself, exit formats, etc. are not inherently customizable.   If you wish to change them, I suggest you create your own custom room parent, with my room parent as its parent.  In other words, the parent tree would look like:

   Places Parent --> My Room Parent -->  Your Room Parent -->  Individual Rooms

That way you can still upgrade my room parent and override whatever functionality you need on your own.

## Notes
The installer merely creates the room/exit parents.  You must parent things manually (or set up your master ancenstor to the parent in the MUSH config file, as explained in Installation).  To set up a TP Room, parent the room to the TP Room Parent and then view the +lhelp in the room for further setup instructions.

If your rooms are set NO_COMMAND by default in the server, you'll need to set them !NO_COMMAND for the lhelp and places to work (and have an @startup to set them !NO_COMMAND when the server restarts).  

## Functions
None