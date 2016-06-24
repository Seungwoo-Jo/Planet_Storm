using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Scene_CharacterSelect : MonoBehaviour {

    private bool m_bSound = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Select(int CharacterNum)
    {
        StartCoroutine("NextScene");
        PlayerManager.Instance.m_Character = CharacterNum;
    }

    IEnumerator NextScene()
    {
        transform.Find("CharacterButton0").GetComponent<Animator>().SetTrigger("Disabled");
        transform.Find("CharacterButton1").GetComponent<Animator>().SetTrigger("Disabled");
        transform.Find("CharacterButton2").GetComponent<Animator>().SetTrigger("Disabled");
        transform.Find("CharacterButton3").GetComponent<Animator>().SetTrigger("Disabled");
        transform.Find("CharacterButton4").GetComponent<Animator>().SetTrigger("Disabled");

        GetComponent<Animator>().SetBool("NextScene", true);
        yield return new WaitForSeconds(4.5f);

        //Application.LoadLevel("Stage_Select");
        SceneManager.LoadScene("Stage_Select");
        yield return null;
    }

    

    public void CanSelect()
    {
        m_bSound = true;
    }

    public void CantSelect()
    {
        m_bSound = false;
    }

    public bool GetSoundPlay()
    {
        return m_bSound;
    }
}