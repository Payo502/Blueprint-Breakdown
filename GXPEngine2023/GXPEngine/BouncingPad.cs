using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using Physics;


public class BouncingPad : EasyDraw
{
    public Vec2 start;
    public Vec2 end;

    public float bounceForce;

    private float degreeChange = 15;
    public float maxAngle;

    private Vec2 center;

    public int lineWidth = 1;

    ColliderManager engine;
    List<Physics.Collider> colliders = new List<Physics.Collider> { };
    AnimationSprite bouncePadSprite;
    Vec2 bottomCenter;

    public bool hasBounced = false;
    public BouncingPad(Vec2 pStart, Vec2 pEnd/*, float initialAngle = 0f*/, float pBounceForce = 10f) : base(2000, 2000)
    {
        start = pStart;
        end = pEnd;
        center = (start + end) / 2f;

        bounceForce = pBounceForce;

        AddSprite();

        //bouncePadSprite.rotation = initialAngle;

        EasyDraw canvas = new EasyDraw(50, 50, false);
        AddChild(canvas);

        bottomCenter.x = center.x;
        bottomCenter.y = center.y + bouncePadSprite.height;

        //start.RotateAroundDegrees(bottomCenter, initialAngle);
        Console.WriteLine("X {0} and Y {1}", start.x, start.y);
        //end.RotateAroundDegrees(bottomCenter, initialAngle);

        AddColliders();
        Draw();
        Console.WriteLine(bottomCenter);
        Vector2 point1 = TransformPoint(0, 0);
        Vector2 point2 = TransformPoint(bouncePadSprite.width, 0);
        //Vector2 point1 = TransformPoint(start.x, start.y);
        //Vector2 point2 = TransformPoint(end.x, end.y);
        Console.WriteLine("point1 {0} ", point1);
        Console.WriteLine("point2 {0} ", point2);
    }

    void AddColliders()
    {

        colliders.Add(new Physics.LineSegment(this, start, end));
        colliders.Add(new Physics.LineSegment(this, end, start));
        colliders.Add(new Physics.Circle(this, start, 0));
        colliders.Add(new Physics.Circle(this, end, 0));
        engine = ColliderManager.main;
        foreach (Physics.Collider col in colliders)
            engine.AddSolidCollider(col);


    }

    void AddSprite()
    {
        if (bounceForce > 10)
        {
            //bouncePadSprite = new Sprite();
        }
        else
        {
            bouncePadSprite = new AnimationSprite("bouncepadAnimation.png", 2, 4);
            bouncePadSprite.SetOrigin(bouncePadSprite.width / 2, bouncePadSprite.height);
            bouncePadSprite.SetCycle(0, 1);
            bouncePadSprite.scale = (end - start).Length() / bouncePadSprite.width;
            bouncePadSprite.x = center.x;
            bouncePadSprite.y = center.y + bouncePadSprite.height;
            AddChild(bouncePadSprite);
        }
        
    }

    public void AnimateBouncePad()
    {
        bouncePadSprite.SetCycle(0, 8);
        bouncePadSprite.Animate(0.2f);
        if (bouncePadSprite.currentFrame >= bouncePadSprite.frameCount - 1)
        {
            bouncePadSprite.currentFrame = 0;
            hasBounced = false;
        }
    }

    public float GetBounceForce()
    {
        return bounceForce;
    }

    void Draw()
    {
        Clear(Color.Empty);
        //Stroke(0, 255, 0);
        //StrokeWeight(2);//was 0
        //Line(start.x, start.y, end.x, end.y);
    }

    public void RemoveColliders()
    {
        foreach (Physics.Collider col in colliders)
            engine.RemoveSolidCollider(col);
    }

    public void RotateToAngle(float targetAngle)
    {
        center = (start + end) / 2f;
        float currentAngle = start.GetAngleDegreesTwoPoints(end);
        float angleDifference = targetAngle - currentAngle;

        Ellipse(bottomCenter.x, bottomCenter.y, 50, 50);
        Draw();

        start.RotateAroundDegrees(bottomCenter, angleDifference);
        end.RotateAroundDegrees(bottomCenter, angleDifference);
        bouncePadSprite.rotation = targetAngle;

        RemoveColliders();
        AddColliders();

        foreach (Physics.Collider col in colliders)
        {
            if (col is Circle)
            {
                col.position.RotateAroundDegrees(bottomCenter, angleDifference);
            }
            else
            {
                ((LineSegment)col).start.RotateAroundDegrees(bottomCenter, angleDifference);
                ((LineSegment)col).end.RotateAroundDegrees(bottomCenter, angleDifference);
            }
        }
        Draw();
    }

    void RotateBouncePad()
    {
        Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
        float distanceToMouse = (center - mousePos).Length();
        if (distanceToMouse < 200)
        {
            float currentAngle = start.GetAngleDegreesTwoPoints(end);
            if (Input.GetMouseButtonDown(1))
            {
                RotateToAngle(currentAngle + degreeChange);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                RotateToAngle(currentAngle - degreeChange);
            }
        }

    }

    void Update()
    {
        RotateBouncePad();
        if (hasBounced)
        {
            AnimateBouncePad();
        }
    }
}