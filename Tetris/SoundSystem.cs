using FMOD;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public class SoundSystem
    {
        FMOD.Studio.System soundSystem;
        FMOD.Studio.Bank soundBank;
        FMOD.Studio.Bank stringBank;
        FMOD.Studio.EventDescription[] events;
        FMOD.Studio.EventInstance TetrisTitleEvent;
        RESULT result;

        public SoundSystem()
        {
            FMOD.Studio.System.create(out soundSystem);
            soundSystem.initialize(1, FMOD.Studio.INITFLAGS.NORMAL, INITFLAGS.NORMAL, System.IntPtr.Zero);
            result = soundSystem.loadBankFile(Environment.CurrentDirectory + @"\Content\Master Bank.bank", FMOD.Studio.LOAD_BANK_FLAGS.NORMAL, out soundBank);
            result = soundSystem.loadBankFile(Environment.CurrentDirectory + @"\Content\Master Bank.strings.bank", FMOD.Studio.LOAD_BANK_FLAGS.NORMAL, out stringBank);
            result = soundBank.getEventList(out events);
            events[1].createInstance(out TetrisTitleEvent);
            TetrisTitleEvent.setParameterValue("Level", 1);
            TetrisTitleEvent.setVolume(1);
            TetrisTitleEvent.start();
        }

        public void Update(GameTime gameTime)
        {
            soundSystem.update();
        }

        public void UnloadContent()
        {

            TetrisTitleEvent.release();
            for (int i = 0; i < events.Length; i++)
            {
                events[i].releaseAllInstances();
            }
            stringBank.unload();
            soundBank.unload();
            soundSystem.release();
        }
    }
}
