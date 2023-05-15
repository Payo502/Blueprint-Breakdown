using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using Physics;

public class EndBlock : CircleBase
{
    Sprite endBlock;

    public EndBlock(int pRadius, Vec2 pPosition) : base(pRadius, pPosition)
    {
        Draw(230, 200, 0);


        //AddSprite();
    }
    /*protected override void Draw(byte red, byte green, byte blue)
    {
        Clear(Color.Empty);
        *//*if (isMoving)
        {
            Fill(red, green, blue);
        }
        else
        {
            red = 255;
            green = 255;
            blue = 255;
            Fill(red, green, blue, 0);
        }

        Stroke(red, green, blue);
        Ellipse(radius, radius, 2 * radius, 2 * radius);*//*
    }*/
    void AddSprite()
    {
        endBlock = new Sprite("triangle.png");
        endBlock.SetOrigin(endBlock.width / 2, endBlock.height / 2);
        AddChild(endBlock);
    }
}

