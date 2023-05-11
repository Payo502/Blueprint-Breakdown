using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{
    private Vec2 clawPosition;

    int startLevelNumber = 1;

    List<MapObject> movers;

    public MyGame() : base(800, 600, false)
    {
        movers = new List<MapObject>();
        LoadLevel(startLevelNumber);
    }



    void LoadLevel(int levelNumber)
    {
        startLevelNumber = levelNumber;

        DestroyAll();

        switch (levelNumber)
        {
            case 1:

                //AddChild(new Player(new Vec2(200, 200), 30));

                AddChild(new Line(new Vec2(0, 300), new Vec2(400, 500))); // Bottom Slanted
                //AddChild(new Line(new Vec2(0, 500), new Vec2(800, 500))); // Bottom Straight
                AddChild(new Line(new Vec2(100, 0), new Vec2(100, 800))); // Left
                AddChild(new Line(new Vec2(700, 0), new Vec2(700, 800))); // Right
                //AddChild(new Line(new Vec2(0, 100), new Vec2(800, 100))); //Top

                Claw claw = new Claw(new Vec2(200, 100));
                AddChild(claw);


                AddChild(new BouncingPad(new Vec2(300, 500), new Vec2(700, 500)));
                //AddChild(new BouncingPad(new Vec2(500, 100), new Vec2(600, 300)));
                //AddChild(new Fan(new Vec2(300, 500), new Vec2(600, 500)));


                break;
            case 2:
                AddChild(new Line(new Vec2(800, 300), new Vec2(400, 500))); // Bottom Slanted
                AddChild(new Line(new Vec2(0, 500), new Vec2(800, 500))); // Bottom Straight
                AddChild(new Line(new Vec2(100, 0), new Vec2(100, 800))); // Left
                AddChild(new Line(new Vec2(700, 0), new Vec2(700, 800))); // Right
                AddChild(new Line(new Vec2(0, 100), new Vec2(800, 100))); //Top

                Claw claw2 = new Claw(new Vec2(500, 200));
                AddChild(claw2);

                break;

        }
    }


    void DestroyAll()
    {
        List<GameObject> children = GetChildren();
        foreach (GameObject child in children)
        {
            if (child is Line line)
            {
                line.RemoveColliders();
            }
            else if (child is MapObject mapObject)
            {
                mapObject.engine.RemoveSolidCollider(mapObject.myCollider);
                movers.Remove(mapObject);
            }
            else if (child is BouncingPad bouncingPad)
            {
                bouncingPad.RemoveColliders();
            }

            child.Destroy();
        }
        children.Clear();
    }

    public int GetNumberOfMovers()
    {
        return movers.Count;
    }

    public MapObject GetMover(int index)
    {
        if (index >= 0 && index < movers.Count)
        {
            return movers[index];
        }
        return null;
    }


    void HandleInput()
    {
        if (Input.GetKeyDown(Key.R))
        {
            LoadLevel(startLevelNumber);
        }
        if (Input.GetKeyDown(Key.ONE))
        {
            startLevelNumber = 1;
            LoadLevel(startLevelNumber);
        }
        if (Input.GetKeyDown(Key.TWO))
        {
            startLevelNumber = 2;
            LoadLevel(startLevelNumber);
        }
        if (Input.GetKeyDown(Key.THREE))
        {
            startLevelNumber = 3;
            LoadLevel(startLevelNumber);
        }
        if (Input.GetKeyDown(Key.FOUR))
        {
            startLevelNumber = 4;
            LoadLevel(startLevelNumber);
        }
    }

    void Update()
    {
        HandleInput();
        
    }

    static void Main()
    {
        new MyGame().Start();
    }
}