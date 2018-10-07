using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager _gameManager;
    private UIManager _uiManager;
    private AudioSource _audioSource;

    [SerializeField]
    private GameObject _singeLaserPrefab;
    [SerializeField]
    private GameObject _tripleLaserPrefab;
    [SerializeField]
    private GameObject _explosionAnimation;
    [SerializeField]
    private GameObject _playerShield;
    [SerializeField]
    private GameObject[] _engines;

    [SerializeField]
    private float _movementSpeed = 5f;
    [SerializeField]
    private float _fireRate = 0.25f;
    [SerializeField]
    private int _lives = 3;

    private float _lastTimeFired = 0.0f;
    private float _movementSpeedPowerupMultiplier = 2.0f;
    private bool _hasTripleShotPowerup = false;
    private bool _hasSpeedPowerup = false;
    private bool _hasShieldPowerup = false;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _uiManager = FindObjectOfType<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(_lives);
        }
    }

    private void Update()
    {
        Movement();

        if (Input.GetButtonDown("Fire"))
        {
            Shoot();
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (_hasSpeedPowerup)
        {
            transform.Translate(Vector3.right * _movementSpeed * _movementSpeedPowerupMultiplier * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _movementSpeed * _movementSpeedPowerupMultiplier * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _movementSpeed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _movementSpeed * verticalInput * Time.deltaTime);
        }

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f);
        }

        if (transform.position.x > 8.6f)
        {
            transform.position = new Vector3(-8.6f, transform.position.y);
        }
        else if (transform.position.x < -8.6f)
        {
            transform.position = new Vector3(8.6f, transform.position.y);
        }
    }

    private void Shoot()
    {
        if (Time.time > _lastTimeFired)
        {
            _audioSource.Play();
            if (_hasTripleShotPowerup)
            {
                Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_singeLaserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
            _lastTimeFired = Time.time + _fireRate;
        }
    }

    public void TakeDamage()
    {
        if (_hasShieldPowerup)
        {
            _hasShieldPowerup = false;
            _playerShield.SetActive(false);
            return;
        }

        _lives--;
        _uiManager.UpdateLives(_lives);

        if (_lives == 0)
        {
            Instantiate(_explosionAnimation, transform.position, Quaternion.identity);
            _gameManager.EndGame();
            Destroy(gameObject);
        }
        else if (_lives > 0 && _lives < 3)
        {
            _engines[_lives - 1].SetActive(true);
        }
    }

    private void CheckEngines()
    {

    }
    private void CheckLives()
    {

    }

    public void ActivateTripleShotPowerup(float cooldownTime)
    {
        StartCoroutine(StartTripleShotPowerupCooldown(cooldownTime));
    }
    private IEnumerator StartTripleShotPowerupCooldown(float cooldownTime)
    {
        _hasTripleShotPowerup = true;
        yield return new WaitForSeconds(cooldownTime);
        _hasTripleShotPowerup = false;
    }

    public void ActivateSpeedPowerup(float cooldownTime)
    {
        StartCoroutine(StartSpeedPowerupCooldown(cooldownTime));
    }
    private IEnumerator StartSpeedPowerupCooldown(float cooldownTime)
    {
        _hasSpeedPowerup = true;
        yield return new WaitForSeconds(cooldownTime);
        _hasSpeedPowerup = false;
    }

    public void ActivateShieldPowerup(float cooldownTime)
    {   // NOTE: Parameter is not currently being used.
        _hasShieldPowerup = true;
        _playerShield.SetActive(true);
        // Maybe add a coroutine to slowly disable shield
    }
}