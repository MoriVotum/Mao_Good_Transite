using System;
using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "9f2a48e548fce5001184945f708f56327af03fe9")]
public class TrigerObjects : Component
{
	// list of objects with objects world position
	struct ObjectPosition
	{
		public string name;
		// public Unigine.Object Object;
		public vec3 position;
	}

	List<ObjectPosition> triggerObjectsCollection = new List<ObjectPosition>();

	private void Init()
	{
		// write here code to be called on component initialization
		
	}
	
	private void Update()
	{
		// write here code to be called before updating each render frame
		
	}
}