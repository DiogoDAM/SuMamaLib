using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using System;

namespace SuMamaLib;

public class SuMamaGame : Game
{
	private static SuMamaGame s_Instance;
	public static SuMamaGame Instance => s_Instance;

	//Game Graphics Util Properties
	public new static ContentManager Content;
	public new static GraphicsDevice GraphicsDevice;
	public static GraphicsDeviceManager Graphics;
	public static SpriteBatch SpriteBatch;

	//Game Utils Properties
	public Time Time;
	public LevelManager LevelManager;
	public static AssetsManager Assets;
	public static Input Input;
	public static Level CurrentLevel => LevelManager.CurrentLevel;

	//Window
	public string Title => Window.Title;
	public int WindowWidth => Graphics.PreferredBackBufferWidth;
	public int WindowHeight => Graphics.PreferredBackBufferHeight;
	public bool IsFullscreen => Graphics.IsFullScreen;

	public SuMamaGame(string title, int ww, int wh, bool isFullscreen=false)
	{
		s_Instance = this;

		Graphics = new(this);

		Content = base.Content;
		Content.RootDirectory = "Content";

		Window.Title = title;
		ResizeWindow(ww, wh);
		SetWindowFullscreen(isFullscreen);

		Time = new();

		LevelManager = new();
		Input = new();
		Assets = new();

		IsMouseVisible = true;
	}

	protected override void Initialize()
	{
		base.Initialize();

		GraphicsDevice = base.GraphicsDevice;
		SpriteBatch = new(GraphicsDevice);

		Drawer.Initialize(GraphicsDevice);
	}

	protected override void Update(GameTime gameTime)
	{
		Time.GameTime = gameTime;
		Input.Update();

		LevelManager.PreUpdate(Time);
		LevelManager.Update(Time);
		LevelManager.AfterUpdate(Time);

		base.Update(gameTime);
	}

	protected override void Draw(GameTime gameTime)
	{
		GraphicsDevice.Clear(CurrentLevel.BackgroundColor);
		LevelManager.Draw();

		base.Draw(gameTime);
	}

	public void ResizeWindow(int ww, int wh)
	{
		Graphics.PreferredBackBufferWidth = ww;
		Graphics.PreferredBackBufferHeight = wh;
		Graphics.ApplyChanges();
	}

	public void SetWindowFullscreen(bool value)
	{
		Graphics.IsFullScreen = value;
		Graphics.ApplyChanges();
	}

}

