Thicket - an unofficial Level Loader for Ultrakill

written in the span of like a week or two for fun

feature set: it loads levels (amazing i know)

how it works:
  unlike Tundra, the official level editor for Ultrakill, Thicket loads levels from a massive prefab, which requires a multitude of various bits to function properly
  the most basic things you need for a level boil down to:
    -- a level start
    -- a level end
    -- level geometry
    -- a navigation mesh (baking it normally won't work you need to use something else to bake it into the prefab)


Cool Disclaimer:
- thicket doesn't work anymore after the halloween update and i'm too lazy to fix it
- i suspect the issue is somewhere in the new post processing the camera does
- so go fix that in your own fork or give a pull request i'll accept it if it works
