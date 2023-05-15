using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;
using GXPEngine.Core;

public class MyGame : Game
{
    public int startLevelNumber = 0;

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
                AddChild(new Background("mainScreenBackground.png"));

                AddChild(new Button("Start_WithoutHover.png", "Start_Hover.png", new Vec2(width/2 - 200, height/2 + 250), 280, 132, "play"));

                AddChild(new Button("Quit_withouthover.png", "Quit_Hover.png", new Vec2(width/2 + 200,height/2 + 250), 280, 132, "quit"));
                break;
            case 1:

                AddChild(new Background("background.png"));

                SetupWalls();
                
                AddChild(new Claw(new Vec2(300, 300)));

                AddChild(new BouncingPad(new Vec2(400, 500), new Vec2(600, 500)));
                AddChild(new BouncingPad(new Vec2(50, 800), new Vec2(250, 800)));
                
                AddChild(new Fan(new Vec2(700, 800), new Vec2(900, 800), new Vec2(1,1)));

                AddChild(new BouncingPad(new Vec2(500, -150), new Vec2(700, -150)));

                AddChild(new BouncingPad(new Vec2(1200, 500), new Vec2(1400, 500)));

                AddChild(new EndBlock(50, new Vec2(1650, 500)));

                AddChild(new SecondBackground("Background_transparent.png"));
                break;


            case 2:
                AddChild(new Background("background.png"));

                SetupWalls();

                AddChild(new Claw(new Vec2(300, 300)));
                AddChild(new SecondBackground("Background_transparent.png"));

                break;

            case 3:
                AddChild(new Background("background.png"));

                SetupWalls();

                AddChild(new Claw(new Vec2(300, 300)));
                AddChild(new SecondBackground("Background_transparent.png"));


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