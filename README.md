# BZ Godot Physics

![Godot 4+](https://img.shields.io/badge/Godot-4%2B-478CBF)
![Version](https://img.shields.io/badge/version-1.0-orange)
![License](https://img.shields.io/badge/license-MIT-green)

A clean, high-performance C# physics utility for Godot 4+. Simplifies low-level 3D raycasting with structured result containers and provides easy collision mask generation. Just one file, zero dependencies.

## ✨ Features

### 🎯 Easy Raycasting
- Extension methods for `Node3D` to perform raycasts and line casts
- Clean, intuitive API that integrates seamlessly with your existing code

### 📦 Structured Results
- `CastHit` readonly struct provides clean access to hit data
- Hit position, surface normal, collider reference, and more

### 🎭 Collision Masks
- Simple helper method to generate collision layer masks
- Uses 1-based layer numbers for intuitive usage

### ⚡ Zero Dependencies
- Just one file (`PhysicsHelper.cs`)
- Pure Godot 4+ C# code

## 📋 Requirements

- Godot 4.0 or later
- .NET-enabled Godot build
- Basic C# knowledge

## 📦 Installation

1. Download `PhysicsHelper.cs` from this repository
2. Add it anywhere in your Godot project's `res://` directory
3. Add `using BZ.Physics;` to your scripts

## 🚀 Quick Start

### Cast a Ray

```csharp
using BZ.Physics;

// Cast a ray from a position in a direction
CastHit hit = this.CastRay3D(
    from: GlobalPosition,
    direction: -GlobalBasis.Y,
    distance: 100f,
    layerMask: PhysicsHelper.GetCollisionMask(1, 2, 3)
);

if (hit.NonEmpty)
{
    GD.Print($"Hit {hit.ColliderOwnerName} at {hit.HitPosition}");
}
```

### Cast a Line Between Two Points

```csharp
CastHit lineHit = this.CastLine3D(
    from: startPoint,
    to: endPoint,
    collideWithAreas: true
);
```

### Generate Collision Masks

```csharp
// Create a mask for layers 1, 3, and 5
uint mask = PhysicsHelper.GetCollisionMask(1, 3, 5);
```

> **Note**: Methods are available as extensions on any `Node3D`. Ensure you have `using BZ.Physics;` at the top of your script.

## 📋 API Reference

### CastHit Struct

A readonly struct containing raycast intersection data.

| Property | Type | Description |
|----------|------|-------------|
| `NonEmpty` | `bool` | `true` if the cast hit something, `false` otherwise |
| `HitPosition` | `Vector3` | The world-space position of the intersection point |
| `Normal` | `Vector3` | The surface normal at the hit point |
| `Collider` | `CollisionObject3D` | The collider that was hit |
| `ColliderId` | `uint` | The instance ID of the collider |
| `Rid` | `Rid` | The low-level resource ID of the collider |
| `ColliderOwnerName` | `string` | The name of the collider node |

### PhysicsHelper Methods

#### CastRay3D

```csharp
CastHit CastRay3D(this Node3D sender, Vector3 from, Vector3 direction, float distance, uint layerMask = uint.MaxValue, bool collideWithAreas = false)
```

Performs a 3D raycast from a starting point in a specific direction for a set distance.

**Parameters:**
- `from` - The starting point of the ray in global space
- `direction` - The direction vector for the ray
- `distance` - The maximum distance the ray should travel
- `layerMask` - The collision mask to check against (defaults to all layers)
- `collideWithAreas` - If true, the ray will detect Area3D nodes as well

#### CastLine3D

```csharp
CastHit CastLine3D(this Node3D sender, Vector3 from, Vector3 to, uint layerMask = uint.MaxValue, bool collideWithAreas = false)
```

Performs a 3D line cast between two specific points in global space.

**Parameters:**
- `from` - The starting point of the line in global space
- `to` - The ending point of the line in global space
- `layerMask` - The collision mask to check against (defaults to all layers)
- `collideWithAreas` - If true, the ray will detect Area3D nodes in addition to bodies

#### GetCollisionMask

```csharp
uint GetCollisionMask(params uint[] layers)
```

Generates a collision mask bitfield by combining the specified physics layer numbers.

**Parameters:**
- `layers` - An array of 1-based layer numbers to include in the resulting mask

## 📂 Project Structure

```
BZ-Godot-Physics/
├── PhysicsHelper.cs    # Main utility file (add this to your project)
├── LICENSE             # MIT License
└── README.md           # This file
```

## 💡 Example Use Cases

- **Ground Detection**: Cast rays downward to check if a character is grounded
- **Line of Sight**: Cast lines between objects to check visibility
- **Projectile Systems**: Use raycasts for hitscan weapons
- **Obstacle Detection**: Check for obstacles in a character's path

## 📝 License

This project is licensed under the [MIT License](LICENSE). See the LICENSE file for details.

## 🤝 Contributing

Contributions are welcome! Feel free to:

- Report bugs via [Issues](https://github.com/BZ-Interactive/BZ-Godot-Physics/issues)
- Submit feature requests
- Create pull requests

## ⚠️ Notes

- This utility is for **3D physics only**
- Uses C# - .NET-enabled Godot builds required
- Layer numbers are **1-based** (matching Godot's editor UI)

---

Made with ❤️ for the Godot Community

