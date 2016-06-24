using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Title_Camera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void NextScene()
    {
        StartCoroutine("NextAnimation");
    }

    IEnumerator NextAnimation()
    {
        transform.Find("Canvas").GetComponent<Animator>().SetBool("NextScene", true);
        yield return new WaitForSeconds(4.0f);
        //Application.LoadLevel("Character_Select");
        SceneManager.LoadScene("Character_Select");
    }
}
