using System.Collections;
using UnityEngine;

public class Scale : MonoBehaviour
{
    [SerializeField] private float _scaleSpeed;

    private float _scaleSpeedDivisor = 100;
    private Vector3 _startScale;
    private Vector3 _maxScale = new Vector3(1.8f, 1.8f, 1.8f);
    private Vector3 _additionScale;
    private Coroutine _changeSize;

    private void Awake()
    {
        _startScale = transform.localScale;
        _additionScale = new Vector3(_scaleSpeed / _scaleSpeedDivisor, _scaleSpeed / _scaleSpeedDivisor,
            _scaleSpeed / _scaleSpeedDivisor);
    }

    private void Start()
    {
        if (_changeSize != null)
            StopCoroutine(_changeSize);

        _changeSize = StartCoroutine(ChangeSize());
    }

    private IEnumerator ChangeSize()
    {
        var waitFixedUpdate = new WaitForFixedUpdate();
        bool isPlaying = true;
        bool isMaxScale = false;

        while (isPlaying)
        {
            if (transform.localScale.x >= _maxScale.x)
                isMaxScale = true;
            else if (transform.localScale.x <= _startScale.x)
                isMaxScale = false;

            if (isMaxScale)
                transform.localScale -= _additionScale;
            else
                transform.localScale += _additionScale;

            yield return waitFixedUpdate;
        }
    }
}