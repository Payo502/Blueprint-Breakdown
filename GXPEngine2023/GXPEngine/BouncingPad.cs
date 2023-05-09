using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using Physics;


public class BouncingPad : EasyDraw
{
    public Vec2 start;
    public Vec2 end;
    Vec2 rotationOrigin;
    public readonly bool isRotating = false;
    public readonly int rotationEachFrame = 1;

    public float bounceForce;

    public uint lineWidth = 1;

    ColliderManager engine;
    List<Physics.Collider> colliders = new List<Physics.Collider> { };

    public BouncingPad(Vec2 pStart, Vec2 pEnd, float pBounceForce = 10f) : base(1500, 1500)
    {
        start = pStart;
        end = pEnd;
        rotationOrigin = (start + end) / 2;
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
        StrokeWeight(0);//was 0
        Line(start.x, start.y, end.x, end.y);
        
    }

    public void RemoveColliders()
    {
        foreach (Physics.Collider col in colliders)
            engine.RemoveSolidCollider(col);
    }

    public void Bounce(CollisionInfo pCol)
    {
        Console.WriteLine("Bounce method called");

        if (pCol.other.owner is MapObject)
        {
            CircleBase otherObject = (CircleBase)pCol.other.owner;
            Console.WriteLine("Before velocity update: " + otherObject.velocity);
            otherObject.velocity.Reflect(otherObject._bounciness, pCol.normal);
            otherObject.velocity += pCol.normal * bounceForce;
            Console.WriteLine("After velocity update: " + otherObject.velocity);
            Console.WriteLine(otherObject.velocity);
        }
    }

    void Update()
    {

    }


}