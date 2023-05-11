using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class AirStream : Sprite
{
    Vec2 airStrength;
    float strength;
    MyGame myGame;
    Vec2 position;

    public AirStream(Vec2 pPosition, Vec2 pScale, float pStrength) : base("square.png", false, true)
    {
        alpha = 0.5f;

        SetOrigin(width / 2, height / 2);
        width = (int)pScale.x;
        height = (int)pScale.y;
        position = pPosition;
        UpdateScreenPosition();

        strength = pStrength;

        myGame = (MyGame)MyGame.main;
    }

    void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
        SetXY(x, y);
    }

    void Update()
    {
        for (int i = 0; i < myGame.GetNumberOfMovers(); i++)
        {
            if(HitTest(myGame.GetMover(i)))
            {
                CalculateAirStrength(position.Normal().GetAngleRadians());
                myGame.GetMover(i).velocity += airStrength;
            }
        }
    }

    public void CalculateAirStrength(float ownerRotation)
    {
        airStrength = new Vec2(0, -strength/3);
        airStrength.RotateDegrees(ownerRotation);
    }
}

