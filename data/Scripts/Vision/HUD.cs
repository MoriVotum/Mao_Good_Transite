using System;
using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "77438698575559176082a38a28b43d611c83b74c")]
public class HUD : Component
{
	// crosshair parameters
	[ParameterFile]
	public string crosshairImage = null;
	public int crosshairSize = 16;

	private WidgetSprite sprite = null;

	private void Init()
	{

		// get an instance of the screen Gui
		//Gui screenGui = Gui.GetCurrent();
		Gui screenGui = Gui.Get();
		
		// add a sprite widget
		sprite = new WidgetSprite(screenGui, crosshairImage);
		// set the sprite size
		sprite.Width = crosshairSize;
		sprite.Height = crosshairSize;

		screenGui.AddChild(sprite, Gui.ALIGN_CENTER | Gui.ALIGN_OVERLAP);
	}
	
	private void Update()
	{
		// write here code to be called before updating each render frame

		int width = App.GetWidth();
		int height = App.GetHeight();

		sprite.SetPosition(width / 2 - 8, height / 2 - 8);
	}
}