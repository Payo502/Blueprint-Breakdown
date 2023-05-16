using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

internal class Arrow:Sprite
{
    Vec2 position;
    public Arrow(string filename, Vec2 pPosition) : base(filename,false,false)
    {
        position = pPosition;
        SetOrigin(width / 2, height / 2);

        UpdateScreen();
    }

    void UpdateScreen()
    {
        x = position.x;
        y = position.y;
    }

}

