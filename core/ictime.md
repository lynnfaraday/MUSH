---
layout: module
title: IC Time
resource: true
categories: [ core ]
description: Configurable IC time.
---

## Features 
* Provides a simple ictime() function that uses the same day/month as RL but a different year.
* Provides OOC +time command to adapt to your timezone.

## Customization 
This data can be configured on the data object:

     YEAR_OFFSET - The value to add/subtract from the current year to get the desired IC year.
         Example: Use -163 to make the year 1848 (if it's 2011 today)

## Functions

    ictime() - Shows the current IC Time
