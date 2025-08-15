using Microsoft.Xna.Framework;

namespace SuMamaLib;

public class Time
{
	public GameTime GameTime;
	public float DeltaTime => (float)GameTime.ElapsedGameTime.TotalSeconds;

	public Time(GameTime gameTime)
	{
		GameTime = gameTime;
	}
}
