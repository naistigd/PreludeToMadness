using UnityEngine;
using System.Collections;

// makes a box fill out the full screen
public class FullSize : MonoBehaviour 
{
	void Awake()
	{
		float scaleX = 2.0f*Camera.main.orthographicSize*Camera.main.aspect;
		float scaleY = 2.0f*Camera.main.orthographicSize;
		Vector3 scale = new Vector3(scaleX, scaleY, Constants.SCALE_Z);
		transform.localScale = scale;
	}
	
	void Start() 
	{
	
	}
	
	void Update() 
	{
	
	}
}
