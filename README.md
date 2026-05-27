# 🌙 Night Heist: Stealth and Security Challenge

A 3D stealth game built in Unity where you play as a thief breaking into guarded apartments at night — collect valuables, dodge cameras, outsmart AI guards, and escape clean.

## 🎮 How It Works

The core loop is simple:

**Sneak in → Collect items → Avoid detection → Escape**

You have a limited time. Security cameras sweep the rooms. In Level 2, AI guards patrol and chase — get spotted once and it's game over.

---

## 🗺️ Levels

### Level 1 — Apartment Blackout
Break into an apartment and steal **6 items** before the **4-minute** timer runs out. Only security cameras to worry about — learn their scan patterns and slip past them.

### Level 2 — NPC Threat
Same goal, but the timer is **cut in half** and AI-controlled guards are now patrolling. They have a detection meter — if it fills to 100%, you lose immediately.

---

## 🛠️ How It Was Built

### Engine & Rendering
- **Unity** with **Universal Render Pipeline (URP)**
- Baked directional light simulating moonlight
- Realtime point/spot lights on guard positions for dynamic shadows the player can exploit

### Player
- `Rigidbody`-based movement (walk, sprint, crouch)
- `Cinemachine FreeLook` camera — orbits the player, zooms in tighter when crouching
- Interactions via **E key** (terminals, fuse box)
- Coin-throw mechanic to distract guards

### Security Cameras
- `CameraScanner.cs` — sweeps a configurable view angle and range
- Spot Light set to **Realtime** to cast live shadows
- Triggers alarm if player enters the cone

### NPC Guards (Level 2)
- **NavMesh**-based pathfinding with waypoint patrol routes
- State machine: **Patrol → Suspicious → Alert**
- `NPCWanderChase.cs` manages chase logic and detection meter changes
- `NPCAnimationEventsReceiver.cs` syncs animation events to state transitions

### Game Manager
`GameManager.cs` handles everything global:
- Item counter and win condition
- Countdown timer (halved on Level 2)
- Victory / Game Over popups with 1.5s delay
- Scene transitions to next level

### UI / HUD
- Timer — top right
- Items collected counter — top right (`0/6`)
- Pause menu via `Escape` → Resume / Restart / Main Menu
- Settings — mute music, SFX, master volume

### Physics
- `Box` and `Capsule Colliders` on all walls, objects, and guards
- **Trigger Colliders** for vision cones and objective zones

### Audio & VFX
- Background music + footsteps + guard voice lines + alarm
- Particle systems for flashlight beam and camera laser tracking effect
- All sounds controllable via Settings sliders

### Visual Style
- Dark noir aesthetic — muted colors, high shadow contrast
- PBR materials (metallicity + roughness maps)
- Custom dark night skybox
- Low-to-medium poly models from Unity Asset Store

---

## 📂 Project Structure

```
Assets/
├── Scenes/
│   ├── MainMenu
│   ├── Apartment_Blackout   ← Level 1
│   └── Level2
├── Scripts/
│   ├── PlayerController.cs
│   ├── GameManager.cs
│   ├── UIManager.cs
│   ├── CameraScanner.cs
│   ├── NPCWanderChase.cs
│   ├── NPCAnimationEventsReceiver.cs
│   ├── BillboardToCamera.cs
│   └── ...
├── Prefabs/
├── Materials/
└── Audio/
```

---

## 📦 Build

Windows-only build. Tested on 2 machines, no launch errors.

```
Night_Heist/
├── D3D12/
├── MonoBleedingEdge/
├── Night_Heist_Data/
├── Night_Heist_BurstDebugInformation_DoNotShip/
└── UnityPlayer.dll
```

---

## ⚠️ Known Issues

- Guard AI can briefly stall on Level 2 stairs (NavMesh baking limitation)
- Smoke screen item has no HUD cooldown timer
- No save/load — progress resets on close
- 3D spatial audio for guard footsteps is incomplete; currently 2D
- WebGL and mobile builds untested

---

## 📎 Assets Used

- [Unity Asset Store — Publisher 37259](https://assetstore.unity.com/publishers/37259)
- [Unity Asset Store — Publisher 32000](https://assetstore.unity.com/publishers/32000)
- [Free Wood Door Pack](https://assetstore.unity.com/packages/3d/props/interior/free-wood-door-pack-280509)
- [Unity Docs](https://docs.unity3d.com/) · [Unity Learn](https://learn.unity.com/)
