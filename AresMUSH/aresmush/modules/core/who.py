# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

from aresmush.modules.management.baseModule import BaseModule

class Who(BaseModule):

    name = "Who"
    
    def anonCommand_who(self, command):
        self.handleWho(command)
        return True
    
    def command_who(self, command):
        self.handleWho(command)
        return True
        
    def handleWho(self, command):
        numPlayers = self.numPlayersConnected(command.connection.server)
        connection.send("WHO: %(len)d players connected", { "len" : numPlayers })
        for connection in command.connection.server.connections:
            connection.send(self.getWhoString(command.connection))
    
    def numPlayersConnected(self, server):
        conns = server.connections
        return len(conns)
    
    def getWhoString(self, connection):
        whoStr = str(connection.client_address)
        if (connection.player == None):
            whoStr += '---'
        else:
            whoStr += connection.player.name
        return whoStr
