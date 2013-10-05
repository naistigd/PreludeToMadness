using UnityEngine;
using System.Collections;

public class BackgroundTexture : MonoBehaviour {
	
	public Texture BackgroundImage;
	private float alpha_ = 1.0f;
	private int depth_ = 0;
	
	
	void Awake() 
	{
		
	}
	
	void Update() 
	{
	
	}
	
	void OnGUI()
	{
		if (alpha_ <= 0.0f)
		{
			return;
		}
		
		Rect r = new Rect(0, 0, Screen.width, Screen.height);
		
		GUI.color = new Color(1.0f, 1.0f, 1.0f, alpha_);
		GUI.depth = depth_;
		GUI.DrawTexture(r, BackgroundImage);		
	}
	
	public void SetDepth(int d)
	{
		depth_ = d;
	}
	
	public void SetAlpha(float a)
	{
		alpha_ = a;
	}	
	
	public float GetAlpha()
	{
		return alpha_;
	}
}
