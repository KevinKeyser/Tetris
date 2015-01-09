using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiLib.CoreTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class Block : Sprite
    {
        public Block (Texture2D texture, Vector2 position, Color color)
            : base(texture, position, color)
        {

        }
    }
}
