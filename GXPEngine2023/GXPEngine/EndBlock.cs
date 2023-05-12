using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class EndBlock : GameObject 
{
    public EndBlock(float x, float y)
    {
        Sprite endBlock = new Sprite("triangle.png");
        endBlock.SetOrigin(endBlock.width / 2, endBlock.height / 2);
        AddChild(endBlock);

        this.x = x;
        this.y = y;
    }


}

