# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

from aresmush.modules.management.baseModule import BaseModule

class Who(BaseModule):

    name = "Who"
    
    def anonCommand_who(self, connection, command):
        self.handleWho(connection, command)
        return True
    
    def command_who(self, connection, command):
        self.handleWho(connection, command)
        return True
        
    def handleWho(self, connection, command):
        connection.send("WHO: %(len)d players connected", { "len" : self.numPlayersConnected(connection.server) })
        for connection in connection.server.connections:
            connection.send(self.getWhoString(connection))
    
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
