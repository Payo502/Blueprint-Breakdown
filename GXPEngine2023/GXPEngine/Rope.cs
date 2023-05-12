using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using System.Collections;

public class Rope : GameObject
{
    private float ropeLength;
    private float lineSegmentSize = 0.25f;
    private Vec2 startPos;
    List<Vec2> segmentList = new List<Vec2>();
    List<Vec2> oldsegmentList = new List<Vec2>();
    EasyDraw drawingSpace;

    MapObject mapObject;
    public Rope(float Length, MapObject mapObject) : base(false)
    {
        this.mapObject = mapObject;
        drawingSpace = new EasyDraw(1080, 1920, false);
        ropeLength = Length;

        startPos.x = Input.mouseX;
        startPos.y = Input.mouseY;
        //creates all segments of the line
        for (int i = 0; i < ropeLength; i++)
        {
            segmentList.Add(startPos);
            oldsegmentList.Add(startPos);
            startPos.y -= lineSegmentSize;
            Console.WriteLine(startPos);
        }
    }

    private Vec2 CalculateRopeForce()
    {
        Vec2 ropeEnd = segmentList.Last();
        Vec2 objectPosition = new Vec2(mapObject.x, mapObject.y);
        Vec2 direction = (objectPosition - ropeEnd).Normalized();
        float distance = (objectPosition - ropeEnd).Length();

        float forceMultiplier = -1f;
        Vec2 force = direction * distance * forceMultiplier;

        return force;
    }

    void Update()
    {
        if (Input.GetKeyDown(Key.G))
        {
            mapObject.gravityEnabled = !mapObject.gravityEnabled;
        }

        if (Input.GetMouseButton(1))
        {
            Vec2 ropeForce = CalculateRopeForce();
            mapObject.ApplyForce(ropeForce);
        }

        linePhysics();
        contraints();
        drawLine();
    }

    void linePhysics()
    {
        for (int i = 0; i < ropeLength; i++)
        {
            Vec2 firstSegment = segmentList[i];
            segmentList[0] = firstSegment;
            Vec2 Velocity = firstSegment - oldsegmentList[i];
            oldsegmentList[i] = firstSegment;
            firstSegment += Velocity;
            segmentList[i] = firstSegment;
        }

    }

    void contraints()
    {
        for (int i = 0; i < 50; i++)
        {
            contraining();
        }
    }

    void contraining()
    {
        /*Vec2 firstSegment = segmentList[0];
        firstSegment.x = Input.mouseX;
        firstSegment.y = Input.mouseY;*/
        Vec2 firstSegment = new Vec2(mapObject.x, mapObject.y);
        segmentList[0] = firstSegment;
        Vec2 lastSegment = new Vec2(Input.mouseX, Input.mouseY);
        segmentList[(int)ropeLength - 1] = lastSegment;
        for (int i = 0; i < ropeLength - 1; i++)
        {
            Vec2 firstSeg = segmentList[i];
            Vec2 secondSeg = segmentList[i + 1];
            float dist = (firstSeg - secondSeg).Length();
            float error = Mathf.Abs(dist - ropeLength);
            Vec2 changeDir = new Vec2(0, 0);

            if (dist > ropeLength)
            {
                changeDir = (firstSeg - secondSeg).Normalized();
            }
            else if (dist < ropeLength)
            {
                changeDir = (secondSeg - firstSeg).Normalized();
            }
            Vec2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg -= changeAmount * 0.5f;
                segmentList[i] = firstSeg;
                secondSeg += changeAmount * 0.5f;
                segmentList[i + 1] = secondSeg;
            }
            else
            {
                secondSeg += changeAmount;
                segmentList[i + 1] = secondSeg;
            }
        }
    }

    public void drawLine()
    {

        Vec2 oldVector;
        Vec2 newVector;
        for (int i = 1; i < ropeLength; i++)
        {

            Console.WriteLine(segmentList[0]);
            oldVector = segmentList[i - 1];
            newVector = segmentList[i];
            Gizmos.DrawLine(oldVector.x, oldVector.y, newVector.x, newVector.y);

        }

    }
}
