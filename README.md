# XOOLOD-Towering 🎮

XOOLOD-Towering is an RPG game built using the Unity Engine (C#), featuring a variety of engaging mechanics designed to enhance the gameplay experience.

## Key Features

- **Skill Shops**: Players can purchase and change abilities to enable various gameplay.
- **Leveling & Stats System**: Experience progression allows players to level up by defeating enemies that spawn in designated areas.
- **Skills & Cooldown System**: A diverse set of player abilities with cooldown mechanics and custom stats for each ability, including:
  - Number of hits
  - Damage
  - Casting time
  - Cooldown duration
- **Key Bindings**: Players can customize key bindings for their skills, enhancing control and personalization.
- **Lock System**: Dynamically displays a crosshair over the nearest enemy, showing their information such as level, name, and health bar.
- **State Machines**: Complex state management for both player actions and enemy behaviors.
![image](https://github.com/user-attachments/assets/ff8bc594-df2a-474e-8f4c-c5842fa64e9e)

- **Enemy Variety**: Seven different enemy types, each with unique and complex state machines, creating diverse combat challenges:
  - Each enemy has its own stats, including:
    - Base Damage
    - HP
    - Spawn level range
    - Gold drop
    - Experience drop
    - Base speed
    - Walk range
    - Walk cooldown
    - Walk speed modifier
    - Aggro range
    - Chase cooldown
    - Chase speed modifier
    - Attack damage modifier [for each attack]
    - Attack hit count [for each attack]
    - Attack range [for each attack]
    - Attack cooldown [for each attack]
    - Rotation speed
  - Enemies spawn with a random level range, adding variability [except for Boss enemy].
  - Specific behaviors for each enemy:
    - **Idle** [All]
    - **Attack Patterns** (1 - 2 attacks) [All]
    - **Patrol / Walk** [All]
    - **Chase / Run** [All]
    - **Spawn Minions** [Boss enemy]
    - **Taking Off** [Boss enemy]
    - **Fly and Chase** [Boss enemy]
    - **Landing** [Boss enemy]
    - **Died** [All]
- **Maze Generation**: Procedurally generated mazes developed by Felix, a fellow developer.
- **Cheats**: Typing certain keyword will enable cheats (intended for developing purpose).

# Preview 📷

**Opening**
----------
![image](https://github.com/user-attachments/assets/49ef4731-2f0f-4623-962c-7678c4c1239a)

**Enemy Areas**
----------
![image](https://github.com/user-attachments/assets/3a68f24c-f6a9-4e06-bfc4-00aeab08c29e)

![image](https://github.com/user-attachments/assets/7e91f704-d362-4541-a2ee-f6db7aeed1d8)

![image](https://github.com/user-attachments/assets/33ab818b-2479-4368-8956-8069c9e97f29)

![image](https://github.com/user-attachments/assets/04c25854-c572-4809-8a24-fc1612833037)

**Maze**
----------
![image](https://github.com/user-attachments/assets/b597610a-e453-4b24-ae40-6a7cde2a65e5)

![image](https://github.com/user-attachments/assets/f5fc16ca-c87d-4ea2-87e0-bb902a6917cc)

**Stats**
----------
![image](https://github.com/user-attachments/assets/4594781f-e08a-4e86-8555-57f41db8f379)

**Skills**
----------
![image](https://github.com/user-attachments/assets/c5b72de1-ddbf-4581-8abe-2f994f5dffae)

![image](https://github.com/user-attachments/assets/43b23ef2-f712-4ce2-a3c4-204a58cf7ca5)

![image](https://github.com/user-attachments/assets/84ed348f-bed5-4b9f-aba8-7d9cc2c6e80c)

![image](https://github.com/user-attachments/assets/b1e4dd0b-cd82-4189-9214-e65376efe3b7)

![image](https://github.com/user-attachments/assets/1cff327d-d155-4de8-b509-6982e6b99595)

**Boss**
----------
![image](https://github.com/user-attachments/assets/d4f608ce-0b80-4052-8941-ae17c736a5dd)

![image](https://github.com/user-attachments/assets/992bbb27-56c4-41ec-90dd-72ba2579a8df)

![image](https://github.com/user-attachments/assets/acacfda0-d4f7-44c1-8724-5bc3a26223a5)

![image](https://github.com/user-attachments/assets/fb293194-0aa3-4a0c-be8d-5d285055cd0e)
