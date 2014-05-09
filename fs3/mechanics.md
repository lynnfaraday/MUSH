---
layout: module
title: FS3 Mechanics
resource: true
categories: [ fs3 ]
---

This file describes some of the dice mechanics behind the FS3 system.  It may help game admins understand how to configure or tweak the system.

## Ability Rolls

It rolls a number of eight-sided dice equal to the skill plus the ruling attribute.

> Exmaple  Firearms (3) + Reactive (2) would roll 5d8.

A die roll of 7 or higher is considered a Hit.

In general, a single Hit is sufficient for the roll to succeed, but sometimes it is desirable to get a more fine-grained appraisal of success. In those cases, count the total number of Hits and consult the table below.

| Total Hits | Success Level |
|-----------|--------------|￼
| 0 |￼￼Failure |￼
|￼1 |￼Success |￼
|￼2-3 |￼Good Success |￼
|￼4-5 |￼Great Success |￼￼
|￼6+ |￼Amazing Success |


It is also possible for you to mess up royally. If you get no Hits and the number of ‘1’s is equal to or greater than your **Attribute** rating, you have suffered an Embarrassing Failure. Usually this means something really bad has happened – not only did you fail, you may have made things worse.

NPCs have only a single rating, and use that in place of Attribute+Skill.

## Opposed Rolls

In an Opposed Roll, each character makes an Ability Roll as normal. Whoever gets the most Hits “beats” the other one, though it’s still possible for both characters to fail if neither one gets any Hits.

Sometimes it is useful to determine the margin of success – how badly did the winner crush his opponent. If desired, subtract the loser’s Hits from the winner’s to determine the net Hits, and consult the table below.

| Net Hits | Margin of Success | 
|-----------|--------------|￼
| 0 | Draw |
|￼1 | Marginal Victory |
|￼2-3 | Solid Victory |
| 4+ | Crushing Victory |

## Combat

### Aiming

Aiming is automatically successful, and will give a bonus of +3 to attack that same target next turn. Spending more than one turn aiming does not increase the modifier, and the modifier is lost if you switch targets.

### Suppressing

Firearms combat is disconcerting, which is reflected by Suppression. Every time a character is fired upon, he receives a single Suppression Point.

If you deliberately try to keep someone’s head down using the Suppress action, you don't do damage but you do twice as many Suppression Points. Note that melee attacks do not inherently suppress someone, but you can deliberately suppress someone using the Suppress action, even with melee combat.

All attacks suffer a modifier of -1 for every suppression point. Suppression does not apply to defense rolls.

A character may have at most 5 Suppression Points at any given time. At the end of every turn, all characters subtract 2 Suppression Points from their total.

### Stance Modifiers

* Banzai -  +3 to attack, -3 to defense
* Evade - -3 to attack, +3 to defense
* Cautious - -1 to attack, +1 to defense
* Cover - no modifier, but attacks aimed at you have a chance of hitting the cover instead and being stopped

### Other Modifiers

* Luck -- +5
* Suppression -- -1 for each Suppression Point, up to -5
* Attacking with / Defending with a melee weapon -- +3.   (this prevents slugfests where everyone keeps missing, which is silly and unrealistic)
* Range -- -1 for medium, -3 for long

### Armor

The **Stop Chance** is the chance that armor has of stopping the bullet completely, based on the Penetration value of the weapon and the Protection value of the armor.  It is a complicated formula.  You'll need a calculator if you want to do it by hand.  Otherwise let the code figure it out.

    Stop Chance = ((((Protection/Penetration) * 2) - 1) * 25) + 10

Even if the armor doesn't stop the bullet completely, it can still reduce its lethality.  Here's how it all works:

* Determine the Penetration and Protection values of the weapon and armor.
* Use the formula below to determine the **Stop Chance**, which is the chance of the armor stopping the attack.
* Roll percentile dice.
* A roll of 100 means that the hit bypasses the armor completely.
* A roll less than or equal to the Stop Chance means that the armor stops the attack completely.
* Otherwise roll percentile dice again. The maximum result is equal to the Stop Chance. This is the damage modifier.
* Apply the damage modifier to the damage roll. 

> Example: Bob got shot in the chest (armor protection 4) with a rifle (pen 4). Stop Chance is 35, so there's a 35% chance that the armor will stop the bullet completely. If it doesn't - lethality will be reduced anywhere from 0 to 35%.

### Cover

If someone is behind cover, there is a chance that the attack will hit the cover instead of the intended target. To determine the effect of cover:

