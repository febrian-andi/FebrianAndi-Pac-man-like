using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private float _powerUpDuration;

    private Rigidbody _rigidbody;
    private Coroutine _powerUpCoroutine;

    // Code to hide and lock the cursor
    private void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PickPowerUp()
    {
        if (_powerUpCoroutine != null)
        {
            StopCoroutine(_powerUpCoroutine);
        }

        _powerUpCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp()
    {
        // Debug.Log("Power up started");
        if (OnPowerUpStart != null)
        {
            OnPowerUpStart();
        }
        yield return new WaitForSeconds(_powerUpDuration);
        if (OnPowerUpStop != null)
        {
            OnPowerUpStop();
        }
        // Debug.Log("Power up ended");
    }

    // Start is called before the first frame update
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        HideAndLockCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }   
        // Horizontal = a or left (-) & d or right (+)
        float horizontal = Input.GetAxis("Horizontal");
        // Vertical = s or down (-) & w or up (+)
        float vertical = Input.GetAxis("Vertical");

        // Camera direction
        Vector3 horizontalDirection = _camera.transform.right * horizontal;
        Vector3 verticalDirection = _camera.transform.forward * vertical;
        verticalDirection.y = 0;
        horizontalDirection.y = 0;

        // Movement Player
        Vector3 movementDirection = horizontalDirection + verticalDirection;

        _rigidbody.velocity = movementDirection * _speed * Time.fixedDeltaTime;

        // Debug.Log("Horizontal: " + horizontal);
        // Debug.Log("Vertical: " + vertical);
    }
}
