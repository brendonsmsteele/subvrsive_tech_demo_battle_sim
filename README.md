# subvrsive_tech_demo_battle_sim

## Overview
An MVP demo written to simulate battle between many characters, with a focus on scalable architecture.

### How would your code change if weapons had special effects, like the ability to make targets catch fire?

I would introduce a new ScriptableObject data class called Effect.cs, which would store effect properties and be referenced by ID within AmmoState. When an ammo object triggers a collision via OnTriggerStay, it would request the corresponding effect from EffectFactory.cs. The factory would then instantiate the effect at the collision point, allowing it to animate naturally and follow the standard life/death cycle of a GameObject component.

### How might this system be incorporated into a larger items and inventory system?

The Unity Addressables API is a powerful tool for loading AssetBundles, allowing us to dynamically download only the necessary data after installation. This approach optimizes cold storage memory usage by ensuring that we load assets on demand rather than keeping everything in memory.

Before each battle, we selectively load only the assets required for rendering, keeping memory overhead low and performance high. To maintain structured and efficient asset management, we establish a dedicated ItemState, linking key gameplay elements such as weapons, ammo, effects, and players.

The system is designed with scalability and performance in mind, leveraging techniques such as Monobehaviour lifecycle events, object pooling, pub/sub architecture, and Single Responsibility Principle (SRP) to ensure consistent, decoupled interactions. Game state is managed monolithically, while systems execute game logic, keeping operations efficient as the game expands.

Additionally, the event-driven architecture provides a natural foundation for integrating networking, minimizing the complexity of scaling the experience to a connected, multiplayer environment. 

## Unreal Demo Pseudo-code

Light Cube (pseudo-code)

This is where the DotProduct will shine for us.  We can create a Blueprint with the following variables made public.  Array of 2 floats each clamped to float range (-1,1), MaxDistanceThreshold.  The logic would be in an Update.  Get the vector from box to pawn.  Determine first if the magnitude of that vector is greater than the MaxDistanceThreshold, if so, then set the color to green and return.  Perform the DotProduct(vectorBoxForward, vectorBoxToPawn).  Using the 2 floats divide the number range -1 to 1 into three float ranges.  Determine which range the DotProduct falls into.  The full range is -1 to 1 and each range you derived corresponds to an index in the following color array [red,yellow, green].   Set the appropriate color.

Keypad (pseudo-code)

Expose a public variable in your Blueprint “KeypadCode”.  Place the numeric keypad object into your level.  Upon key press, validate the text in the textfield.  If incorrect, set the input field to “”.  If correct, broadcast a game event “SECURITY_ACTIVATED”.  Upon receipt of this event turn the security camera on. 


## Design, Approach and Assumptions

I designed this to leverage strong architectural design patterns such as object pooling, pub/sub architecture, script decoupling, monolithic state management, SRP state modification, composition over inheritance, and Unity Monobehaviour events.  For this demo I was undecided at first how far I would pursue the architectural standards I had envisioned vs just describing them for a future deliverable.  I landed on an approach that is a nice balance of Unity component based architectural preferences and ECS full stateless entity architecture.  

Object pooling increases performance output.  Especially when increasing the amount of bullets.  Pooling does necessitate additional coding discipline, managing your “cleanup” step before putting the objects back in the pool.  You can get very gnarly bugs during the development, due to lingering values that obfuscate the true state of things.

Monolithic state management in this case is very similar to an ECS (Entity Component System) pattern.  The state is stored in a single location.  That is the source of truth.  A handful of systems act on that state.  State changes are broadcasted via the pub/sub and any gameObject that cares responds appropriately.  Systems modify the game's state in a core OnUpdate loop, this serves as a single point of state modification increasing performance and contextual awareness when making decisions or changes to an entity's state.

Unity Monobehaviour standards, with the pseudo-ECS architecture I found myself milling over decisions IE what script actually spawns the bullet object?  I’ve been a part of projects that break Unity wide open and are very opinionated in a strictly code logical fashion and throw Unity events under the bus.  This strategy abandons the core tools and requires devs to learn beyond what they already know.  Every decision balances the advantages of the Unity core workflow.  Bullets spawn from the WeaponController which holds an AmmoAnchor, which leverages Transform localPosition for easy description of position and rotation to spawn the ammo.  Bullets maintain colliders and rigidbodies using Unity physics to invoke OnTriggerEnter events when the bullet hits another target.  Scriptable Objects are used for stateless scripts IE pub/sub, and data items.  Data item configuration is super easy and intuitive when the flow is right click create/ weapon then fill in my desired parameters.  Data items can be swapped in/out during runtime.  OnStart, OnUpdate, OnEnable, OnDisable are all utilized and the implementation of other scripts like PrefabPoolers is tweaked to take advantage of this logic.  GetComponentsInChildren used for automatic configuration of private vars.  An object's ID (guid) is stored on the component to filter out pub/sub events intended for the object specified by ID.  

SRP is enforced by the ECS design pattern, and very important for doing the minimal render or game logic when state changes.  Pub/sub events can come from anywhere, debug at editor time, from the local device or data from a remote device in a networked experience.  All individual components are built to be idempotent (spammable) thus pub/sub events can come in at any frequency and not degrade performance.  

Interfaces allow for composition over inheritance which is great for flexible but reusable coding boilerplate. 

