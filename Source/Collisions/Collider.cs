using Microsoft.Xna.Framework;
using System;

namespace SuMamaLib;

public abstract class Collider : IDisposable, IPrototype
{
	public bool Disposed { get; protected set; }

	public Vector2 Offset;
	public Vector2 Position { get => (Entity != null) ? Entity.Transform.Position + Offset : Offset; }

	public Entity Entity;

	public abstract int Width { get; set; }
	public abstract int Height { get; set; }

	public abstract Vector2 Center { get; }

	public Collider()
	{
	}

	public Collider(Entity e)
	{
		Entity = e;
	}

	public bool Collide(Entity other)
	{
		return Collide(other.Collider);
	}

	public bool Collide(Collider col)
	{
		switch(col)
		{
			case BoxCollider box: return Collide(box);
			case CircleCollider circle: return Collide(circle);
			case ColliderList list: return Collide(list);
			default: return false;
		}

	}

	public abstract bool Collide(BoxCollider box);
	public abstract bool Collide(CircleCollider circle);
	public abstract bool Collide(ColliderList list);
	public abstract bool Collide(Vector2 vec);

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposable)
	{
		if(disposable)
		{
			if(!Disposed)
			{
				Entity = null;
				Disposed = true;
			}
		}
	}

    public virtual IPrototype ShallowClone()
    {
		return (Collider)MemberwiseClone();
    }

    public virtual IPrototype DeepClone()
    {
		Collider clone = (Collider)MemberwiseClone();

		clone.Entity = Entity;

		return clone;
    }
}
