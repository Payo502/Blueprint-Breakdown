using System;
using System.Collections.Generic;
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

    void AddSprite()
    {
        endBlock = new Sprite("triangle.png");
        endBlock.SetOrigin(endBlock.width / 2, endBlock.height / 2);
        AddChild(endBlock);
    }
}

