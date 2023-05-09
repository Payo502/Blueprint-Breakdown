using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using Physics;

public class MapObject : CircleBase
{
    public List<Fan> fans = new List<Fan>();
    public MapObject(int pRadius, Vec2 pPosition, Vec2 pVelocity = new Vec2(), bool moving = true) : base(pRadius, pPosition)
    {
        isMoving = moving;
        Draw(230, 200, 0);
        _density = 0.9f;

    }

    protected override void Move()
    {

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
        if (isMoving)
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
        Ellipse(radius, radius, 2 * radius, 2 * radius);
    }

    void ResolveCollisions(CollisionInfo pCol)
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
            Console.WriteLine("BouncingPad collision detected");
            BouncingPad bouncePad = (BouncingPad)pCol.other.owner;
            float bounceForce = bouncePad.GetBounceForce();
            Console.WriteLine("Before velocity update: " + velocity);
            velocity.Reflect(_bounciness, pCol.normal);
            velocity += pCol.normal * bounceForce;
            Console.WriteLine("After velocity update: " + velocity);
            return;
        }

        if (pCol.other.owner is Fan)
        {
            Console.WriteLine("Collision with fan");
            Fan fan = (Fan)pCol.other.owner;
            fan.Push(pCol);
            return;
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
}

