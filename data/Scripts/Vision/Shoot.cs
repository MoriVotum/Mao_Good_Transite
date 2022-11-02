using System;
using System.Collections;
using System.Collections.Generic;
using Unigine;


[Component(PropertyGuid = "02f4d20b71a0ffee25d2c67584a2a550eb5f386f")]
public class Shoot : Component
{
	public PlayerDummy shootingCamera = null;
	public ShootInput shootInput = null;

	// Корректное выделение объекта
	static float lastVector;
	static vec3 deltaObject;

	Unigine.Object mainHitObject;

	[ParameterMask(MaskType = ParameterMaskAttribute.TYPE.INTERSECTION)]
	public int mask = ~0;

	vec3 setDirection;

	// define world intersection instance

	// create a counter to show the message once

	private void Init()
	{
		// write here code to be called on component initialization
		
	}


	public void CreateNewWorld()
	{
		Log.Message("New world created\n");
	}


	public void DirectionShoot(Unigine.Object Object, vec3 p0, vec3 p1)
	{
		if (Object.RootNode.Name == "dynamic_content")
		{

			// if (Object.Name == "material_ball")
			// 	CreateNewWorld();

			WorldIntersectionNormal hitInfo = new WorldIntersectionNormal();
			Unigine.Object hitObject = World.GetIntersection(p0, p1, mask, hitInfo);

			Object.WorldPosition = p0 - deltaObject + ((p1 - p0).Normalized * lastVector);

			Visualizer.RenderMessage3D(Object.WorldPosition, 0, Object.Name, vec4.GREEN, 0, 25);
		}
	}


	public void SelecteObject(Unigine.Object Object)
	{
		if (Object.RootNode.Name == "dynamic_content")
		{
			Visualizer.RenderObjectSurfaceBoundBox(Object, 0, vec4.BLUE, 0.05f);

			Visualizer.RenderMessage3D(Object.WorldPosition, 0, Object.Name, vec4.GREEN, 0, 25);
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
			mainHitObject = null;
		}
	}


	private void Update()
	{
		Visualizer.Enabled = true;


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