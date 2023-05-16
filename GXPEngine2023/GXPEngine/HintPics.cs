using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Hints : Sprite
{

    public Hints(string filename, float pX, float pY, int pWidth, int pHeight) : base(filename, false, false)
    {
        x = pX;
        y = pY;
        width = pWidth;
        height = pHeight;
        SetOrigin(width / 2, height / 2);
    }
}