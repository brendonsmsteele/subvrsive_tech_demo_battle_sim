# subvrsive_tech_demo_battle_sim
An MVP demo written to simulate battle between many characters, with a focus on scalable architecture.

### How would your code change if weapons had special effects, like the ability to make targets catch fire?

I would introduce a new ScriptableObject data class called Effect.cs, which would store effect properties and be referenced by ID within AmmoState. When an ammo object triggers a collision via OnTriggerStay, it would request the corresponding effect from EffectFactory.cs. The factory would then instantiate the effect at the collision point, allowing it to animate naturally and follow the standard life/death cycle of a GameObject component.


### How might this system be incorporated into a larger items and inventory system?

The Unity Addressables API is a powerful tool for loading AssetBundles, allowing us to dynamically download only the necessary data after installation. This approach optimizes cold storage memory usage by ensuring that we load assets on demand rather than keeping everything in memory.

Before each battle, we selectively load only the assets required for rendering, keeping memory overhead low and performance high. To maintain structured and efficient asset management, we establish a dedicated ItemState, linking key gameplay elements such as weapons, ammo, effects, and players.

The system is designed with scalability and performance in mind, leveraging techniques such as Monobehaviour lifecycle events, object pooling, pub/sub architecture, and Single Responsibility Principle (SRP) to ensure consistent, decoupled interactions. Game state is managed monolithically, while systems execute game logic, keeping operations efficient as the game expands.

Additionally, the event-driven architecture provides a natural foundation for integrating networking, minimizing the complexity of scaling the experience to a connected, multiplayer environment. 
