---
layout: custom
---

![A happy turtle with a die](/media/capsule_image.png)

{% for category in site.category-list %}
# {{ category }}
<ul>
  {% assign sorted_pages = site.pages | sort:"order" %}
  {% for page in sorted_pages %}
    {% for page-category in page.categories %}
      {% if page-category == category %}
        <li><a href="{{ site.baseurl }}{{ page.url }}">{{ page.title }}</a></li>
      {% endif %}
    {% endfor %}
  {% endfor %}

  {% for post in site.posts reversed %}
    {% for post-category in post.categories %}
      {% if post-category == category %}
        <li><a href="{{ site.baseurl }}{{ post.url }}">{{ post.title }}</a></li>
      {% endif %}
    {% endfor %}
  {% endfor %}
</ul>
{% endfor %}

## images
- [Initial prototyping, two hours in](/media/hours_02.png)
- [The test area for all gameplay mechanics](/media/test_area.png)