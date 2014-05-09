---
layout: module
title: Who
resource: true
categories: [ addons ]
description: A configurable +who/+where system.
---

## Features 
* Comes with a ready-to-go default setup for +who and +where.
* Customize how the fields are displayed.
* Add new fields easily.
 
## Requirements 
* The default who configuration expects various fields that are set by FS3 Chargen.

## Customization

The Who module comes with a default setup, but is highly customizable, allowing you to specify the fields and appearance.    All configuration is setup on the Who Data object.

The Who display consists of several sections:

* Top Line 
* Header (shows the MUSH name by default)
* Titles (optional)
* Separator (a dashed line)
* Who Lines (one per player, with configurable fields)
* Separator (a dashed line)
* Footer (shows a summary of who's online by default)
* Bottom Line

You can also set up a Left Border and Right Border to give the display a 'boxed in' appearance.

For example:

  

    ########################################################################## 
      ## -                             The Fall MUSH                              -
      ## -       Name            Faction      Rank/Position                  Idle -
      ## --------------------------------------------------------------------------
      ## - STF   Noh             Staff        Masked Dude                    7h   - 
      ## - STF   Faraday         Staff        Captain - Test                 1s   - 
      ## - OOC   Jerad           Techie       Vet Tech                       3m   - 
      ## - OOC   Miriam          Townie       Batty Old Cow                  1h   -
      ## --------------------------------------------------------------------------
      ## - 4 Online                         0 IC                        24 Record -
      ##########################################################################


## Sections

You can configure the appearance of the lines involved:

**TOP_LINE**

**BOTTOM_LINE**

**SEPARATOR_LINE** - The dashed lines displayed after the titles and before the footer

**LEFT_BORDER** - The left "boxed in" display characters.  Leave it blank if you don't want a box.

**RIGHT_BORDER** - The right "boxed in" display characters.  Leave it blank if you don't want a box.

## Header and Footer

You can configure what appears at the top and bottom of the who display.

**HEADER** - By default, this shows the configured MUSH name.

**FOOTER** - By default, this shows the who's online summary.  You can't configure the summary, but you could replace it with your own.

## Choosing Fields and Titles

The first thing you configure is the fields you want to show and their length. 

 &WHO_FIELDS Who Data=Status:5 Name:15 Faction:12 RankAndPos:30 IdleTime:4
 &WHERE_FIELDS Who Data=Status:5 Name:20 Location:38 IdleTime:4

You'll want to make sure your lengths add up to a full screen width.  Be sure to account for the blank spaces placed between each field, and the left/right borders.

Each field will be padded or trimmed using padstr() to make it the appropriate length.

You can also configure titles to show above the columns, if you wish.   Column names must be separated with pipes "|".  Make the title attribute empty to omit the title line completely.

   &WHO_TITLES Who Data=|Name|Faction|Rank/Position|Idle
   &WHERE_TITLES Who Data=

## Field Data

Each field specified in WHO_FIELDS or WHERE_FIELDS must have an attribute on the data object with the same name.  For example, since we specified "Name" in the WHO_FIELDS list above, we would need a "NAME" attribute on the data object to show the name.

   &NAME Who Data=name(%0)

Remember that the Who system will automatically pad or trim the fields to the appropriate length, so you don't have to worry about that.  Also, it will automatically place a blank space between fields.

You can customize the existing fields at will, or create your own.

## Sorting

You can configure how the who/where entries are sorted:

**FUN_SORT_WHO** - Sorts the +who list.  %0 and %1 are two player DB#s to compare.  The default sort order is by faction.

**FUN_SORT_WHERE** - Sorts the +where list.  %0 and %1 are two player DBs to compare.  The default sort order is by room area then room name.

## Miscellaneous

**COLOR** - The ansi color used on the who display.

**MUSH_NAME** - MUSH name, displayed by default in the who/where header.

**TP_ROOM_PARENT** - If using the TP Room system, set this to be the DB# of the TP room parent so it will count people in TP rooms as being IC.

## Functions
None
