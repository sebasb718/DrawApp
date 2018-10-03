using System;
using System.Collections.Generic;
using Main;
class DrawCommandProcessor
    {
        public static bool CanvasExist
        {
            get
            {
                if (CanvasArea != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static Canvas CanvasArea { get; set; }
        public static void Execute(Command CommandReceived)
        {
            try
            {
                if (CommandReceived.CommandIdentifier == 'C' && !CanvasExist)
                {
                    CanvasArea = new Canvas(CommandReceived.CommandParameters);
                }
                else if (CommandReceived.CommandIdentifier == 'C' && CanvasExist)
                {
                    throw new Exception("Canvas already created, use another command");
                }
                else if (CommandReceived.CommandIdentifier != 'C' && !CanvasExist)
                {
                    throw new Exception("No canvas for draw, create one first");
                }
                else if (CommandReceived.CommandIdentifier != 'C' && CanvasExist)
                {
                    ObjectForCanvas oObjectForCanvas = ObjectForCanvasFactory.CreateInstance(CommandReceived);
                    oObjectForCanvas.Insert(CanvasArea);
                    CanvasArea.RefreshDraw();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
