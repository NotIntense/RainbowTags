![Total Downloads](https://img.shields.io/github/downloads/NotIntense/RainbowTags/total)


# RainbowTags
EXILED port of RainbowTags from xNexus-ACS that was a port of [Build](https://github.com/BuildBoy12-SL), [originally](https://github.com/sirmeepington/RainbowTag/) created by [@SirMeepington](https://github.com/sirmeepington). Makes server badges go through a spectrum of colours in SCP:SL. 

This plugin makes use of [EXILED 7](https://github.com/Exiled-Team/EXILED/releases/latest) for [SCP:SL](https://scpslgame.com/)

## Configuration

```yaml
rainbow_tags:
  is_enabled: true
  debug: false
  color_interval: 0.5
  sequences:
    - red
    - orange
    - yellow
    - green
    - blue_green
    - magenta
    - silver
    - crimson
  ranks_with_rtags:
    - owner
    - moderator
    - admin
```

**Valid Colours**:
* pink
* red
* brown
* silver
* light_green
* crimson
* cyan
* aqua
* deep_pink
* tomato
* yellow
* magenta
* blue_green
* orange
* lime
* green
* emerald
* carmine
* nickel
* mint
* army_green
* pumpkin


#### FAQ

If you get issues about `TAG FAIL`, you've either used a invalid / prohibited color. If you're certain you're not using a prohibited color, remove the color and restart.
