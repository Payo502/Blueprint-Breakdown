using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;


public class Button : Sprite
{
    Vec2 position;
    bool isPressed = false;
    string action;

    MyGame myGame;
    //Level level;


    public Button(string filename, Vec2 pPosition, int pWidth, int pHeight, string pAction) : base(filename)
    {
        SetOrigin(width / 2, height / 2); 
        position = pPosition;
        width = pWidth;
        height = pHeight;
        UpdateScreenPosition();
        action = pAction;
        myGame = game.FindObjectOfType<MyGame>();
    }

    public void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
        position.SetXY(x, y);
    }


    void Update()
    {
        Click();
    }

    void Click()
    {
        float mouseX = Input.mouseX;
        float mouseY = Input.mouseY;
        if (HitTestPoint(mouseX,mouseY))
        {
            if (Input.GetMouseButtonDown(0))
            {
                DoAction();
                Console.WriteLine("pressed");
            }
        }
    }

    void DoAction()
    {
        QuitAction();
        PlayAction();
        SelectLevel();
        Console.WriteLine("did action");
    }

    void QuitAction()
    {
        if (action == "quit")
        {
            myGame.Destroy();
        }
    }
    
    void PlayAction()
    {
        if (action == "play")
        {
            myGame.LoadNextLevel();
            Console.WriteLine("switched level");
        }
    }


    void SelectLevel()
    {
        if (action == "select")
        {
            myGame.LoadNextLevel();
            Console.WriteLine("did something");
        }
    }

}