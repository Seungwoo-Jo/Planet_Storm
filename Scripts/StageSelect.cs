using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour {

    public Sprite _Normal;
    public Sprite _Select;

    private Image _LeftButton;
    private Image _RightButton;

    private Text _StageText;

    private int _StageIndex = 1;
    private bool _NextScene = false;

	// Use this for initialization
	void Start () {
        _LeftButton = transform.Find("Arrow/Left").GetComponent<Image>();
        _RightButton = transform.Find("Arrow/Right").GetComponent<Image>();

        _StageText = transform.Find("StageText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown("left")) {
            _LeftButton.sprite = _Select;
            _StageIndex -= 1;
        }
        else if(Input.GetKeyDown("right")) {
            _RightButton.sprite = _Select;
            _StageIndex += 1;
        }
        else {
            _LeftButton.sprite = _Normal;
            _RightButton.sprite = _Normal;
        }

        _StageIndex = Mathf.Clamp(_StageIndex, 1, 2);

        _StageText.text = "Stage 0" + _StageIndex.ToString();

        if(Input.GetButton("Submit") && !_NextScene) {
            _NextScene = true;
            GetComponent<Animator>().SetBool("NextScene", true);
            StartCoroutine("Load");
        }
	}

    IEnumerator Load() {
        yield return new WaitForSeconds(2.0f);
        string level = "Level0" + _StageIndex.ToString();
        print(level);
        
        //Application.LoadLevel(level);
        SceneManager.LoadScene(level);
    }

    public int GetStageIndex()
    {
        return _StageIndex;
    }
}
