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
        public Color?[][] blocks;

        public Piece(Color?[][] blocks)
        {
            this.blocks = blocks;
            Position = new Vector2(0, 0);
            CanRotate = true;
        }

        public void RotateLeft()
        {
            if (CanRotate)
            {
                Color?[][] tempblocks = new Color?[blocks.Length][];
                for (int i = 0; i < blocks.Length; i++)
                {
                    tempblocks[i] = new Color?[5];
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
                Color?[][] tempblocks = new Color?[blocks.Length][];
                for (int i = 0; i < blocks.Length; i++)
                {
                    tempblocks[i] = new Color?[5];
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

        public void Draw(SpriteBatch spriteBatch, Texture2D pixel)
        {
            Draw(spriteBatch, pixel, Vector2.Zero);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D pixel, Vector2 boardPosition)
        {
            for (int y = 0; y < blocks.Length; y++)
            {
                for (int x = 0; x < blocks[y].Length; x++)
                {
                    if(blocks[y][x].HasValue)
                    {
                        spriteBatch.Draw(pixel, new Rectangle((int)boardPosition.X + ((int)Position.X + x) * 35, (int)boardPosition.Y + ((int)Position.Y + y) * 35, 35, 35), Color.Black);
                        spriteBatch.Draw(pixel, new Rectangle((int)boardPosition.X + ((int)Position.X + x) * 35 + 1, (int)boardPosition.Y + ((int)Position.Y + y) * 35 + 1, 33, 33), blocks[y][x].Value);
                    }
                }
            }
        }
    }
}
