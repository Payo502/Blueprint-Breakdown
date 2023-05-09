using System;                                   
using GXPEngine;                                
using System.Drawing;                           

public class MyGame : Game {
	Player player;
	public MyGame() : base(800, 600, false)    
	{
		player = new Player(new Vec2(200,200),30);
		AddChild(player);

        AddChild(new Line(new Vec2(0, 500), new Vec2(800, 500)));
		AddChild(new Line(new Vec2(100, 0), new Vec2(100, 800)));
		AddChild(new Line(new Vec2(700, 0), new Vec2(700, 800)));
		AddChild(new Line(new Vec2(0, 100), new Vec2(800, 100)));

		AddChild(new MapObject(30, new Vec2(400, 400)));

        Console.WriteLine("Pee pee poo poo");
    }


	void Update() {
		// Empty
	}

	static void Main()                         
	{
		new MyGame().Start();                   
	}
}