using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{


    int startLevelNumber = 1;

    public List<MapObject> movers;

    public MyGame() : base(1920, 1080, false, false)
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

                Background background1 = new Background();
                AddChild(background1);
                //AddChild(new Player(new Vec2(200, 200), 30));

                AddChild(new Line(new Vec2(0, 300), new Vec2(400, 500))); // Bottom Slanted
                //AddChild(new Line(new Vec2(0, 500), new Vec2(800, 500))); // Bottom Straight
                AddChild(new Line(new Vec2(100, 0), new Vec2(100, 800))); // Left
                AddChild(new Line(new Vec2(700, 0), new Vec2(700, 800))); // Right
                //AddChild(new Line(new Vec2(0, 100), new Vec2(800, 100))); //Top

                Claw claw = new Claw(new Vec2(200, 50));
                AddChild(claw);

                //EndBlock endBlock1 = new EndBlock(30, new Vec2(400, 400));
                //AddChild(endBlock1);

                AddChild(new BouncingPad(new Vec2(400, 500), new Vec2(600, 500)));
                //AddChild(new BouncingPad(new Vec2(500, 100), new Vec2(600, 300)));
                //AddChild(new Fan(new Vec2(300, 500), new Vec2(600, 500)));



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

                Background background3 = new Background();
                AddChild(background3);


                AddChild(new Line(new Vec2(800, 300), new Vec2(400, 500))); // Bottom Slanted
                AddChild(new Line(new Vec2(0, 500), new Vec2(800, 500))); // Bottom Straight
                AddChild(new Line(new Vec2(100, 0), new Vec2(100, 800))); // Left
                AddChild(new Line(new Vec2(700, 0), new Vec2(700, 800))); // Right
                //AddChild(new Line(new Vec2(0, 100), new Vec2(800, 100))); //Top

                Console.WriteLine("third level");

                AddChild(new Fan(new Vec2(300, 500), new Vec2(500, 500), new Vec2(1, 1)));

                Claw claw3 = new Claw(new Vec2(200, 100));
                AddChild(claw3);

                break;

            case 4:
                AddChild(new Button("square.png", new Vec2(width / 2, height / 2), 100, 100, "play"));
                AddChild(new Button("square.png", new Vec2(width / 2, height / 2 + 100), 100, 100, "select"));
                AddChild(new Button("square.png", new Vec2(width / 2, height / 2 + 200), 100, 100, "play"));
                Console.WriteLine("four");
                break;
            case 5:
                AddChild(new Button("triangle.png", new Vec2(width / 2, height / 2), 200, 200, "quit"));
                Console.WriteLine("five");
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
            if (child is MapObject mapObject)
            {
                mapObject.engine.RemoveSolidCollider(mapObject.myCollider);
                movers.Remove(mapObject);
            }
            if (child is BouncingPad bouncingPad)
            {
                bouncingPad.RemoveColliders();
            }
            if (child is Fan fan)
            {
                fan.RemoveColliders();
            }
            if (child is EndBlock endBlock)
            {
                endBlock.engine.RemoveSolidCollider(endBlock.myCollider);
            }
            child.LateDestroy();

        }
        children.Clear();
    }

    public void LoadNextLevel()
    {
        startLevelNumber++;
        LoadLevel(startLevelNumber);
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
        if (Input.GetKeyDown(Key.FIVE))
        {
            startLevelNumber = 5;
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