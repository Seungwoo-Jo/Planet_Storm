using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class ModuleSelect : MonoBehaviour {

    public Button _AtkModule;
    public Button _DefModule;
    public Button _SpdModule;
    public Button _StartButton;

    public float _LerpDuration = 1.0f;

    private float _LerpTime;
    private Vector3 _IconSrc;
    private Vector3 _IconDest;
    private Vector3 _IconPrev;

    private int _curIndex = -1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(_curIndex != -1) 
        {
            if(Input.GetButtonDown("Cancel")) {
                Deselect(_curIndex);
            }
        }
        else if(Input.GetButton("Submit") && !GetComponent<Animator>().GetBool("NextScene"))
        {
            GetComponent<Animator>().SetBool("NextScene", true);
            StartCoroutine("NextScene");
        }
	}

    public void Select(int index)
    {
        if(index == 0) {
            StartCoroutine("AtkModuleSelect");
        }
        else if(index == 1) {
            StartCoroutine("DefModuleSelect");
        }
        else if(index == 2) {
            StartCoroutine("SpdModuleSelect");
        }
        else if(index == 3) {
            StartCoroutine("StartSelect");
        }
        else {
            _curIndex = -1;
        }
    }

    void Deselect(int CurIndex)
    {
        if(CurIndex == 0) {
            StartCoroutine("AtkModuleDeselect");
        }
        else if(CurIndex == 1) {
            StartCoroutine("DefModuleDeselect");
        }
        else if(CurIndex == 2) {
            StartCoroutine("SpdModuleDeselect");
        }
        else {
            _curIndex = -1;
        }

        _curIndex = -1;
    }


    IEnumerator AtkModuleSelect()
    {
        _LerpTime = 0.0f;
        _IconSrc = _AtkModule.transform.Find("Icon").transform.position;
        _IconDest = _AtkModule.transform.Find("Icon").transform.position;
        _IconPrev = _AtkModule.transform.Find("Icon").transform.position;
        EventSystem.current.sendNavigationEvents = false;
        _AtkModule.transform.Find("Focus").gameObject.SetActive(true);

        while(_LerpTime <= _LerpDuration) {
            Color _color = Color.Lerp(new Color(0.7f, 0.7f, 0.7f, 1.0f), Color.clear, _LerpTime);
            _AtkModule.transform.Find("Image").transform.position = Vector3.Lerp(_AtkModule.transform.position, _AtkModule.transform.position, _LerpTime);
            _AtkModule.transform.Find("Icon").transform.position = Vector3.Lerp(_IconSrc, _IconDest, _LerpTime);
            transform.Find("Explain").gameObject.GetComponent<Image>().color = Color.white - _color;
            _DefModule.transform.Find("Image").gameObject.GetComponent<Image>().color = _color;
            _DefModule.transform.Find("Icon").gameObject.GetComponent<Image>().color = _color;
            _SpdModule.transform.Find("Image").gameObject.GetComponent<Image>().color = _color;
            _SpdModule.transform.Find("Icon").gameObject.GetComponent<Image>().color = _color;
            _StartButton.transform.Find("Image").gameObject.GetComponent<Image>().color = Color.Lerp(new Color(0.03f, 1.0f, 1.0f, 1.0f), Color.clear, _LerpTime);
            _LerpTime += Time.deltaTime * _LerpDuration;
            yield return null;
        }
        _AtkModule.transform.Find("Icon").transform.position = _IconDest;
        _StartButton.transform.Find("Image").gameObject.GetComponent<Image>().color = Color.clear;
        _curIndex = 0;
    }

    IEnumerator DefModuleSelect()
    {
        _LerpTime = 0.0f;
        _IconSrc = _DefModule.transform.Find("Icon").transform.position;
        _IconDest = _AtkModule.transform.Find("Icon").transform.position;
        _IconPrev = _DefModule.transform.Find("Icon").transform.position;
        EventSystem.current.sendNavigationEvents = false;
        _DefModule.transform.Find("Focus").gameObject.SetActive(true);

        while(_LerpTime <= _LerpDuration) {
            Color _color = Color.Lerp(new Color(0.7f, 0.7f, 0.7f, 1.0f), Color.clear, _LerpTime);
            _DefModule.transform.Find("Image").transform.position = Vector3.Lerp(_DefModule.transform.position, _AtkModule.transform.position, _LerpTime);
            _DefModule.transform.Find("Icon").transform.position = Vector3.Lerp(_IconSrc, _IconDest, _LerpTime);
            transform.Find("Explain").gameObject.GetComponent<Image>().color = Color.white - _color;
            _AtkModule.transform.Find("Image").gameObject.GetComponent<Image>().color = _color;
            _AtkModule.transform.Find("Icon").gameObject.GetComponent<Image>().color = _color;
            _SpdModule.transform.Find("Image").gameObject.GetComponent<Image>().color = _color;
            _SpdModule.transform.Find("Icon").gameObject.GetComponent<Image>().color = _color;
            _StartButton.transform.Find("Image").gameObject.GetComponent<Image>().color = Color.Lerp(new Color(0.03f, 1.0f, 1.0f, 1.0f), Color.clear, _LerpTime);
            _LerpTime += Time.deltaTime * _LerpDuration;
            yield return null;
        }
        _DefModule.transform.Find("Icon").transform.position = _IconDest;
        _StartButton.transform.Find("Image").gameObject.GetComponent<Image>().color = Color.clear;
        _curIndex = 1;
    }

    IEnumerator SpdModuleSelect()
    {
        _LerpTime = 0.0f;
        _IconSrc = _SpdModule.transform.Find("Icon").transform.position;
        _IconDest = _AtkModule.transform.Find("Icon").transform.position;
        _IconPrev = _SpdModule.transform.Find("Icon").transform.position;
        EventSystem.current.sendNavigationEvents = false;
        _SpdModule.transform.Find("Focus").gameObject.SetActive(true);

        while(_LerpTime <= _LerpDuration) {
            Color _color = Color.Lerp(new Color(0.7f, 0.7f, 0.7f, 1.0f), Color.clear, _LerpTime);
            _SpdModule.transform.Find("Image").transform.position = Vector3.Lerp(_SpdModule.transform.position, _AtkModule.transform.position, _LerpTime);
            _SpdModule.transform.Find("Icon").transform.position = Vector3.Lerp(_IconSrc, _IconDest, _LerpTime);
            transform.Find("Explain").gameObject.GetComponent<Image>().color = Color.white - _color;
            _AtkModule.transform.Find("Image").gameObject.GetComponent<Image>().color = _color;
            _AtkModule.transform.Find("Icon").gameObject.GetComponent<Image>().color = _color;
            _DefModule.transform.Find("Image").gameObject.GetComponent<Image>().color = _color;
            _DefModule.transform.Find("Icon").gameObject.GetComponent<Image>().color = _color;
            _StartButton.transform.Find("Image").gameObject.GetComponent<Image>().color = Color.Lerp(new Color(0.03f, 1.0f, 1.0f, 1.0f), Color.clear, _LerpTime);
            _LerpTime += Time.deltaTime * _LerpDuration;
            yield return null;
        }
        _SpdModule.transform.Find("Icon").transform.position = _IconDest;
        _StartButton.transform.Find("Image").gameObject.GetComponent<Image>().color = Color.clear;
        _curIndex = 2;
    }

    IEnumerator AtkModuleDeselect()
    {
        _LerpTime = 0.0f;
        _IconSrc = _AtkModule.transform.Find("Icon").transform.position;
        _IconDest = _IconPrev;

        while(_LerpTime <= _LerpDuration) {
            Color _color = Color.Lerp(Color.clear, new Color(0.7f, 0.7f, 0.7f, 1.0f), _LerpTime);
            _AtkModule.transform.Find("Image").transform.position = Vector3.Lerp(_AtkModule.transform.position, _AtkModule.transform.position, _LerpTime);
            _AtkModule.transform.Find("Icon").transform.position = Vector3.Lerp(_IconSrc, _IconDest, _LerpTime);
            transform.Find("Explain").gameObject.GetComponent<Image>().color = Color.white - _color;
            _DefModule.transform.Find("Image").gameObject.GetComponent<Image>().color = _color;
            _DefModule.transform.Find("Icon").gameObject.GetComponent<Image>().color = _color;
            _SpdModule.transform.Find("Image").gameObject.GetComponent<Image>().color = _color;
            _SpdModule.transform.Find("Icon").gameObject.GetComponent<Image>().color = _color;
            _StartButton.transform.Find("Image").gameObject.GetComponent<Image>().color = Color.Lerp(Color.clear, new Color(0.03f, 1.0f, 1.0f, 1.0f), _LerpTime);
            _LerpTime += Time.deltaTime * _LerpDuration;
            yield return null;
        }
        EventSystem.current.sendNavigationEvents = true;
        _AtkModule.transform.Find("Icon").transform.position = _IconDest;
        _AtkModule.transform.Find("Focus").gameObject.SetActive(false);
        _StartButton.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(0.03f, 1.0f, 1.0f, 1.0f);
        _AtkModule.gameObject.GetComponent<Animator>().SetTrigger("Highlighted");
    }

    IEnumerator DefModuleDeselect()
    {
        _LerpTime = 0.0f;
        _IconSrc = _AtkModule.transform.Find("Icon").transform.position;
        _IconDest = _IconPrev;

        while(_LerpTime <= _LerpDuration) {
            Color _color = Color.Lerp(Color.clear, new Color(0.7f, 0.7f, 0.7f, 1.0f), _LerpTime);
            _DefModule.transform.Find("Image").transform.position = Vector3.Lerp(_AtkModule.transform.position, _DefModule.transform.position, _LerpTime);
            _DefModule.transform.Find("Icon").transform.position = Vector3.Lerp(_IconSrc, _IconDest, _LerpTime);
            transform.Find("Explain").gameObject.GetComponent<Image>().color = Color.white - _color;
            _AtkModule.transform.Find("Image").gameObject.GetComponent<Image>().color = _color;
            _AtkModule.transform.Find("Icon").gameObject.GetComponent<Image>().color = _color;
            _SpdModule.transform.Find("Image").gameObject.GetComponent<Image>().color = _color;
            _SpdModule.transform.Find("Icon").gameObject.GetComponent<Image>().color = _color;
            _StartButton.transform.Find("Image").gameObject.GetComponent<Image>().color = Color.Lerp(Color.clear, new Color(0.03f, 1.0f, 1.0f, 1.0f), _LerpTime);
            _LerpTime += Time.deltaTime * _LerpDuration;
            yield return null;
        }
        EventSystem.current.sendNavigationEvents = true;
        _DefModule.transform.Find("Icon").transform.position = _IconDest;
        _DefModule.transform.Find("Focus").gameObject.SetActive(false);
        _StartButton.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(0.03f, 1.0f, 1.0f, 1.0f);
        _DefModule.gameObject.GetComponent<Animator>().SetTrigger("Highlighted");
    }

    IEnumerator SpdModuleDeselect()
    {
        _LerpTime = 0.0f;
        _IconSrc = _AtkModule.transform.Find("Icon").transform.position;
        _IconDest = _IconPrev;

        while(_LerpTime <= _LerpDuration) {
            Color _color = Color.Lerp(Color.clear, new Color(0.7f, 0.7f, 0.7f, 1.0f), _LerpTime);
            _SpdModule.transform.Find("Image").transform.position = Vector3.Lerp(_AtkModule.transform.position, _SpdModule.transform.position, _LerpTime);
            _SpdModule.transform.Find("Icon").transform.position = Vector3.Lerp(_IconSrc, _IconDest, _LerpTime);
            transform.Find("Explain").gameObject.GetComponent<Image>().color = Color.white - _color;
            _AtkModule.transform.Find("Image").gameObject.GetComponent<Image>().color = _color;
            _AtkModule.transform.Find("Icon").gameObject.GetComponent<Image>().color = _color;
            _DefModule.transform.Find("Image").gameObject.GetComponent<Image>().color = _color;
            _DefModule.transform.Find("Icon").gameObject.GetComponent<Image>().color = _color;
            _StartButton.transform.Find("Image").gameObject.GetComponent<Image>().color = Color.Lerp(Color.clear, new Color(0.03f, 1.0f, 1.0f, 1.0f), _LerpTime);
            _LerpTime += Time.deltaTime * _LerpDuration;
            yield return null;
        }
        EventSystem.current.sendNavigationEvents = true;
        _SpdModule.transform.Find("Icon").transform.position = _IconDest;
        _SpdModule.transform.Find("Focus").gameObject.SetActive(false);
        _StartButton.transform.Find("Image").gameObject.GetComponent<Image>().color = new Color(0.03f, 1.0f, 1.0f, 1.0f);
        _SpdModule.gameObject.GetComponent<Animator>().SetTrigger("Highlighted");
    }

    IEnumerator StartSelect()
    {
        _curIndex = 3;
        print("NextScene");
        GetComponent<Animator>().SetTrigger("NextScene");
        yield return new WaitForSeconds(5.0f);
        //Application.LoadLevel("Stage_Select");
        SceneManager.LoadScene("Stage_Select");
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(1.5f);
        //Application.LoadLevel("Stage_Select");
        SceneManager.LoadScene("Stage_Select");
    }
}
