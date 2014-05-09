---
layout: module
title: Global Functions
resource: true
categories: [ core ]
description: Important utility functions.
---

## Features 
* Various line functions
* Query functions for staff, player, IC status, etc.
* String formatting functions to supplement ljust/rjust/center 

## Setup 
You'll want to customize these settings on the **Global Utilities Functions Data** object:

* NON_PLAYERS:  Set this attribute with the DB#'s of your builders, staffers and other non-real-player objects so they will be excluded from the REAL_PLAYERS() function.
* FUN_LINE: The various line functions can be customized as you like.

## Functions
The following global functions are provided:

    *APPEND()*
    Appends text to an attribute. Separator is optional and defaults to a blank space.

    Usage: append(db#,attribute,text to append,separator)


    *CAPSTR_ALL()*
    This function will capitalize each word of a multi-word string. It defaults to a space-delimited string but you can pass a delimiter if you want.

    Usage: capstr_all(<string>,[delimiter])


    *CENTER_STR()*
    This function centers a string, just like the center() function, but it also chops the string if it's too long to fit. It returns the formatted string.

    Usage: center_str(<string>, <length>)


    *ESCAPE_CR()*
    When storing user input on an attribute, the MUSH will put in hard line breaks for any carriage return(%R). This function escapes them out so you can later @decompile the attribute easily. It returns the string with all carriage returns escaped out. You use it in conjunction with the 'set' function. For example:
    set(#123,foo:[escape_cr(%0)])

    Its partner is replace_cr, which will un-escape the linebreaks so you can display them.


    *FIND_AND_REPLACE()*
    This function is a simple find/replace combination. It will find the first instance (and ONLY the first instance) of a pattern match and replace it with the provided string.

    Usage: find_and_replace(<original string>,<find string>,<replace string>,<delimiter>)

    Example: find_and_replace(Art:2|Cooking:3|Melee:1,Cooking:*,Cooking:4,|)

    *ISIC()*
    This function returns 1 if a player is set IC, and 0 if they are not.
    Usage: isic(<player DB#>)


    *ISPLAYER()*
    This function tells whether an object is a player or not. It returns 1 if the object is a player, and 0 if not.

    Usage: isplayer(<db#>)


    *IS_REAL_PLAYER()*
    This function returns 1 if the player is a REAL player (i.e. not a builder char or game wizard). Non-players are marked by adding their DB# to the NON_PLAYERS list on this object. You can also use the real_players() function to get a list of all real players.

    Usage: is_real_player(<player db#>)


    *ISSTAFF()*
    This function returns 1 if the player is on staff, and 0 if not. It goes by the Wizard, Royal and Judge flags. If you use a different system for staff members, you can edit this function.

    Usage: isstaff(<db#>)


    *ISWIZARD()*
    This function returns 1 if the object is wizard or if it inherits wizard powers from its owner, and 0 otherwise.

    Usage: iswizard(<db#>)

    *LINE()*
    This is a helpful utility you can use to standardize all the lines around your commands. By default, it gives a standard colored line. You can edit this to customize the style of your lines.
    Usage: line()

    See also related functions: line2(), line_no_color(), and line_with_text()


    *LINE2()*
    This is a rather plain-looking line. You can edit this to customize the style of your lines.
    Usage: line2()

    See also related functions: line(), line_no_color(), and line_with_text()


    *LINE_NO_COLOR()*
    The same style of line as the line() function, but without color.
    Usage: line_no_color()

    See also related functions: line(), line2(), and line_with_text()


    *LINE_WITH_TEXT()*
    The same style of line as the line() function, but text centered in the middle of the line.
    Usage: line_with_text()

    See also related functions: line(), line2(), and line_no_color()


    *MATCHCON()*
    This function searches the contents of a room to find a match (either an object or a player) to a given name. It returns the DB# of the first match it finds, or #-1 if no match is found.

    Usage: matchcon(<name search string>,<room DB#>)


    *MONTHNAME()*
    Takes in a number from 1-12 and gives back a month name in the format that makes convtime() happy. Returns #-1 if you give it something nonsensical.

    Usage: monthname(<number>)


    *NUMCOMP()*
    This function compares two numbers,much like strcmp compares strings. If the first number is bigger, it returns 1. If the first number is smaller, it returns -1. If the numbers are equal, it returns 0.

    Usage: numcomp(<num1>, <num2>)


    *ORDINAL()*
    This function gives the ordinal representation "first, second, third, etc.) of a number.

    Usage: ordinal(<number>)


    *PADSTR()*
    This function is a combination of the mid() and ljust/rjust/center() functions. If a string is too long, it will truncate it. If a string is too short, it will pad it (on the left, right or center, as you specify it). By default, it will left justify. Also, it uses spaces to pad by default by you can override that by specifying a padding character.

    Usage: padstr(<string>, <length>) (pads left with spaces)
    padstr(<string>,<length>,<left/right/center>)
    padstr(<string>, <length>, <left/right/center>, <pad character>)


    *PARSE_POSE()*
    This function takes a standard pose string and formats it with the player's name. It accepts the tokens : (for action poses), " (for say poses), ; (for possessive poses) and . (for emits).

    Usage: parse_pose(<pose string>,<player name>)


    *PRETTIFY()*
    Makes a string pretty by replacing underscores with spaces and capitalizing each word in the resulting string. So Sleight_of_hand becomes Sleight Of Hand.

    Usage: prettify(<string>)

    See also: unprettify()


    *RAND_COLOR()*
    This function gives you a random ansi color. You can use it to make your lines change color in commands. Because the color changes every 5 seconds, you can call it more than once in the same command and be reasonably sure of getting the same color both times. But if you call it from two different commands, you may get two different colors. To get around this, you can store the value returned from this function and store it in one of the registers.

    Usage: ansi(rand_color(),<string>) or  setq(0,rand_color()) then later: ansi(%q0,<string>)


    *REAL_PLAYERS()*
    This function gives you back a list of all 'real' players (i.e. not wizards, builders, etc.).

    Usage: real_players()


    *REPLACE_CR()*
    This function is the partner to escape_cr. If you have a string that you removed carriage returns from using escape_cr, you use this function to get them back before displaying them.

    Usage: replace_cr(<string>)


    *SAFE_TEL()*
    This is a safer version of the teleport function tel(). It will not try to teleport a player to a bad location (which can sometimes cause the object doing the teleporting to end up in the player's inventory!).

    Usage: safe_tel(<player>,<location>)


    *SORT_NAMES()*
    This function will take a list of DB numbers and sort them according to the object names.

    Usage: sort_names(<list>)


    *SORT_OLDEST()*
    This function will take a list of items in the form "prefix_number" and sort them by number with the oldest coming first. For example: MSG_1 MSG_2 MSG_3 MSG_10 would be sorted as MSG_1 MSG_2 MSG_3 MSG10.

    Usage: sort_oldest(<list>)

    See also the companion function sort_recent.


    *SORT_RECENT()*
    This function will take a list of items in the form "prefix_number" and sort them by number with the most recent coming first. For example: MSG_1 MSG_2 MSG_3 MSG_10 would be sorted as: MSG_10 MSG_3 MSG_2 MSG_1.

    Usage: sort_recent(<list>)

    See also the companion function sort_oldest


    *UNPRETTIFY()*
    Makes a string code-friendly by replacing underscores with spaces and doing a squish. So Sleight Of   Hand becomes Sleight_Of_Hand.

    Usage: unprettify(<string>)

    See also: prettify()


    *WEB_EDIT()*
    This function will take a MUSH string and perform basic formatting to turn it into a HTML-friendly version. For example, it will replace < and > with &lt and &gt, and will replace escaped-out carriage returns (obtained using the escape_cr function) with paragraph and line breaks.

    Usage: web_edit(<string>)


    *WIKIFI()*
    This function will take a MUSH string and perform basic formatting to turn it into a Mediawiki-friendly version.

    Usage: wikifi(<string>)