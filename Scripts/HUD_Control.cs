using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD_Control : MonoBehaviour {

    public GameObject _Player;
    public float[] _PowerCursorRotation;
    public float _PowerCursorSpeedMultiply = 1.0f;


    private Image _LeftCursor;
    private Image _RightCursor;
    private int _CurPower = 0;
    private int _NextPower = 0;
    private bool _GetPower = false;

	// Use this for initialization
	void Start () {
        _LeftCursor = transform.Find("Canvas/Cursor/Left").GetComponent<Image>();
        _RightCursor = transform.Find("Canvas/Cursor/Right").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if(_CurPower != _Player.GetComponent<Animator>().GetInteger("iPower") && !_GetPower) {
            _NextPower = _Player.GetComponent<Animator>().GetInteger("iPower");
            StartCoroutine("RightCursorMove");
        }
	}

    IEnumerator RightCursorMove()
    {
        float LerpTime = 0.0f;
        float DeltaAngle = (_PowerCursorRotation[_NextPower] - _PowerCursorRotation[_CurPower]) * (Time.deltaTime * _PowerCursorSpeedMultiply);
        _GetPower = true;

        while(LerpTime < 1.0f) {

            _RightCursor.rectTransform.Rotate(Vector3.forward, DeltaAngle);
            LerpTime += Time.deltaTime * _PowerCursorSpeedMultiply;
            yield return null;
        }
        _RightCursor.rectTransform.rotation = Quaternion.AngleAxis(_PowerCursorRotation[_NextPower], Vector3.forward);
        
        _CurPower = _NextPower;
        _GetPower = false;
    }
}
