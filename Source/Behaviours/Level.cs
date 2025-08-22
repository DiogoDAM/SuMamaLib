using System;
using System.Collections.Generic;
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

	public Camera Camera;

	public SortedDictionary<string, LevelLayer> Layers;

	public int Id;

	public Level()
	{
		Content = new(SuMamaGame.Content.ServiceProvider);
		Layers = new();

		Camera = new(SuMamaGame.WindowWidth, SuMamaGame.WindowHeight);
		Camera.Transform.Scale = Vector2.One;
	}

	//Behaviour Methods
	public virtual void Start()
	{
		Active();

		foreach(var pairs in Layers)
		{
			pairs.Value.Start();
		}
	}

	public virtual void PreUpdate(Time time)
	{
		foreach(var pairs in Layers)
		{
			pairs.Value.PreUpdate(time);
		}
	}

	public virtual void Update(Time time)
	{
		foreach(var pairs in Layers)
		{
			pairs.Value.Update(time);
		}
	}

	public virtual void AfterUpdate(Time time)
	{
		foreach(var pairs in Layers)
		{
			pairs.Value.AfterUpdate(time);
		}
	}

	public virtual void Draw()
	{
		foreach(var pairs in Layers)
		{
			pairs.Value.Draw();
		}
	}

	public virtual void UnloadContent()
	{
		Content.Unload();
		Layers.Clear();
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
				foreach(var pairs in Layers)
				{
					pairs.Value.Dispose();
				}
				Layers.Clear();
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
