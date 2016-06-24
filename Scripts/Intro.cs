using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Intro : MonoBehaviour {

    private float m_Time = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        m_Time += Time.deltaTime;
        /*
        if(m_Time >= 8.75f) {
            Application.LoadLevel("Title");
        }
         * */
	}

    void NextScene()
    {
        GameObject.Find("/Canvas").GetComponent<Animator>().SetBool("NextScene", true);
        StartCoroutine("NextLevel");

    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Title");
    }
}
