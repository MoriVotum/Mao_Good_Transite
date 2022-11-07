using System;
using System.Collections;
using System.Collections.Generic;
using Unigine;


[Component(PropertyGuid = "02f4d20b71a0ffee25d2c67584a2a550eb5f386f")]
public class Shoot : Component
{
	struct ObjectPosition
	{
		public string name;
		// public Unigine.Object Object;
		public vec3 position;
	}

	List<ObjectPosition> triggerObjectsCollection = new List<ObjectPosition>();

	public PlayerDummy shootingCamera = null;
	public ShootInput shootInput = null;

	// public TrigerObjects trigerObjects = null;

	// Корректное выделение объекта
	static float lastVector;
	static vec3 deltaObject;

	Unigine.Object mainHitObject;

	[ParameterMask(MaskType = ParameterMaskAttribute.TYPE.INTERSECTION)]
	public int mask = ~0;

	vec3 setDirection;

	// massive data when store objects and objects`s position in the world

	// create a counter to show the message once

	private void Init()
	{
		// write here code to be called on component initialization
		Visualizer.Enabled = true;
	}


	public void CreateNewWorld()
	{
		Log.Message("New world created\n");
	}


	public void DirectionShoot(Unigine.Object Object, vec3 p0, vec3 p1)
	{
		if (Object.RootNode.Name == "dynamic_content")
		{

			WorldIntersectionNormal hitInfo = new WorldIntersectionNormal();
			Unigine.Object hitObject = World.GetIntersection(p0, p1, mask, hitInfo);

			Object.WorldPosition = p0 - deltaObject + ((p1 - p0).Normalized * lastVector);

			// Direction ojbject to the hit point
			// Object.WorldDirection = (hitInfo.GetPosition() - Object.WorldPosition).Normalized;

			Visualizer.RenderMessage3D(Object.WorldPosition, new vec3 (0.0f, 0.0f, 1.0f), Object.Name, vec4.GREEN, 0, 25);

			// Visualizer.RenderMessage2D(Object.WorldPosition, new vec3 (0, 0, 1), Object.Name, vec4.GREEN, 0, 25);
		}
	}


	public void SelecteObject(Unigine.Object Object)
	{
		if (Object.RootNode.Name == "dynamic_content")
		{
			Visualizer.RenderObjectSurfaceBoundBox(Object, 0, vec4.BLUE, 0.05f);

			Visualizer.RenderMessage3D(Object.WorldPosition, new vec3 (0.0f, 0.0f, 1.0f), Object.Name, vec4.GREEN, 0, 25);
		}
	} 


	public void intersectionObject(bool choseObject)
	{
		vec3 p0, p1;
		shootingCamera.GetDirectionFromScreen(out p0, out p1);

		WorldIntersectionNormal hitInfo = new WorldIntersectionNormal();
		Unigine.Object hitObject = World.GetIntersection(p0, p1, mask, hitInfo);

		if (choseObject)
			DirectionShoot(mainHitObject, p0, p1);
		else if (hitObject)
			SelecteObject(hitObject);
	}


	public void Selection()
	{
		intersectionObject(false);
	}


	public void Shooting()
	{
		intersectionObject(true);
	}


	public void trackingObject(bool trackObject)
	{
		if (trackObject)
		{
			vec3 p0, p1;
			shootingCamera.GetDirectionFromScreen(out p0, out p1);

			WorldIntersectionNormal hitInfo = new WorldIntersectionNormal();
			Unigine.Object hitObject = World.GetIntersection(p0, p1, mask, hitInfo);

			if (hitObject)
			{
				if (hitObject.RootNode.Name == "dynamic_content")
				{
					// if in triggerObjectsCollection name located in the list then do nothing else add new object
					if (!triggerObjectsCollection.Exists(x => x.name == hitObject.Name))
					{
						ObjectPosition objectPosition = new ObjectPosition();
						objectPosition.name = hitObject.Name;
						objectPosition.position = hitObject.WorldPosition;
						triggerObjectsCollection.Add(objectPosition);

						Log.Message("Object " + hitObject.Name + " position: " + hitObject.WorldPosition + "\n");
					}

					lastVector = (hitObject.WorldPosition - p0).Length;
					deltaObject = (p1 - p0).Normalized*lastVector - (hitObject.WorldPosition - p0).Normalized*lastVector;

					// setdirection = (hitObject.WorldPosition - p0).Normalized; // Направление выстрела (по умолчанию) 

					setDirection = hitObject.GetDirection(); // Направление выстрела (по умолчанию) 

					mainHitObject = hitObject;
				}
			}
		}
		else
		{
			// if dictance between object and triggerObjectsCollection is less than 0.1 then object position is equal to triggerObjectsCollection position 
			if (triggerObjectsCollection.Exists(x => x.name == mainHitObject.Name))
			{
				// dictance between object and triggerObjectsCollection
				float dictance = (mainHitObject.WorldPosition - triggerObjectsCollection.Find(x => x.name == mainHitObject.Name).position).Length;
				if (dictance < 0.3f)
				{
					mainHitObject.WorldPosition = triggerObjectsCollection.Find(x => x.name == mainHitObject.Name).position;
				}
			}
			mainHitObject = null;
		}
	}


	private void Update()
	{
		if (Input.IsMouseButtonDown(Input.MOUSE_BUTTON.LEFT))
			trackingObject(true);

		if (Input.IsMouseButtonUp(Input.MOUSE_BUTTON.LEFT))
			trackingObject(false);

		if (shootInput.IsShooting())
		{
			Shooting();
		}
		else
		{
			Selection();
		}
	}
}