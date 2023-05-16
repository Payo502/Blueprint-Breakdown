using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using Physics;

public class Wall : Sprite
{
    ColliderManager engine;
    List<Physics.Collider> colliders = new List<Physics.Collider> { };
    public Wall(string filename, float pX, float pY, int pWidth, int pHeight) : base(filename, false, false)
    {
        SetOrigin(0, 0);
        width = pWidth;
        height = pHeight;
        x = pX;
        y = pY;
        AddColliders();
    }

    void Update()
    {

    }

    void AddColliders()
    {
        colliders.Add(new Physics.LineSegment(this, new Vec2(x, y), new Vec2(x + width, y)));
        colliders.Add(new Physics.LineSegment(this, new Vec2(x + width, y), new Vec2(x, y)));

        colliders.Add(new Physics.LineSegment(this, new Vec2(x, y), new Vec2(x, y + height)));
        colliders.Add(new Physics.LineSegment(this, new Vec2(x, y + height), new Vec2(x, y)));

        colliders.Add(new Physics.LineSegment(this, new Vec2(x, y + height), new Vec2(x + width, y + height)));
        colliders.Add(new Physics.LineSegment(this, new Vec2(x + width, y + height), new Vec2(x, y + height)));

        colliders.Add(new Physics.LineSegment(this, new Vec2(x + width, y), new Vec2(x + width, y + height)));
        colliders.Add(new Physics.LineSegment(this, new Vec2(x + width, y + height), new Vec2(x + width, y)));

        colliders.Add(new Physics.Circle(this, new Vec2(x, y), 0));
        colliders.Add(new Physics.Circle(this, new Vec2(x + width, y), 0));
        colliders.Add(new Physics.Circle(this, new Vec2(x, y + height), 0));
        colliders.Add(new Physics.Circle(this, new Vec2(x+ width, y + height), 0));
        engine = ColliderManager.main;
        foreach (Physics.Collider col in colliders)
            engine.AddSolidCollider(col);
    }

    public void RemoveColliders()
    {
        foreach (Physics.Collider col in colliders)
            engine.RemoveSolidCollider(col);
    }
}