---
layout: module
title: FS3 Configuration
resource: true
categories: [ fs3 ]
---

This file describes how to configure the FS3 code.  

## Skills

All of the skills configuration data is on the FS3 Skill Data object.  Things you can configure include:

### Attributes

Set up (MUSH) attributes containing the (FS3) attribute name and description.  For example:

    &ATTRIBUTE_SOCIAL Skill Data=Dealing with people.
    
Attributes can be anything you want, from broad skill spheres (Academic, Technical, Social) to more traditional RPG attributes (Intelligence, Dexterity, etc.)

Things to bear in mind:
• Attributes cannot change with experience, so it might be best to avoid attributes
that are malleable, like Strength or Beauty.
• The more attributes you have, the more trouble you’ll have preventing people
from min-max-ing points between attributes and skills.

### Action Skills

Set up a MUSH attribute for each action skill containing the name and description.  For example:

    &ACTIONSKILL_ALERTNESS Skill Data=Noticing things.

> **Cardinal rule of FS3:**
> **Limit the number of action skills.**

One of the hallmarks of FS3 is the distinction between **Action Skills**, which are relevant to the conflict in the game, and **Background Skills**, which are fluff skills to promote character depth. Another hallmark is a fast, easy character creation experience because the skill list is limited.

Generally games should have no more than **10-12** action skills. Focus on the things that are likely to come up in conflict situations. Combat skills, Perception checks, Healing rolls and vehicle piloting are pretty standard. But don’t leave out life-or-death skills that might be peculiar to your setting, like Swimming in a water game.

If you find yourself needing more than a dozen action skills, consider:
• **Are your skills too narrow?** For instance: Instead of separate skills for Rifles, Pistols, SMGs, etc. consider one over-arching Firearms skill. They’re really not all that different.
• **Are you including skills that really don’t matter?**  For instance: Do you really need to have Demolitions as an Action Skill for the 1 or 2 explosives experts you may or may not ever get? Or can that just be handled as a Background profession skill for those people?

### Ruling Attributes
You need to configure the ruling attributes for each action skill.  

    &RULING_ATTR Skill Data=Alertness:Reactive Demolitions:Technical

You can also set the default ruling attribute for background skills.

    &RULING_ATTR_BG Skill Data=Academic
    
There will never be a perfect 1-1 correspondence between skills and attributes. You will always be able to find situations where different attributes fit the same skill. Just pick the one that fits the majority of situations.

### Suggested background skills
Background skills are freeform, so there is no list, per se.  However, you can configure some suggestions, via the BACKGROUND_SKILLS attribute.  Separate the names with pipes to make them appear one per line.

    &BACKGROUND_SKILLS Skill Data=A hobby, like basketweaving|A science, like chemistry|A sport, like baseball

Consider ahead of time what sorts of things you would consider ‘common knowledge’ that does not require a skill. This will help you guide players who might be putting skill points into silly things just to have them.

### Languages
As with background skills, language skills are freeform.  You can list suggested languages via the LANGUAGE_SKILLS attribute.  Separate the names with pipes to make them appear one per line.

    &LANGUAGE_SKILLS Skill Data=English|German|French

Also set the default language that they start with in Chargen

    &DEFAULT_LANG Skill Data=English

### Chargen Points and Limits
Chargen automatically limits certain things:

**The number of abilities you can have at or above certain level.**
You can configure both the number and the level.  
For example, to say that they can only have 4 abilities at 7 or higher:

    &HIGH_ABILITY_LEVEL Skill Data=7
    &MAX_HIGH_ABILITIES Skill Data=4

**The number of points you can spend on action skills and attributes.**

    &MAX_POINTS_ACTION Skill Data=32
    &MAX_POINTS_ATTRIBUTES Skill Data=20

**The minimum and max number of quirks they can have.**

    &MIN_QUIRKS Skill Data=1
    &MAX_QUIRKS Skill Data=4

**The minimum number of background skills they must have.**

    &MIN_BGSKILLS Skill Data=2

**The number of points they start with in chargen.**

Bear in mind that the system will automatically give them the average rating (2) in every attribute and the default language, but this counts towards their overall point total!   

    &STARTING_POINTS Skill Data=80
    
