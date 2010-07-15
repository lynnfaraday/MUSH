#!/usr/bin/python
# -*- coding: utf-8 -*-
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
        self.command.crack("test")
        self.assertEqual(self.command.name, "test")
        self.assertEqual(self.command.args, "")
        self.assertEqual(self.command.switch, "")

    def test_can_crack_command_and_switch(self):
        self.command.crack("test/switch")
        self.assertEqual(self.command.name, "test")
        self.assertEqual(self.command.args, "")
        self.assertEqual(self.command.switch, "switch")

    def test_can_crack_command_and_args(self):
        self.command.crack("test args")
        self.assertEqual(self.command.name, "test")
        self.assertEqual(self.command.args, "args")
        self.assertEqual(self.command.switch, "")

    def test_can_crack_command_switch_and_args(self):
        self.command.crack("test/switch args")
        self.assertEqual(self.command.name, "test")
        self.assertEqual(self.command.args, "args")
        self.assertEqual(self.command.switch, "switch")

    def test_can_crack_empty_command(self):
        self.command.crack("")
        self.assertEqual(self.command.name, "")
        self.assertEqual(self.command.args, "")
        self.assertEqual(self.command.switch, "")

    def test_can_crack_command_with_switch_and_spaces_and_slashes_in_arg(self):
        self.command.crack("test/switch arg/arg space")
        self.assertEqual(self.command.name, "test")
        self.assertEqual(self.command.args, "arg/arg space")
        self.assertEqual(self.command.switch, "switch")

    def test_can_crack_command_with_spaces_and_slashes_in_arg(self):
        self.command.crack("test arg/arg space")
        self.assertEqual(self.command.name, "test")
        self.assertEqual(self.command.args, "arg/arg space")
        self.assertEqual(self.command.switch, "")
        
    def test_can_crack_command_with_newline(self):
        self.command.crack("test/switch args\n")
        self.assertEqual(self.command.name, "test")
        self.assertEqual(self.command.args, "args")
        self.assertEqual(self.command.switch, "switch")
    
    def test_can_crack_command_with_unicode_chars(self):
        cmd = u"say Très bien"
        self.command.crack(cmd)
        self.assertEqual(self.command.name, "say")
        self.assertEqual(self.command.args, u"Très bien")
        

