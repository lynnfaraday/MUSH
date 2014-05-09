---
layout: page
title: Releases
---

The pages below describe the available code versions.

  {% assign sorted_pages = site.pages | sort:"name" %}
  {% for page in sorted_pages %}
  {% if page.resource == true %}
  {% for pc in page.categories %}
  {% if pc == "release" %}
  * <a href="{{site.siteroot}}/{{ page.url }}">{{ page.title }}</a>
  {% endif %}   
  {% endfor %}  
  {% endif %}  
  {% endfor %} 
