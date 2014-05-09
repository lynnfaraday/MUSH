---
layout: module
title: Finger
resource: true
categories: [ addons ]
description: Provides a player profile (aka +finger).
---

## Features 
* Configurable sections: Top, Middle (divided into left and right) and Bottom
* Configurable fields in each section.
* Configurable security level, for evaluating notes fields.
 
## Requirements 
* The default +finger configuration expects various fields that are set by FS3 Chargen.

## Customization

The +finger module comes with a default setup, but is highly customizable, allowing you to specify the fields and appearance.   See +shelp +finger for detailed information.  The following diagrams show how the various sections can be arranged depending on which fields you specify:

           ---   TOP   ---
           - L   ---   R - 
           ---  BOTTOM ---
     or
           ---   TOP   ---
           ---  BOTTOM --- 
     or
           - L   ---   R -
           ---  BOTTOM ---
     or 
           ---   TOP   ---
           - L   ---   R -

 
 Example #1
 
     Top Fields:
     Left Fields:   NAME_AND_ALIAS FULLNAME BLANK SEX AGE RACE BLANK POSITION
     Right Fields:  STATUS VACATION BLANK ALTS MAIL BLANK AWARDS
     Bottom Fields: CONCEPT BACKGROUND NOTES
 
     ------------------------------------------------------------------------------- 
     ##           Faraday (Fara)            -  Last On: Sat Apr 15 15:21:43 2000   
     ##            Lynn Faraday             -       Gone till next millenium       
     ##                                     -                                      
     ##             Sex: Female             -              Alts: Lynn              
     ##               Age: 00               -            Mail: 0 Unread            
     ##             Race: Human             -                                      
     ##                                     -             Awards: Test             
     ##           Position: Coder           -                                     
     ------------------------------------------------------------------------------
                                        Concept                                    
     I'm a coder, I don't need a concept. 
     ------------------------------------------------------------------------------
                                       Background                                  
     This is my public background 
     ------------------------------------------------------------------------------
                                         Notes                                     
     These are my notes
     ------------------------------------------------------------------------------- 
 

     Example #2
 
     Top Fields:    NAME BLANK ALIAS SEX RACE BLANK LOCATION
     Left Fields: 
     Right Fields: 
     Bottom Fields: CONCEPT NOTES
 
     ##################################+  MY MUSH 
     Name:      Faraday 
  
     Alias:     Fara 
     Sex:       Female 
     Race:      Human 
  
     Location:  Somewhere out There
     ------------------------------------------------------------------------------
                                        Concept                                    
     I'm a coder, I don't need a concept. 
     ------------------------------------------------------------------------------
                                         Notes                                     
     These are my notes
     ############################################################################+ 
 

     Example #3
 
     Top Fields: NAME
     Left Fields:   FULLNAME SEX LOCATION STATUS
     Right Fields:  ALIAS RACE
     Bottom Fields: VACATION BLANK MAIL AWARDS CONCEPT NOTES
     ** This appearance requires some modifications from the default field settings
          but is very easy to accomplish.  Just add %B's after the field titles to make
          the fields line up right, and edit the bottom fields to all have a %R at the
          beginning.
 
     ############################################################################+ 
                                     --- Faraday ---
     Full Name: Lynn Faraday                            Alias:    Fara             
     Sex:       Female                                  Race:     Human            
     Location:  Somewhere Out There                                         
     Last On:   Sat Apr 15 15:21:43 2000                                           
                                                                                
     Vacation:  Gone till next millenium                                                
                                                                               
     Mail:      0 Unread  
     Awards:    Test  
 
     Concept:   I'm a coder, I don't need a concept. 
 
     Notes:     These are my notes
     ############################################################################+ 

## Functions
None

## Notes

None
