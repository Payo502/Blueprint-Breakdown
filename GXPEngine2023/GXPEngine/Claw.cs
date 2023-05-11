﻿using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Managers;
using Physics;

public class Claw : GameObject
{
    MyGame myGame;

    Vec2 position;
    Vec2 velocity;
    AnimationSprite clawSprite;
    float speed = 5f;

    public float Mass;

    public bool hasBall;

    private float minX, maxX;
    private float minY, maxY;

    public Claw(Vec2 pPosition) : base()
    {
        position = pPosition;
        clawSprite = new AnimationSprite("claw.png",1,2);
        clawSprite.SetOrigin(clawSprite.width / 2, clawSprite.height / 2 + 30);
        clawSprite.SetCycle(1,1);
        clawSprite.scale = 0.4f;
        AddChild(clawSprite);

        Mass = 10f;

        minX = position.x - 100;
        maxX = position.x + 100;
        minY = position.y - 50;
        maxY = position.y + 50;

        UpdateScreen();

        
        
    }

    void UpdateScreen()
    {
        x = position.x;
        y = position.y;
    }

    void HandleInput()
    {
        Vec2 oldPosition = new Vec2(x, y);

        float newX = x;
        float newY = y;

        if (Input.GetKey(Key.RIGHT))
        {
            newX += speed;
        }
        else if (Input.GetKey(Key.LEFT))
        {
            newX -= speed;
        }
        else if (Input.GetKey(Key.UP))
        {
            newY -= speed;
            Console.WriteLine("going up");
            Console.WriteLine(newY);
        }
        else if (Input.GetKey(Key.DOWN))
        {
            newY += speed;
        }

        if (newX >= minX && newX <= maxX)
        {
            x = newX;
        }
        if (newY >= minY && newY <= maxY)
        {
            y = newY;
        }

        Vec2 newPosition = new Vec2(x, y);
        Vec2 clawVelocity = newPosition - oldPosition;

        if (Input.GetKeyDown(Key.SPACE) && !hasBall)
        {

            if (myGame == null)
            {
                myGame = game.FindObjectOfType<MyGame>();
            }
            MapObject ball1 = new MapObject(30, new Vec2(x, y + 120), clawVelocity);
            parent.AddChild(ball1);
            myGame.movers.Add(ball1);
            
            hasBall = true;

            
            /*Rope rope = new Rope(20, ball1);
            parent.AddChild(rope);*/

            float v1_final = ((Mass - ball1.Mass) / (Mass + ball1.Mass)) * clawVelocity.x + ((2 * ball1.Mass) / (Mass + ball1.Mass)) * ball1.velocity.x;
            float v2_final = ((2 * Mass) / (Mass + ball1.Mass)) * clawVelocity.x - ((Mass - ball1.Mass) / (Mass + ball1.Mass)) * ball1.velocity.x;

            velocity.x = v1_final;
            ball1.velocity.x = v2_final;

            AnimateClawOpen();
        }
    }


    void AnimateClawOpen()
    {
        clawSprite.SetCycle(0, 1);
        clawSprite.Animate(0.5f);
    }

    void Update()
    {
        HandleInput();
    }
}

