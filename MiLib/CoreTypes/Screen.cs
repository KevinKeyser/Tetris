using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiLib.Interfaces;
using MiLib.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiLib.CoreTypes
{
    public abstract class Screen
    {
        protected List<ISprite> sprites = new List<ISprite>();
        protected List<UIComponent> uiComponents = new List<UIComponent>();
        public Color DimColor;
        public bool Enabled;
        protected Texture2D pixel;


        public bool UIFocused { get; protected set; }

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

        public virtual List<UIComponent> UIComponents
        {
            get
            {
                return uiComponents;
            }
            set
            {
                uiComponents = value;
            }
        }

        public Screen()
        {
            pixel = new Texture2D(GraphicsManager.GraphicsDeviceManager.GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
            Enabled = true;
            DimColor = Color.Transparent;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                foreach (ISprite sprite in sprites)
                {
                    sprite.Update(gameTime);
                }
                foreach (UIComponent uicomponent in uiComponents)
                {
                    uicomponent.Update(gameTime);
                    if (uiComponents is IFocusable && ((IFocusable)uiComponents).IsFocused)
                    {
                        UIFocused = true;
                    }
                }
            }
        }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            foreach (UIComponent uicomponent in uiComponents)
            {
                uicomponent.Render(spriteBatch);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach(ISprite sprite in sprites)
            {
                sprite.Draw(spriteBatch);
            }
            foreach (UIComponent uicomponent in uiComponents)
            {
                uicomponent.Draw(spriteBatch);
            }
            if(!Enabled)
            {
                spriteBatch.Draw(pixel, new Rectangle(0, 0, GraphicsManager.ScreenWidth, GraphicsManager.ScreenHeight), DimColor);
            }
        }

        public virtual void UnloadContent()
        {

        }
    }
}
