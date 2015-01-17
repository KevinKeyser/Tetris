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
        public int ID;

        public Piece(Color?[][] blocks, int id)
        {
            this.blocks = blocks;
            Position = new Vector2(0, 0);
            CanRotate = true;
            ID = id;
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
                        for (int cross = 0; cross < 35; cross++)
                        {
                            spriteBatch.Draw(pixel, new Vector2(boardPosition.X + (Position.X + x) * 35 + cross, boardPosition.Y + (Position.Y + y) * 35 + cross), Color.Black);
                            spriteBatch.Draw(pixel, new Vector2(boardPosition.X + (Position.X + x) * 35 + 35 - cross, boardPosition.Y + (Position.Y + y) * 35 + cross), Color.Black);
                        }
                        spriteBatch.Draw(pixel, new Rectangle((int)boardPosition.X + ((int)Position.X + x) * 35 + 8, (int)boardPosition.Y + ((int)Position.Y + y) * 35 +8, 20, 20), Color.Black);
                        spriteBatch.Draw(pixel, new Rectangle((int)boardPosition.X + ((int)Position.X + x) * 35 + 9, (int)boardPosition.Y + ((int)Position.Y + y) * 35 + 9, 18, 18), blocks[y][x].Value);
                    }
                }
            }
        }
    }
}
