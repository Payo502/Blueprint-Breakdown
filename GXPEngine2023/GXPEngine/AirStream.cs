using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class AirStream : AnimationSprite
{
    Vec2 airStrength;
    float strength;
    MyGame myGame;
    Vec2 position;

    public AirStream(Vec2 pPosition, Vec2 pScale, float pStrength) : base("wind.png", 2, 4, -1, false, true)
    {


        SetOrigin(width / 2, height);
        width = (int)pScale.x;
        height = (int)pScale.y;
        position = pPosition;
        UpdateScreenPosition();
        SetCycle(0, 8);

        strength = pStrength;

        myGame = (MyGame)MyGame.main;
    }

    void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
        SetXY(x, y);
    }

    public void SetRotation(float angle)
    {
        //position.SetAngleRadians(angle);
        rotation = angle;
    }

    void Update()
    {
        for (int i = 0; i < myGame.GetNumberOfMovers(); i++)
        {
            if (HitTest(myGame.GetMover(i)))
            {
                CalculateAirStrength(position.Normal().GetAngleRadians());
                myGame.GetMover(i).velocity += airStrength;
            }
        }

        Animate(0.25f);
    }

    public void CalculateAirStrength(float ownerRotation)
    {
        airStrength = new Vec2(0, -strength / 3);
        airStrength.RotateDegrees(ownerRotation);
    }
}

