---
title: Day 2
date: 2022-09-16
categories: [blog]
layout: custom
---
# GMTK Game Jam 2022: {{ page.title }}
{{ page.date | date: '%B %d, %Y' }}

## Progress

Overnight thoughts:
- Why is turtle switching even on the road map? Just go all-in with the jet pack and related puzzles
  - Flying around as a jet turtle should be fun enough on its own
  - Picking up blocks, then picking them up and flinging them with velocity makes sense

Work completed today (in hours):
- 0.50 beginning turtle walk
- 0.75 turtle walk finish + colored
- 0.50 turtle idle, jet idle, turtle fly all done
- 0.50 separate and create spritesheet + break
- 1.50 setting up animation controller, correcting turtle, and upside-down controls
- 1.00 jet propulsion is a go
- 0.50 wasted trying to get particles to work, made 3 systems instead
- 1.50 sound effects
- 0.50 music
- 1.00 screenshake + this upkeep


Time is running out, what's the focus?
- Pickup jetpack
- Move respawner 
- Background + foreground (tiles) painted
- Level design
  - Intro with floating text tutorials
  - create bridge
  - respawn
  - little lip choice
  - never-ending block fall
  - "Roll of the dice" chance to get to the end
  - Victory screen
  
Stretch goals:
- Graphical audio control
- In-game credits
- Start menu

Total time:
5.75hr + 8.25hr

## Over-scope

As always, some aspects caught me by surprise with how much effort it took, eating into available hours:
- Really needed to learn about Unity particles before this. I really have no clue about rendering pipelines, shaders, etc.
  - Instead I just added 3 different base color particle systems. Good enough.
- Actually getting your sprites perfectly aligned inside the sprite sheet seems very important
  - Slight, unintended up-down fluctuations are very noticeable when animated
- Balancing feature programming, asset generation, and level creation is hard
  - If you have no code re-use, "one key feature" is really all you can expect (here, walking and flying)
  - The more features you add, the more assets needed, and the more levels need to be created
  - Just like in writing, short and snappy has to be the goal
