using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using Physics;


public class BouncingPad : EasyDraw
{
    public Vec2 start;
    public Vec2 end;

    public float bounceForce;

    private bool followingMouse = false;

    private Vec2 center;

    public int lineWidth = 1;

    ColliderManager engine;
    List<Physics.Collider> colliders = new List<Physics.Collider> { };

    public BouncingPad(Vec2 pStart, Vec2 pEnd, float pBounceForce = 20f) : base(1500, 1500)
    {
        start = pStart;
        end = pEnd;
        center = (start + end) / 2f;
        Draw();
        colliders.Add(new Physics.LineSegment(this, start, end));
        colliders.Add(new Physics.LineSegment(this, end, start));
        colliders.Add(new Physics.Circle(this, start, 0));
        colliders.Add(new Physics.Circle(this, end, 0));
        engine = ColliderManager.main;
        foreach (Physics.Collider col in colliders)
            engine.AddSolidCollider(col);

        bounceForce = pBounceForce;
    }

    public float GetBounceForce()
    {
        return bounceForce;
    }

    void Draw()
    {
        Clear(Color.Empty);
        Stroke(0, 255, 0);
        StrokeWeight(2);//was 0
        Line(start.x, start.y, end.x, end.y);

    }

    public void RemoveColliders()
    {
        foreach (Physics.Collider col in colliders)
            engine.RemoveSolidCollider(col);
    }

    void RotateToAngle(float targetAngle)
    {
        center = (start + end) / 2f;
        float currentAngle = start.GetAngleDegreesTwoPoints(center);
        float angleDifference = targetAngle - currentAngle;

        start.RotateAroundDegrees(center, angleDifference);
        end.RotateAroundDegrees(center, angleDifference);

        foreach (Physics.Collider col in colliders)
        {
            if (col is Circle)
            {
                col.position.RotateAroundDegrees(center, angleDifference);
            }
            else
            {
                ((LineSegment)col).start.RotateAroundDegrees(center, angleDifference);
                ((LineSegment)col).end.RotateAroundDegrees(center, angleDifference);
            }
        }

        Draw();

    }

    void Update()
    {

        Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
        float distanceToMouse = (center - mousePos).Length();
        if (distanceToMouse < 150)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Console.WriteLine("Mouse clicked near bouncepad");
                if (!followingMouse)
                {
                    Console.WriteLine("started following the mouse");
                    followingMouse = true;
                }
                else
                {
                    Console.WriteLine("stopped following the mouse");
                    followingMouse = false;
                }
            }
        }

        if (followingMouse)
        {
            float angle = center.GetAngleDegreesTwoPoints(mousePos);
            RotateToAngle(angle);
        }

    }


}