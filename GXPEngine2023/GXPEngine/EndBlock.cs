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

    AnimationSprite rayLight;

    public EndBlock(int pRadius, Vec2 pPosition) : base(pRadius, pPosition)
    {
        AddSprite();
        alpha = 0;
        AddRay();
    }
    void AddSprite()
    {
        endBlock = new Sprite("endBlock.png");
        scale = 0.4f;
        endBlock.SetOrigin(endBlock.width / 2, endBlock.height / 2);
        endBlock.Mirror(false, true);
        AddChild(endBlock);

        
    }
    void AddRay()
    {
        rayLight = new AnimationSprite("animation_Glowing.png", 2, 2);
        rayLight.SetCycle(0, 1);
        rayLight.SetOrigin(rayLight.width/2 + 100, rayLight.height / 2 -100);
        rayLight.rotation =45;
        AddChild(rayLight);
    }
    void AnimateRay()
    {
        rayLight.SetCycle(0, 4);
        rayLight.Animate(0.1f);
    }
    void Update()
    {
        AnimateRay();
    }

}

