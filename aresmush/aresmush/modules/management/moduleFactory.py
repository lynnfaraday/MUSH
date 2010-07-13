# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

from aresmush.modules.core import descriptions
from aresmush.modules.core import game
from aresmush.modules.core import movement
from aresmush.modules.management.aresModule import AresModule

class ModuleFactory:
    
    def createMovementModule(self):
        return movement.Movement()
    
    def createDescriptionsModule(self):
        return descriptions.Descriptions()
    
    def createGameModule(self):
        return game.Game()
    
    def allModules(self):
        all = []
        all.append(AresModule(movement, None, self.createMovementModule))
        all.append(AresModule(game, None, self.createGameModule))
        all.append(AresModule(descriptions, None, self.createDescriptionsModule))
        return all