---
layout: page
title: Core
---

The **Core Modules** are the heart of the codebase.  They provide essential functionality and are required for all of the other systems to run.

  {% for page in site.pages %}
  {% if page.resource == true %}
  {% for pc in page.categories %}
  {% if pc == "core" %}
  * <a href="{{ page.url }}">{{ page.title }}</a>
  {% endif %}   
  {% endfor %}  
  {% endif %}  
  {% endfor %} 
