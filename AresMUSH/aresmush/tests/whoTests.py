# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

import unittest
import re

from aresmush.modules.core.who import Who
from aresmush.engine.models.player import Player

class MockServer:
    connections = []
    
class MockConnection:
    client_address = ""
    player = None
    
class WhoTests(unittest.TestCase):

    def setUp(self):
        self.whoModule = Who()

    def test_who_counts_number_players(self):
        server = MockServer()
        server.connections.append('1')
        server.connections.append('2')
        self.assertEqual(2, self.whoModule.numPlayersConnected(server))

    def whoStringMatches(self, connection, matchString):
        whoStr = self.whoModule.getWhoString(connection)
        matches = re.match(matchString, whoStr)
        self.assertTrue(matches)

    def test_who_returns_client_address(self):
        conn = MockConnection()
        conn.client_address = "123"
        self.whoStringMatches(conn, ".*123.*")
         
    def test_who_returns_anon_player_name(self):
        conn = MockConnection()
        self.whoStringMatches(conn, ".*---.*")
        
    def test_who_returns_real_player_name(self):
        conn = MockConnection()
        conn.player = Player(name="Bob")
        self.whoStringMatches(conn, ".*Bob.*")
