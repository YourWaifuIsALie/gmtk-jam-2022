---
title: Day 3
date: 2022-09-17
categories: [blog]
layout: custom
order: 5
---
# GMTK Game Jam 2022: {{ page.title }}
{{ page.date | date: '%B %d, %Y' }}

## Progress

Work completed today (in hours):
- 0.50 pickup jetpack + respawner gate + limit movement to left/right press/release
- 0.75 big sky + ground tile palettes
- 2.00 stage creation, transporter, final playtest
- 1.00 build and upload

And that's it! I'm slowly getting a feel for this gamedev thing. Will I ever make it a living? Almost assuredly not, but it's great to dream.

Total time:
14hr + 4.25hr = 18.25hr = 18 hours, 15 minutes

## Final thoughts

- As always, waiting until the last minute to try and submit means the website will be down when you try
- I only uploaded a windows.exe file and not a browser-playable game, making me very sad
  - A web build was something I wanted going into this
    - It + new input system worked for a toy example I had made
  - I was dumb and forgot to build as I go, so when the built WebGL keyboard inputs didn't work I had no time to debug
- This experience has definitely swayed me away from Unity
  - Unity's package hell questionably supported features annoys me greatly
    - Old input/new input, HDRP/URP, etc.
    - I see people complaining about half-complete features and deprecations and it's starting to get to me too
  - The controversies (real and fake) aren't helping
  - If I'm going to be fiddling with an engine because it doesn't "just work", I'd rather work on Godot
    - I'm used to digging into open-source code to figure out problems so that's a big plus for me