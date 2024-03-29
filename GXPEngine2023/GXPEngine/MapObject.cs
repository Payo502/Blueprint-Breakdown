﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
    private float lineCollisionTime = 0;
    private bool levelComplete = false;
    private bool levelReset = false;

    private float delayAfterEndBlock = 3000f;
    private float delayAfterLine = 1000;

    bool lineBounce = false;
    bool hasPlayed = false;

    Vec2 normal;

    private Claw claw;

    Sound endBlockSound;
    SoundChannel endBlockSoundChannel;

    Sound deathSound;
    SoundChannel deathSoundChannel;


    public MapObject(int pRadius, Vec2 pPosition, Vec2 pVelocity = new Vec2(), bool moving = true) : base(pRadius, pPosition)
    {
        velocity = pVelocity;
        isMoving = true;
        Draw(230, 200, 0);
        _density = 0.9f;      

        AddSprite();

        endBlockSound = new Sound("sucess.mp3",false,false);
        deathSound = new Sound("DeathSound.mp3", false, false);


    }

    protected virtual void AddSprite()
    {
        ballSprite = new AnimationSprite("ball_animation.png", 4, 4);
        ballSprite.SetCycle(0, 8);
        ballSprite.SetOrigin(ballSprite.width / 2, ballSprite.height / 2);
        ballSprite.scale = 0.25f;
        AddChild(ballSprite);
    }
    


    void AnimateDeath()
    {
        ballSprite.SetCycle(9, 7);
        ballSprite.Animate(0.1f);
        
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
        if (normal.x != 0 || normal.y != 0)
        {
            Gizmos.DrawLine(normal.x, normal.y);
            Console.WriteLine(normal);
        }
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
            lineCollisionTime = Time.time - lineCollisionTime;
            levelReset = true;

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
                //velocity.Reflect(bounciness, pCol.normal);
                MoveClaw(3);
                lineBounce = true;
                if (lineBounce)
                {
                    
                }
                if(!lineBounce)
                {

                }
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
            normal = pCol.normal;
            Console.WriteLine("THIS IS THE NORMAL " + pCol.normal);
            return;
        }
        if (pCol.other.owner is Wall)
        {
            Wall wall = (Wall)pCol.other.owner;
            velocity.Reflect(_bounciness, pCol.normal);
            velocity += pCol.normal;
            return;
        }
        if (pCol.other.owner is EndBlock)
        {
            isMoving = false;
            //Console.WriteLine("EndBlock collision detected");
            lastCollisionTime = Time.time - lastCollisionTime;
            levelComplete = true;
            if (levelComplete)
            {
                endBlockSound.Play(false, 0, 0.7f);
            }
            else
            {
                endBlockSoundChannel.Stop();
            }
            MoveClaw(1);
        }
    }

    void MoveClaw(int upSpeed)
    {
        if (claw == null)
        {
            claw = game.FindObjectOfType<Claw>();
        }
        claw.MoveUpward(upSpeed);
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

    void ResetLevel()
    {
        if (levelReset && Time.time > lineCollisionTime + delayAfterLine)
        {
            Console.WriteLine("Reseting Level....");
            ((MyGame)game).ResetCurrentLevel();
            
            lineCollisionTime = 0;
            levelReset = false;
        }
    }


    protected override void Update()
    {
        base.Update();
        MoveToNextLevel();
        ResetLevel();
        if (lineBounce)
        {
            AnimateDeath();
        }
        else
        {
            AnimateBall();
            //AnimateDeath();
        }

    }
}

