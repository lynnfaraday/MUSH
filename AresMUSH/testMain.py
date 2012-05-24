import unittest

import aresmush.tests
from aresmush.tests.commandTests import CommandTests
from aresmush.tests.moduleManagerTests import ModuleManagerTests
from aresmush.tests.whoTests import WhoTests
from aresmush.tests.baseModuleTests import BaseModuleTests

if __name__ == '__main__':
    testSuites = []
    suite1 = unittest.TestLoader().loadTestsFromTestCase(CommandTests)
    suite2 = unittest.TestLoader().loadTestsFromTestCase(ModuleManagerTests)
    suite3 = unittest.TestLoader().loadTestsFromTestCase(WhoTests)
    suite4 = unittest.TestLoader().loadTestsFromTestCase(BaseModuleTests)
    
    alltests = unittest.TestSuite((suite1, suite2, suite3, suite4))
    unittest.TextTestRunner(verbosity=2).run(alltests)
    

