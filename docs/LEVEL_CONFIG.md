# Level Configuration

Level definitions live in `public/data/levels.json`. The file is versioned JSON with a `levels` array. Add a new object with a unique sequential `id`; the engine reads it without source changes.

## Fields
| Field | Type | Meaning |
|---|---|---|
| id | integer | Unique level number |
| name | string | Display name |
| speed | number | World scroll speed |
| trackWidth | number | Available track width |
| gateValues | string[] | Gate operations |
| obstaclesCount | integer | Number of obstacles |
| movingObstacles | boolean | Enable lateral obstacle motion |
| enemyPower | integer | Enemy crowd size at finish |
| distance | number | Course length |

## Gate notation
- `+N`: add N units
- `-N`: remove N units
- `xN`: multiply crowd by N
- `÷N`: divide crowd by N, rounded down (minimum one)

Example:
```json
{"id":51,"name":"Level 51","speed":18,"trackWidth":5,"gateValues":["+10","x3","-8","÷2"],"obstaclesCount":60,"movingObstacles":true,"enemyPower":800,"distance":1600}
```

Keep IDs unique and validate the JSON before committing. For a future content pipeline, consider splitting levels into files and adding a manifest while retaining LevelManager's public API.
