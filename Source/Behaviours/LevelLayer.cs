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
	public Camera Camera;

	public LevelLayer(Level level)
	{
		Entities = new();
		Level = level;
	}

	public void Add(Entity e)
	{
		Entities.Add(e);
		e.Level = Level;
		e.Layer = Name;
	}

	public bool Remove(Entity e)
	{
		bool value = Entities.Remove(e);

		return value;
	}

	public bool Destroy(Entity e)
	{
		bool value = Entities.Free(e);

		return value;
	}

	public void Clear()
	{
		Entities.Clear();
	}

	public T GetType<T>() where T : Entity => (T)Entities.Find(e => e is T);

	//Behaviour Methods
	public void Start()
	{
		Entities.ProcessAddAndRemove();

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
		SuMamaGame.SpriteBatch.Begin(samplerState: SamplerState, transformMatrix: Camera.GetMatrix());

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
