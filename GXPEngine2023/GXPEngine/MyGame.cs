using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{

    int startLevelNumber = 1;
    public MyGame() : base(800, 600, false)
    {
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
                AddChild(new Line(new Vec2(0, 500), new Vec2(800, 500))); // Bottom Straight
                AddChild(new Line(new Vec2(100, 0), new Vec2(100, 800))); // Left
                AddChild(new Line(new Vec2(700, 0), new Vec2(700, 800))); // Right
                AddChild(new Line(new Vec2(0, 100), new Vec2(800, 100))); //Top

                AddChild(new MapObject(30, new Vec2(200, 200)));
                //AddChild(new MapObject(30, new Vec2(400, 200)));

                AddChild(new BouncingPad(new Vec2(300,500), new Vec2(600,500)));


                break;
            case 2:
                AddChild(new Line(new Vec2(800, 300), new Vec2(400, 500))); // Bottom Slanted
                AddChild(new Line(new Vec2(0, 500), new Vec2(800, 500))); // Bottom Straight
                AddChild(new Line(new Vec2(100, 0), new Vec2(100, 800))); // Left
                AddChild(new Line(new Vec2(700, 0), new Vec2(700, 800))); // Right
                AddChild(new Line(new Vec2(0, 100), new Vec2(800, 100))); //Top

                AddChild(new MapObject(30, new Vec2(600, 200)));
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
            }

            child.Destroy();
        }
        children.Clear();
    }


    void HandleInput()
    {
        if (Input.GetKeyDown(Key.R))
        {
            LoadLevel(startLevelNumber);
        }
        if (Input.GetKeyDown(Key.Q))
        {
            startLevelNumber++;
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