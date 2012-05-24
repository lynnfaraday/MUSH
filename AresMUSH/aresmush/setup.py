# -----------------------------------------------------------------------------
# -- AresMUSH.  Copyright 2010 by Linda Naughton ("Faraday")
# -- See http://www.wordsmyth.org/aresmush for documentation and license info.
# -----------------------------------------------------------------------------

from distutils.core import setup
from babel.messages import frontend as babel

setup(name='AresMUSH',
      version='0.1',
      description='Ares MUSH Server',
      author='Linda Naughton',
      author_email='gward@python.net',
      url='http://www.wordsmyth.org/aresmush',
      package_dir = {'': 'aresmush'},
      cmdclass = {'compile_catalog': babel.compile_catalog,
                'extract_messages': babel.extract_messages,
                'init_catalog': babel.init_catalog,
                'update_catalog': babel.update_catalog}
      )
