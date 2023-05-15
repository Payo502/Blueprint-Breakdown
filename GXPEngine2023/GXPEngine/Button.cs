using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;


public class Button : GameObject
{
    Vec2 position;
    string action;

    MyGame myGame;
    //Level level;
    Sprite hoverSprite;
    Sprite buttonSprite;


    public Button(string filename, string hoverFilename, Vec2 pPosition, int pWidth, int pHeight, string pAction) : base()
    {
        position = pPosition;

        buttonSprite = new Sprite(filename);
        hoverSprite = new Sprite(hoverFilename);

        buttonSprite.SetOrigin(buttonSprite.width / 2, buttonSprite.height / 2);
        buttonSprite.width = pWidth;
        buttonSprite.height = pHeight;

        hoverSprite.SetOrigin(hoverSprite.width / 2, hoverSprite.height / 2);
        hoverSprite.width = pWidth;
        hoverSprite.height = pHeight;
        hoverSprite.visible = false;

        AddChild(buttonSprite);
        AddChild(hoverSprite);

        action = pAction;
        myGame = game.FindObjectOfType<MyGame>();

        UpdateScreenPosition();

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
        Hover();
    }

    void Click()
    {
        float mouseX = Input.mouseX;
        float mouseY = Input.mouseY;
        if (buttonSprite.HitTestPoint(mouseX, mouseY) || hoverSprite.HitTestPoint(mouseX, mouseY))
        {
            if (Input.GetMouseButtonDown(0))
            {
                DoAction();
                Console.WriteLine("pressed");
            }
        }
    }

    void Hover()
    {
        float mouseX = Input.mouseX;
        float mouseY = Input.mouseY;
        if (buttonSprite.HitTestPoint(mouseX, mouseY) || hoverSprite.HitTestPoint(mouseX, mouseY))
        {
            buttonSprite.visible = false;
            hoverSprite.visible = true;
        }
        else
        {
            buttonSprite.visible = true;
            hoverSprite.visible = false;
        }
    }

    void DoAction()
    {
        QuitAction();
        PlayAction();
        RestartAction();
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

    void RestartAction()
    {
        if (action == "restart")
        {
            myGame.LoadFirstLevel();
            Console.WriteLine("switched to first level");
        }
    }
}