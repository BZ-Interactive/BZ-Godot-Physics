/*
 * PhysicsHelper.cs
 * Developed by Barkın ZORLU
 * Repository: https://github.com/ZorluBarkin/BZ-Godot-Physics
 * * A high-performance utility for Godot 4+ in C# to simplify 3D Raycasting.
 * Provides structured results (CastHit) and bitmask generation.
 */

using Godot;

namespace BZ.Physics;
public readonly struct CastHit
{
	public readonly bool NonEmpty { get; }
	public readonly Vector3 HitPosition { get; }
	public readonly Vector3 Normal { get; }
	//public readonly int FaceIndex {get;}
	public readonly CollisionObject3D Collider { get; }
	public readonly uint ColliderId { get; }
	//public readonly int /*Shape3D*/ Shape { get; }
	public readonly Rid Rid { get; } // its a ulong, low-end ID
	public readonly string ColliderOwnerName { get; }

	// NOTE: collision result keys:
	// "position", "normal", "face_index", "collider_id", "collider", "shape", "rid"

	public CastHit(Godot.Collections.Dictionary intersectResult)
	{
		NonEmpty = true;
		HitPosition = (Vector3) intersectResult["position"];
		Normal = (Vector3) intersectResult["normal"];
		//FaceIndex = (int) intersectResult["face_index"];
		Collider = (CollisionObject3D) intersectResult["collider"];
		ColliderId = (uint) intersectResult["collider_id"];
		//Shape = (int) intersectResult["shape"];
		Rid = (Rid) intersectResult["rid"];
		ColliderOwnerName = Collider.Name;
	}

	public CastHit()
	{
		NonEmpty = false;
		HitPosition = Vector3.Zero;
		Normal = Vector3.Zero;
		//FaceIndex = 0;
		Collider = null;
		ColliderId = 0;
		//Shape = 0;
		Rid = new Rid();
		ColliderOwnerName = "";
	}
}

public static class PhysicsHelper
{
	/// <summary>
	/// Performs a 3D line cast between two specific points in global space as an extension of Node3D.
	/// </summary>
	/// <param name="sender">The node instance calling this method.</param>
	/// <param name="from">The starting point of the line in global space.</param>
	/// <param name="to">The ending point of the line in global space.</param>
	/// <param name="layerMask">The collision mask to check against (defaults to all layers).</param>
	/// <param name="collideWithAreas">If true, the ray will detect Area3D nodes in addition to bodies.</param>
	/// <returns>A CastHit containing intersection data, or an empty CastHit if nothing was hit.</returns>
	public static CastHit CastLine3D(this Node3D sender, Vector3 from, Vector3 to, uint layerMask = uint.MaxValue, bool collideWithAreas = false)
	{
		PhysicsDirectSpaceState3D spaceState = sender.GetWorld3D().DirectSpaceState;
		PhysicsRayQueryParameters3D query = PhysicsRayQueryParameters3D.Create(from, to, layerMask);
		
		query.CollideWithAreas = collideWithAreas;

		var result = spaceState.IntersectRay(query);

		return result.Count > 0 ? new CastHit(result) : new CastHit();
	}

	/// <summary>
	/// Performs a 3D raycast from a starting point in a specific direction for a set distance as an extension of Node3D.
	/// </summary>
	/// <param name="sender">The node instance calling this method.</param>
	/// <param name="from">The starting point of the ray in global space.</param>
	/// <param name="direction">The direction vector for the ray.</param>
	/// <param name="distance">The maximum distance the ray should travel.</param>
	/// <param name="layerMask">The collision mask to check against.</param>
	/// <param name="collideWithAreas">If true, the ray will detect Area3D nodes as well.</param>
	/// <returns>A CastHit containing intersection data, or an empty CastHit if nothing was hit.</returns>
	public static CastHit CastRay3D(this Node3D sender, Vector3 from, Vector3 direction, float distance, uint layerMask = uint.MaxValue, bool collideWithAreas = false)
	{
		Vector3 to = from + (direction.Normalized() * distance);
		return CastLine3D(sender, from, to, layerMask, collideWithAreas);
	}

	/// <summary>
	/// Generates a collision mask bitfield by combining the specified physics layer numbers.
	/// </summary>
	/// <param name="layers">An array of 1-based layer numbers to include in the resulting mask.</param>
	/// <returns>A uint bitmask where each specified layer number is set as a collision layer.</returns>
	public static uint GetCollisionMask(params uint[] layers)
	{
		uint layermask = 0;

		foreach (uint layer in layers)
			layermask |= 1u << (int)(layer - 1);

		return layermask;
	}
}