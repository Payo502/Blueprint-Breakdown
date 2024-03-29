﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class AirStream : AnimationSprite
{
    float strength;
    MyGame myGame;
    Vec2 position;


    public AirStream(Vec2 pPosition, Vec2 pScale, float pStrength) : base("wind.png", 2, 2, -1, false, true)
    {
        alpha = 0.5f;

        SetOrigin(width / 2, height);
        width = (int)pScale.x;
        height = (int)pScale.y;
        position = pPosition;
        UpdateScreenPosition();
        SetCycle(0, 9);
        strength = pStrength;

        myGame = (MyGame)MyGame.main;
    }
    public void SetRotation(float angle)
    {
        rotation = angle;
    }

    void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
        SetXY(x, y);
    }

    public void Push()
    {
        if (!visible)
        {
            return;
        }
        Vec2 airStrength = new Vec2(0, -strength);
        airStrength.RotateDegrees(rotation);

        for (int i = 0; i < myGame.GetNumberOfMovers(); i++)
        {
            if (HitTest(myGame.GetMover(i)))
            {
                myGame.GetMover(i).velocity += airStrength;
            }
        }
    }

    void Update()
    {
        Animate(0.1f);
    }
}

