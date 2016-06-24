using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_CharacterFace : MonoBehaviour {

    public Sprite[] m_Face;

	// Use this for initialization
	void Start () {
        GetComponent<Image>().sprite = m_Face[PlayerManager.Instance.m_Character];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    
}
