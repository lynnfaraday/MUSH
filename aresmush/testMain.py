import unittest

from aresmush.tests.commandTests import CommandTests

if __name__ == '__main__':
    suite = unittest.TestLoader().loadTestsFromTestCase(CommandTests)
    unittest.TextTestRunner(verbosity=2).run(suite)
