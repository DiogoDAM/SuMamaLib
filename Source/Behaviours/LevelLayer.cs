using System;

using Microsoft.Xna.Framework.Graphics;

namespace SuMamaLib;

public class LevelLayer : IDisposable
{
	public bool Disposed { get; private set; }

	public ObjectManager<Entity> Entities;

	public string Name;
	public Level Level { get; private set; }

	public SamplerState SamplerState = SamplerState.PointWrap;

	public LevelLayer(Level level)
	{
		Entities = new();
		Level = level;
	}

	public void Start()
	{
		foreach(Entity e in Entities)
		{
			e.Start();
		}
	}

	public void PreUpdate(Time time)
	{
		Entities.ProcessAddAndRemove();

		foreach(Entity e in Entities)
		{
			e.PreUpdate(time);
		}
	}

	public void Update(Time time)
	{
		foreach(Entity e in Entities)
		{
			e.Update(time);
		}
	}

	public void AfterUpdate(Time time)
	{
		foreach(Entity e in Entities)
		{
			e.AfterUpdate(time);
		}
	}

	public void Draw()
	{
		SuMamaGame.SpriteBatch.Begin(samplerState: SamplerState);

			foreach(Entity e in Entities)
			{
				e.Draw();
			}

		SuMamaGame.SpriteBatch.End();
	}

	public void Dispose()
	{
		if(!Disposed)
		{
			foreach(Entity e in Entities)
			{
				e.Dispose();
			}
			Entities.Clear();

			Level = null;

			Disposed = true;
		}
	}
}
