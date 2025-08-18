using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace SuMamaLib;

public static class CollisionHelper
{
	public static bool CheckRectCircle(Rectangle rect, Circle circle)
	{
		float closestX = MathHelper.Clamp(circle.Center.X, rect.Left, rect.Right);
		float closestY = MathHelper.Clamp(circle.Center.Y, rect.Top, rect.Bottom);

		float dx = circle.Center.X - closestX;
		float dy = circle.Center.Y - closestY;

		return dx * dx + dy * dy <= circle.Radius * circle.Radius;
	}

	//Singles
	public static bool Collide(Entity e, IEnumerable<Entity> colliders)
	{
		foreach(Entity e2 in colliders)
		{
			if(e.Collider.Collide(e2.Collider)) return true;
		}

		return false;
	}

	public static void Collide(Entity e, IEnumerable<Entity> colliders, Action<Entity, Entity> resolve)
	{
		foreach(Entity e2 in colliders)
		{
			if(e.Collider.Collide(e2.Collider)) resolve(e, e2);
		}
	}

	public static bool Collide(Collider c, IEnumerable<Collider> colliders)
	{
		foreach(Collider c2 in colliders)
		{
			if(c.Collide(c2)) return true;
		}

		return false;
	}

	public static void Collide(Collider c, IEnumerable<Collider> colliders, Action<Collider, Collider> resolve)
	{
		foreach(Collider c2 in colliders)
		{
			if(c.Collide(c2)) resolve(c, c2);
		}
	}


	//IEnumerables
	public static bool Collide(IEnumerable<Entity> colliders1, IEnumerable<Entity> colliders2)
	{
		foreach(Entity e2 in colliders2)
		{
			foreach(Entity e1 in colliders1)
			{
				if(e2.Collider.Collide(e1)) return true;
			}
		}

		return false;
	}

	public static void Collide(IEnumerable<Entity> colliders1, IEnumerable<Entity> colliders2, Action<Entity, Entity> resolve)
	{
		foreach(Entity e2 in colliders2)
		{
			foreach(Entity e1 in colliders1)
			{
				if(e2.Collider.Collide(e1)) resolve(e1, e2);
			}
		}
	}

	public static bool Collide(IEnumerable<Collider> colliders1, IEnumerable<Collider> colliders2)
	{
		foreach(Collider c2 in colliders2)
		{
			foreach(Collider c1 in colliders1)
			{
				if(c2.Collide(c1)) return true;
			}
		}

		return false;
	}

	public static void Collide(IEnumerable<Collider> colliders1, IEnumerable<Collider> colliders2, Action<Collider, Collider> resolve)
	{
		foreach(Collider c2 in colliders2)
		{
			foreach(Collider c1 in colliders1)
			{
				if(c2.Collide(c1)) resolve(c1, c2);
			}
		}
	}

}
