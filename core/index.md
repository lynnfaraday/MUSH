---
layout: page
title: Core
---

The **Core Modules** are the heart of the codebase.  They provide essential functionality and are required for all of the other systems to run.

  {% assign sorted_pages = site.pages | sort:"name" %}
  {% for page in sorted_pages %}
  {% if page.resource == true %}
  {% for pc in page.categories %}
  {% if pc == "core" %}
  * <a href="{{site.siteroot}}/{{ page.url }}">{{ page.title }}</a>
  {% endif %}   
  {% endfor %}  
  {% endif %}  
  {% endfor %} 
