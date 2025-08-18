using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SuMamaLib;

public class Level : IDisposable, IEquatable<Level>
{
	public bool Disposed { get; protected set; }
	public bool IsActive { get; protected set; }
	public bool IsDrawable { get; protected set; }

	public Color BackgroundColor = Color.DimGray;

	public ContentManager Content;
	public AssetsManager Assets;

	public LevelLayer Instances;

	public int Id;

	public Level()
	{
		Content = new(SuMamaGame.Content.ServiceProvider);
		Assets = new();
		Instances = new(this);
	}

	//Behaviour Methods
	public virtual void Start()
	{
		Active();

		Instances.Start();
	}

	public virtual void PreUpdate(Time time)
	{
		Instances.PreUpdate(time);
	}

	public virtual void Update(Time time)
	{
		Instances.Update(time);
	}

	public virtual void AfterUpdate(Time time)
	{
		Instances.AfterUpdate(time);
	}

	public virtual void Draw()
	{
		Instances.Draw();
	}

	public virtual void UnloadContent()
	{
		Content.Unload();
		Assets.Unload();
		Instances.Clear();
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
				Instances.Dispose();
				UnloadContent();
				Disposed = true;
			}
		}
	}

    public bool Equals(Level other)
    {
		return Id == other.Id;
    }
}
