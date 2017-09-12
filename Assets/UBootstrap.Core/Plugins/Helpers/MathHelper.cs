using UnityEngine;
using System.Collections;

namespace UBootstrap
{
    static public partial class MathHelper
    {
        /// <summary>
        /// Round to nearest multiples of steps. For instance, 3 rounded to nearest 5 results in 5,
        /// 2 rounded to nearest 5 results in 0
        /// </summary>
        /// <returns>The to nearest.</returns>
        /// <param name="value">Value.</param>
        /// <param name="step">Round to nearest.</param>
        public static float RoundToNearest (float value, float step)
        {
            return Mathf.Round (value / step) * step;
        }

        public static bool ApproximatelyEqual (float a, float b)
        {
            return Mathf.Abs (a - b) < Mathf.Epsilon;
        }

        public static bool ApproximatelyEqual (double a, double b)
        {
            return Abs (a - b) < Mathf.Epsilon;
        }

        public static double Abs (double d)
        {
            return d > 0 ? d : -d;
        }

        public static bool IsZero (double d)
        {
            return Abs (d) < Mathf.Epsilon;
        }

        public static bool IsZero (float d)
        {
            return Mathf.Abs (d) < Mathf.Epsilon;
        }
    }

}
