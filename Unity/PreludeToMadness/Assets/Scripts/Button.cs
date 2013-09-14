using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public int levelID = 0;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnMouseDown()
	{
		Application.LoadLevel(levelID);
	}
}
