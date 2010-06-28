# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

import unittest

from aresmush.engine.commands.command import Command

class CommandTests(unittest.TestCase):

    def setUp(self):
        self.command = Command('')
        
    def test_can_crack_command_only(self):
        self.command.Crack("test")
        self.assertEqual(self.command.Name, "test")
        self.assertEqual(self.command.Args, "")
        self.assertEqual(self.command.Switch, "")

    def test_can_crack_command_and_switch(self):
        self.command.Crack("test/switch")
        self.assertEqual(self.command.Name, "test")
        self.assertEqual(self.command.Args, "")
        self.assertEqual(self.command.Switch, "switch")

    def test_can_crack_command_and_args(self):
        self.command.Crack("test args")
        self.assertEqual(self.command.Name, "test")
        self.assertEqual(self.command.Args, "args")
        self.assertEqual(self.command.Switch, "")

    def test_can_crack_command_switch_and_args(self):
        self.command.Crack("test/switch args")
        self.assertEqual(self.command.Name, "test")
        self.assertEqual(self.command.Args, "args")
        self.assertEqual(self.command.Switch, "switch")

    def test_can_crack_empty_command(self):
        self.command.Crack("")
        self.assertEqual(self.command.Name, "")
        self.assertEqual(self.command.Args, "")
        self.assertEqual(self.command.Switch, "")

    def test_can_crack_command_with_switch_and_spaces_and_slashes_in_arg(self):
        self.command.Crack("test/switch arg/arg space")
        self.assertEqual(self.command.Name, "test")
        self.assertEqual(self.command.Args, "arg/arg space")
        self.assertEqual(self.command.Switch, "switch")

    def test_can_crack_command_with_spaces_and_slashes_in_arg(self):
        self.command.Crack("test arg/arg space")
        self.assertEqual(self.command.Name, "test")
        self.assertEqual(self.command.Args, "arg/arg space")
        self.assertEqual(self.command.Switch, "")
        
    def test_can_crack_command_with_newline(self):
        self.command.Crack("test/switch args\n")
        self.assertEqual(self.command.Name, "test")
        self.assertEqual(self.command.Args, "args")
        self.assertEqual(self.command.Switch, "switch")

