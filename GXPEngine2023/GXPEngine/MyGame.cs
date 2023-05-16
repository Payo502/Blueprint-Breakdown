using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;
using GXPEngine.Core;

public class MyGame : Game
{
    public int startLevelNumber = 0;

    public List<MapObject> movers;

    Sound backgroundSound;
    SoundChannel backgroundSoundChannel;
    public MyGame() : base(1920, 1080, false, false)//, 1440, 810)
    {
        movers = new List<MapObject>();
        LoadLevel(startLevelNumber);
        backgroundSound = new Sound("Background_Music.mp3", true);
        
    }

    public void ResetCurrentLevel()
    {
        DestroyAll();
        LoadLevel(startLevelNumber);
    }

    void PlaySound()
    {
        if (backgroundSoundChannel != null)
        {
            backgroundSoundChannel.Stop();
        }
        backgroundSoundChannel = backgroundSound.Play();
        backgroundSoundChannel.Volume = 1f;
    }

    public void LoadLevel(int levelNumber)
    {
        startLevelNumber = levelNumber;

        DestroyAll();

        if (startLevelNumber > 0)
        {
            PlaySound();
        }
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

                AddChild(new BouncingPad(new Vec2(220, 750), new Vec2(350, 750)));

                AddChild(new Wall("wall.png", 600, 0, 100, 500));
                
                AddChild(new Fan(new Vec2(800, 800), new Vec2(1000, 800), new Vec2(1,1)));

                AddChild(new BouncingPad(new Vec2(930, 50), new Vec2(1030, 50),20));

                AddChild(new BouncingPad(new Vec2(1300, 700), new Vec2(1450, 700)));

                AddChild(new EndBlock(50, new Vec2(1650, 300)));

                AddChild(new SecondBackground("Background_transparent.png"));
                break;


            case 2:
                AddChild(new Background("background.png"));

                SetupWalls();

                AddChild(new Claw(new Vec2(300, 300)));

                AddChild(new SecondBackground("Background_transparent.png"));

                AddChild(new Wall("wall.png", 400, 700, 100, 500));

                AddChild(new BouncingPad(new Vec2(375, 550), new Vec2(525, 550)));

                AddChild(new BouncingPad(new Vec2(500, 800), new Vec2(650, 800)));

                AddChild(new Wall("wall.png", 700, 0, 200, 700));

                AddChild(new BouncingPad(new Vec2(950, 800), new Vec2(1100, 800), 15));

                AddChild(new Fan(new Vec2(950, 0), new Vec2(1150, 0), new Vec2(1, 1)));

                AddChild(new Wall("wall.png", 1200, 500, 100, 500));

/*                Wall wall = new Wall("wall.png", 1900, 300, 100, 400);
                wall.rotation = 90;
                AddChild(wall);*/

                AddChild(new EndBlock(50, new Vec2(1650, 700)));

                AddChild(new SecondBackground("Background_transparent.png"));
                break;

            case 3:
                AddChild(new Background("background.png"));

                SetupWalls();

                AddChild(new Claw(new Vec2(300, 300)));

                Wall wall1 = new Wall("wall.png", 400, 600, 100, 400);
                wall1.rotation = 90;
                AddChild(wall1);

                AddChild(new BouncingPad(new Vec2(700, 800), new Vec2(850, 800), 30));

                BouncingPad bp1 = new BouncingPad(new Vec2(700, 0), new Vec2(850, 0));
                bp1.RotateToAngle(180);
                AddChild(bp1);                

                AddChild(new BouncingPad(new Vec2(250, 700), new Vec2(400, 700)));

                AddChild(new Wall("wall.png", 900, 600, 100, 400));

                BouncingPad bp2 = new BouncingPad(new Vec2(1700, 0), new Vec2(1850, 0));
                bp2.RotateToAngle(-90);
                AddChild(bp2);

                AddChild(new BouncingPad(new Vec2(1600, 500), new Vec2(1750, 500)));

                Wall wall2 = new Wall("wall.png", 1950, 600, 100, 400);
                wall2.rotation = 90;
                AddChild(wall2);

                AddChild(new Fan(new Vec2(1100, 0), new Vec2(1250, 0), new Vec2(1, 1)));

                AddChild(new BouncingPad(new Vec2(1000, 800), new Vec2(1150, 800)));

                AddChild(new Wall("wall.png", 1200, 700, 100, 400));

                AddChild(new BouncingPad(new Vec2(1300, 800), new Vec2(1450, 800)));

                AddChild(new EndBlock(50, new Vec2(1650, 800)));

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