The number of ability points you give in character creation will depend on a number of factors:
* The power level of your game. An ultra heroic game will require more points than a gritty one.
* How many attributes you have. You’ll probably want to give characters enough points to have average in every attribute and above average in a couple.
* How many action skills your typical character will need.
* How many background skills you require. Bear in mind that most background skills will only be at low levels, so they probably won’t need a ton of points.
* Don’t forget to give them a couple points for their native language.

**The system will let you assign bonus points to players above and beyond the starting points.**

You can do this manually or automatically (based on faction or position or whatever) 
just by setting the BONUS_POINTS attribute on the player. 
 
You can also tell players that it's ok to +request extra points by configuring the 
max # of points they can request.  If you set this to 0 they will not see a message about extra points at all.

    &BONUS_POINTS *Bob=5
    &MAX_BONUS_POINTS_REQUEST Skill Data=0

> **A note about points and chargen:**
> Players familiar with other systems will tend to highball their ancillary skill ratings when creating FS3 characters. In particular, they will tend to have too many skills in the 4-6 range. This range should be reserved for skills that are truly at a professional level of competence; something a character could make a living at. It would be unusual (but not impossible!) for someone to have that high of a rating in hobbies or casual interests.
> 
> Ironically, people also tend to lowball professional skills that they should have in the 7-9 range. This range is absolutely fair for characters with a good many years of experience, or younger characters who are just exceptionally gifted in their area of expertise.
>
> Some games shy away from or discourage characters with skills in the 10-12 range. This is really not necessary and in fact can be **harmful** because it reduces differentiation between characters. When you look at the ability roll charts, there’s not that much difference between rating 7 and rating 10. It gives an advantage, but not an overpowering one. There’s nothing wrong with wanting to be an expert in something.

## Character Sheets

You can completely configure the appearance of the +sheet command, and can even set up multiple pages.  

Each page should have an attribue of the form:

    &FUN_DISPLAY_SHEET_PAGE0 Skill Data=code to show the first page (+sheet)
    &FUN_DISPLAY_SHEET_PAGE2 Skill Data=code to show the second page (+sheet2)
    &FUN_DISPLAY_SHEET_PAGE3 Skill Data=code to show the second page (+sheet3)
    etc.

The function is passed %0 for the player DB#.  The system comes with a default version of Page0

