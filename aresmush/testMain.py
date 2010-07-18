import unittest

from aresmush.tests.commandTests import CommandTests
from aresmush.tests.moduleManagerTests import ModuleManagerTests
from aresmush.tests.whoTests import WhoTests

if __name__ == '__main__':
    suite = unittest.TestLoader().loadTestsFromTestCase(CommandTests)
    unittest.TextTestRunner(verbosity=2).run(suite)
    
    suite = unittest.TestLoader().loadTestsFromTestCase(ModuleManagerTests)
    unittest.TextTestRunner(verbosity=2).run(suite)
    
    suite = unittest.TestLoader().loadTestsFromTestCase(WhoTests)
    unittest.TextTestRunner(verbosity=2).run(suite)
