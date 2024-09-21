# XOOLOD-Towering

XOOLOD-Towering is an RPG game built using the Unity Engine (C#), featuring a variety of engaging mechanics designed to enhance the gameplay experience.

## Key Features

- **Skill Shops**: Players can purchase and upgrade abilities to improve their skills throughout the game.
- **Leveling & Stats System**: Experience progression allows players to level up by defeating enemies that spawn in designated areas.
- **Skills & Cooldown System**: A diverse set of player abilities with cooldown mechanics and custom stats for each ability, including:
  - Number of hits
  - Damage
  - Casting time
  - Cooldown duration
- **State Machines**: Complex state management for both player actions and enemy behaviors.
- **Enemy Variety**: Seven different enemy types, each with unique and complex state machines, creating diverse combat challenges:
  - Each enemy has its own stats, including:
    - Damage
    - HP
    - Level
    - Gold drop
    - Experience drop
  - Enemies spawn with a random level range, adding variability to encounters [except for Boss enemy].
  - Specific behaviors for each enemy:
    - **Idle** [All]
    - **Attack Patterns** (1 - 2 attacks) [All]
    - **Patrol / Walk** [All]
    - **Chase / Run** [All]
    - **Spawn Minions** [Boss enemy]
    - **Flying Up** [Boss enemy]
    - **Fly and Chase** [Boss enemy]
    - **Landing** [Boss enemy]
    - **Died** [All]
- **Maze Generation**: Procedurally generated mazes developed by Felix, a fellow developer.
