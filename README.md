# BZ-Godot-Physics

A clean, optimized C# physics utility for Godot 4+. Simplifies low-level raycasting with structured result containers and provides easy collision mask generation.

## Features

- **Easy Raycasting**: Extension methods for `Node3D` to perform raycasts and line casts
- **Structured Results**: `CastHit` struct provides clean access to hit position, normal, collider, and more
- **Collision Masks**: Simple helper method to generate collision layer masks
- **Zero Dependencies**: Just one file, pure Godot 4+ C# code

## Installation

Simply add `PhysicsHelper.cs` to your Godot project, anywhere in your `res://` directory.

## Usage

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

// Cast a line between two points
CastHit lineHit = this.CastLine3D(
    from: startPoint,
    to: endPoint,
    collideWithAreas: true
);
```

> **Note**: Methods are available as extensions on any `Node3D`. Ensure you have `using BZ.Physics;` at the top of your script.

## License

MIT License - See [LICENSE](LICENSE) for details.
