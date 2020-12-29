# Rolling Game

[SimpleInput by Yasirkula](https://github.com/yasirkula/UnitySimpleInput) - used for the on-screen joysticks in the mobile version.

## Ideas for scoring/timing

- Survive as long as possible, time going down and pickups refresh the time
- Just scored based on the number of pickups gathered

## Path types

| Index | Description              |
| ----- | ------------------------ |
| 0     | Flat                     |
| 1     | Flat ending in half pipe |
| 2     | Half Pipe                |
| 3     | Half pipe ending in flat |
|       |                          |

If the index is odd, cycle to the next index. Otherwise chance of staying on same index or moving forward one.

