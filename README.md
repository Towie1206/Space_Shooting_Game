# 🚀 Space Shooting Game

A 2D space shooter built with Unity, featuring wave-based enemy spawning, a leveling system, boss encounters, and a parallax space environment.

---

## 🛠️ Tech Stack

| Technology | Description |
|---|---|
| **Unity** | Main game engine (2D) |
| **C#** | Programming language |
| **TextMesh Pro** | UI text rendering |
| **Unity Physics 2D** | Collision detection, Rigidbody |
| **Unity Animator** | Animation state machine for player and bosses |
| **Unity Particle System** | Boost and explosion visual effects |

---

## 🎮 Gameplay

The player controls a spaceship flying through space, eliminating enemies and dodging obstacles. The goal is to survive, collect experience, and keep upgrading your weapon.

### Controls

| Key | Action |
|---|---|
| `W A S D` / Arrow Keys | Move |
| `Left Mouse Click` | Shoot |
| `Space` / `Left Alt` | Activate Boost |
| `Escape` | Pause |

### Core Mechanics

**Boost**
- Consumes Energy to increase world speed, letting the player move faster relative to the environment
- Energy regenerates automatically when not boosting
- Boost deactivates automatically if Energy runs out

**Level Up System**
- Destroying enemies and asteroids grants Experience points
- Gaining enough EXP levels up the player, increasing max health and upgrading the weapon
- Higher weapon levels fire more bullets spread across a wider vertical range

**Phaser Weapon**
- Level 1: single bullet
- Higher levels: multiple simultaneous bullets spread vertically

---

## 👾 Enemies

### Critter
Small creatures that wander randomly across the screen. Cannot be destroyed by bullets — only by direct collision with the player or a bullet tag. Killing 10 Critters triggers a Boss spawn.

### Beetlemorph
Moves in a **sine wave** pattern along the Y axis, creating an unpredictable weaving trajectory. Spawns with a randomly chosen sprite variant.

### Locustmorph
Has two phases:
- **Idle**: Drifts slowly across the screen
- **Charge**: Once health drops below 50%, it charges hard to the left at high speed. Comes in multiple sprite variants.

### SquidMorph
Always rotates to face the player. Every 2 seconds it fires a **SquidCritter** directly toward the player.

### SquidCritter
Launched from a SquidMorph. After the initial launch burst, it **homes in on the player** and continuously rotates to face its movement direction.

---

## 💀 Bosses

### Boss 1 — Charge Boss
Spawns every time the player kills **10 Critters**. Switches between two states:
- **Patrol**: Drifts up and down randomly
- **Charge**: Lunges hard to the left toward the player

Adjusts its movement speed when the player activates Boost. Has a dedicated animation for each state.

### Boss 2
Moves in a bounce pattern: charges from left to right at high speed, then slowly drifts back the other way. Switches state based on its X position on screen.

---

## 🌌 Obstacles

### Asteroid
- Spawns at a random size (0.5x – 1.5x scale)
- Takes **5 hits** to destroy
- Drifts in a random direction with real physics
- Grants EXP when destroyed by the player (no EXP if destroyed by a Boss)

### Whale
A space creature that drifts along with the world. Touching the special **LostWhale** completes the level.

---

## 🏗️ Technical Architecture

### Singleton Pattern
Core managers use the Singleton pattern for global access:
- `GameManager` — world speed, boss spawning, game over flow
- `PlayerController` — player state, health, energy, leveling
- `PhaserWeapon` — shooting logic
- `AudioManager` — sound playback with pitch randomization

### Object Pooling
All bullets, enemies, and explosion effects use an **Object Pool** instead of `Instantiate/Destroy` calls, keeping performance smooth. The pool auto-expands when all objects are in use.

### Wave Spawner
`ObjectSpawner` manages sequential wave spawning. Each wave has its own:
- Object pool reference
- Spawn interval timer
- Maximum object count before advancing to the next wave

### Enemy Inheritance
All enemies inherit from the `Enemy` base class, which handles shared logic: taking damage, flashing white on hit, dying, and granting EXP. Each enemy type overrides only what it needs.

### Parallax Background
Multiple background layers scroll at different speeds to create a sense of depth. The main menu has its own parallax system as well.

---

## 📁 Project Structure

```
Assets/
├── Script/
│   ├── Enemies/        # Beetlemorph, Locustmorph, SquidMorph, SquidCritter, Boss1, Boss2, Critter1
│   ├── Weapons/        # PhaserWeapon, PhaserBullet, Weapon (base)
│   ├── Obstacles/      # Asteroid, Whale, LostWhale
│   ├── MenuScene/      # MenuManager, MenuParallax
│   ├── Utils/          # FlashWhite, FloatInSpace, FaceMovementDirection, DestroyWhenAnimationFinished
│   ├── GameManager.cs
│   ├── PlayerController.cs
│   ├── AudioManager.cs
│   ├── UIController.cs
│   ├── ObjectPooler.cs
│   ├── ObjectSpawner.cs
│   └── ParallaxBackground.cs
```

---

## 🚀 Getting Started

1. Clone the repository
2. Open with **Unity Hub** (Unity 2022.x or later recommended)
3. Open the `MainMenu` scene to start from the main menu
4. Or open `Level 1` directly to jump into gameplay

---

## 👤 Author

**Towie1206**
