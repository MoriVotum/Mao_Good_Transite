using System;
using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "c2f1131b0decadbe363b7a059a8f23a09885827f")]
public class Trigger : Component
{
	vec4 color;
	public Shoot shoot;

	void EnterCallback(Body body)
	{
		//color = body.Object.GetMaterialInherit(0).GetParameterFloat4("albedo_color");
		//body.Object.GetMaterialInherit(0).SetParameterFloat4("albedo_color",vec4.BLACK);
		if(body.Object.RootNode.Name == "dynamic_content")
		{
			shoot.trackingObject(false);
			vec3 o = physicalTrigger.WorldPosition;
			body.Object.WorldPosition = o + new vec3(0,0,1);
		}
	}

	void LeaveCallback(Body body)
	{
		if(body.Object.RootNode.Name == "dynamic_content")
		{
		//body.Object.GetMaterialInherit(0).SetParameterFloat4("albedo_color",color);
		}
	}

PhysicalTrigger physicalTrigger;

	private void Init()
	{
		// write here code to be called on component initialization
		physicalTrigger = node as PhysicalTrigger;
		if (physicalTrigger!=null)
		{
			physicalTrigger.AddEnterCallback(EnterCallback);
			physicalTrigger.AddLeaveCallback(LeaveCallback);
		}
	}
	
	private void Update()
	{
		// write here code to be called before updating each render frame
		Visualizer.RenderBoundSphere(node.WorldBoundSphere, mat4.IDENTITY, vec4.BLUE);
		
	}
}