# ARainbowTags
EXILED port of RainbowTags, [originally](https://github.com/sirmeepington/RainbowTag/) created by [@SirMeepington](https://github.com/sirmeepington). Makes server badges go through a spectrum of colours in SCP:SL. 

This mod makes use of [EXILED](https://github.com/galaxy119/EXILED/) for [SCP:SL](https://scpslgame.com/)

## Configuration

```yaml
rainbow_tags:
  is_enabled: true
  use_custom_sequence: false
  tag_interval: 0.5
  active_groups:
  - owner
  - admin
  - moderator
  custom_sequence:
  - red
  - orange
  - yellow
  - green
  - blue_green
  - magenta
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

If you get issues about `TAG FAIL`, you've either used a invalid / prohibited color. If you're certain you're not using a prohibited color, restart the server.
