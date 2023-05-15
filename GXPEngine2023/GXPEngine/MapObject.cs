using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GXPEngine;
using Physics;

public class MapObject : CircleBase
{
    AnimationSprite ballSprite;

    private float lastCollisionTime = 0;
    private bool levelComplete = false;

    private float delayAfterEndBlock = 3000f;

    private Claw claw;
    public MapObject(int pRadius, Vec2 pPosition, Vec2 pVelocity = new Vec2(), bool moving = true) : base(pRadius, pPosition)
    {
        velocity = pVelocity;
        isMoving = true;
        Draw(230, 200, 0);
        _density = 0.9f;      

        AddSprite();

    }

    protected virtual void AddSprite()
    {
        ballSprite = new AnimationSprite("ball.png", 4, 2);
        ballSprite.SetOrigin(ballSprite.width / 2, ballSprite.height / 2);
        ballSprite.scale = 0.6f;
        AddChild(ballSprite);
    }

    void AnimateBall()
    {
        ballSprite.SetCycle(0, 8);
        ballSprite.Animate(0.25f);
    }


    protected override void Move()
    {
        if (!enablePhysics) return;

        bool repeat = true;
        int iteration = 0;
        while (repeat && iteration < 2)
        {
            repeat = false;

            oldPosition = position;
            position += velocity;
            CollisionInfo colInfo = engine.MoveUntilCollision(myCollider, velocity);
            if (colInfo != null)
            {
                if (colInfo.timeOfImpact < 0.01f) repeat = true;
                ResolveCollisions(colInfo);
            }
            iteration++;
        }

        base.Move();
        UpdateScreenPosition();
    }

    protected override void Draw(byte red, byte green, byte blue)
    {
        Clear(Color.Empty);
        /*if (isMoving)
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
        Ellipse(radius, radius, 2 * radius, 2 * radius);*/
    }

    public void ApplyForce(Vec2 force)
    {
        Vec2 acceleration = force / Mass;

        velocity += acceleration;
    }

    protected virtual void ResolveCollisions(CollisionInfo pCol)
    {
        //Console.WriteLine("ResolveCollisions Called");
        if (pCol.other.owner is Line)
        {

            Line segment = (Line)pCol.other.owner;
            if (segment.isRotating)
            {
                Vec2 tempVelocity = pCol.normal * 6;
                velocity -= tempVelocity;
                velocity.Reflect(bounciness, pCol.normal);
                velocity += tempVelocity;

                return;
            }
            else
            {
                velocity.Reflect(bounciness, pCol.normal);
                return;
            }

        }

        if (pCol.other.owner is BouncingPad)
        {
            BouncingPad bouncePad = (BouncingPad)pCol.other.owner;
            float bounceForce = bouncePad.GetBounceForce();
            bouncePad.hasBounced = true;
            velocity.Reflect(_bounciness, pCol.normal);
            velocity += pCol.normal * bounceForce;            
            return;
        }
        if (pCol.other.owner is EndBlock)
        {
            //Console.WriteLine("EndBlock collision detected");
            lastCollisionTime = Time.time - lastCollisionTime;
            levelComplete = true;

            
            if (claw == null)
            {
                claw = game.FindObjectOfType<Claw>();
            }
            claw.MoveUpward();
            
        }


        if (pCol.other.owner is Player)
        {
            NewtonLawsBalls((MapObject)pCol.other.owner, pCol);
        }
        if (pCol.other.owner is MapObject)
        {
            if (((MapObject)pCol.other.owner).isMoving)
            {
                NewtonLawsBalls((MapObject)pCol.other.owner, pCol);
            }
            else
            {
                velocity.Reflect(bounciness, pCol.normal);
            }

        }

    }

    void MoveToNextLevel()
    {
        //Console.WriteLine($"Time: {Time.time}, LastCollisionTime: {lastCollisionTime}, Delay: {delayAfterEndBlock}");
        if (levelComplete && Time.time  > lastCollisionTime + delayAfterEndBlock)
        {
            Console.WriteLine("Loading level...");
            lastCollisionTime = 0;
            levelComplete = false;

            ((MyGame)game).LoadNextLevel();
        }
    }

    protected override void Update()
    {
        base.Update();
        AnimateBall();

        MoveToNextLevel();
    }
}

