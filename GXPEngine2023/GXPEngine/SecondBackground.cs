﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class SecondBackground : Sprite
{

    public SecondBackground(string filename) : base(filename, false, false)
    {

        SetOrigin(0, 0);

        width = game.width;
        height = game.height;

        width = game.width;
        height = game.height;
    }

    void Update()
    {

    }
}
