using System;

namespace SuMamaLib;

public abstract class Level : IDisposable
{
	public bool Disposed { get; protected set; }
	public bool IsActive { get; protected set; }
	public bool IsDrawable { get; protected set; }

	public Level()
	{
	}

	public virtual void Start()
	{
		Active();
	}

	public virtual void PreUpdate()
	{
	}

	public virtual void Update()
	{
	}

	public virtual void AfterUpdate()
	{
	}

	public virtual void Active()
	{
		IsActive = true;
		IsDrawable = true;
	}

	public virtual void Desactive()
	{
		IsActive = false;
		IsDrawable = false;
	}

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
				Desactive();
				Disposed = true;
			}
		}
	}
}
