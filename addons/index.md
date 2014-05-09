---
layout: page
title: Addons
---

**Addons** are optional modules.  Most Addons require only the Core Modules in order to operate, but a few depend on each other.  Any special dependencies are explained on the addon pages.

  {% assign sorted_pages = site.pages | sort:"name" %}
  {% for page in sorted_pages %}
  {% if page.resource == true %}
  {% for pc in page.categories %}
  {% if pc == "addons" %}
  * <a href="{{site.siteroot}}/{{ page.url }}">{{ page.title }}</a> - {{ page.description }}
  {% endif %}   
  {% endfor %}  
  {% endif %}  
  {% endfor %} 
