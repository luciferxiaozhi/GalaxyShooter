using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool canTripleShot = false;
    public bool canSpeedBoost = false;
    public bool canShieldOn = false;
    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;
    public int life = 3;

    [SerializeField]
    private GameObject _playerExplosionPrefab;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleLaserPrefab;
    [SerializeField]
    private GameObject _shieldGameObject;
    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private GameObject[] _engines;
 
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _nextFire = 0.0f;

    private Coroutine currentCoroutine_0 = null;
    private Coroutine currentCoroutine_1 = null;

    private UIManager _uiManager;
    private GameManager _gameManger;
    private AudioSource _audioSource;

    private int hitCount = 0;
    private bool leftHitFlag = false;
    // Use this for initialization
    void Start ()
    {

        _gameManger = GameObject.Find("GameManager").GetComponent<GameManager>();

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (!_gameManger.isCoopMode)
        {
            transform.position = new Vector3(0f, 0f, 0f);
        }

        if(_uiManager != null)
        {
            _uiManager.UpdateLives(life);
        }

        _audioSource = GetComponent<AudioSource>();
        hitCount = 0;
	}

    // Update is called once per frame
    void Update()
    {
        if (isPlayerOne && !isPlayerTwo)
        {
            Movement();
        
            //Shooting
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                Shoot();
            }
        }

        if (isPlayerTwo && !isPlayerOne)
        {
            PlayerTwoMovement();
            if (Input.GetKey(KeyCode.KeypadEnter))
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        //spawn my laser
        if (Time.time > _nextFire)
        {
            _audioSource.Play();
            if (canTripleShot)
            {
                Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.9f, 0), Quaternion.identity);
            }
            _nextFire = Time.time + _fireRate;
        }
    }

    private void Movement()
    {
        // left right up down movements
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if(canSpeedBoost) // speed boost mode
        {
            transform.Translate(new Vector3(0, 1, 0) * _speed * verticalInput * 2.0f * Time.deltaTime);
            transform.Translate(new Vector3(1, 0, 0) * _speed * horizontalInput * 2.0f * Time.deltaTime);
        }
        else // normal speed mode
        {
            transform.Translate(new Vector3(0, 1, 0) * _speed * verticalInput * Time.deltaTime);
            transform.Translate(new Vector3(1, 0, 0) * _speed * horizontalInput * Time.deltaTime);
        }

        // boundary set up
        if (transform.position.y >= 4.3f)
        {
            transform.position = new Vector3(transform.position.x, 4.3f, 0);
        }
        else if (transform.position.y <= -4.3f)
        {
            transform.position = new Vector3(transform.position.x, -4.3f, 0);
        }

        if (transform.position.x >= 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x <= -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    private void PlayerTwoMovement()
    {
        if (canSpeedBoost) // speed boost mode
        {
            if (Input.GetKey(KeyCode.Keypad8))
            {
                transform.Translate(Vector3.up * _speed * 1.5f * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Keypad4))
            {
                transform.Translate(Vector3.left * _speed * 1.5f * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Keypad2))
            {
                transform.Translate(Vector3.down * _speed * 1.5f * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Keypad6))
            {
                transform.Translate(Vector3.right * _speed * 1.5f * Time.deltaTime);
            }
        }
        else // normal speed mode
        {
            if (Input.GetKey(KeyCode.Keypad8))
            {
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Keypad4))
            {
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Keypad2))
            {
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Keypad6))
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
            }
        }

        // boundary set up
        if (transform.position.y >= 4.3f)
        {
            transform.position = new Vector3(transform.position.x, 4.3f, 0);
        }
        else if (transform.position.y <= -4.3f)
        {
            transform.position = new Vector3(transform.position.x, -4.3f, 0);
        }

        if (transform.position.x >= 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x <= -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }
    // take damage
    public void Damage()
    {
        if(canShieldOn) // when shield on
        {
            canShieldOn = false;
            _shieldGameObject.SetActive(false);
            return;
        }
       
        life--;
        _uiManager.UpdateLives(life); // update lives image

        hitCount++;
        if(hitCount == 1)
        {
            // turn one engine_failure on
            int randomEngine = Random.Range(0, 2);
            if(randomEngine == 0) //left hit
            {
                leftHitFlag = true;
            }
            else if(randomEngine == 1)
            {
                leftHitFlag = false;
            }
            _engines[randomEngine].SetActive(true);
        }
        else if(hitCount == 2)
        {
            // turn both engine_failure on
            if(leftHitFlag)
            {
                _engines[1].SetActive(true);
            }
            else
            {
                _engines[0].SetActive(true);
            }
        }

        if(life < 1)
        {
            Destroy(Instantiate(_playerExplosionPrefab, transform.position, Quaternion.identity), 3);
            _gameManger.gameOver = true;
            _uiManager.ShowTitleScreen();
            _uiManager.CheckForBestScore();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        if(currentCoroutine_0 != null)
        {
            StopCoroutine(currentCoroutine_0);
        }
        currentCoroutine_0 = StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedUpPowerupOn()
    {
        canSpeedBoost = true;
        if (currentCoroutine_1 != null)
        {
            StopCoroutine(currentCoroutine_1);
        }
        currentCoroutine_1 = StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    public void SheildUpPowerupOn()
    {
        canShieldOn = true;
        _shieldGameObject.SetActive(true);
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedBoost = false;
    }
}