* If the attacker got 3 or more hits on his attack roll, he has hit an exposed body part. Cover does not apply.
* Otherwise roll percentile dice. There is a 75% chance that cover will apply.
* If cover applies, treat it like armor with a protection value of "4".  Just like armor, it will have a chance of stopping the attack completely, or reducing its damage.
* Apply the damage modifier to the damage roll. 

Note: If the character is wearing body armor, the effects of armor and cover are cumulative.

### Burst Fire

Automatic fire is resolved just like a regular attack, but with one attack and defense roll per bullet. All bullets after the first one receive a negative modifier based on the recoil statistic of the weapon multiplied by the number of bullets so far (excluding the first).

> Example: Bob is firing full-auto. His weapon has a recoil modifier of 1. He makes 10 attack rolls total, the first with a -0 modifier, the second with -1, the third with -2, etc. Defense rolls, damage, armor, etc. must be determined separately for each of the bullets.

Full-auto fire can be directed at up to 5 targets in a single turn, with the bullets distributed as equally as possible among the targets. One bullet is “lost” between each target. Resolve all bullets against a single target before switching to the next one.

> Example: This time Bob is firing full-auto at Jane, Harry and Marcus. It is a 10-round burst, but he loses 1 bullet every time he switches targets. That leaves him with 8 bullets, divided among 3 people. Jane gets 3, Harry gets 3, and Marcus gets 2.

Note: If a character doesn’t have enough bullets in their clip for a complete burst, they can empty the clip and do a partial burst, as long as there are enough bullets for all the targets. The only change is that you’ll make fewer attack rolls.

### Explosions

Explosives do two types of damage - concussion and possibly shrapnel. Concussion is an automatic wound to the head representing the force of the explosion. Shrapnel is a￼number of separate wounds, each to a different hit location. Some explosive weapons don’t do shrapnel damage; this usually means they are armor-piercing anti-vehicle rounds rather than anti-personnel rounds.

Explosion damage is determined using a “zone” system. The table below shows the number of shrapnel wounds and the damage modifier for the five different explosive zones.

Zone Shrapnel Damage Modifier
0 4d4 +50
1 1d6 0
2 1d4 -25
3 1d4 / 2 -50
4 No Shrapnel -100

You can determine the people affected by the explosion using the intended target point and the weapon’s blast radius statistic:

Distance from Intended Target Zone 
Within the blast radius 0
Within double the blast radius 1
Further away n/a – safe from explosion

Zones 2-4 come into play after the attack and defense rolls. They represent situations where either the attacker didn’t get the explosive quite where he wanted it, or the defender managed to dive away, get behind something sturdy, etc. before the blast.

If an attack roll fails, it doesn’t necessarily mean they missed completely. “Close” counts in hand grenades, after all. Increase each target’s zone by 1 for a regular failure, and 2 for an Embarrassing Failure.

> Example: Jesse throws a grenade at Mike, intending it to land right next to him. This would have put Mike in Zone 0, but Jesse rolled poorly and got no hits. This puts Mike in Zone 1 instead.

If the defender rolls better than the attacker, the zone is further modified by the difference between the attack and defense rolls.

> Example: Not only did Jesse roll no Hits (putting Mike in Zone 1), Mike rolled a 2 on his defense roll. This puts Mike all the way out in Zone 3.

Once you’ve determined which Zone everyone ends up in, resolving damage is straightforward:

* Apply a single concussion wound to the character’s head.
* If the weapon’s shrapnel statistic says that it does shrapnel damage, roll the dice indicated in the explosive Zone table to determine how many pieces of shrapnel hit the person. Apply that many individual shrapnel wounds to the character. Resolve each wound like a normal attack, only you don’t have to make attack and defense rolls – the attack automatically hits.
* Shrapnel has a damage modifier based on the Zone and a penetration value of 3.
* Concussion has a damage modifier based on the Zone plus the weapon’s damage modifier, and a penetration based on the weapon.

### Vehicle Weapons

Weapons are classified as either Personal weapons or Vehicle weapons. Although you can shoot a Personal weapon at a vehicle, chances are it’s not going to do very much. A bullet may be able to pierce a car door, but unless it hits something critical in the engine, the car will run just fine.

Likewise, a main tank gun may have a damage modifier of 0, but that’s against tanks. If that hits a person, you’re going to be picking up body parts. Of course, aiming a tank gun at a person presents another set of challenges.

