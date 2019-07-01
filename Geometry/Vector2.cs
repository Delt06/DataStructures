using System;
using System.Globalization;
using JetBrains.Annotations;

namespace DataStructures.Geometry
{
    public struct Vector2 : IEquatable<Vector2>
    {
        #region Fields

        public readonly double X, Y;

        #endregion

        #region Contructors

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Public properties

        public double SqrMagnitude => X * X + Y * Y;

        public double Magnitude => Math.Sqrt(SqrMagnitude);

        #endregion

        #region Object members

        public override bool Equals(object obj)
        {
            if (!(obj is Vector2 otherVector2)) return false;
            return Equals(otherVector2);
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public override string ToString() => ToString(CultureInfo.CurrentCulture);

        [NotNull]
        public string ToString([NotNull] IFormatProvider formatProvider)
        {
            if (formatProvider is null) throw new ArgumentNullException(nameof(formatProvider));
            return $"({X.ToString(formatProvider)}, {Y.ToString(formatProvider)})";
        }

        #endregion

        #region IEquatable members

        public bool Equals(Vector2 other)
        {
            return X == other.X && Y == other.Y;
        }

        public bool Equals(Vector2 other, double epsilon)
        {
            if (epsilon < 0d) throw new ArgumentOutOfRangeException(nameof(epsilon));

            return Math.Abs(X - other.X) <= epsilon &&
                   Math.Abs(Y - other.Y) <= epsilon;
        }

        #endregion

        #region Static members

        #region Arithmetics

        public static Vector2 Plus(Vector2 vector) => vector;

        public static Vector2 Minus(Vector2 vector) => new Vector2(-vector.X, -vector.Y);

        public static Vector2 Add(Vector2 vector1, Vector2 vector2) => new Vector2(vector1.X + vector2.X, vector1.Y + vector2.Y);

        public static Vector2 Subtract(Vector2 vector1, Vector2 vector2) => new Vector2(vector1.X - vector2.X, vector1.Y - vector2.Y);

        public static Vector2 Multiply(Vector2 vector, double number) => new Vector2(vector.X * number, vector.Y * number);

        public static Vector2 Divide(Vector2 vector, double number) => new Vector2(vector.X / number, vector.Y / number);

        #region Operators

        public static Vector2 operator +(Vector2 vector) => Plus(vector);

        public static Vector2 operator -(Vector2 vector) => Minus(vector);

        public static Vector2 operator +(Vector2 vector1, Vector2 vector2) => Add(vector1, vector2);

        public static Vector2 operator -(Vector2 vector1, Vector2 vector2) => Subtract(vector1, vector2);

        public static Vector2 operator *(Vector2 vector, double number) => Multiply(vector, number);

        public static Vector2 operator *(double number, Vector2 vector) => Multiply(vector, number);

        public static Vector2 operator /(Vector2 vector, double number) => Divide(vector, number);

        #endregion

        #endregion

        #region Other operations

        public static double DotProduct(Vector2 vector1, Vector2 vector2)
        {
            return vector1.X * vector2.X + vector1.Y * vector2.Y;
        }

        public static double Distance(Vector2 position1, Vector2 position2)
        {
            return (position2 - position1).Magnitude;
        }

        public static double SqrDistance(Vector2 position1, Vector2 position2)
        {
            return (position2 - position1).SqrMagnitude;
        }

        #endregion

        #region Predefined vectors

        public static Vector2 Zero => new Vector2(0d, 0d);

        public static Vector2 One => new Vector2(1d, 1d);

        public static Vector2 Right => new Vector2(1d, 0d);

        public static Vector2 Left => new Vector2(-1d, 0d);

        public static Vector2 Up => new Vector2(0d, 1d);

        public static Vector2 Down => new Vector2(0d, -1d);

        #endregion

        #endregion
    }
}
