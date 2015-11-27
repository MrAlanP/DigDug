using UnityEngine;

public struct IntVector2 {
	public int x, y;
	public IntVector2(int _x, int _y){
		x = _x;
		y = _y;
	}
	
	public static IntVector2 operator -(IntVector2 a, IntVector2 b){
		return new IntVector2 (a.x - b.x, a.y - b.y);
	}
	
	public static IntVector2 operator +(IntVector2 a, IntVector2 b){
		return new IntVector2 (a.x + b.x, a.y + b.y);
	}

	public static IntVector2 operator +(IntVector2 a, Vector2 b){
		return new IntVector2 (a.x + (int)b.x, a.y + (int)b.y);
	}
	
	public float magnitude{
		get{
			return Mathf.Sqrt((x * x) + (y * y));
		}
	}
	
	public static float Distance (IntVector2 a, IntVector2 b)
	{
		return (a - b).magnitude;
	}

//	public static bool operator ==(IntVector2 c1, IntVector2 c2){
//		return false;
//	}
//
//	public static bool operator !=(IntVector2 c1, IntVector2 c2){
//		return false;
//	}
	public static bool operator ==(IntVector2 a, IntVector2 b)
	{
		// If both are null, or both are same instance, return true.
		if (System.Object.ReferenceEquals(a, b))
		{
			return true;
		}
		
		// If one is null, but not both, return false.
		if (((object)a == null) || ((object)b == null))
		{
			return false;
		}
		
		// Return true if the fields match:
		return a.x == b.x && a.y == b.y;
	}
	
	public static bool operator !=(IntVector2 a, IntVector2 b)
	{
		return !(a == b);
	}
}