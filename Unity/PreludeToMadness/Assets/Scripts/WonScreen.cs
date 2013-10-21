using UnityEngine;
using System.Collections;

public class WonScreen : MonoBehaviour 
{
	public TextureButton AgainButton;
	public TextureButton ExitButton;
	
	Fader fader;
	
	//-------------------------------------------------------------------------
	void Awake()
	{
		fader = new Fader(0.0f, 3.0f, 0.0f);
		AgainButton.SetScale(0.30f);
		ExitButton.SetScale(0.30f);
		float x = Screen.width - ExitButton.GetWidth();
		AgainButton.SetPosition(new Vector2(x - 405, 655.0f));
		ExitButton.SetPosition(new Vector2(x - 100, 655.0f));
	}
	//-------------------------------------------------------------------------
	void Start () 
	{
		fader.FadeIn();
	}
	//-------------------------------------------------------------------------
	void Update () 
	{
		fader.Update(Time.deltaTime);
		AgainButton.SetAlpha(fader.GetAlpha());
		ExitButton.SetAlpha(fader.GetAlpha());
		
		if (AgainButton.IsPressed() && fader.GetAlpha() == 1.0f)
		{
			Application.LoadLevel(3);
		}
		
		if (ExitButton.IsPressed() && fader.GetAlpha() == 1.0f)
		{
			Application.Quit();
		}
	}
	//-------------------------------------------------------------------------
	void OnGUI()
	{

	}
	//-------------------------------------------------------------------------
}
