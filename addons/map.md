---
layout: module
title: Maps
resource: true
categories: [ addons ]
description: A simple IC map system.
---

## Features 
* Keys off an AREA attribute on the room to show the map for that area.
* Maps can be drawn using simple ASCII art like [http://www.sigsoftware.com/emaileffects Email Effects]
* Display an optional map legend.

         ##########################+  Main_Level 
           +-----------+    +---------------------------------+    
           | Sentinel  +----+                                 |    
           | Square    |    |         Housing Area            |    
           |         3 |    +                                 |    
           +----+------+    +-------+-------------------------+    
                |                   |                   |           
           +----+------+      +-----+-----+       +-----------+    
           | Pioneer   |      | Colonial  |       | The       |    
           | Village   +------+ Square    +-------+ Grove     |    
           |         4 |      |         1 |       |         5 |    
           +----+------+      +----+------+       +-----------+    
                |                  |                  |            
           +----+------+     +---------------------------------+   
           |The        |     +                                 |   
           |Parthenon  +-----|        Freedom Park             |   
           |           |     +                              2  |   
           +-----------+     +---------------------------------+   
 
         Type +map/legend for a key to the numbers or abbreviations on the map.
         ############################################################################+ 

## Customization 
Maps are stored on the **Map DB** object in attributes named MAP_<area> and LEGEND_<area>. A legend is not required. You only need to use it if you want supplemental information to appear. The system expects the area to match an &AREA attribute on the room.

## Functions
None