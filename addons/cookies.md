---
layout: module
title: Cookies
resource: true
categories: [ addons ]
description: Rewards players with cookies, which turn into Luck Points.
---

## Features 
* Players can give each other cookies each week.
* At the end of the week the cookies are translated into luck points.
* Configure how many luck points per cookie.
* Configurable cookie levels (bronze, silver, etc.) to track lifetime cookies.

Note: Luck Points are considered part of the FS3 module, but they can be installed separately if you want to use them without using FS3.

## Customization 
You can create a Cookie Notices bulletin board if you want a weekly cookie summary posted publicly.  A detailed summary will be posted to the Staff Jobs Notices board.

On the **Cookie DB** object you can customize the cookie award levels, using attributes of the form:

     &COOKIE_LEVEL_<number> Cookie DB=NAME|# OF COOKIES REQUIRED|ANSI COLOR

You can have anywhere from one to 99 cookie levels, but they must be numbered with two digits so they sort right (cookie_level_01, cookie_level_02, etc.).

## Functions
The following global functions are provided:

**COOKIE_AWARD(<db# or name>)** - Tells you the person's cookie award level.
