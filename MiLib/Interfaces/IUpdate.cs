﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiLib.Interfaces
{
    public interface IUpdate
    {
        bool isUpdating { get; set; }

        void Update(GameTime gameTime);
    }
}
