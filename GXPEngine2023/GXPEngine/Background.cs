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

        SetOrigin(width / 2, height / 2);


    }

    void Update()
    {

    }
}