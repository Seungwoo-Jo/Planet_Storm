using UnityEngine;
using System.Collections;

public class UI_CharacterSelect : MonoBehaviour {

    public AudioSource m_SelectSound;
    public Scene_CharacterSelect m_Parent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SelectSoundPlay()
    {
        if(m_Parent.GetSoundPlay()) {
            m_SelectSound.Play();
        }
        
    }
}
