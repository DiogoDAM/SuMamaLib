using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuMamaLib;

public class Camera
{
	public Transform Transform;
	public Viewport Viewport;

	public Camera()
	{
		Transform = new();
		Viewport = new();
	}

	public Camera(Transform parent, int width, int height)
	{
		Transform = new();
		Transform.Parent = parent;
		Viewport = new();
		Viewport.Width = width;
		Viewport.Height = height;
	}

	public Matrix GetMatrix() => Transform.CreateGlobalMatrix();
}
