using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;
using GXPEngine.Core;

public class MyGame : Game
{
    public int startLevelNumber = 1;

    public List<MapObject> movers;

    public MyGame() : base(1920, 1080, false, false)//, 1440, 810)
    {
        movers = new List<MapObject>();
        LoadLevel(startLevelNumber);
    }

    public void ResetCurrentLevel()
    {
        DestroyAll();

        LoadLevel(startLevelNumber);
    }

    public void LoadLevel(int levelNumber)
    {
        startLevelNumber = levelNumber;

        DestroyAll();

        switch (levelNumber)
        {
            case 0:
                AddChild(new Button("Start_WithoutHover.png", "Start_Hover.png", new Vec2(width/2, height/2), 714, 349, "play"));

                AddChild(new Button("circle.png","triangle.png", new Vec2(width / 2, height / 2 + 200), 100, 100, "quit"));
                break;

            case 1:

                AddChild(new Background("background.png"));

                SetupWalls();

                AddChild(new Claw(new Vec2(300, 300)));

                AddChild(new BouncingPad(new Vec2(220, 750), new Vec2(350, 750)));

                AddChild(new Wall("wall.png", 600, 0, 100, 500));
                
                AddChild(new Fan(new Vec2(800, 800), new Vec2(1000, 800), new Vec2(1,1)));

                AddChild(new BouncingPad(new Vec2(930, 50), new Vec2(1030, 50), 20));

                AddChild(new BouncingPad(new Vec2(1300, 700), new Vec2(1450, 700)));

                AddChild(new EndBlock(50, new Vec2(1650, 300)));

                AddChild(new SecondBackground("Background_transparent.png"));
                break;


            case 2:
                AddChild(new Background("background.png"));

                SetupWalls();

                AddChild(new Claw(new Vec2(300, 300)));

                AddChild(new Wall("wall.png", 400, 700, 100, 500));

                AddChild(new BouncingPad(new Vec2(375, 550), new Vec2(525, 550)));

                AddChild(new BouncingPad(new Vec2(500, 800), new Vec2(650, 800)));

                AddChild(new Wall("wall.png", 700, 0, 200, 700));

                AddChild(new BouncingPad(new Vec2(950, 800), new Vec2(1100, 800), 30));

                AddChild(new Fan(new Vec2(950, 0), new Vec2(1150, 0), new Vec2(1, 1)));



                AddChild(new SecondBackground("Background_transparent.png"));
                break;

            case 3:

                Background background3 = new Background("background.png");
                AddChild(background3);


                AddChild(new Line(new Vec2(800, 300), new Vec2(400, 500)));
                AddChild(new Line(new Vec2(0, 500), new Vec2(800, 500)));
                AddChild(new Line(new Vec2(100, 0), new Vec2(100, 800)));
                AddChild(new Line(new Vec2(700, 0), new Vec2(700, 800)));


                Console.WriteLine("third level");

                AddChild(new Fan(new Vec2(300, 500), new Vec2(500, 500), new Vec2(1, 1)));

                Claw claw3 = new Claw(new Vec2(200, 100));
                AddChild(claw3);

                break;
            case 4:
                Console.WriteLine("four");
                break;
            case 5:
                Console.WriteLine("five");
                break;
        }
    }

    void SetupWalls()
    {
        AddChild(new Line(new Vec2(210, 140), new Vec2(210, 935))); // Left
        AddChild(new Line(new Vec2(1718, 140), new Vec2(1718, 935))); // Right
        AddChild(new Line(new Vec2(210, 935), new Vec2(1700, 935))); //Bottom
        AddChild(new Line(new Vec2(210, 140), new Vec2(1718, 140))); //Top 
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

    public void LoadFirstLevel()
    {
        startLevelNumber = 1;
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