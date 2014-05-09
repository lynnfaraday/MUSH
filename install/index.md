---
layout: page
title: Installation
---

These documents talk about how to install and upgrade the code.

  {% assign sorted_pages = site.pages | sort:"name" %}
  {% for page in sorted_pages %}
  {% if page.resource == true %}
  {% for pc in page.categories %}
  {% if pc == "install" %}
  * <a href="{{site.siteroot}}/{{ page.url }}">{{ page.title }}</a> - {{ page.description }}
  {% endif %}   
  {% endfor %}  
  {% endif %}  
  {% endfor %} 
