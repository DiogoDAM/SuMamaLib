using System;

namespace SuMamaLib;

public abstract class Entity : IDisposable
{
	public bool Disposed { get; protected set; }
	public bool IsActive { get; protected set; }
	public bool IsDrawable { get; protected set; }

	public Entity()
	{
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


	public void Dispose()
	{
		Dispose(true);
	}

	protected void Dispose(bool disposable)
	{
		if(disposable)
		{
			if(!Disposed)
			{
				Desactive();
				Disposed = true;
			}
		}
	}
}
