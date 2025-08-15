using System.Collections.Generic;
using System;

namespace SuMamaLib;

public class LevelManager
{
	private Dictionary<int, Level> _levels;

	public static Level CurrentLevel { get; private set; }
	private (int, bool) _nextLevelInfo;

	public LevelManager()
	{
		_levels = new();
	}

	public void Start()
	{
		CurrentLevel?.Start();
	}

	public void PreUpdate(Time time)
	{
		if(_nextLevelInfo.Item1 != -1) _ChangeLevel();

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

	public void Add(int id, Level level)
	{
		if(id == -1) throw new Exception("A level id don't could be -1");

		level.Id = id;
		_levels.Add(id, level);
	}

	public void Remove(int id)
	{
		_levels.Remove(id);
	}

	public void SwitchLevel(int id, bool disposable=false)
	{
		if(!_levels.ContainsKey(id)) throw new KeyNotFoundException();

		if(_levels[id] == null) _levels[id] = new();

		_nextLevelInfo = (id, disposable); 
	}

	private void _ChangeLevel()
	{
		CurrentLevel?.Desactive();

		CurrentLevel = _levels[_nextLevelInfo.Item1];

		CurrentLevel.Active();

		if(_nextLevelInfo.Item2) CurrentLevel?.Dispose();

		_nextLevelInfo.Item1 = -1;
		_nextLevelInfo.Item2 = false;
	}
}
