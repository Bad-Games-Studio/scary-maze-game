# Scary Maze Game

Good news - it's not actually scary!

This Unity demo shows how you can:
  - Implement a simple Top-Down camera.
  - Build a maze at runtime from an image.
  - Build a NavMesh at runtime and make an Agent move around freely.
  - Abuse Observer Pattern (instead of `Awake`/`Start` order).

Notes:
  - The maze generation can be improved (procedural generation).
  - The maze generation can be optimized (no lags after that thankfully).
  - ~~Maybe I overused the Observer pattern... But I like it very much.~~
  - Why generate the maze? I was lazy to spend an hour making it with hands, so I had spent 8 to save up some time.

## Controls

- Holding `Right Mouse Button` allows to move camera.
- *While holding* `RMB`, you can also `Scroll` to zoom in and out.
- Click `Left Mouse Button` to tell the agent where to go.

![scary-maze-game-top](https://user-images.githubusercontent.com/49134679/163216000-42792577-b522-4ecf-ae0a-2bc317f35bbb.png)

![scary-maze-game-side](https://user-images.githubusercontent.com/49134679/163215991-d7175e89-228d-4dcb-ab68-a17b30afbfc2.png)

## Credits

- [NavMeshComponents](https://github.com/Unity-Technologies/NavMeshComponents) for making it possible to bake NavMesh at runtime.
