# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

import unittest
import re

from aresmush.modules.management.baseModule import BaseModule
from aresmush.engine.commands.command import Command

class MockModule(BaseModule):
    wasCalled = ""
    
    def command_foo(self, command):
        self.wasCalled = "foo"
        return True
    def command_noprefix_bar(self, command):
        self.wasCalled = "bar"
        return True
    def command_plus_baz(self, command):
        self.wasCalled = "baz"
        return True
    def anoncommand_whee(self, command):
        self.wasCalled = "whee"
        return True
    
class BaseModuleTests(unittest.TestCase):

    def setUp(self):
        self.mod = MockModule()

    def verifyAnonCommandVals(self, commandString, expectWasCalled, expectedReturn):
        command = Command(commandString)
        returnVal = self.mod.handleAnonCommand(command)
        self.assertEqual(expectWasCalled, self.mod.wasCalled)
        self.assertEqual(expectedReturn, returnVal)
    
    def verifyCommandVals(self, commandString, expectWasCalled, expectedReturn):
        command = Command(commandString)
        returnVal = self.mod.handleCommand(command)
        self.assertEqual(expectWasCalled, self.mod.wasCalled)
        self.assertEqual(expectedReturn, returnVal)
        
    def test_catchall_handler_mapped_correctly(self):
        self.verifyCommandVals("+foo", "foo", True)
        
    def test_noprefix_handler_mapped_correctly(self):
        self.verifyCommandVals("bar", "bar", True)
        
    def test_prefix_handler_mapped_correctly(self):
        self.verifyCommandVals("+baz", "baz", True)
    
    def test_prefix_doesnt_match_noprefix_handler(self):
        self.verifyCommandVals("@bar", "", False)
    
    def test_other_prefix_doesnt_match_prefix_handler(self):
        self.verifyCommandVals(".baz", "", False)

    def test_anon_command_matching_map(self):
        self.verifyAnonCommandVals("+whee", "whee", True)

    def test_anon_command_doesnt_match_map(self):
        self.verifyAnonCommandVals("+foo", "", False)


        
        