using System;

namespace GXPEngine.Core
{
    public struct Vector2
    {
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        override public string ToString()
        {
            return "[Vector2 " + x + ", " + y + "]";
        }




        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x + right.x, left.y + right.y);
        }
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x - right.x, left.y - right.y);
        }
        public static Vector2 operator *(float left, Vector2 right)
        {
            return new Vector2(left * right.x, left * right.y);
        }

        public static Vector2 operator *(Vector2 left, float right)
        {
            return new Vector2(left.x * right, left.y * right);
        }

        public static Vector2 operator /(Vector2 left, float right)
        {
            return new Vector2(left.x / right, left.y / right);
        }

        
    public float Length()
    {
        return (float)(Math.Sqrt(this.x * this.x + this.y * this.y));
    }

    public void Normalize()
    {
        float length = Length();
        if (length != 0)
        {
            this.x /= length;
            this.y /= length;
        }
    }

    public Vector2 Normalized()
    {
        float length = Length();
        if (length != 0)
            return new Vector2(this.x / length, this.y / length);
        else
            return new Vector2(0, 0);
    }
    }
}

