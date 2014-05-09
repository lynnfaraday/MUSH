---
layout: module
title: Census
resource: true
categories: [ addons ]
description: Lets you organize factions, organizations and positions and view census totals.
---

## Features 
* View complete census of basic info for all players.
* View census breakdown by faction, position, organization or sex.  (others can be added).
* Commands for viewing factions, organizations and positions.
* Global functions for factions, organizations and positions so they can be referenced from Chargen or other systems.

      ------------------------------------------------------------------------------
      Complete Census
  
      Name           Sex Faction        Position     Organization   
      ------------------------------------------------------------------------------
      Lynn            F  Red            Reporter     Army           
      Rob                                                           
      TestDummy                                                     
   
      Gray characters are not yet approved.
      See +help +census for other census commands.
       ------------------------------------------------------------------------------

       ------------------------------------------------------------------------------
       Census Breakdown By Faction
       Blue            0 
       Red             1
   
       See +help +census for other census commands.
       ------------------------------------------------------------------------------
    
       ------------------------------------------------------------------------------
       Organizations
   
       Army 
       Navy
   
       See +orgs <name> for more info on an organization.
       ------------------------------------------------------------------------------

## Customization 
You can configure your positions, organizations and factions.  These are all stored in attributes on the **Census DB** object.

    &POSITION_<name> Census DB=<description>
    &FACTION_<name> Census DB=<description>
    &ORG_<name> Census DB=<description>

## Functions 
The system exposes the following global functions, primarily for character generation support.

**POSITIONS()** - Lists all positions

**ORGS()** - Lists all orgs

**FACTIONS()** - Lists all factions
