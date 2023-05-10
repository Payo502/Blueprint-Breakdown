using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using Physics;

public class Fan : EasyDraw
{
    public Vec2 start;
    public Vec2 end;

    public int lineWidth = 1;

    ColliderManager engine;
    List<Physics.Collider> colliders = new List<Physics.Collider> { };

    public Fan(Vec2 pStart, Vec2 pEnd) : base(2000, 2000)
    {
        start = pStart;
        end = pEnd;
        Draw();
        colliders.Add(new Physics.LineSegment(this, start, end));
        colliders.Add(new Physics.LineSegment(this, end, start));
        colliders.Add(new Physics.Circle(this, start, 0));
        colliders.Add(new Physics.Circle(this, end, 0));
        engine = ColliderManager.main;
        foreach (Physics.Collider col in colliders)
            engine.AddSolidCollider(col);
    }

    void Draw()
    {
        Clear(Color.Empty);
        Stroke(0, 0, 255);
        StrokeWeight(0);//was 0
        Line(start.x, start.y, end.x, end.y);
    }

    void RemoveColliders()
    {
        foreach (Physics.Collider col in colliders)
            engine.RemoveSolidCollider(col);
    }


    void Update()
    {

    }
}

