using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using Physics;

public class Claw : GameObject
{
    private MapObject heldball;
    public bool holdingBall;
    private float speed = 5f;
    private Sprite clawSprite;
    


    public Claw(MapObject ball) : base()
    {
        heldball = ball;
        holdingBall = true;
        clawSprite = new Sprite("square.png");

        AddChild(clawSprite);
    }

    private void Update()
    {
        if (holdingBall)
        {
            heldball.SetXY(x, y);
        }

        if (Input.GetKey(Key.SPACE))
        {
            holdingBall = false;
            heldball.isMoving = true;
        }

        if (Input.GetKey(Key.LEFT))
        {
            x -= speed;
        }

        if (Input.GetKey(Key.RIGHT))
        {
            x += speed;
        }

        if (Input.GetKey(Key.UP))
        {
            y -= speed;
        }

        if (Input.GetKey(Key.DOWN))
        {
            y += speed;
        }
    }

}

