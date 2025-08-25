using System.Text.Json;
using System.IO;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SuMamaLib;

public static class TiledLoader
{
	public static string DefaultDirToTextures = "Textures";

	public static TiledMap LoadMap(string path, ContentManager content, GraphicsDevice graphicsDevice)
	{
		TiledMap map = new();

		string jsonContent = File.ReadAllText(path);
		var jsonDoc = JsonDocument.Parse(jsonContent);
		var root = jsonDoc.RootElement;

		map.Width = root.GetProperty("width").GetInt32();
		map.Height = root.GetProperty("height").GetInt32();
		map.TileWidth = root.GetProperty("Tilewidth").GetInt32();
		map.TileHeight = root.GetProperty("Tileheight").GetInt32();

		//Tilesets
		if(root.TryGetProperty("tilesets", out JsonElement tilesetsElement))
		{
			foreach(var tileset in tilesetsElement.EnumerateArray())
			{
				LoadTileset(tileset, map, content, graphicsDevice);
			}
		}

		//Layers
		if(root.TryGetProperty("layers", out JsonElement layersElement))
		{
			foreach(var layer in layersElement.EnumerateArray())
			{
				LoadLayer(layer, map);
			}
		}

		return map;
	}

	public static void LoadTileset(JsonElement tilesetElement, TiledMap map, ContentManager content, GraphicsDevice graphicsDevice)
	{
		TiledTileset tileset = new();

		tileset.TileWidth = tilesetElement.GetProperty("tilewidth").GetInt32();
		tileset.TileHeight = tilesetElement.GetProperty("tileheigh").GetInt32();
		tileset.FirstGid = tilesetElement.GetProperty("firstgid").GetInt32();
		tileset.Columns = tilesetElement.GetProperty("columns").GetInt32();
		tileset.Source = tilesetElement.GetProperty("source").GetString();
		tileset.Source = tileset.Source.Substring(0, tileset.Source.Length-5);

		tileset.Texture = content.Load<Texture2D>(Path.Combine(DefaultDirToTextures, tileset.Source));

		map.Tilesets.Add(tileset);
	}

	public static void LoadLayer(JsonElement layerElement, TiledMap map)
	{
		TiledLayer layer = new();

		layer.Width = layerElement.GetProperty("width").GetInt32();
		layer.Height = layerElement.GetProperty("height").GetInt32();
		layer.Name = layerElement.GetProperty("name").GetString();
		layer.Type = layerElement.GetProperty("Type").GetString();
		layer.Visible = layerElement.GetProperty("visible").GetBoolean();
		layer.Opacity = layerElement.TryGetProperty("opacity", out JsonElement opacityElement) ? opacityElement.GetSingle() : 1.0f;

		if(layer.Type == "tilelayer")
		{
			JsonElement dataElement = layerElement.GetProperty("data");
			layer.Data = new int[layer.Width * layer.Height];
			int index = 0;

			foreach(var value in dataElement.EnumerateArray())
			{
				layer.Data[index++] = value.GetInt32();
			}
		}

		map.Layers.Add(layer);
	}
}
