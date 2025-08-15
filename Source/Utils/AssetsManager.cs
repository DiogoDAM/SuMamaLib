using System;
using System.Collections.Generic;

namespace SuMamaLib;

public class AssetsManager
{
	private Dictionary<string, ITextureRegion> _textures;

	public AssetsManager()
	{
		_textures = new();
	}

	public void CreateTexture(string textureName, ITextureRegion texture)
	{
		if(texture == null) throw new ArgumentNullException();
		if(String.IsNullOrEmpty(textureName)) throw new ArgumentNullException();

		_textures.Add(textureName, texture);
	}

	public T LoadTexture<T>(string textureName) where T : ITextureRegion
	{
		if(String.IsNullOrEmpty(textureName)) throw new ArgumentNullException();

		if(!_textures.ContainsKey(textureName)) throw new KeyNotFoundException();

		return (T)_textures[textureName];
	}
}
