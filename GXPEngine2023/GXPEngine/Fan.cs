using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using GXPEngine;
using Physics;

public class Fan : EasyDraw
{
    public Vec2 start;
    public Vec2 end;

    public int lineWidth = 1;

    private float degreeChange = 15;

    AirStream airStream;

    private Vec2 center;

    readonly ColliderManager engine;
    readonly List<Physics.Collider> colliders = new List<Physics.Collider> { };

    public Fan(Vec2 pStart, Vec2 pEnd, Vec2 pScale, int airLength = 500) : base(1000, 1000, false)
    {
        Clear(255);
        start = pStart;
        end = pEnd;
        scaleX = pScale.x;
        scaleY = pScale.x;
        Draw();
        colliders.Add(new Physics.LineSegment(this, start, end));
        colliders.Add(new Physics.LineSegment(this, end, start));
        colliders.Add(new Physics.Circle(this, start, 0));
        colliders.Add(new Physics.Circle(this, end, 0));
        engine = ColliderManager.main;
        foreach (Physics.Collider col in colliders)
            engine.AddSolidCollider(col);

        float angle = GetAngle();
        center = (start + end) / 2;
        float length = (end - start).Length();
        airStream = new AirStream(center, new Vec2(length, 500), 2);
        airStream.SetRotation(angle);
        AddChild(airStream);

        RemoveColliders();
    }

    public float GetAngle()
    {
        Vec2 diff = end - start;
        float angle = Vec2.Rad2Deg(diff.GetAngleRadians());
        return angle;
    }

    void Draw()
    {
        Clear(Color.Empty);
        Stroke(0, 0, 255);
        StrokeWeight(0);//was 0
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

        airStream.SetRotation(targetAngle);

        Draw();

    }

    void RotateFan()
    {
        Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
        float distanceToMouse = (center - mousePos).Length();
        if (distanceToMouse < 200)
        {
            float currentAngle = start.GetAngleDegreesTwoPoints(center);
            if (Input.GetMouseButtonDown(0))
            {
                Console.WriteLine("Mouse clicked near fan");
                RotateToAngle(currentAngle + degreeChange);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                RotateToAngle(currentAngle - degreeChange);
            }
        }
    }

    void Update()
    {
        RotateFan();
    }
}

