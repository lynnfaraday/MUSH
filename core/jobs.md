---
layout: module
title: Jobs
resource: true
categories: [ core ]
description: Tracks staff jobs and notifications.
---

## Features 
* Organize jobs by category.
* Assign jobs to different staff members.
* Allow players to submit +requests directly into the job system.
* Set due date, priority and status.
* Send mail to players regarding their jobs.  Players can add comments back.
* Highlights jobs that are overdue.
* Customize your default jobs display.
 
         ------------------------------------------------------------------------------- 
          #    Cat   Title                Submitter     Handler       Due Date   Status
          1    CODE  Line Format          Faraday       -----------   None Set   NEW 
          2    BUILD Build Something      Joe           Joe           None Set   25%
          3    REQ   Test                 Faraday       -----------   08/05/04   75%
         ------------------------------------------------------------------------------- 
 
         ------------------------------------------------------------------------------- 
                                             ~ Test ~                                   
          Category:     REQ                    ---    Priority:   MEDIUM
          Submitted By: Faraday                ---    Handled By: 
          Submit Date:  07/10/04
          Due Date:     08/05/04               ---    Status:     75%
 
          Description:
          This is just a test.
 
          Comments:
          [Faraday - Wed Jun 30 23:20:13 2004]
          I did something for this job once.
         ------------------------------------------------------------------------------- 

## Customization 
The system comes set up with several default categories. You can add more as you see fit.   See +shelp +jobs.

If you want an archive of staff jobs, create a Staff Job Notices BBS.

You can also configure which job fields are displayed in the main job lists.  Possible fields include: Category, DueDate, Handler, Number, Overdue (a simpler version of due date that only shows the general due condition), Status, Submitter, Title.   The format is in the FIELDS attribute on the Jobs database.  List the fields you want in the form  

  fieldname:title:length

For example:

  DueDate:Due:5
  this will show the DueDate field with a title "Due" and pad/limit it to 5 characters in length

## Functions 
**CREATE_JOB(<category>,<title>,<text>,<submitter DB#>)** - Creates a new staff job.

For example: create_job(BG,BG Submittal,%N has submitted a background.,%#)

NOTE: This will use register 'j' to store the job number. You can access that with %qj after calling the create_job function.

**FIND_JOB(<job#>)** - Returns the category object number and attribute number of the specified job.
