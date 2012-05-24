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

class TestModuleFactory:
    
    def createFooModule(self):
        return Foo()
        
    def createBarModule(self):
        return Bar()
    
    def allModules(self):
        allModules = []
        allModules.append(AresModule(testModules, None, self.createFooModule))
        allModules.append(AresModule(testModules, None, self.createBarModule))
        return allModules
    
class ModuleManagerTests(unittest.TestCase):
    
    def setUp(self):
        factory = TestModuleFactory()
        self.manager = ModuleManager(factory)
        
    def test_manager_reports_foo_installed(self):
        self.assertTrue(self.manager.isInstalled("Foo"))
    
    def test_manager_reports_bar_installed(self):
        self.assertTrue(self.manager.isInstalled("Bar"))
    
    def test_manager_reports_baz_not_installed(self):
        self.assertFalse(self.manager.isInstalled("Baz"))
    
    def test_manager_can_load_foo(self):
        foo = self.manager.currentInstance("Foo")
        self.assertEqual(1, foo.loadCount)
        
    def test_manager_can_reload_foo(self):
        foo = self.manager.currentInstance("Foo")
        foo.loadCount = 27
        self.manager.reload("Foo")
        
        foo = self.manager.currentInstance("Foo")
        self.assertEqual(1, foo.loadCount)

    def test_reloading_foo_does_not_affet_bar(self):
        bar = self.manager.currentInstance("Bar")
        bar.loadCount = 27
        self.manager.reload("Foo")
        
        bar = self.manager.currentInstance("Bar")
        self.assertEqual(27, bar.loadCount)        
    
    def test_reloading_all_affects_both(self):
        foo = self.manager.currentInstance("Foo")
        bar = self.manager.currentInstance("Bar")
        foo.loadCount = 27
        bar.loadCount = 28
        self.manager.reloadAll()
        
        foo = self.manager.currentInstance("Foo")
        self.assertEqual(1, foo.loadCount)
    
        bar = self.manager.currentInstance("Bar")
        self.assertEqual(1, bar.loadCount)        
    
        

