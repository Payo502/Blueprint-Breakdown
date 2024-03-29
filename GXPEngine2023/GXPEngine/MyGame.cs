using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;
using GXPEngine.Core;
using System.Runtime.InteropServices;

public class MyGame : Game
{
    public int startLevelNumber = 0;

    public List<MapObject> movers;

    Sound backgroundSound;
    SoundChannel backgroundSoundChannel;

    List<Fan> OnlyFans = new List<Fan>();
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
        foreach (var fan in OnlyFans)
        {
            fan.StopFanSound();
        }
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

                AddChild(new Button("Start_WithoutHover.png", "Start_Hover.png", new Vec2(width / 2 - 200, height / 2 + 250), 280, 132, "play"));

                AddChild(new Button("Quit_withouthover.png", "Quit_Hover.png", new Vec2(width / 2 + 200, height / 2 + 250), 280, 132, "quit"));
                break;

            case 1:

                AddChild(new Background("tutorial.png"));
                AddChild(new Button("Next_WithoutHover.png", "Next_Hover.png", new Vec2(1528, 800), 200, 100, "play"));

                break;

            case 2:
                //level 1
                AddChild(new Background("background.png"));

                SetupWalls();

                AddChild(new Hints("dht.png", 800, 230, 200, 150));

                Hints arrow = new Hints("a1.png", 975, 250, 200, 100);
                arrow.rotation = -45;
                AddChild(arrow);

                AddChild(new Claw(new Vec2(300, 300)));

                AddChild(new BouncingPad(new Vec2(250, 600), new Vec2(400, 600)));

                AddChild(new Hints("rm.png", 475, 600, 150, 100));

                //AddChild(new Wall("wall.png", 600, 0, 100, 400));

                AddChild(new Hints("tmo.png", 650, 850, 200, 100));

                Fan fan1 = new Fan(new Vec2(800, 800), new Vec2(1000, 800), new Vec2(1, 1), 500, 1f);
                OnlyFans.Add(fan1);
                AddChild(fan1);

                AddChild(new BouncingPad(new Vec2(1200, 700), new Vec2(1350, 700)));

                AddChild(new EndBlock(50, new Vec2(1650, 420)));

                AddChild(new Hints("end.png", 1600, 550, 100, 50));

                AddChild(new SecondBackground("Background_transparent.png"));
                break;

            case 3:
                //in between level screen

                AddChild(new Background("levelCompleted.png"));

                AddChild(new Button("nextLevel.png", "nextLevelHover.png", new Vec2(width / 2, height / 2 + 200), 280, 132, "play"));

                break;

            case 4:
                //level 2
                AddChild(new Background("background.png"));

                SetupWalls();

                AddChild(new Claw(new Vec2(300, 300)));

                AddChild(new SecondBackground("Background_transparent.png"));

                AddChild(new Wall("wall.png", 400, 700, 100, 500));

                AddChild(new BouncingPad(new Vec2(375, 550), new Vec2(525, 550), 5));

                //AddChild(new BouncingPad(new Vec2(500, 800), new Vec2(650, 800)));

                AddChild(new Wall("wall.png", 750, 0, 150, 500));

                AddChild(new Hints("uwu.png", 600, 300, 200, 100));

                AddChild(new BouncingPad(new Vec2(950, 800), new Vec2(1100, 800), 16));

                AddChild(new Hints("ims.png", 750, 800, 200, 100));

                Fan fan = new Fan(new Vec2(950, 250), new Vec2(1100, 250), new Vec2(1, 1), 500, 0.5f);
                fan.RotateToAngle(90);
                AddChild(fan);

                Fan fan2 = new Fan(new Vec2(950, 250), new Vec2(1100, 250), new Vec2(1, 1), 500, 0.5f);
                fan2.RotateToAngle(90);
                OnlyFans.Add(fan2);
                AddChild(fan2);

                AddChild(new Wall("wall.png", 1200, 500, 100, 500));

                AddChild(new EndBlock(50, new Vec2(1650, 700)));

                AddChild(new SecondBackground("Background_transparent.png"));
                break;

            case 5:
                // in between level screen
                AddChild(new Background("levelCompleted.png"));

                AddChild(new Button("nextLevel.png", "nextLevelHover.png", new Vec2(width / 2, height / 2 + 200), 280, 132, "play"));

                break;

            case 6:
                //level 3
                AddChild(new Background("background.png"));

                SetupWalls();

                AddChild(new Claw(new Vec2(300, 300)));

                Wall walls = new Wall("wall.png", 200, 700, 200, 400);
                walls.rotation = 90;
                AddChild(walls);

                Wall wall4 = new Wall("wall.png", 0, 800, 100, 400);
                wall4.rotation = -90;
                AddChild(wall4);

                AddChild(new Hints("useme.png", 350, 650, 200, 100));

                AddChild(new BouncingPad(new Vec2(700, 800), new Vec2(850, 800), 20));

                AddChild(new Wall("wall.png", 900, 600, 100, 400));

                AddChild(new BouncingPad(new Vec2(875, 500), new Vec2(1025, 500), 10));

                AddChild(new Wall("wall.png", 500, 0, 150, 550));

                Fan fan4 = new Fan(new Vec2(1500, 300), new Vec2(1650, 300), new Vec2(1,1), 500, 0.7f);
                fan4.RotateToAngle(180);
                OnlyFans.Add(fan4);
                AddChild(fan4);


                AddChild(new EndBlock(50, new Vec2(1650, 800)));

                AddChild(new SecondBackground("Background_transparent.png"));

                break;


            case 7:
                // final screen
                AddChild(new Background("finalScreen.png"));

                AddChild(new Button("restartNormal.png", "Restart_Hover.png", new Vec2(width / 2 - 200, height / 2 + 250), 280, 132, "restart"));

                AddChild(new Button("Quit_withouthover.png", "Quit_Hover.png", new Vec2(width / 2 + 200, height / 2 + 250), 280, 132, "quit"));


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
            if (child is Wall wall)
            {
                wall.RemoveColliders();
            }
            child.LateDestroy();

        }
        children.Clear();
    }

    public void LoadNextLevel()
    {
        startLevelNumber++;
        LoadLevel(startLevelNumber);

        foreach (var fan in OnlyFans)
        {
            fan.StopFanSound();
        }
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
        if (Input.GetKeyDown(Key.SIX))
        {
            startLevelNumber = 6;
            LoadLevel(startLevelNumber);
        }
        if (Input.GetKeyDown(Key.SEVEN))
        {
            startLevelNumber = 7;
            LoadLevel(startLevelNumber);
        }
        if (Input.GetKeyDown(Key.EIGHT))
        {
            startLevelNumber = 8;
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