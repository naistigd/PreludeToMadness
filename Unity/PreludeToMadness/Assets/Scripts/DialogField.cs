using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// class that displays text during a certain time span relative from the current
// point in time as specified by [start] and [end] in the [Display] method.
public class DialogField : MonoBehaviour 
{
	class TextTimeTuple
	{
		public string Text;
		public Color Color;
		public float Start;	// time when the text should be display or erased
		public float End;
	}
	
	
	public GUISkin DialogGUISkin;
	
	private List<TextTimeTuple> futureText_;
	private float time_ = 0.0f;
	private string currentText_ = "";
	private Color currentColor_;
	private float eraseTime_ = 0.0f;
	
	//-------------------------------------------------------------------------
	void Awake()
	{
		futureText_ = new List<TextTimeTuple>();
	}
	//-------------------------------------------------------------------------
	void Update()
	{
		time_ += Time.deltaTime;
		
		// reset time if possible for precision
		if (futureText_.Count == 0 && currentText_ == "")
		{
			time_ = 0.0f;
		}
		
		// check if some of the future text should be displayed
		foreach (TextTimeTuple ttt in futureText_)
		{
			if (ttt.Start <= time_)
			{
				//futureText_.Remove(ttt);	
				currentText_ = ttt.Text;
				currentColor_ = ttt.Color;
				eraseTime_ = ttt.End;
			}
		}
		
		futureText_.RemoveAll(
			delegate(TextTimeTuple ttt) 
			{
    			return ttt.Start <= time_;
			}
		);		
		
		// check if the current text should be erased
		if (time_ >= eraseTime_)
		{
			currentText_ = "";
		}
	}
	//-------------------------------------------------------------------------
	public void Display(string text, Color color, float start, float end)
	{
		// SET [text] TO BE DISPLAYED AFTER [start] AND TO BE ERASED AFTER [end]
		// SECONDS
		
		TextTimeTuple ttt = new TextTimeTuple();
		ttt.Text = text;
		ttt.Color = color;
		ttt.Start = time_ + start;
		ttt.End = time_ + end;
		futureText_.Add(ttt);
	}
	//-------------------------------------------------------------------------
	void OnGUI()
	{
		Rect r = new Rect(0, 0, Screen.width, 100);
		GUI.skin = DialogGUISkin;
		GUI.skin.label.normal.textColor = currentColor_;
		GUI.Label(r, currentText_);
	}
	//-------------------------------------------------------------------------
}
