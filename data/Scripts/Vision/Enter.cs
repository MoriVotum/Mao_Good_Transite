using System;
using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "941b8f43ba74ebc650a5d06dc08af6e87e85ed27")]
public class Enter : Component
{
	public Node player;

	private void Init()
	{
		// write here code to be called on component initialization
		
	}
	
	private void Update()
	{
		// write here code to be called before updating each render frame
		if (Input.IsKeyDown(Input.KEY.E))
		{
			player.WorldPosition = new vec3(0, 0, 5);
		}
	}
}