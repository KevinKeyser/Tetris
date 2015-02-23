using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiLib.CoreTypes;
using MiLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiLib.UserInterface
{
    public class Window : UIComponent, IParent
    {
        public Color WindowColor;
        public Color ForegroundSelectionColor;
        public Color BackgroundSelectionColor;

        public Texture2D WindowTexture;
        public SpriteFont Font;

        public List<UIComponent> Items;

        public bool IsDraggable;

        public int SelectedIndex = 0;
        public float X
        {
            get
            {
                return position.X;
            }
            set
            {
                position.X = value;
            }
        }

        public float Y
        {
            get
            {
                return position.Y;
            }
            set
            {
                position.Y = value;
            }
        }

        public Window(GraphicsDevice graphicsDevice, Vector2 position, Vector2 size, SpriteFont font, Texture2D windowTexture)
            : base(graphicsDevice, position, size)
        {
            Items = new List<UIComponent>();
            Font = font;
            WindowTexture = windowTexture;
            WindowColor = Color.White;
            ForegroundSelectionColor = Color.White;
            BackgroundSelectionColor = Color.Blue;
            IsDraggable = true;
        }

        bool mouseDown = false;

        public override void Update(GameTime gameTime)
        {
            if (isVisible)
            {
                foreach (UIComponent component in Items)
                {
                    component.Parent = this;
                    component.Update(gameTime);
                }
                if(InputManager.IsLeftClicked(bounds))
                {
                    mouseDown = true;
                }
                if(InputManager.IsLeftUp())
                {
                    mouseDown = false;
                }
                if(IsDraggable && mouseDown)
                {
                    Position += InputManager.MouseDragAmount();
                }
                base.Update(gameTime);
            }
        }

        public override void Render(SpriteBatch spriteBatch)
        {

            if (IsVisible)
            {
                base.Render(spriteBatch);

                //render all componenets first
                foreach (UIComponent component in Items)
                {
                    component.Render(spriteBatch);
                }
                
                //drawing to the window's rendertarget
                renderTarget.GraphicsDevice.SetRenderTarget(renderTarget);
                spriteBatch.Begin();
                
                //draws the background
                spriteBatch.Draw(WindowTexture, new Rectangle(0,0, bounds.Width, bounds.Height), WindowColor);
                
                //draws all RENDERED component
                foreach(UIComponent component in Items)
                {
                    component.Draw(spriteBatch);
                }
                
                spriteBatch.End();
                renderTarget.GraphicsDevice.SetRenderTarget(null);
            }
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            if(isVisible)
            {
                spriteBatch.Draw(renderTarget, new Vector2(bounds.X, bounds.Y), Color.White);
            }
        }

    }
}
