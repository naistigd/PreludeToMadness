using UnityEngine;
using System.Collections;

public class CompanyLogo : MonoBehaviour 
{

	public BackgroundTexture Background;
	
	Fader fader;
	
	void Start () 
	{
		fader = new Fader(1.0f, 0.0f, 0.5f);
		fader.FadeOut();
	}
	
	void Update () 
	{
		if (Time.time >	 2.0)
		{
			fader.Update(Time.deltaTime);
		}
		
		if (fader.GetAlpha() == 0.0f) 
		{
			Application.LoadLevel(1);	
		}
		
		Background.SetAlpha(fader.GetAlpha());
		
	}
}
