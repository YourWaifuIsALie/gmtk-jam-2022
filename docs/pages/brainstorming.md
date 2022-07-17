---
title: It begins
date: 2022-09-15
categories: [blog]
layout: custom
order: 2
---
# GMTK Game Jam 2022: {{ page.title }}
{{ page.date | date: '%B %d, %Y' }}

## Theme

Roll of the Dice

## Criteria

The only goal is to put out a real, legal game. The only principle to remember is to KISS and write as sloppy code as I need.

Judging is based on: enjoyment, creativity, and presentation. 

Notable rules:
- Keyboard + mouse must be supported
- No nudity or hateful language. Avoid strong language and excessive gore.

Start: Friday July 15th, 1000 PDT / 1300 EDT

End:   Sunday July 17th, 1000 PDT / 1300 EDT

*Submissions allowed until 1200 PDT / 2200 EDT*

## Brainstorming

### Definitions

**Dice**
noun
1. a small cube with each side having a different number of spots on it, ranging from one to six, thrown and used in gambling and other games involving chance.

verb
1. play or gamble with dice.
2. cut (food or other matter) into small cubes.

**Roll**
verb
1. move or cause to move in a particular direction by turning over and over on an axis.
2. (of a vehicle) move or run on wheels.
3. turn (something flexible) over and over on itself to form a cylinder, tube, or ball.
4. flatten or spread (something) by using a roller or by passing it between rollers.
5. (of a loud, deep sound such as that of thunder or drums) reverberate.
6. rob (someone, typically when they are intoxicated or asleep).

noun
1. a cylinder formed by winding flexible material around a tube or by turning it over and over on itself without folding.
2. a movement in which someone or something turns or is turned over on itself.

### Initial thoughts

I'd like to start with interesting wordplay into a core theme of the game
First things that come to mind:
- RNG
- Literal dice like tabletop games
- Chance in a metaphorical decision (as in, the outcome is unknown)
- Gambling

"To dice" = to cut up
Dicing dice? 
- Manipulating RNG

Rolling = physically pushing, like in Katamari
- Really big oversized dice

Puzzle game? Rolling dices onto sides for certain numbers + cutting dice
Combat?

When are dice rolled? In a tabletop game, by a player, during skill checks/combat for their character's actions
- People attribute superstition dice. Weighted dice, cursed, etc.
- Assume a dice fairy, manipulating the dice? 

If trying some watercolor-themed game that's easy to make (i.e. no complex turn-based systems), puzzle/platformer makes sense

So:

A puzzle game, where the player controls a "dice fairy", manipulating rolls such that the RPG character succeeds
- Instead of a fairy, an animal? Like a hamster in a wheel, except it's a die
- Graphics wise:
  - 3d + 2d mix
  - 3d is table + environ + dice mechanics
  - 2d is "the RPG" backdrop (mostly a set piece then?)

Gameplay?
- Katamari, the fun is sticking things and growing bigger
- Traditional RPG doesn't have complex mechanics (it's just passing skill checks)
- Physics-based gameplay?
  - A hand rolls the dice, player needs to manipulate the roll to land appropriately
  - This seems harder than I'm prepared for (math-wise, 3d-wise, etc.)
- Can 2d dice work?
  - A perspective thing? If you can only roll a die in 2d, there's 4 possible face-up values


Step back. What's the cute idea in head? Turtles flipping other turtles that are stuck on their back.
- So, a cute game about turtles trying to roll dice.
- It can be 2d physics, since dices are generally square-like
  - You need to use the environment to "right" the dice (turtle)
  - Size and shape of dice changes, making things easier or harder
  - A bite ability?
- Is this supposed to make sense? Probably not

### Turtle-puzzler

A pretty generic platform-puzzler: pushing the "correct" cube (die) into the correct spot to open a door to continue

But you're a turtle. That makes life hard.
- No jumping means we need environmental physics to transition vertical layers
- The concept is already ridiculous (turtles + giant dice) so we might as well go for broke. Saving turtles gives you additional helpers
  - Control one turtle at a time? But they can climb on top of eachother and move like a Mario Odyssey goomba

Controls:
- Left + right key movement
- Click to change control of turtle
- That's it

How is this "Roll" of the dice? We're going to do a lot of pushing dice over ledges...

And we can juice the silliness by doing things like having jet-engine turtles

Concerns:
- Not a strong correlation to the theme
  - Can we make RNG a factor?
  - The act of rolling the dice should cause effects
    - A dice slowly rolls down a hill, collision of face causes a small jump effect?
    - Need to squash things by rolling the dice onto enemies
- A little simple
  - But it's a 48hr jam so we need something simple
- Narrative is wacky
  - See above 48hr jam thing

...good enough?

## Design document

Listing out what we need, and what would be nice to have

Assets:
- Animated turtle
- Platforms
- Background
- Dice
- Some kind of barrier
- Some kind of environment
  - Various human trash?
- SFX 
  - Turtle movement
  - Collision sounds
    - Turtle-Turtle
    - Turtle-Ground
    - Turtle-Dice
    - Dice-Dice
    - Dice-Ground

Code:
- Multi-turtle control
  - Proper left-right movement
  - Activating turtles
- Proper platform tileset
- SFX control
  - Collision sounds
  - Screenshake
  - Both related to the mass of the involved components
    - Greater/deeper for bigger dice
- Reset button
- On-screen control explanation

Theoreticals:
- Bite ability (manipulate environment)
- Jet engine backpack (slow turtle is boring)
- Proper turtle stacking movement