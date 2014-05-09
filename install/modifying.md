---
layout: module
title: Modifying the Code
resource: true
categories: [ install ]
description: Making custom code modifications.
---

Most code modules provide various customizations.  Their instruction pages detail things that you can configure.  If the system tells you to configure it, then your customizations should be safely kept even when you upgrade to future versions.

However, if you customize the code beyond these standard configurations, you make it so that you won't be able to use the automatic upgrade installers in the future.

Two important caveats if you intend to use the upgrade installer in the future:

* **Do not add commands to the objects.**   My installer will wipe out any command and help attributes CMD-* and HELP_* when it does the upgrade. Your changes will be lost.  Put them on a different object.
* **Do not rename the objects.**   The install manager system relies on object names, and if you go around renaming everything, you won't be able to upgrade them later.

Before you start modifying the code, I'd suggest you talk to me and see if what you want to change can be made configurable.  If it's something reasonable that I think future users will benefit from, chances are I'll go ahead and release a new version with that as a customizable option.

If you don't, you'll be on your own for upgrades. You can use the diffs on the code repository to see out what changed so you can make those changes in your own code.