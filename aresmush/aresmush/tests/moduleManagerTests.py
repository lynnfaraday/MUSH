# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

import unittest

from aresmush.modules.management.aresModule import AresModule
from aresmush.modules.management.moduleManager import ModuleManager
from aresmush.tests import testModules
from aresmush.tests.testModules import Foo
from aresmush.tests.testModules import Bar

class TestFactory:
    
    def CreateFooModule(self):
        return Foo()
        
    def CreateBarModule(self):
        return Bar()
    
    def AllModules(self):
        allModules = []
        allModules.append(AresModule(testModules, None, self.CreateFooModule))
        allModules.append(AresModule(testModules, None, self.CreateBarModule))
        return allModules
    
class ModuleManagerTests(unittest.TestCase):
    
    def setUp(self):
        factory = TestFactory()
        self.manager = ModuleManager(factory)
        
    def test_manager_reports_foo_installed(self):
        self.assertTrue(self.manager.IsInstalled("Foo"))
    
    def test_manager_reports_bar_installed(self):
        self.assertTrue(self.manager.IsInstalled("Bar"))
    
    def test_manager_reports_baz_not_installed(self):
        self.assertFalse(self.manager.IsInstalled("Baz"))
    
    def test_manager_can_load_foo(self):
        foo = self.manager.CurrentInstance("Foo")
        self.assertEqual(1, foo.LoadCount)
        
    def test_manager_can_reload_foo(self):
        foo = self.manager.CurrentInstance("Foo")
        foo.LoadCount = 27
        self.manager.Reload("Foo")
        
        foo = self.manager.CurrentInstance("Foo")
        self.assertEqual(1, foo.LoadCount)

    def test_reloading_foo_does_not_affet_bar(self):
        bar = self.manager.CurrentInstance("Bar")
        bar.LoadCount = 27
        self.manager.Reload("Foo")
        
        bar = self.manager.CurrentInstance("Bar")
        self.assertEqual(27, bar.LoadCount)        
    
    def test_reloading_all_affects_both(self):
        foo = self.manager.CurrentInstance("Foo")
        bar = self.manager.CurrentInstance("Bar")
        foo.LoadCount = 27
        bar.LoadCount = 28
        self.manager.ReloadAll()
        
        foo = self.manager.CurrentInstance("Foo")
        self.assertEqual(1, foo.LoadCount)
    
        bar = self.manager.CurrentInstance("Bar")
        self.assertEqual(1, bar.LoadCount)        
    
        

