using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{   
    public class Piece
    {
        public Vector2 Position;
        public bool CanRotate;
        public bool[][] blocks;
        public Piece(bool[][] blocks)
        {
            this.blocks = blocks;
            Position = new Vector2(0, 0);
            CanRotate = true;
        }

        public void RotateLeft()
        {
            if (CanRotate)
            {
                bool[][] tempblocks = new bool[blocks.Length][];
                for (int i = 0; i < blocks.Length; i++)
                {
                    tempblocks[i] = new bool[5];
                }
                for (int y = 0; y < blocks.Length; y++)
                {
                    for (int x = 0; x < blocks[y].Length; x++)
                    {
                        tempblocks[x][blocks.Length - 1 - y] = blocks[y][x];
                    }
                }
                blocks = tempblocks;
            }
        }

        public void RotateRight()
        {
            if (CanRotate)
            {
                bool[][] tempblocks = new bool[blocks.Length][];
                for (int i = 0; i < blocks.Length; i++)
                {
                    tempblocks[i] = new bool[5];
                }
                for (int y = 0; y < blocks.Length; y++)
                {
                    for (int x = 0; x < blocks[y].Length; x++)
                    {
                        tempblocks[blocks.Length - x - 1][y] = blocks[y][x];
                    }
                }
                blocks = tempblocks;
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            for(int y = 0; y < blocks.Length; y++)
            {
                for(int x = 0; x < blocks[y].Length; x++)
                {
                    string text = blocks[y][x] == true ? " 1 " : " 0 ";
                    Color color = Color.Black;
                    if(text == " 1 ")
                    {
                        color = Color.White;
                        spriteBatch.DrawString(font, text, Position * font.MeasureString(text) + new Vector2(x * font.MeasureString(text).X, y * font.MeasureString(text).Y), color);
                    }
                }
            }
        }
    }
}
