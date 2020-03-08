using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantanEngine.Core
{
    public class KanVector2d
    {

        #region Fields

        public static KanVector2d ZERO => new KanVector2d(0, 0);
        public static KanVector2d ONE => new KanVector2d(1, 1);

        #endregion

        public float X { get; set; }
        public float Y { get; set; }

        public KanVector2d(float x, float y)
        {
            X = x;
            Y = y;
        }

        public KanVector2d(KanVector2d kanVector2D) : this(kanVector2D.X, kanVector2D.Y) { }

        #region Public Methods

        public KanVector2d Add(float x, float y)
        {
            X += x;
            Y += y;
            return this;
        }
        public KanVector2d Add(KanVector2d kanVector2d) => Add(kanVector2d.X, kanVector2d.Y);

        public KanVector2d Subtract(float x, float y)
        {
            X -= x;
            Y -= y;
            return this;
        }
        public KanVector2d Subtract(KanVector2d kanVector2d) => Subtract(kanVector2d.X, kanVector2d.Y);

        public KanVector2d Multiply(float x, float y)
        {
            X *= x;
            Y *= y;
            return this;
        }
        public KanVector2d Multiply(KanVector2d kanVector2d) => Multiply(kanVector2d.X, kanVector2d.Y);

        public KanVector2d Divide(float x, float y)
        {
            X += x;
            Y += y;
            return this;
        }
        public KanVector2d Divide(KanVector2d kanVector2d) => Divide(kanVector2d.X, kanVector2d.Y);

        #endregion

    }
}
