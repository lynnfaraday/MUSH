---
layout: module
title: Weather
resource: true
categories: [ addons ]
description: A simple weather system.  
---

## Features 
* Displays season, time of day, temperature and weather in a simple format.
* Requires no setup or configuration if you time and seasons are the same as RL.
* Automatically changes the weather periodically.
* Performs global emit when the weather changes.


   <WEATHER> It is a winter evening. The weather is cold and fair.

## Requirements 
* Expects the ICTIME function from Core Modules, but you can easily code your own or just use time() instead.

## Customization 
If your months/seasons/times are the same as RL (Northern Hemisphere) then the system requires no special setup.  If you're using a custom IC time system, you'll need to tweak some of the settings as described in the Design section below.

You can include the weather in your room descs using the FUN_WEATHER function.

## Functions 
The following global functions are provided.

**WEATHER()** - Gives you the current weather string.

## Design 
TR-CHANGEWEATHER is @triggered automatically at startup and then every UPDATE_WEATHER_SECS after that (default every half hour).  This updates the weather and time of day, and sends out a global @emit with the new weather.  The emit is only sent to players who are in rooms where the SHOW_WEATHER attribute is set to a 1 on the room.  I usually set that by default in the room parent and then override it for special rooms.

FUN_COMPUTE_TIME tells you the time of day (night, evening, etc.) based on the ICTIME function.  If you want the time of day to be determined by another function (such as TIME), go ahead and change that function.  FUN_DETERMINE_SEASON does the same for seasons.

There are objects for each season. Attributes named TYPE-CLEAR, TYPE-DRIZZLING, etc. store the list of potential "next weather" types based on the current weather.  An attribute named TEMP stores the randomized temperatures for that season.  The more often a weather type or temperature appears in the list, the more likely you are to transition to that weather type.  If you don't want the weather to change very often, make the current weather type appear more often.

For example:  If TYPE-raining said - "raining raining raining raining raining overcast overcast stormy fair fair", when it was raining, there'd be a 50% chance it would stay raining, a 20% chance it would go to overcast or fair, and a 10% chance it would start storming.

To create a new weather type (like hurricane), you'd need to:

* Create a TYPE-HURRICANE attribute  on each season that says what the next weather might be once a hurricane has started.
* Add hurricane to at least one of the existig TYPE-xxx lists so a hurricane can begin.

There is no support for local weather. It assumes that the entire game shares the same weather.
