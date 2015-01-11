using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public static class PieceFactory
    {
        public static Piece Straight(Color color)
        {
            return new Piece(new Color?[5][] { new Color?[] { null, null, null, null, null }, new Color?[] { null, null, null, null, null }, new Color?[] { color, color, color, color, null }, new Color?[] { null, null, null, null, null }, new Color?[] { null, null, null, null, null } });
        }
        public static Piece L(Color color)
        {
            return new Piece(new Color?[5][] { new Color?[] { null, null, null, null, null }, new Color?[] { null, null, null, color, null }, new Color?[] { null, color, color, color, null }, new Color?[] { null, null, null, null, null }, new Color?[] { null, null, null, null, null } });
        }

        public static Piece ReverseL(Color color)
        {
            return new Piece(new Color?[5][] { new Color?[] { null, null, null, null, null }, new Color?[] { null, color, null, null, null }, new Color?[] { null, color, color, color, null }, new Color?[] { null, null, null, null, null }, new Color?[] { null, null, null, null, null } });
        }

        public static Piece Square(Color color)
        {
            //return new Piece(new Color?[5][] { new Color?[] { color, color, color, color, color }, new Color?[] { color, color, color, color, color }, new Color?[] { color, color, color, color, color }, new Color?[] { color, color, color, color, color }, new Color?[] { color, color, color, color, color } }) { CanRotate = null };
            return new Piece(new Color?[5][] { new Color?[] { null, null, null, null, null }, new Color?[] { null, null, color, color, null }, new Color?[] { null, null, color, color, null }, new Color?[] { null, null, null, null, null }, new Color?[] { null, null, null, null, null } }) { CanRotate = false };
        }

        public static Piece ReverseZ(Color color)
        {
            return new Piece(new Color?[5][] { new Color?[] { null, null, null, null, null }, new Color?[] { null, null, color, color, null }, new Color?[] { null, color, color, null, null }, new Color?[] { null, null, null, null, null }, new Color?[] { null, null, null, null, null } });
        }

        public static Piece Z(Color color)
        {
            return new Piece(new Color?[5][] { new Color?[] { null, null, null, null, null }, new Color?[] { null, color, color, null, null }, new Color?[] { null, null, color, color, null }, new Color?[] { null, null, null, null, null }, new Color?[] { null, null, null, null, null } });
        }

        public static Piece T(Color color)
        {
            return new Piece(new Color?[5][] { new Color?[] { null, null, null, null, null }, new Color?[] { null, null, color, null, null }, new Color?[] { null, color, color, color, null }, new Color?[] { null, null, null, null, null }, new Color?[] { null, null, null, null, null } });
        }
    }
}
