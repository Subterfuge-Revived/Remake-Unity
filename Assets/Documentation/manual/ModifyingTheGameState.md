# Understanding the GameState

The `GameState` object contains all information about the game. If an object is not explicitly added to the `GameState`
object, then the game will not know about it. For example, you can create a new Outpost:
```
// This constructor isn't valid but this is an example.
Outpost newOutpost = new Outpost()
```

Even though you created a new outpost, the `GameState` has no knowledge of this object. All objects must be tracked
through the `GameState`. In order to actually add this outpost to the game, you would need to access the `GameState`
object and add it:

```
// This constructor isn't valid but this is an example.
Outpost newOutpost = new Outpost()

// Add the new outpost to the GameState.
Game.timeMachine.getState().getSubList().Add(newOutpost);
```

This will ensure that the `GameState` knows about the outpost. This same logic also applies to subs, players,
and specialists.