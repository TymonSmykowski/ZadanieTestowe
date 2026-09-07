using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SphereManager : MonoBehaviour
{

    [SerializeField] bool _isMoving = true;
    
    private Renderer _rend;
    private Rigidbody _rb;

    public float radius = 10.0f;
    public float speed = 3.0f;
    public float maxSpeed = 3.0f;
    public float acceleration = 0.2f;

    private float _currentAngle;

    [SerializeField] GameObject fireworks;

    private float _dist = 0.0f;

    [SerializeField] TMP_Text _uiTextDist;
    [SerializeField] TMP_Text _uiTextSpeed;

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
            _rend.material.color = new Color(transform.position.y / 20.0f, 0.0f, 0.0f);

            if (speed < maxSpeed)
            {
                speed += acceleration;
            }

            _currentAngle += speed * Time.deltaTime;

            float newX = Mathf.Cos(_currentAngle) * radius;
            float newZ = Mathf.Sin(_currentAngle) * radius;

            Vector3 startPos = transform.position;
            transform.position = new Vector3(newX, transform.position.y, newZ);
            Vector3 newPos = transform.position;
            _dist += Vector3.Distance(startPos, newPos);
        }

        if (transform.position.y < 0)
        {
            DestroySphere();
        }

        _uiTextSpeed.text = "Speed: " + (Mathf.Round(speed * 100) * 0.01f).ToString();
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
        speed = 0;
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
        Instantiate(fireworks,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
