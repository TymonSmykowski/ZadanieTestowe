using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [SerializeField] private SphereManager _sphereManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _sphereManager.TryToStop();
        }
    }
}
