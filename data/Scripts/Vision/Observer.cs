using System;
using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "d93e167b338e199ef94934652de3da80b0900bdf")]
public class Observer : Component
{
	public Unigine.Object Child = null;
	public Node Parent = null;

	private void Init()
	{
		// write here code to be called on component initialization
		
	}
	
	private void Update()
	{
		// write here code to be called before updating each render frame

		Parent.WorldPosition = Child.WorldPosition;
		
	}
}