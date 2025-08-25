using Microsoft.Xna.Framework.Graphics;

namespace SuMamaLib;

public class TiledTileset
{
	public int FirstGid { get; set; }
	public string Source { get; set; }
	public int TileWidth { get; set; }
	public int TileHeight { get; set; }
	public int Columns { get; set; }
	public Texture2D Texture { get; set; }
}
