using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using Physics;

public class Player : CircleBase
{
    float acceleration = 0.3f;
    float maxSpeed = 7f;
    int lastCooldown = -10000;

    private float deltaTime;
    public Player(Vec2 startPosition, int pRadius) : base(pRadius,startPosition)
    {
        bounciness = 0.2f;
        Draw(0, 255, 0);
    }

    protected override void AddCollider()
    {
        engine.AddTriggerCollider(myCollider);
    }

    protected override void OnDestroy()
    {
        engine.RemoveTriggerCollider(myCollider);
    }

    protected override void Move()
    {
        HandleInput();

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
                if (colInfo.timeOfImpact < 0.01f)
                {
                    repeat = true;   
                }
                ResolveCollisions(colInfo);

            }
            iteration++;
        }
        base.Move();

        UpdateScreenPosition();

    }

    void ResolveCollisions(CollisionInfo pCol)
    {
        if (pCol.other.owner is Line)
        {
            Line segment = (Line)pCol.other.owner;
            if (segment.isRotating)
            {
                Vec2 tempVelocity = pCol.normal * maxSpeed;

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
    }

    protected override void Update()
    {
        float timeSinceLastFrame = Time.deltaTime;
        deltaTime = timeSinceLastFrame/30;
        base.Update();
    }

    void HandleInput()
    {
        Vec2 moveDirection = new Vec2(0, 0);

        if (Input.GetKey(Key.A))
        {
            moveDirection -= new Vec2(1, 0);
        }
        if (Input.GetKey(Key.D))
        {
            moveDirection += new Vec2(1, 0);
        }
        if (Input.GetKey(Key.W))
        {
            moveDirection -= new Vec2(0, 1);
        }
        if (Input.GetKey(Key.S))
        {
            moveDirection += new Vec2(0, 1);
        }

        moveDirection.Normalize();
        moveDirection *= acceleration * deltaTime;
        velocity += moveDirection;
        if (velocity.Length() > maxSpeed)
        {
            velocity = velocity.Normalized() * maxSpeed;
        }
    }
}

