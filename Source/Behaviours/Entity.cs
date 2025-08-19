using System;

namespace SuMamaLib;

public abstract class Entity : IDisposable, IPrototype, ICollider
{
	public bool Disposed { get; protected set; }
	public bool IsActive { get; protected set; }
	public bool IsDrawable { get; protected set; }

	public Transform Transform; 

	public Level Level;
	public string Layer;
	public int Id;

	public Collider Collider { get; set; }

	public ObjectManager<Component> Components;

	public Entity()
	{
		Transform = new();
		Components = new();
	}

	public virtual void Start()
	{
		Active();

		Components.ProcessAddAndRemove();

		foreach(Component c in Components)
		{
			c.Start();
		}
	}

	public virtual void PreUpdate(Time time)
	{
		Components.ProcessAddAndRemove();

		foreach(Component c in Components)
		{
			c.PreUpdate(time);
		}
	}

	public virtual void Update(Time time)
	{
		foreach(Component c in Components)
		{
			c.Update(time);
		}
	}

	public virtual void AfterUpdate(Time time)
	{
		foreach(Component c in Components)
		{
			c.AfterUpdate(time);
		}
	}

	public virtual void Draw()
	{
		foreach(Component c in Components)
		{
			c.Draw();
		}
	}

	public void Active()
	{
		IsActive = true;
		IsDrawable = true;
	}

	public void Desactive()
	{
		IsActive = false;
		IsDrawable = false;
	}

	public virtual void OnCollisionEnter(Collider other)
	{
	}


	//COmponents Methods 
	public void AddComponent(Component c) 
	{
		c.Entity = this;

		Components.Add(c);
	}

	public bool RemoveComponent(Component c) 
	{
		if(Components.Contains(c)) return false;

		return Components.Remove(c);
	}

	public bool DestroyComponent(Component c) 
	{
		if(Components.Contains(c)) return false;

		return Components.Free(c);
	}

	public bool ContainsComponent(Component c) 
	{
		return Components.Contains(c);
	}

	public T GetComponentType<T>() where T : Component
	{
		return (T)Components.Find(c => c is T);
	}


	//INterfaces Implementation
    public IPrototype ShallowClone()
    {
		return (Entity)MemberwiseClone();
    }

    public IPrototype DeepClone()
    {
		Entity clone = (Entity)MemberwiseClone();

		clone.Transform = new(Transform);
		clone.Collider = (Collider)Collider.DeepClone();

		return (Entity)clone;
    }


	public void Dispose()
	{
		Dispose(true);
	}

	protected virtual void Dispose(bool disposable)
	{
		if(disposable)
		{
			if(!Disposed)
			{
				Transform = null;
				Collider?.Dispose();

				Components.Clear();

				Level = null;
				Layer = null;

				Desactive();
				Disposed = true;
			}
		}
	}

}
