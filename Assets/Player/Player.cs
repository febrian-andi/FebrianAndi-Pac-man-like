using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    private int _health;

    [SerializeField]
    private TMP_Text _healthText;

    [SerializeField]
    private Transform _respawnPoint;

    private Rigidbody _rigidbody;
    private Coroutine _powerUpCoroutine;
    private bool _isPowerUpActive = false;

    // Code to hide and lock the cursor
    private void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Dead()
    {
        if (_isPowerUpActive != true)
        {
            Debug.Log(_health);
            _health -= 1;
            Debug.Log(_health);
            UpdateUI();
            if (_health > 0)
            {
                transform.position = _respawnPoint.position;
            }
            else
            {
                _health = 0;
                // Debug.Log("Game Over");
                SceneManager.LoadScene("LoseScreen");
            }
        }
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
        _isPowerUpActive = true;
        // Debug.Log("Power up started");
        if (OnPowerUpStart != null)
        {
            OnPowerUpStart();
        }
        yield return new WaitForSeconds(_powerUpDuration);
        _isPowerUpActive = false;
        if (OnPowerUpStop != null)
        {
            OnPowerUpStop();
        }
        // Debug.Log("Power up ended");
    }

    // Start is called before the first frame update
    private void Awake()
    {
        UpdateUI();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (_isPowerUpActive)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Dead();
            }
        }
    }

    private void UpdateUI()
    {
        _healthText.text = "Health : " + _health;
    }
}
