---
layout: page
title: Installation
---

These documents talk about how to install and upgrade the code.

  {% for page in site.pages %}
  {% if page.resource == true %}
  {% for pc in page.categories %}
  {% if pc == "install" %}
  * <a href="{{ page.url }}">{{ page.title }}</a> - {{ page.description }}
  {% endif %}   
  {% endfor %}  
  {% endif %}  
  {% endfor %} 
