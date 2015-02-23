using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiLib.CoreTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiLib.UserInterface
{
    public class TextBox : UIComponent
    {
        Texture2D pixel;
        SpriteFont font;
        public string text;
        int cursorPosition = 0;
        TimeSpan elaspedBlinkTime = new TimeSpan();
        TimeSpan blinkTime = TimeSpan.FromMilliseconds(500);
        bool blinkOn = false;
        Keys currentKey;
        Keys lastKey;
        TimeSpan elaspedKeyPressed = new TimeSpan();
        TimeSpan keyPressedTime = TimeSpan.FromMilliseconds(500);
        Keys[] currentKeys;
        Keys[] lastKeys;
        bool isCaps = false;
        bool isInsert = false;
        bool isMultiLine = false;
        int cursorEndPosition = 0;

        public bool isPassword = false;


        public TextBox(GraphicsDevice graphicsDevice, Rectangle bounds, SpriteFont font)
            : base(graphicsDevice, bounds)
        {
            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
            text = "";
            this.font = font;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsFocused)
            {
                lastKey = currentKey;
                currentKeys = InputManager.KeysPressed;
                if (currentKeys.Length > 0)
                {
                    if(currentKeys.Contains(Keys.CapsLock) && !lastKeys.Contains(Keys.CapsLock))
                    {
                        isCaps = !isCaps;
                    }
                    else if(currentKeys.Contains(Keys.Insert) && !lastKeys.Contains(Keys.Insert))
                    {
                        isInsert = !isInsert;
                    }
                    for (int i = 0; i < currentKeys.Length; i++)
                    {
                        if (!lastKeys.Contains(currentKeys[i]))
                        {
                            currentKey = currentKeys[i];
                            break;
                        }
                    }
                    bool pressed = false;
                    if (lastKey != currentKey)
                    {
                        keyPressedTime = TimeSpan.FromMilliseconds(500);
                        elaspedKeyPressed = new TimeSpan();
                        pressed = true;
                    }
                    else if (elaspedKeyPressed > keyPressedTime)
                    {
                        pressed = true;
                        keyPressedTime = TimeSpan.FromMilliseconds(75);
                        elaspedKeyPressed = new TimeSpan();
                    }
                    else
                    {
                        elaspedKeyPressed += gameTime.ElapsedGameTime;
                    }
                    if (pressed)
                    {
                        bool isShift = false;
                        if(currentKeys.Contains(Keys.RightShift) || currentKeys.Contains(Keys.LeftShift))
                        {
                            isCaps = !isCaps;
                            isShift = true;
                        }
                        switch (currentKey)
                        {
                            case Keys.A:
                            case Keys.B:
                            case Keys.C:
                            case Keys.D:
                            case Keys.E:
                            case Keys.F:
                            case Keys.G:
                            case Keys.H:
                            case Keys.I:
                            case Keys.J:
                            case Keys.K:
                            case Keys.L:
                            case Keys.M:
                            case Keys.N:
                            case Keys.O:
                            case Keys.P:
                            case Keys.Q:
                            case Keys.R:
                            case Keys.S:
                            case Keys.T:
                            case Keys.U:
                            case Keys.V:
                            case Keys.W:
                            case Keys.X:
                            case Keys.Y:
                            case Keys.Z:
                                text = text.Insert(cursorPosition, isCaps ? currentKey.ToString() : ((char)(currentKey.ToString()[0] + 32)).ToString());
                                cursorPosition++;
                                /*if (isInsert && text.Length > 0 && cursorPosition < text.Length)
                                {
                                    text = text.Remove(cursorPosition, 1);
                                }*///works
                                break;
                            case Keys.D0:
                                text = text.Insert(cursorPosition, isCaps ? ")" : "0");
                                cursorPosition++;
                                /*if (isInsert && text.Length > 0 && cursorPosition < text.Length)
                                {
                                    text = text.Remove(cursorPosition, 1);
                                }*///works
                                break;
                            case Keys.NumPad0:
                                text = text.Insert(cursorPosition, "0");
                                cursorPosition++;
                                break;
                            case Keys.D1:
                                text = text.Insert(cursorPosition, isCaps ? "!" : "1");
                                cursorPosition++;
                                break;
                            case Keys.NumPad1:
                                text = text.Insert(cursorPosition, "1");
                                cursorPosition++;
                                break;
                            case Keys.D2:
                                text = text.Insert(cursorPosition, isCaps ? "@" : "2");
                                cursorPosition++;
                                break;
                            case Keys.NumPad2:
                                text = text.Insert(cursorPosition, "2");
                                cursorPosition++;
                                break;
                            case Keys.D3:
                                text = text.Insert(cursorPosition, isCaps ? "#" : "3");
                                cursorPosition++;
                                break;
                            case Keys.NumPad3:
                                text = text.Insert(cursorPosition, "3");
                                cursorPosition++;
                                break;
                            case Keys.D4:
                                text = text.Insert(cursorPosition, isCaps ? "$" : "4");
                                cursorPosition++;
                                break;
                            case Keys.NumPad4:
                                text = text.Insert(cursorPosition, "4");
                                cursorPosition++;
                                break;
                            case Keys.D5:
                                text = text.Insert(cursorPosition, isCaps ? "%" : "5");
                                cursorPosition++;
                                break;
                            case Keys.NumPad5:
                                text = text.Insert(cursorPosition, "5");
                                cursorPosition++;
                                break;
                            case Keys.D6:
                                text = text.Insert(cursorPosition, isCaps ? "^" : "6");
                                cursorPosition++;
                                break;
                            case Keys.NumPad6:
                                text = text.Insert(cursorPosition, "6");
                                cursorPosition++;
                                break;
                            case Keys.D7:
                                text = text.Insert(cursorPosition, isCaps ? "&" : "7");
                                cursorPosition++;
                                break;
                            case Keys.NumPad7:
                                text = text.Insert(cursorPosition, "7");
                                cursorPosition++;
                                break;
                            case Keys.D8:
                                text = text.Insert(cursorPosition, isCaps ? "*" : "8");
                                cursorPosition++;
                                break;
                            case Keys.NumPad8:
                                text = text.Insert(cursorPosition, "8");
                                cursorPosition++;
                                break;
                            case Keys.Multiply:
                                text = text.Insert(cursorPosition, "*");
                                cursorPosition++;
                                break;
                            case Keys.D9:
                                text = text.Insert(cursorPosition, isCaps ? "(" : "9");
                                cursorPosition++;
                                break;
                            case Keys.NumPad9:
                                text = text.Insert(cursorPosition, "9");
                                cursorPosition++;
                                break;
                            case Keys.Add:
                                text = text.Insert(cursorPosition, "+");
                                cursorPosition++;
                                break;
                            case Keys.OemMinus:
                                text = text.Insert(cursorPosition, isCaps ? "_" : "-");
                                cursorPosition++;
                                break;
                            case Keys.Subtract:
                                text = text.Insert(cursorPosition, "-");
                                cursorPosition++;
                                break;
                            case Keys.OemPeriod:
                                text = text.Insert(cursorPosition, isCaps ? ">" : ".");
                                cursorPosition++;
                                break;
                            case Keys.Decimal:
                                text = text.Insert(cursorPosition, ".");
                                cursorPosition++;
                                break;
                            case Keys.Home:
                                cursorPosition = 0;
                                break;
                            case Keys.Insert:
                                //isInsert = true;
                                break;
                            case Keys.OemComma:
                                text = text.Insert(cursorPosition, isCaps ? "<" : ",");
                                cursorPosition++;
                                break;
                            case Keys.OemBackslash:
                                text = text.Insert(cursorPosition, isCaps ? "|" : @"\");
                                cursorPosition++;
                                break;
                            case Keys.OemPipe:
                                text = text.Insert(cursorPosition, isCaps ? "|" : @"\");
                                cursorPosition++;
                                break;
                            case Keys.Divide:
                                text = text.Insert(cursorPosition, @"\");
                                cursorPosition++;
                                break;
                            case Keys.OemCloseBrackets:
                                text = text.Insert(cursorPosition, isCaps ? "}" : "]");
                                cursorPosition++;
                                break;
                            case Keys.OemOpenBrackets:
                                text = text.Insert(cursorPosition, isCaps ? "{" : "[");
                                cursorPosition++;
                                break;
                            case Keys.OemPlus:
                                text = text.Insert(cursorPosition, isCaps ? "+" : "=");
                                cursorPosition++;
                                break;
                            case Keys.OemQuestion:
                                text = text.Insert(cursorPosition, isCaps ? "/" : "?");
                                cursorPosition++;
                                break;
                            case Keys.OemQuotes:
                                text = text.Insert(cursorPosition, isCaps ? ((char)34).ToString() : "'");
                                cursorPosition++;
                                break;
                            case Keys.OemSemicolon:
                                text = text.Insert(cursorPosition, isCaps ? ":" : ";");
                                cursorPosition++;
                                break;
                            case Keys.OemTilde:
                                text = text.Insert(cursorPosition, isCaps ? "~" : "`");
                                cursorPosition++;
                                break;
                            case Keys.Tab:
                                break;
                            case Keys.CapsLock:
                                //isShift = !isShift;
                                break;
                            case Keys.RightShift:
                                //isShift = !isShift;
                                break;
                            case Keys.LeftShift:
                                //isShift = !isShift;
                                break;
                            case Keys.Space:
                                text = text.Insert(cursorPosition, " ");
                                cursorPosition++;
                                break;
                            case Keys.Left:
                                if (cursorPosition > 0)
                                {
                                    cursorPosition--;
                                }
                                break;
                            case Keys.Right:
                                if (cursorPosition < text.Length)
                                {
                                    cursorPosition++;
                                }
                                break;
                            case Keys.Back:
                                if (text.Length > 0 && cursorPosition > 0)
                                {
                                    text = text.Remove(cursorPosition - 1, 1);
                                    cursorPosition--;
                                }
                                break;
                            case Keys.Delete:
                                if (text.Length > 0 && cursorPosition < text.Length)
                                {
                                    text = text.Remove(cursorPosition, 1);
                                }
                                break;
                            case Keys.End:
                                cursorPosition = text.Length;
                                break;
                            case Keys.Enter:
                                if (isMultiLine)
                                {
                                    text = text.Insert(cursorPosition, "\n");
                                    cursorPosition++;
                                }
                                break;
                            case Keys.Escape:
                                IsFocused = false;
                                break;
                            case Keys.Apps:
                                break;
                            case Keys.Attn:
                                break;
                            case Keys.BrowserBack:
                                break;
                            case Keys.BrowserFavorites:
                                break;
                            case Keys.BrowserForward:
                                break;
                            case Keys.BrowserHome:
                                break;
                            case Keys.BrowserRefresh:
                                break;
                            case Keys.BrowserSearch:
                                break;
                            case Keys.BrowserStop:
                                break;
                            case Keys.ChatPadGreen:
                                break;
                            case Keys.ChatPadOrange:
                                break;
                            case Keys.Crsel:
                                break;
                            case Keys.Down:
                                break;
                            case Keys.EraseEof:
                                break;
                            case Keys.Execute:
                                break;
                            case Keys.Exsel:
                                break;
                            case Keys.F1:
                                break;
                            case Keys.F10:
                                break;
                            case Keys.F11:
                                break;
                            case Keys.F12:
                                break;
                            case Keys.F13:
                                break;
                            case Keys.F14:
                                break;
                            case Keys.F15:
                                break;
                            case Keys.F16:
                                break;
                            case Keys.F17:
                                break;
                            case Keys.F18:
                                break;
                            case Keys.F19:
                                break;
                            case Keys.F2:
                                break;
                            case Keys.F20:
                                break;
                            case Keys.F21:
                                break;
                            case Keys.F22:
                                break;
                            case Keys.F23:
                                break;
                            case Keys.F24:
                                break;
                            case Keys.F3:
                                break;
                            case Keys.F4:
                                break;
                            case Keys.F5:
                                break;
                            case Keys.F6:
                                break;
                            case Keys.F7:
                                break;
                            case Keys.F8:
                                break;
                            case Keys.F9:
                                break;
                            case Keys.Help:
                                break;
                            case Keys.ImeConvert:
                                break;
                            case Keys.ImeNoConvert:
                                break;
                            case Keys.Kana:
                                break;
                            case Keys.Kanji:
                                break;
                            case Keys.LaunchApplication1:
                                break;
                            case Keys.LaunchApplication2:
                                break;
                            case Keys.LaunchMail:
                                break;
                            case Keys.LeftAlt:
                                break;
                            case Keys.LeftControl:
                                break;
                            case Keys.LeftWindows:
                                break;
                            case Keys.MediaNextTrack:
                                break;
                            case Keys.MediaPlayPause:
                                break;
                            case Keys.MediaPreviousTrack:
                                break;
                            case Keys.MediaStop:
                                break;
                            case Keys.None:
                                break;
                            case Keys.NumLock:
                                break;
                            case Keys.Oem8:
                                break;
                            case Keys.OemAuto:
                                break;
                            case Keys.OemClear:
                                break;
                            case Keys.OemCopy:
                                break;
                            case Keys.OemEnlW:
                                break;
                            case Keys.Pa1:
                                break;
                            case Keys.PageDown:
                                break;
                            case Keys.PageUp:
                                break;
                            case Keys.Pause:
                                break;
                            case Keys.Play:
                                break;
                            case Keys.Print:
                                break;
                            case Keys.PrintScreen:
                                break;
                            case Keys.ProcessKey:
                                break;
                            case Keys.RightAlt:
                                break;
                            case Keys.RightControl:
                                break;
                            case Keys.RightWindows:
                                break;
                            case Keys.Scroll:
                                break;
                            case Keys.Select:
                                break;
                            case Keys.SelectMedia:
                                break;
                            case Keys.Separator:
                                break;
                            case Keys.Sleep:
                                break;
                            case Keys.Up:
                                break;
                            case Keys.VolumeDown:
                                break;
                            case Keys.VolumeMute:
                                break;
                            case Keys.VolumeUp:
                                break;
                            case Keys.Zoom:
                                break;
                            default:
                                break;
                        }
                        if(isShift)
                        {
                            isCaps = !isCaps;
                        }
                    }
                }
                else
                {
                    elaspedKeyPressed = new TimeSpan();
                    currentKey = Keys.None;
                }
                elaspedBlinkTime += gameTime.ElapsedGameTime;
                if (elaspedBlinkTime >= blinkTime)
                {
                    elaspedBlinkTime = new TimeSpan();
                    blinkOn = !blinkOn;
                }
            }
            if (InputManager.IsLeftClicked())
            {
                if (InputManager.IsLeftClicked(bounds))
                {
                    IsFocused = true;
                }
                else
                {
                    IsFocused = false;
                    blinkOn = false;
                }
            }
            lastKeys = currentKeys;
            base.Update(gameTime);
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            pixel.GraphicsDevice.SetRenderTarget(renderTarget);

            pixel.GraphicsDevice.Clear(Color.Black);
            Vector2 cursorVect = new Vector2(4 + font.MeasureString(text.Substring(0, cursorPosition == 0 ? 0 : cursorPosition)).X, bounds.Height - font.MeasureString(text).Y - 4);

            string pass = "";
            for (int i = 0; i < text.Length; i++)
            {
                pass += "*";
            }
            if (isPassword)
            {
                cursorVect = new Vector2(4 + font.MeasureString(pass.Substring(0, cursorPosition == 0 ? 0 : cursorPosition)).X, bounds.Height - font.MeasureString(pass).Y - 4);
            }
            
            float offset = 0;
            if (cursorVect.X > bounds.Width)
            {
                offset = cursorVect.X - bounds.Width;
            } 
            spriteBatch.Begin();
            spriteBatch.Draw(pixel, new Rectangle(0,0,bounds.Width, bounds.Height), Color.Black);
            spriteBatch.Draw(pixel, new Rectangle(2, 2, bounds.Width - 4, bounds.Height - 4), Color.White);
            if (isPassword)
            {
                spriteBatch.DrawString(font, pass, new Vector2(4 - offset, cursorVect.Y), Color.Black);
            }
            else
            {
                spriteBatch.DrawString(font, isMultiLine ? text : text.Replace("\n", ""), new Vector2(4 - offset, cursorVect.Y), Color.Black);
            }
            if (blinkOn)
            {
                spriteBatch.DrawString(font, "|", cursorVect, Color.Black);
            }
            spriteBatch.End();

            pixel.GraphicsDevice.SetRenderTarget(null);
            base.Render(spriteBatch);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(renderTarget, new Vector2(bounds.X, bounds.Y), Color.White);
            base.Draw(spriteBatch);
        }
    }
}
