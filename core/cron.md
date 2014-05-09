---
layout: module
title: Cron (Myrddin's)
resource: true
categories: [ core ]
description: Runs tasks on a periodic schedule.
---

## Credits

This is a branch of Myrddin's [MUSH Cron System](http://www.firstmagic.com/~merlin/mushcode/mc.mushcron.html), for running tasks on a periodic schedule.

Note:  This system is an exception to the Copyright/License statement.  I claim no ownership of it.

## Features 
* Adds a +cron command to list jobs.
* Integrates with my install manager.

## Customization 
You can create 'jobs' on the Cron system to have things run periodically.  Each job needs two attributes, a CRON_TIME_<name> attribute to tell it when to run, and a CRON_JOB_<name> attribute to tell it what to do.

Pattern of CRON_TIME_* attributes:

    <month>|<date>|<day of week>|<hour>|<minute>|<args>
    (ex. Mar||Sun|04|54| would trigger a cron job at 4:54am every sunday in the month of march)

## Functions
None
