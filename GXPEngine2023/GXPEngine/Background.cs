using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class Background : Sprite
{

    public Background() : base("background.png",false,false)
    {

        SetOrigin(0, 0);

        width = game.width;
        height = game.height;

    }

    void Update()
    {

    }
}