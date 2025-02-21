using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Camera _camera;

    private Rigidbody _rigidbody;

    // Code to hide and lock the cursor
    private void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
