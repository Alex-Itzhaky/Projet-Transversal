using System.Collections;
using UnityEngine;

public class SelectBoatShader : MonoBehaviour
{
    [SerializeField] private Color _selectColor;
    [SerializeField] private float _selectOpacity;
    [SerializeField] private float _selectTime;
    [SerializeField] private AnimationCurve _selectAnimCurve;
    [SerializeField] private AnimationCurve _unselectAnimCurve;
    private Material _material;
    private SpriteRenderer _spriteRender;

    private void Awake()
    {
        _spriteRender = GetComponent<SpriteRenderer>();
        Init();
    }

    private void Init()
    {
        _material = _spriteRender.material;
        SetOpacity(0f);
    }

    public void CallSelect()
    {
        StartCoroutine(SelectCoroutine());
    }

    public void CallUnselect()
    {
        StartCoroutine(UnselectCoroutine());
    }

    private IEnumerator SelectCoroutine()
    {
        Debug.Log("SelectShader");
        _material.SetColor("_SelectColor", _selectColor);


        float currentOpacity = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < _selectTime)
        {
            elapsedTime += Time.deltaTime;
            currentOpacity = Mathf.Lerp(_selectOpacity, _selectAnimCurve.Evaluate(elapsedTime), (elapsedTime / _selectTime));
            SetOpacity(currentOpacity);
            yield return null;
        }
    }

    private IEnumerator UnselectCoroutine()
    {
        Debug.Log("UnselectShader");
        _material.SetColor("_SelectColor", _selectColor);

        float currentOpacity = _selectOpacity;
        float elapsedTime = 0f;

        while (elapsedTime < _selectTime)
        {
            elapsedTime += Time.deltaTime;
            currentOpacity = Mathf.Lerp(0f, _unselectAnimCurve.Evaluate(elapsedTime), (elapsedTime / _selectTime));
            SetOpacity(currentOpacity);
            yield return null;
        }
    }

    private void SetOpacity(float amount)
    {
        _material.SetFloat("_OpacityAmount", amount);
    }
}
