using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Score : MonoBehaviour {

    private float _CurScore;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(ScoreManager.Instance.GetScore() > _CurScore) {
            _CurScore = Mathf.Lerp(_CurScore, ScoreManager.Instance.GetScore(), Time.deltaTime * 5.0f);
        }
        else {
            _CurScore = ScoreManager.Instance.GetScore();
        }

        GetComponent<Text>().text = _CurScore.ToString("##########");
	}
}
