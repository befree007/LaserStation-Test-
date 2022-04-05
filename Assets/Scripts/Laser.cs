using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
	[SerializeField]private int _reflections;
	[SerializeField]private float _maxLength;
	[SerializeField]private float _startPower;

    private List<GameObject> _listHitObject = new List<GameObject>();

    private LineRenderer _lineRenderer;
	private Ray _ray;
	private RaycastHit _hit;

	private void Awake()
	{
		_lineRenderer = GetComponent<LineRenderer>();
    }

	private void Update()
	{
		CreateRay();
	}

	public void CreateRay()
    {
		_ray = new Ray(transform.position, transform.up);

		_lineRenderer.positionCount = 1;
		_lineRenderer.SetPosition(0, transform.position);
		float remainingLength = _maxLength;

        for (int i = 0; i < _listHitObject.Count; i++)
        {
            _startPower += _listHitObject[i].GetComponent<Reflect>().indexReflect;
            _listHitObject.RemoveAt(i);
        }

        for (int i = 0; i < _reflections; i++)
        {
            if (Physics.Raycast(_ray.origin, _ray.direction, out _hit, remainingLength))
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _hit.point);
                remainingLength -= Vector3.Distance(_ray.origin, _hit.point);
                _ray = new Ray(_hit.point, Vector3.Reflect(_ray.direction, _hit.normal));

                if (_listHitObject.IndexOf(_hit.collider.gameObject) == -1)
                {
                    _listHitObject.Add(_hit.collider.gameObject);
                    _startPower -= _hit.collider.gameObject.GetComponent<Reflect>().indexReflect;
                }

                if (_hit.collider.tag != "Reflect" || _startPower <= 0)
                {
                    break;
                }
            }
            else
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, _ray.origin + _ray.direction * remainingLength);
            }            
        } 
    }
}
