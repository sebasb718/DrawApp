using System;
using System.Collections.Generic;
using Main;

class Canvas
{
    public int Width { get; set; }
    public int Height { get; set; }
    object[] DrawParams { get; set; }
    int ArrayHeight { get; set; }
    int ArrayWidth { get; set; }
    public char[,] CanvasArray { get; set; }
    public void ValidateCanvasSpace(int xPoint, int yPoint)
    {
        try
        {
            if (xPoint > Width || yPoint > Height)
            {
                throw new Exception("Coordinates for start or end are outside the canvas");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public Canvas(object[] CanvasParams)
    {
        DrawParams = CanvasParams;
        Width = Convert.ToInt32(DrawParams[0]);
        Height = Convert.ToInt32(DrawParams[1]);
        CanvasArray = new char[Height + 2, Width + 2];
        ArrayHeight = CanvasArray.GetLength(0);
        ArrayWidth = CanvasArray.GetLength(1);
        for (int i = 0; i < ArrayWidth; i++) { CanvasArray[0, i] = '-'; CanvasArray[ArrayHeight - 1, i] = '-'; }
        for (int i = 1; i < ArrayHeight - 1; i++) { CanvasArray[i, 0] = '|'; CanvasArray[i, ArrayWidth - 1] = '|'; }
        RefreshDraw();
    }
    public void RefreshDraw()
    {
        try
        {
            for (int y = 0; y < ArrayHeight; y++)
            {
                for (int x = 0; x < ArrayWidth; x++)
                {
                    if (CanvasArray[y, x] == char.MinValue)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write(CanvasArray[y, x]);
                    }
                }
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
class ObjectForCanvasFactory
{
    public static ObjectForCanvas CreateInstance(Command CommandToExecute)
    {
        ObjectForCanvas oObjectObtained;
        switch (CommandToExecute.CommandIdentifier)
        {
            case 'L':
                oObjectObtained = new Line(CommandToExecute.CommandParameters);
                break;
            case 'R':
                oObjectObtained = new Rectangle(CommandToExecute.CommandParameters);
                break;
            case 'B':
                oObjectObtained = new Bucket(CommandToExecute.CommandParameters);
                break;
            default:
                oObjectObtained = null;
                break;
        }
        return oObjectObtained;
    }
}
abstract class ObjectForCanvas
{
    public abstract void Insert(Canvas CanvasArea);
}
class Line : ObjectForCanvas
{
    int X1 { get; set; }
    int Y1 { get; set; }
    int X2 { get; set; }
    int Y2 { get; set; }
    public Line(object[] LineParams)
    {
        try
        {
            X1 = Convert.ToInt32(LineParams[0]);
            Y1 = Convert.ToInt32(LineParams[1]);
            X2 = Convert.ToInt32(LineParams[2]);
            Y2 = Convert.ToInt32(LineParams[3]);
            if (X1 != X2 && Y1 != Y2)
            {
                throw new Exception("Diagonal lines not allowed (Parameters should be X1=X2 or Y1=Y2)");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public override void Insert(Canvas CanvasArea)
    {
        try
        {
            CanvasArea.ValidateCanvasSpace(X1, Y1);
            CanvasArea.ValidateCanvasSpace(X2, Y2);
            for (int x = X1; x <= X2; x++)
            {
                for (int y = Y1; y <= Y2; y++)
                {
                    CanvasArea.CanvasArray[y, x] = 'x';
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
class Rectangle : ObjectForCanvas
{
    int X1 { get; set; }
    int Y1 { get; set; }
    int X2 { get; set; }
    int Y2 { get; set; }
    public Rectangle(object[] RectangleParams)
    {
        try
        {
            X1 = Convert.ToInt32(RectangleParams[0]);
            Y1 = Convert.ToInt32(RectangleParams[1]);
            X2 = Convert.ToInt32(RectangleParams[2]);
            Y2 = Convert.ToInt32(RectangleParams[3]);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public override void Insert(Canvas CanvasArea)
    {
        try
        {
            CanvasArea.ValidateCanvasSpace(X1, Y1);
            CanvasArea.ValidateCanvasSpace(X2, Y2);
            for (int x = X1; x <= X2; x++)
            {
                CanvasArea.CanvasArray[Y1, x] = 'x';
                CanvasArea.CanvasArray[Y2, x] = 'x';
            }
            for (int y = Y1; y <= Y2; y++)
            {
                CanvasArea.CanvasArray[y, X1] = 'x';
                CanvasArea.CanvasArray[y, X2] = 'x';
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
class Bucket : ObjectForCanvas
{
    int X { get; set; }
    int Y { get; set; }
    char color { get; set; }
    public Bucket(object[] BucketParams)
    {
        X = Convert.ToInt32(BucketParams[0]);
        Y = Convert.ToInt32(BucketParams[1]);
        color = Convert.ToChar(BucketParams[2]);
    }
    public override void Insert(Canvas CanvasArea)
    {
        try
        {
            CanvasArea.ValidateCanvasSpace(X, Y);
            FillSpace(X, Y, color, CanvasArea);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    void FillSpace(int x, int y, char color, Canvas CanvasArea)
    {
        if (x < 0 || y < 0 || x > CanvasArea.Width || y > CanvasArea.Height)
        {
            return;
        }
        if (CanvasArea.CanvasArray[y, x] != char.MinValue)
        {
            return;
        }
        CanvasArea.CanvasArray[y, x] = color;
        FillSpace(x + 1, y, color, CanvasArea);
        FillSpace(x, y + 1, color, CanvasArea);
        FillSpace(x - 1, y, color, CanvasArea);
        FillSpace(x, y - 1, color, CanvasArea);
    }
}