using System;

namespace SuMamaLib;

public abstract class Entity : IDisposable, IPrototype
{
	public bool Disposed { get; protected set; }
	public bool IsActive { get; protected set; }
	public bool IsDrawable { get; protected set; }

	public Transform Transform; 

	public Level Level;
	public string Layer;
	public int Id;

	public Collider Collider;

	public Entity()
	{
		Transform = new();
	}

	public virtual void Start()
	{
		Active();
	}

	public virtual void PreUpdate(Time time)
	{
	}

	public virtual void Update(Time time)
	{
	}

	public virtual void AfterUpdate(Time time)
	{
	}

	public virtual void Draw()
	{
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

				Level = null;
				Layer = null;

				Desactive();
				Disposed = true;
			}
		}
	}

}