(Note that the system uses PAGE0 as the name for the first page.  Although you can program a PAGE1, typing +sheet1 doesn't really make much sense.)

You can also decide whether non-staff players can view other players' sheets:

    &PLAYER_VISIBLE_SHEETS Skill Data=1   (use 0 to restrict +sheet <player> to staff only)

## XP

The most fair and realistic way to award XP is to base it on the passage of time in the game world. Above all else, learning and practicing skills takes time.

A general guideline of 1 XP per week will allow advancement at a reasonable rate.  You can configure the amount of XP awarded each week.  
   
     &XP_PER_WEEK FS3 XP Data=1

You can set up the max # of XP someone can save up.  16 is recommended because it lets them save up to improve a rating 11 skill, and have a few points left over.

     &MAX_XP_HOARD FS3 XP Data=16

The time (in seconds) they have to wait before doing +xp/raise again (on the same skill or on a different skill, it doesn't matter).

     &TIME_BETWEEN_RAISES FS3 XP Data=259200

The cost for raising a skill based on the *current* level.  

    &XP_PER_LEVEL FS3 XP Data=0:1 1:2 2:2 3:4 4:4 5:4 6:8 7:8 8:8 9:12 10:12 11:12

> **Special XP Situations**
> The default XP award assumes that your characters are spending most of their time working, hanging out, taking care of their families, etc. If someone is spending most of their time studying, it’s a different story.  You could increase the XP per week, give a bulk XP award that gives them enough points to buy all the skills they would learn in the training program, or just assign them those skills manually.
> 
> Also, if your theme involves constant life-or-death struggles, consider doubling the XP award to 2 XP per week. In these situations, it tends to be survival of the fittest, where you learn fast or die.
>
> Avoid giving XP as OOC rewards.  It never ends well.
>
> If your game has been running for a long time, consider giving new players some XP to start off with beyond their chargen points.  They may be new, but that doesn't mean their characters are newbies.
  
## Combat

### Weapons

Weapons are configured on the Equipment DB object, each with their own attribute.  You can use underscores for weapon names, like MACHINE_GUN.

    &WEAPON_PISTOL FS3Combat Equipment Stats=Pistol|Firearms|5|0|3|Ranged|Personal|0|10|0|50|0|Physical|0|0|1|1

Consider whether you really want to make up different models of weapons (M16 Assault Rifle, AK47 Assault Rifle, etc.) or just lump everything into a generic category (Assault Rifle). It can be tedious creating all the statistics, and having too many weapons to pick from can either be overwhelming or cool depending on your player base.

The parameters are, in order:

- **Description**
- **Attack Skill**
- **Defense Skill**.  For firearms, it is recommended that you use a raw ability level of “3” rather than any specific skill. That means everyone gets 3 dice to dodge.  If you make this a skill, people will miss more often, and there is really no skill involved in dodging bullets, anyway.
- **Lethality**.  When setting lethality modifiers, bear in mind the damage modifiers from hit location. Lethality modifiers on par with “critical” hits (+30) are pretty lethal indeed.
- **Penetration**.  See discussion below about balancing weapons and armor.
- **Weapon Type** - Melee, Ranged, Explosive, Defensive.  Defensive weapons can only 'suppress' and not 'attack'.  Only explosive weapons can use the 'explode' command.
- **Class** - Personal or Vehicle.  This is just for reference and has no real effect.
- **Automatic Fire** - 1=autofire capable, 0=not.   There is currently no way to indicate weapon that can only fire short bursts and not fullauto.  It's all or nothing.
- **Ammo** - How much ammo is in a clip.  If the weapon has no ammo, set this to 0.
- **Recoil**. 1 is a good number for modern rifles. 2 for SMGs or weapons with worse recoil.
- **Range**.  This is just for reference and honestly most people ignore it anyway.
- **Blast Radius**.   This is just for reference and honestly most people ignore it anyway.
- **Damage Type** - Physical or Stun.  Stun wounds heal more quickly; otherwise they're identical.
- **Accuracy**.  This is a modifier (number of dice) applied to the attack skill.   Something that gives a +3 accuracy would turn a novice into a professional (or vice-versa) so that's a pretty big modifier.
- **Shrapnel** - 1=shrapnel, 0=no shrapnel.  Anti-vehicle weapons are typically armor piercing and do not do shrapnel damage. Anti-personnel weapons usually have shrapnel.
- **Reloadable** - 1=reloadable, 0=not.  You can use this for limited weapons - for example, a ship that might have 2 missiles and  can't be reloaded in flight.
- **Reload Time** - The number of turns it takes to reload this weapon.  Set this to 1 for most guns, but you may make it higher for historical firearms that take awhile to load.

### Armor

Armor is also configured on the Equipment DB object, with one attribute per armor type. 

    &ARMOR_HEAVY_VEST FS3Combat Equipment Stats=Military-Grade Kevlar Vest|Neck Chest Abdomen Groin|4|Personal

The parameters are much simpler.  They are, in order:
- **Description**
- **Hit Locations** - List of hit locations covered by the armor.   For instance, military grade body armor usually includes a long vest (covering the groin), a collar for the neck, and a helmet. A standard police vest usually covers only the torso.
- **Protection Value**.  See the discussion on balancing weapons and armor below.
- **Armor Class** - Personal or Vehicle.  This is just a guide to tell players whether the armor is for vehicles or people; it has no game effect.

### Balancing Weapons and Armor

Weapons have a penetration rating; armor has a protection rating.  If you want armor to work right, it's important to get these two numbers in sync with one another.  Otherwise your bullets will go through kevlar like butter.

| Weapon/armor | Penetration or Protection Value |
|------------|-------|
| Knife      | 1 | 
| Interior wall (drywall / plaster)      | 1      | 
| Solid wood furniture | 2      | 
| Anti-Personnel Shrapnel | 3 |
| Light body armor (undershirt vest) | 3 |
| Light small arms (pistols, SMGs) | 3 |
| Military body armor (kevlar vest) | 4 |
| Medium small arms (rifles) | 4 | 
| Car door | 4 |
| Brick wall | 5 |
| Light Truck | 5 | 
| Fighter Jet | 5 |
| Heavy small arms (12.5mm/.50cal) | 8 |
| Cargo aircraft / Bomber | 8 | 
| Anti-Aircraft Missile | 10 | 
| Light cannon (20mm) | 10 | 
| Armored Personnel Carrier (APC)| 15 | 
| Medium cannon (40mm)| 20 | 
| Light tank | 25 | 
| Anti-Tank Missile | 70 | 
| Heavy tank | 75 | 

The best way to sanity-check your values is to plug the numbers from various scenarios into the armor formula. If, for example, you discover that your heavy machinegun regularly bounces off your fighter jet armor, you know you have a problem.



### Vehicles

There are two things to configure if using vehicles:

- The general /types/ of vehicles available (Viper, Raptor, F16, etc.).
- Any /specific/ vehicles you wish to have set up for players to run around in.

The first one is required if you're going to use vehicles at all.  
The second one is optional, and is just there if you want the ability to track damage on vehicles between combats, or to personalize vehicles somehow.  
You can almost think of them as vehicle NPCs - with names and histories.

#### Configure Vehicle Types

    &VEHICLE_VIPER FS3Combat Equipment Stats=Viper space fighter|Piloting|5|Viper|Viper|KEW|0

The parameters are, in order:

- **Description**
- **Piloting Skill**.  The skill used to pilot the vehicle. Has no game effect; it’s just for reference.
- **Toughness**.  1-12 scale same as abilities. 
- **Armor**. Specify which type of armor (from the armor list) applies to this vehicle.
- **Hit Location Chart** - Specify which hit location chart (from the hit location chart list) applies to this vehicle.
- **Weapons** - Valid weapons for this vehicle.  
     The first weapon in the list is assigned to the vehicle pilot by default when they join a combat.  This list is just a guidance for players, and has no game effect.  
- **Dodge Bonus**.  A modifier (number of dice) applied to defense rolls while piloting the vehicle. May be a penalty if negative.

#### Configure Vehicle Armor

As mentioned above, armor used on vehicles needs to be explicitly configured in the Equipment DB just like any other armor. 

    &ARMOR_VIPER FS3Combat Equipment Stats=Viper Vehicle Armor|Body Controls Engine Right_Wing Left_Wing Tail Nose|10|Vehicle

#### Setting up Specific Vehicles

Use the +vehicle/create command to create specific vehicles for the players to use. The +combat system does not need actual MUSH objects for the vehicles; they can be entirely 'virtual', existing only as names in the vehicle database.  

If you actually do have coded vehicle objects, you just need to make sure that each one has a corresponding entry in the vehicle database.  Otherwise it won't be able to be used in combat.

#### Turning off Vehicles

There's currently no way to flip a switch and remove vehicles completely.  A bunch of code relies at least on the "isvehicle" function, so you have to install the vehicle module even if your game doesn't plan on actually ''using'' vehicles.   The best you can do is simply remove the vehicle help files and move the Commands objects out of the globals room.

### NPCs

You can set up any recurring NPCs using the +npc command.  Otherwise there's nothing else to configure for the NPC system.


### Hit Locations

You can customize the hit locations used on your game.  This is particularly important for vehicles, since different vehicle types will have vastly different hit location areas.  You may also need it if you have non-humanoid targets (like bug-eyed monsters with 6 legs).

The basic idea is that you define a number of hit location "charts".  For intstance: Soldier, Viper, Tank, Car, etc.   

**Soldier** is the hit location chart used for people, so you have to have that one.  
You can customize it as you like, though, to add/change/remove hit locations.

For each chart, you have to set up: 

**The list of possible hit locations and the damage severity for each (normal, vital or critical).**

    &DMG_SOLDIER FS3Combat Hit Locations=Head:Critical Neck:Critical Chest:Vital Abdomen:Vital Right_Arm:Normal Left_Arm:Normal Right_Hand:Normal Left_Hand:Normal Right_Leg:Normal Left_Leg:Normal Right_Foot:Normal Left_Foot:Normal
    &DMG_RAPTOR FS3Combat Hit Locations=Cockpit:Critical Controls:Critical Body:Vital Engine:Vital Right_Wing:Normal Left_Wing:Normal Cabin:Normal ECM:Normal

**The default hit location (center of mass)**

    &DEFAULT_SOLDIER FS3Combat Hit Locations=Chest
    &DEFAULT_RAPTOR FS3Combat Hit Locations=Body

**For a vehicle hit location chart, specify which hit locations have a high likelihood of causing crew injury. (i.e. where do the people sit.)**

    &CREWHIT_SOLDIER FS3Combat Hit Locations=
    &CREWHIT_RAPTOR FS3Combat Hit Locations=Cockpit Cabin

**The location where concussion damage should be applied for an explosion.**

  For people this should be the head; for vehicles it's less important.

    &CONCUSS_SOLDIER FS3Combat Hit Locations=Head
    &CONCUSS_RAPTOR FS3Combat Hit Locations=Body

**The target list for each hit location.  More details below**

    &HITLOC_SOLDIER_CHEST FS3Combat Hit Locations=Left_Leg Right_Leg Left_Hand Right_Hand Left_Arm Right_Arm Abdomen Head Neck Abdomen Chest Chest Chest Chest Chest
    &HITLOC_SOLDIER_LEFT_ARM FS3Combat Hit Locations=MISS MISS Head Left_Leg Chest Neck Left_Leg Chest Left_Hand Chest Left_Arm Left_Arm Left_Arm Left_Arm Left_Arm

* Each entry must list 15 hit locations.
* Every possible hit location must have a target list attribute.
* A "MISS" on a target list means that there's a chance that you'll miss completely even if your roll succeeded.  Since there is no die roll penalty for doing a called shot, this is the way you reflect that some places are harder to hit than others.  It is recommended that difficult called shot locations have 3-4 "MISS" entries, and that the default target location (e.g. Chest for a soldier) have 0-1.

#### Hit Location Walkthrough

This section will walk through the steps involved in creating a hit location chart, using a Battlestar Galactica Viper space fighter as an example.

1. Determine all available hit locations.

> Example: For a Viper, there would be: Cockpit, Body, Engine, Right Wing, Left Wing. We could further divide Body up into Nose/Tail if we wanted, or include some secondary targets like Controls, Landing Gear or Weapons, but we’ll choose not to for this example for the sake of brevity.

2. Determine the default hit location, typically the center of mass.

> Example: This would be the Viper’s Body.

3. Determine which hit locations are Vital or Critical.

> Example: We’ll dub Engine and Cockpit to be Critical and Body to be Vital. The wings aren’t terribly important to a Viper since it normally flies in space.

4. Determine which hit locations have a higher percentage for crew injuries. In other words, where do the people sit?

> Example: Vipers don’t have passengers, so the only person is the pilot, sitting in the Cockpit.

5. For each hit location, imagine a bullseye superimposed over that target point with three bands of color – green (in th center) / yellow (closest to green)/ red (furthest out). Now choose 5 hit locations in the green zone, 5 in the yellow zone, and 5 in the red zone. Remember that you can use the same hit location more than once to increase its chances of being rolled.

   Arrange the hit locations in order, red first, green last, and you’ve got your hit location chart for that targeted area.

   For the default hit location, all 15 values should represent hits somewhere on the target. For large target areas (like the torso on a human), there should be a fair chance of hitting exactly where you aimed at. For smaller hit locations, the chance of hitting exactly where you aimed should be smaller , and you should have some values on the chart represent a Near Miss by using "MISS" on the target list.

> Example: Let’s take the Viper’s Body first. The red zone would include the Body and perhaps the Engine. The yellow zone would include the Body, Cockpit, and both Wings. The green zone would include the Body, Wings, Engine.  Thus the final 15 values for Body could be:
>￼￼Left Wing Right Wing Engine Engine Body Left Wing Right Wing Body Body Cockpit Engine Engine Body Body Body
> We repeat that process for all the other hit locations.

### Other Preferences

A few other miscellaneous things to configure:

**Default Rifle - Set up the weapon that soldiers receive by default.**

    &DEFAULT_RIFLE FS3Combat Preferences=FN60 Rifle

**Healing skills - Configure the skills that are used for +heal and +treat on people and vehicles.**

    &FIRSTAID_SKILL FS3Combat Preferences=First_Aid
    &MEDICINE_SKILL FS3Combat Preferences=Medicine
    &JURYRIG_SKILL FS3Combat Preferences=Repair
    &REPAIR_SKILL FS3Combat Preferences=Repair

**Toughness and initiative ability - Configure the ability that is used for KO and initiative rolls.**

  Because these are typically attributes, you may want to provide a modifier to the roll or all them to roll double the number of dice.  You can do this by specifying X+Y as the ability.
  
    &TOUGHNESS_SKILL FS3Combat Preferences=Athletic+Athletic
    &INITIATIVE_ABILITY FS3Combat Preferences=Reactive+Reactive

**Delay between combat poses, in seconds.  You can tune this to give people more or less time to read.**

    &COMBAT_DELAY FS3Combat Preferences=3

**Configure which rooms are to be considered "hospitals", which give a bonus to healing.**

  This is just a space-separated list of room DB#s.  Hospitals only work for people; there is no equivalent for vehicles.

    &HOSPITALS FS3Combat Preferences=#-1

**Configure custom types.   The default types are soldier, pilot and passenger.**
  This is so you don't always have to set the same weapons, armor or vehicles on your badguys.  
  A custom type allows you to define additional types that you can use when joining a combat.  

    &TYPE_CENTURION FS3Combat Preferences=Soldier|LMG_Tripod|Centurion||soldier
    &TYPE_RAIDER FS3Combat Preferences=Pilot|||Raider|soldier

  The fields are:
  
- Type - All custom types must map to one of the standard types:  soldier pilot or passenger
- Weapon - The weapon they will be assigned. For pilots/passengers you can leave this blank and it will use the vehicle's default weapon.
- Armor - The armor they will be assigned. For pilots/passengers you can leave this blank and it will use the vehicle's default armor.
- Vehicle -  For pilot and passenger types, this says what vehicle type they will be assigned. For soldiers leave this blank.
- Hit Location Chart - The hit location type that they should use.  

NOTE: For pilots and passengers, specify the *person's* hit location table (usually "soldier") *not* the vehicle's.

## Functions

The following global functions are provided:

### Chargen Info Module

    RANK(<db#>) - Gets the rank set in Chargen.

### Skills Module

    ROLL_ABILITY(<player DB#, #-1 for NPC>,<ability name>,<modifier, may be blank>,<ruling attr, may be blank>) - Rolls an ability.

    ABILITY_LEVEL(<player DB#>,<ability>) - Staffonly function to get ability level.

    OPPOSED_RESULT(<result for char A>,<result for char B>) - Gives a code for two roll results.

    PRETTY_OPPOSED_RESULT(<name for char A>,<name for char B>,<return value from opposed result>) - Returns a pretty version of the win or lose text for an opposed roll. "Bob wins big!"

Note that the opposed functions need to be called in sequence:

    Call roll_ability() for A
    Call roll_ability() for B
    Call opposed_result() to get the result code
    Call pretty_opposed_result() to get the pretty text version 

I know this is annoying and will probably make it better in a future version.

### Damage Module

    DAMAGEMOD(<name>) - Gets someone's total damage modifier.

### Equipment Module

    WEAPONS() - Lists weapons

    ARMOR() - Lists armor

    VEHICLETYPES() - List vehicle types (as opposed to /specific/ vehicles)

    WEAPONSTAT(<weapon>,<statname>) - Gets a weapon statistic

    VEHTYPESTAT(<vehicle type>,<statname>) - Gets a vehicle type statistic

    ARMORSTAT(<armor>,<statname>) - Gets an armor statistic

### NPC Module

    ISNPC(<name>) - Returns 1 if they're a registered NPC

    NPCSTAT(<name>,<stat>) - Staff-only function to get a NPC's statistic.

### Vehicle Module

    ISVEHICLE(<name>) - Returns 1 if the name is a registered vehicle.

    VEHICLESTAT(<name>) - Gets a vehicle statistic. 
