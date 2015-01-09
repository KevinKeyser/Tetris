using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public static class PieceFactory
    {
        public static Piece Straight()
        {
            return new Piece(new bool[5][] { new bool[] { false, false, false, false, false }, new bool[] { false, false, true, false, false }, new bool[] { false, false, true, false, false }, new bool[] { false, false, true, false, false }, new bool[] { false, false, true, false, false } });
        }
        public static Piece L()
        {
            return new Piece(new bool[5][] { new bool[] { false, false, false, false, false }, new bool[] { false, false, true, false, false }, new bool[] { false, false, true, false, false }, new bool[] { false, false, true, true, false }, new bool[] { false, false, false, false, false } });
        }

        public static Piece ReverseL()
        {
            return new Piece(new bool[5][] { new bool[] { false, false, false, false, false }, new bool[] { false, false, true, false, false }, new bool[] { false, false, true, false, false }, new bool[] { false, true, true, false, false }, new bool[] { false, false, false, false, false } });      
        }

        public static Piece Square()
        {
            return new Piece(new bool[5][] { new bool[] { false, false, false, false, false }, new bool[] { false, false, false, false, false }, new bool[] { false, false, true, true, false }, new bool[] { false, false, true, true, false }, new bool[] { false, false, false, false, false } }) { CanRotate = false };
        }

        public static Piece Z()
        {
            return new Piece(new bool[5][] { new bool[] { false, false, false, false, false }, new bool[] { false, false, false, false, false }, new bool[] { false, true, true, false, false }, new bool[] { false, false, true, true, false }, new bool[] { false, false, false, false, false } });
        }

        public static Piece ReverseZ()
        {
            return new Piece(new bool[5][] { new bool[] { false, false, false, false, false }, new bool[] { false, false, false, false, false }, new bool[] { false, false, true, true, false }, new bool[] { false, true, true, false, false }, new bool[] { false, false, false, false, false } });
        }

        public static Piece T()
        {
            return new Piece(new bool[5][] { new bool[] { false, false, false, false, false }, new bool[] { false, false, false, false, false }, new bool[] { false, true, true, true, false }, new bool[] { false, false, true, false, false }, new bool[] { false, false, false, false, false } });
        }
    }
}
