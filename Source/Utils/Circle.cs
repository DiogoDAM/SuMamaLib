using System;

using Microsoft.Xna.Framework;

namespace SuMamaLib;

public readonly struct Circle : IEquatable<Circle>
{
	public static readonly Circle Empty = new Circle();

	public readonly int X;
	public readonly int Y;
	public readonly int Radius;

	public readonly int Diamater => Radius * 2;
	public readonly float Circunference => (float) (2 * Radius * Math.PI);

	public readonly int Top => Y - Radius;
	public readonly int Bottom => Y + Radius;
	public readonly int Left => X - Radius;
	public readonly int Right => X + Radius;

	public readonly bool IsEmpty => X == 0 && Y == 0 && Radius == 0;

	public readonly Point Location => new Point(X, Y);
	public readonly Vector2 Center => new Vector2(X, Y);

	public Rectangle ToRectangle() => new Rectangle(Left, Top, Right - Left, Bottom - Top);

	public Circle()
	{
		X = 0;
		Y = 0;
		Radius = 0;
	}

	public Circle(int x, int y, int radius)
	{
		X = x;
		Y = y;
		Radius = radius;
	}

	public Circle(Point local, int radius)
	{
		X = local.X;
		Y = local.Y;
		Radius = radius;
	}

	public bool Intersects(Vector2 vec)
	{
		float distance = Vector2.Distance(Location.ToVector2(), vec);
		return distance < Radius;
	}

	public bool Intersects(Point location)
	{
		float distance = Vector2.Distance(Location.ToVector2(), location.ToVector2());
		return distance < Radius;
	}

	public bool Intersects(Circle other)
	{
		int radius = (Radius + other.Radius) * (Radius + other.Radius);
		float distance = Vector2.DistanceSquared(Location.ToVector2(), other.Location.ToVector2());
		return distance < radius;
	}


	//Operators overlod
	public static Circle operator +(Circle c1, Circle c2) => new Circle(c1.X + c2.X, c1.Y + c2.Y, c1.Radius + c2.Radius);
	public static Circle operator -(Circle c1, Circle c2) => new Circle(c1.X - c2.X, c1.Y - c2.Y, c1.Radius - c2.Radius);
	public static Circle operator *(Circle c1, Circle c2) => new Circle(c1.X * c2.X, c1.Y * c2.Y, c1.Radius * c2.Radius);
	public static Circle operator /(Circle c1, Circle c2) => new Circle(c1.X / c2.X, c1.Y / c2.Y, c1.Radius / c2.Radius);

	public static bool operator ==(Circle c1, Circle c2) => c1.Equals(c2);
	public static bool operator !=(Circle c1, Circle c2) => !c1.Equals(c2);

	public static bool operator <(Circle c1, Circle c2) => c1.Radius < c2.Radius;
	public static bool operator >(Circle c1, Circle c2) => c1.Radius > c2.Radius;
	public static bool operator <=(Circle c1, Circle c2) => c1.Radius <= c2.Radius;
	public static bool operator >=(Circle c1, Circle c2) => c1.Radius >= c2.Radius;


	//Object Methods
	public override readonly int GetHashCode() => HashCode.Combine(X, Y, Radius);

	public override string ToString() => $"(X: {X}, Y: {Y}, Radius: {Radius})";

    public bool Equals(Circle other)
    {
		return X == other.X && Y == other.Y && Radius == other.Radius;
    }

    public override bool Equals(object obj) => obj is Circle other && Equals(other);
}
