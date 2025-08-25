using System.Collections.Generic;

namespace SuMamaLib;

public class TiledObjectGroup
{
	public string Name { get; set; }
	public List<TiledObject> Objects { get; set; }

	public TiledObjectGroup()
	{
		Objects = new List<TiledObject>();
	}
}
