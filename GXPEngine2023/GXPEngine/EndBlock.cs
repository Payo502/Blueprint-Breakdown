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
        AddSprite();
        alpha = 0;
    }
    void AddSprite()
    {
        endBlock = new Sprite("endBlock.png");
        scale = 0.4f;
        endBlock.SetOrigin(endBlock.width / 2, endBlock.height / 2);
        endBlock.Mirror(false, true);
        AddChild(endBlock);
        
    }
}

