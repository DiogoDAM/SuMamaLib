using System.Collections.Generic;
using System;

namespace SuMamaLib;

public class LevelManager
{
	private Dictionary<Type, Level> _levels;

	public static Level CurrentLevel { get; private set; }
	private (Type, bool) _nextLevelInfo;

	public LevelManager()
	{
		_levels = new();
	}

	public void Start()
	{
		if(_nextLevelInfo.Item1 != null) _ChangeLevel();

		CurrentLevel?.Start();
	}

	public void PreUpdate(Time time)
	{
		if(_nextLevelInfo.Item1 != null) _ChangeLevel();

		CurrentLevel?.PreUpdate(time);
	}

	public void Update(Time time)
	{
		CurrentLevel?.Update(time);
	}

	public void AfterUpdate(Time time)
	{
		CurrentLevel?.AfterUpdate(time);
	}

	public void Draw()
	{
		CurrentLevel?.Draw();
	}

	public void Add<T>() where T : Level, new()
	{
		T level = new T();
		_levels.Add(level.GetType(), level);
	}

	public void Remove<T>() where T : Level
	{
		_levels.Remove(typeof(T));
	}

	public void SwitchLevel<T>(bool disposable=false) where T : Level, new()
	{
		Type type = typeof(T);
		if(!_levels.ContainsKey(type)) throw new KeyNotFoundException();

		if(_levels[type].Disposed)  _levels[type] = new T();

		_nextLevelInfo = (type, disposable); 
	}

	private void _ChangeLevel()
	{
		var oldLevel = CurrentLevel;
		CurrentLevel?.Desactive();

		CurrentLevel = _levels[_nextLevelInfo.Item1];

		CurrentLevel.Start();

		if(_nextLevelInfo.Item2 && !oldLevel.Disposed) { oldLevel.Dispose(); }

		_nextLevelInfo.Item1 = null;
		_nextLevelInfo.Item2 = false;
	}
}
