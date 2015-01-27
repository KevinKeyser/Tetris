using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiLib.CoreTypes
{
    public abstract class Screen
    {
        protected List<ISprite> sprites = new List<ISprite>();

        public virtual List<ISprite> Sprites
        {
            get
            {
                return sprites;
            }
            set
            {
                sprites = value;
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach(ISprite sprite in sprites)
            {
                sprite.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach(ISprite sprite in sprites)
            {
                sprite.Draw(spriteBatch);
            }
        }

        public virtual void UnloadContent()
        {

        }
    }
}
