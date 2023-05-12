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

    private Vec2 center;


    public Button(string filename, Vec2 pPosition, int pWidth, int pHeight) : base(filename)
    {
        SetOrigin(position.x / 2, position.y / 2); 
        position = pPosition;
        width = pWidth;
        height = pHeight;
        center = new Vec2();
    }


    void Update()
    {
        Click();
    }

    void Click()
    {
        Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
        float distanceToMouse = (center - mousePos).Length();
    }

}