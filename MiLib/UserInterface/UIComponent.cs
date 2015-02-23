using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MiLib.UserInterface
{
    public class UIComponent : IFocusable
    {
        private bool isFocused;
        public bool IsFocused
        {
            get
            {
                return isFocused;
            }
            set
            {
                isFocused = value;
            }
        }

        protected RenderTarget2D renderTarget;

        protected IParent parent;

        public virtual IParent Parent
        {
            get { return parent; }
            set 
            {
                parent = value;
                bounds.X = (int)position.X + (parent != null ? (int)parent.X : 0);
                bounds.Y = (int)position.Y + (parent != null ? (int)parent.Y : 0);
            }
        }
        
        protected bool isVisible;

        public virtual bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        protected bool isEnabled;

        public virtual bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; }
        }
        
        protected Rectangle bounds;
        public virtual Rectangle Bounds
        {
            get
            {
                bounds.X = (int)position.X + (parent != null ? (int)parent.X : 0);
                bounds.Y = (int)position.Y + (parent != null ? (int)parent.Y : 0);
                return bounds;
            }
            set
            {
                bounds = Bounds;
                position.X = bounds.X;
                position.Y = bounds.Y;
                size.X = bounds.Width;
                size.Y = bounds.Height;
            }
        }

        protected Vector2 position;
        public virtual Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                bounds.X = (int)position.X + (parent != null ? (int)parent.X : 0);
                bounds.Y = (int)position.Y + (parent != null ? (int)parent.Y : 0);
            }
        }

        protected Vector2 size;

        public virtual Vector2 Size
        {
            get 
            { 
                return size; 
            }
            set
            {
                if(value.X >= 0 && value.Y >= 0)
                {
                    size = value;
                    bounds.Width = (int)size.X;
                    bounds.Height = (int)size.Y;
                    renderTarget = new RenderTarget2D(renderTarget.GraphicsDevice, bounds.Width > 0 ? bounds.Width : 1, bounds.Height > 0 ? bounds.Height : 1);
                }
                else
                {
                    throw new ArgumentException("UIComponent - Size must be positive");
                }
            }
        }

        public UIComponent(GraphicsDevice graphicsDevice, Vector2 position, Vector2 size)
            : this(graphicsDevice, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y)) { }

        public UIComponent(GraphicsDevice graphicsDevice, Rectangle bounds)
        {
            this.bounds = bounds;
            position = new Vector2(bounds.X, bounds.Y);
            size = new Vector2(bounds.Width, bounds.Height);
            parent = null;
            isEnabled = true;
            isVisible = true;
            renderTarget = new RenderTarget2D(graphicsDevice, bounds.Width > 0 ? bounds.Width : 1, bounds.Height > 0 ? bounds.Height : 1);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Render(SpriteBatch spriteBatch)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
