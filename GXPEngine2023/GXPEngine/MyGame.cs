using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{

    int startLevelNumber = 3;

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
                AddChild(new Line(new Vec2(0, 100), new Vec2(800, 100))); //Top

                MapObject mapObject1 = new MapObject(30, new Vec2(200, 200), default, false);
                AddChild(mapObject1);
                movers.Add(mapObject1);
                
                Claw claw = new Claw(mapObject1);
                claw.SetXY(100, 100);
                AddChild(claw);

                AddChild(new BouncingPad(new Vec2(300,500), new Vec2(600,500)));
                AddChild(new BouncingPad(new Vec2(500, 100), new Vec2(600, 300)));

                break;


            case 2:
                AddChild(new Line(new Vec2(800, 300), new Vec2(400, 500))); // Bottom Slanted
                AddChild(new Line(new Vec2(0, 500), new Vec2(800, 500))); // Bottom Straight
                AddChild(new Line(new Vec2(100, 0), new Vec2(100, 800))); // Left
                AddChild(new Line(new Vec2(700, 0), new Vec2(700, 800))); // Right
                AddChild(new Line(new Vec2(0, 100), new Vec2(800, 100))); //Top

                AddChild(new MapObject(30, new Vec2(600, 200)));

                break;

            case 3: //fan testing
                AddChild(new Line(new Vec2(800, 300), new Vec2(400, 500))); // Bottom Slanted
                AddChild(new Line(new Vec2(0, 500), new Vec2(800, 500))); // Bottom Straight
                AddChild(new Line(new Vec2(100, 0), new Vec2(100, 800))); // Left
                AddChild(new Line(new Vec2(700, 0), new Vec2(700, 800))); // Right
                AddChild(new Line(new Vec2(0, 100), new Vec2(800, 100))); //Top

                Console.WriteLine("third level");

                AddChild(new Fan(new Vec2(300, 400), new Vec2(400, 500), new Vec2(1,1)));

                MapObject mapObject = new MapObject(30, new Vec2(350, 300));
                AddChild(mapObject);
                movers.Add(mapObject);

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
            else if (child is Fan fan)
            {
                fan.RemoveColliders();
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
        if (Input.GetKeyDown(Key.ENTER))
        {
            startLevelNumber++;
            LoadLevel(startLevelNumber);
        }
        if (Input.GetKeyDown(Key.BACKSPACE))
        {
            startLevelNumber--;
            LoadLevel(startLevelNumber);
        }
    }

    void Update()
    {
        HandleInput();
        // Empty
    }

    static void Main()
    {
        new MyGame().Start();
    }
}