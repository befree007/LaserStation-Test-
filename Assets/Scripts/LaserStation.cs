using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserStation : MonoBehaviour
{

    [SerializeField] private GameObject _laserStation;

    [SerializeField] private GameObject _laserDoulo;

    [SerializeField] private float _rotationPower;
    
    public void RotationStationLeft()
    {
        _laserStation.transform.Rotate(0, _rotationPower * Time.deltaTime, 0);
    }

    public void RotationStationRight()
    {
        _laserStation.transform.Rotate(0, -_rotationPower * Time.deltaTime, 0);
    }

    public void RotationDouloUp()
    {
        _laserDoulo.transform.Rotate(0, 0, -_rotationPower * Time.deltaTime);
    }

    public void RotationDouloDown()
    {
        _laserDoulo.transform.Rotate(0, 0, _rotationPower * Time.deltaTime);
    }
}
