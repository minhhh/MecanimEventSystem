using UnityEngine;
using System.Collections;

using System;
using System.Collections;

namespace UBootstrap
{
    static public partial class VectorHelper
    {
        public static Vector3 WithX (this Vector3 vec, float x)
        {
            return new Vector3 (x, vec.y, vec.z);
        }

        public static Vector2 WithX (this Vector2 vec, float x)
        {
            return new Vector2 (x, vec.y);
        }

        public static Vector3 WithY (this Vector3 vec, float y)
        {
            return new Vector3 (vec.x, y, vec.z);
        }

        public static Vector2 WithY (this Vector2 vec, float y)
        {
            return new Vector2 (vec.x, y);
        }

        public static Vector3 WithZ (this Vector3 vec, float z)
        {
            return new Vector3 (vec.x, vec.y, z);
        }

        public static float Angle2D (Vector3 vec1, Vector3 vec2)
        {
            Vector2 diference = vec2 - vec1;
            float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
            return Vector2.Angle (Vector2.right, diference) * sign;
        }
    }
}