No special rules are provided for firing vehicle weapons against people or vice-versa; this is left up to the Storyteller’s discretion.

### Crew Hits

When a vehicle is damaged in its default hit location (the center of mass/main body), there is a chance the passengers inside will be injured. That chance is reflected in the table below. 

If the vehicle is damaged in a crew or passenger compartment (like the cockpit), the chance is 5 times higher than the listed value (up to a max of 90%).

Roll percentile dice for each passenger, and consult the table below. If the die roll is less than the chance to be injured, the passenger is hurt.

Damage Severity Chance for Passenger Hit  Number of Hits
Light 1% 1
Moderate 5% 1
Serious 10% 1d4 / 2
Critical 50% 1d4

If a passenger is hurt, consult the number of hits column to determine how many wounds they suffer. Resolve each wound like a normal attack, only you don’t have to make attack and defense rolls – the attack automatically hits. Shrapnel from vehicle damage has a damage modifier of 0 and a penetration value of 3.

## Damage

### Hit Location

FS3 uses a custom hit location system. Where the attack hit affects damage, determines whether armor applies, and is generally useful for roleplay.  Imagine a dartboard superimposed over the target, with the bullseye centered at the point you’re aiming for.

If you roll well enough, you’re going to hit the bullseye. The worse you roll, the further from the bullseye you’re going to hit. You might still hit the target – albeit in a different spot than you intended – or you might miss completely.

To determine hit location:
* Roll 2d8.
* Add the difference between the attacker’s roll result and the defender’s roll result.
* Find the hit location chart corresponding to the targeted hit location.  Typically this will be the default hit location (center of mass) unless you're doing a called shot.
* Find the hit location in the chart based on the modified die roll.

> Example:  Bob is shooting John, targeting his head.  The head hit location table is:
>    MISS MISS MISS MISS Right_Arm Left_Arm Abdomen Chest Chest Neck Head Head Head Head Head
> The random hit location roll is 7.
> Bob rolled 4 hits on his attack roll.  John rolled 1 hits on defense.  We modify the random hit location by +3 (4-1) to get a 10.
> The 10th hit location on the head chart is Neck.

### Hit Location Damage Modifier

Vital hit locations give a +15 damage modifier.

Critical hit locations give a +30 damage modifier.

### Damage Severity

Wounds in FS3 are tracked individually, contributing to an overall wound modifier based on how hurt you are.

The damage severity of a wound is based on a percentile dice roll modified by:

* The weapon’s lethality statistic.
* Any damage modifiers resulting from hit location. See Hit Location, above.
* Any damage modifiers resulting from armor. See Armor, above.
* Any damage modifiers resulting from cover. See Cover, above.

All modifiers are cumulative, and are added to the percentile roll to give a final result.

| Modified Damage Roll | Severity |
|-----------|--------------|￼
| 40 or less | Light | 
| 41 – 80 | ￼Moderate| 
| 81 – 99| Serious| 
| 100 or higher| Critical| 

### Healing Damage

For a wound to heal down to the next lower severity level, the character must accumulate a number of Healing Points shown in the table below.

Wound Level Healing Points Required
Light 2
Moderate 3
Serious 4
Critical 5

Note: Stun damage wounds require 1/5th the listed number of Healing Points, so they heal very quickly.

As soon as the wound has enough points, it automatically becomes one severity level lower. In other words, a Critical wound becomes Serious, a Serious wound becomes Moderate, etc.

Every 24 hours, the injured character gets to make a toughness roll and his doctor (if there is one) gets to make a medicine roll.

* If the toughness roll is successful, the character receives 2 Healing Points for every wound. If it fails, he receives only 1.
* If the medicine roll is successful, the character receives 1 Healing Point for every wound. If it fails, he receives 0.
* If the character is in a hospital, he receives an extra Healing Point. 

Notes:

* A doctor can make only one medicine roll per patient per day, and can tend to a maximum number of patients equal to 1⁄2 their medicine ability rating.
* Only one doctor can make a medicine roll for a single patient.

### Vehicle Healing

Vehicle repair works like healing, with Healing Points accumulated to reduce wound severity. However, there are a few differences:

* Instead of medicine / first aid, wounds are treated using a repair skill specified by the Storyteller.
* Vehicle wounds require 1/5th the total number of Healing Points, because machines are a lot easier to fix than people.
* Vehicles don’t heal by themselves; they only get better if someone is fixing them. First aid on vehicles represents quick-fix jury-rigging, and can be done in the midst of combat using the Treat action.

