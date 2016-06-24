using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Repair : MonoBehaviour {

    private Text m_Text;
    private string m_BaseString = "Seconds...";

    private float m_RemainTime;

    private bool m_bDoing = false;

	// Use this for initialization
	void Start () {
        m_Text = transform.Find("Text").gameObject.GetComponent<Text>();
        m_Text.gameObject.SetActive(false);
        GetComponent<Image>().color = Color.clear;
	}
	
	// Update is called once per frame
	void Update () {
        if(PlayerManager.Instance.m_bDead && !m_bDoing) {
            StartCoroutine("Fixed");
        }
	}

    IEnumerator Fixed()
    {
        m_RemainTime = PlayerManager.Instance.DeadTime;
        m_Text.gameObject.SetActive(true);
        m_bDoing = true;
        yield return null;

        while(m_RemainTime > 0.0f) {
            GetComponent<Image>().color = Color.Lerp(Color.clear, Color.black, (m_RemainTime / PlayerManager.Instance.DeadTime));

            m_RemainTime -= Time.deltaTime;
            if(m_RemainTime < 0.0f) {
                m_RemainTime = 0.0f;
            }
            if(m_RemainTime >= 1.0f) {
                m_Text.text = "Remain\n" + m_RemainTime.ToString("##.###") + " " + m_BaseString;
            }
            else {
                m_Text.text = "Remain\n" + "0" + m_RemainTime.ToString(".###") + " " + m_BaseString;
            }
            
            yield return null;
        }

        GetComponent<Image>().color = Color.clear;
        m_Text.gameObject.SetActive(false);
        PlayerManager.Instance.m_bDead = false;
        m_bDoing = false;
    }
}
