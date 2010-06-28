# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

from aresmush.modules.core import descriptions
from aresmush.modules.core import game
from aresmush.modules.core import movement
from aresmush.modules.management.aresModule import AresModule

def CreateMovementModule():
    return movement.Movement()

def CreateDescriptionsModule():
    return descriptions.Descriptions()

def CreateGameModule():
    return game.Game()

def AllModules():
    allModules = []
    allModules.append(AresModule(movement, None, CreateMovementModule))
    allModules.append(AresModule(game, None, CreateGameModule))
    allModules.append(AresModule(descriptions, None, CreateDescriptionsModule))
    return allModules