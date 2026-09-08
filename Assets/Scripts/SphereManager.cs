using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SphereManager : MonoBehaviour
{

    [SerializeField] private float _radius = 10.0f;
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private float _maxSpeed = 3.0f;
    [SerializeField] private float _acceleration = 0.2f;
    [SerializeField] private GameObject _fireworks;
    [SerializeField] private TMP_Text _uiTextDist;
    [SerializeField] private TMP_Text _uiTextSpeed;

    private Renderer _rend;
    private Rigidbody _rb;
    private float _currentAngle;
    private float _dist = 0.0f;
    private bool _isMoving = true;

    void Start()
    {
        _rend = GetComponent<Renderer>();
        _rb = GetComponent<Rigidbody>();

        _isMoving = false;
        _rb.useGravity = false;

        _uiTextDist.text = "";
    }


    void Update()
    {
        if (_isMoving)
        {
            _rend.material.color = new Color(transform.position.y / 20.0f,
                                            Mathf.Abs(transform.position.x) / 5.0f, 
                                            Mathf.Abs(transform.position.z) / 5.0f);

            if (_speed < _maxSpeed)
            {
                _speed += _acceleration;
            }

            _currentAngle += _speed * Time.deltaTime;

            float newX = Mathf.Cos(_currentAngle) * _radius;
            float newZ = Mathf.Sin(_currentAngle) * _radius;

            Vector3 startPos = transform.position;
            transform.position = new Vector3(newX, transform.position.y, newZ);
            Vector3 newPos = transform.position;
            _dist += Vector3.Distance(startPos, newPos);
        }

        if (transform.position.y < 6.30f)
        {
            DestroySphere();
        }

        _uiTextSpeed.text = "Speed: " + (Mathf.Round(_speed * 100) * 0.01f).ToString();
    }

    public void StartSphere()
    {
        _isMoving = true;
        _rb.useGravity = true;
    }

    public void TryToStop()
    {
        StartCoroutine("StopSphere");
    }

    IEnumerator StopSphere()
    {
        _speed = 0;
        _uiTextDist.text = _dist.ToString();
        _rb.isKinematic = true;
        _isMoving = false;
        yield return new WaitForSeconds(5.0f);
        _isMoving = true;
        _rb.isKinematic = false;
        _uiTextDist.text = "";
    }

    private void DestroySphere()
    {
        Instantiate(_fireworks,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
