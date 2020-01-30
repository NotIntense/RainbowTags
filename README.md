# ARainbowTags
Atlas port of RainbowTags, [originally](https://github.com/sirmeepington/RainbowTag/) created by [@SirMeepington](https://github.com/sirmeepington). Makes server badges go through a spectrum of colours in SCP:SL. 

This mod makes use of [Atlas](https://gitlab.com/Androxanik/atlas/) for [SCP:SL](https://scpslgame.com/)

## Configuration
It's fairly easy to use, simply create a template, and assign UIDs to it in the config.

**Example template**:
```json
{
  "command_aliases": {},
  "debug": false,
  "template_map": {
    "all_slow": {
        "interval": 2,
    },
    "all_fast": {
        "interval": 0.5
    },
    "police": {
        "interval": 0.5,
        "colors":["cyan", "default"]
    }
  },
  "uid_map": {
    "steamid@steam": "police",
    "uid@discord": "all_slow"
  },
  "event_priorities": {}
}
```
`colors` is an array of strings which the game accepts. Some colours are forbidden, see the list below for details.

If you don't specify a `colors` array in your template, it will assume all colours.
Use `default` to use the normal grey for a lack of role.

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

## Installation
Follow the [Atlas](https://gitlab.com/Androxanik/atlas/) installation guide for SCP:SL, then simply drop the DLL into the mods folder.
After the mod is loaded, simply run `atlas settings save` to flush the settings to disk, from there you can modify them to your heart's desires and use `atlas settings refresh` to update them in memory.

***Note***: Due to how Unity handles components, changes will not show up immediately, you will have to disconnect and reconnect, or reload the mod through Atlas. 

#### FAQ

If you get issues about `TAG FAIL`, you've either used a invalid / prohibited colour, of if you're certain you're not, restart the server; it might have an created a state issue.

For a full list of colours available, you can type `colors` in your game or server console.

**If you have any other concerns, you can find me in the Atlas Discord, linked on the page (@MASONIC#3992).**