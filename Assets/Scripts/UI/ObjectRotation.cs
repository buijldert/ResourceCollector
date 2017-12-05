/*
	ObjectRotation.cs
	Created 10/20/2017 3:31:24 PM
	Project Resource Collector by Base Games
*/
using UnityEngine;

namespace UI
{
	public class ObjectRotation : MonoBehaviour 
	{
        //private Sequence _spinSequence;

        //private float _timeToRotate = 0.55f;
        //private float _timeToScale;
        //private float _timeBetweenTween = 0.5f;

        private float _rotationSpeed = 60f;

        [SerializeField] private Vector3 _rotationDirection;

        private void Update()
        {
            transform.Rotate(_rotationDirection * Time.deltaTime * _rotationSpeed);
            //_timeToScale = _timeToRotate / 2;
            //SpinObject(); 
        }

        //void SpinObject()
        //{
        //    Debug.Log("you spin my head right round");
        //    _spinSequence = DOTween.Sequence();
        //    _spinSequence.AppendInterval(_timeBetweenTween);
        //    _spinSequence.Append(transform.DORotate(new Vector3(0, 0, 180), _timeToRotate));
        //    Sequence scaleSequence = DOTween.Sequence();
        //    scaleSequence.Append(transform.DOScale(Vector3.one * 1.5f, _timeToScale));
        //    scaleSequence.AppendInterval(.25f);
        //    scaleSequence.Append(transform.DOScale(Vector3.one * 1.25f, _timeToScale));
        //    scaleSequence.SetLoops(1);
        //    _spinSequence.Join(scaleSequence);
        //    _spinSequence.AppendInterval(_timeBetweenTween);
        //    _spinSequence.Append(transform.DORotate(new Vector3(0, 0, 360), _timeToRotate));
        //    _spinSequence.SetLoops(-1, LoopType.Restart);
        //}

    }
}