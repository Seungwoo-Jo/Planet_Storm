using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour 
{
    public Button m_Continue;
    public Button m_NewGame;
    public Button m_Setting;
    public Button m_Exit;

	// Use this for initialization
	void Start () 
	{
	}

	public void StartAnimation ()
	{
        Camera.main.GetComponent<Animator>().SetBool("Next", true);
	}

	public void ExitGame ()
	{
		Application.Quit ();
	}

}
