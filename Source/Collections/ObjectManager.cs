using System;
using System.Collections;
using System.Collections.Generic;

public class ObjectManager<T>  where T : IDisposable, IEnumerable
{
	public int MaxSize { get; set; }

	private List<T> _objects;
	private Queue<T> _toAdd;
	private Queue<(T, bool)> _toRemove;

	public int Count { get; private set; }

	public ObjectManager()
	{
		_objects = new();

		MaxSize = 64;
	}

	public void Add(T item)
	{
		if(Count == MaxSize) return;

		_toAdd.Enqueue(item);
		Count++;
	}

	public void Remove(T item)
	{
		_toRemove.Enqueue((item, false));
		Count--;
	}

	public void Free(T item)
	{
		_toRemove.Enqueue((item, true));
		Count--;
	}

	public bool Contains(T item)
	{
		return _objects.Contains(item);
	}

	public T Find(Predicate<T> predicate)
	{
		return _objects.Find(predicate);
	}

	public List<T> FindAll(Predicate<T> predicate)
	{
		return _objects.FindAll(predicate);
	}

	public void ProcessAddAndRemove()
	{
		foreach(var item in _toAdd)
		{
			_objects.Add(item);
			_toAdd.Dequeue();
		}

		foreach(var pair in _toRemove)
		{
			T item = pair.Item1;
			bool disposable = pair.Item2;

			_objects.Remove(item);
			_toRemove.Dequeue();

			if(disposable) item.Dispose();
		}
	}

    public IEnumerator GetEnumerator()
    {
		return GetEnumerator();
    }
}
