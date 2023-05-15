using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Wall : Sprite
{
    Vec2 position;
    public Wall(string filename, float pX, float pY, int pWidth, int pHeight) : base(filename, false, false)
    {
        SetOrigin(0, 0);
        width = pWidth;
        height = pHeight;
        x = pX;
        y = pY;
    }

    void Update()
    {

    }
}