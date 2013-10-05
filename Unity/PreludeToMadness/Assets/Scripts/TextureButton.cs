using UnityEngine;
using System.Collections;

public class TextureButton : MonoBehaviour 
{
	public Texture ButtonTex;
	public GUISkin GUISkin;
	
	public float scale_ = 1.0f;
	public Vector2 position_;
	public int depth_ = -1;	
	private bool isPressed_ = false;
	private float alpha_ = 1.0f;
	private bool isClickable_ = true;

	
	public void Awake()
	{
		
	}
	
	public void SetAlpha(float a)
	{
		alpha_ = a;
	}
	
	public float GetAlpha()
	{
		return alpha_;
	}
	
	public void SetScale(float s)
	{
		scale_ = s;
	}
		
	public void SetPosition(Vector2 pos)
	{
		position_ = pos;
	}
	
	public void SetIsClickable(bool isClickable)	
	{
		isClickable_ = isClickable;
	}
	
	public bool IsPressed()
	{
		return isPressed_;
	}
	
	public float GetWidth()
	{
		return ButtonTex.width*scale_;
	}

	public float GetHeight()
	{
		return ButtonTex.height*scale_;
	}
	
	void OnGUI()
	{
		
		float w = scale_*ButtonTex.width;
		float h = scale_*ButtonTex.height;
		
		
		Rect r = new Rect(position_.x, position_.y, w, h);
		
		GUI.depth = depth_;
		GUI.skin = GUISkin;
		GUI.color = new Color(1.0f, 1.0f, 1.0f, alpha_);
		
		
		if (GUI.Button(r, ButtonTex) && isClickable_)
		{
			isPressed_ = true;
		}
		else
		{
			isPressed_ = false;
		}
	}	
}
