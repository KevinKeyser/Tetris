﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiLib.CoreTypes
{
    public static class GraphicsManager
    {
        public static GraphicsDeviceManager GraphicsDeviceManager;

        public static int ScreenWidth
        {
            get
            {
                return GraphicsDeviceManager.GraphicsDevice.Viewport.Width;
            }
            set
            {
                GraphicsDeviceManager.PreferredBackBufferWidth = value;
                GraphicsDeviceManager.ApplyChanges();
            }
        }

        public static int ScreenHeight
        {
            get
            {
                return GraphicsDeviceManager.GraphicsDevice.Viewport.Height;
            }
            set
            {
                GraphicsDeviceManager.PreferredBackBufferHeight = value;
                GraphicsDeviceManager.ApplyChanges();
            }
        }

        public static bool isFullScreen
        {
            get
            {
                return GraphicsDeviceManager.IsFullScreen;
            }
            set
            {
                GraphicsDeviceManager.IsFullScreen = value;
                GraphicsDeviceManager.ApplyChanges();
            }
        }

        public static void Init(GraphicsDeviceManager graphicsDeviceManager)
        {
            GraphicsDeviceManager = graphicsDeviceManager;
        }
    }
}
