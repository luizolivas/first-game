using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float rollSpeed;




    private Rigidbody2D rig;

    private float initalSpeed;
    private bool _isRunning;
    private bool _isRolling;
    private bool _isCutting;

    private Vector2 _direction;

    private bool estaBotaoDireitoPressionado;
    private float ultimoCliqueDireito;

    public Vector2 direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    public bool isRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }

    public bool isRolling
    {
        get { return _isRolling; }
        set { _isRolling = value; }
    }

    public bool IsCutting { get => _isCutting; set => _isCutting = value; }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        initalSpeed = speed;
    }

    private void Update()
    {
        OnInput();

        OnRun();
        OnRolling();
        OnCutting();
    }


    private void FixedUpdate()
    {
        OnMove();
    }

    #region Movement

    private void OnCutting()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            IsCutting = true;
            speed = 0f;
        }

        if (Input.GetMouseButtonUp(0))
        {
            IsCutting = false;
            speed = initalSpeed;
        }
    }

    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void OnMove()
    {
        rig.MovePosition(rig.position + _direction * speed * Time.fixedDeltaTime);
    }

    void OnRun()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runSpeed;
            _isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = initalSpeed;
            _isRunning = false;
        }
    }

    private void OnRolling()
    {
        if (Input.GetMouseButtonDown(1) && Time.time - ultimoCliqueDireito > 0.75)
        {
            estaBotaoDireitoPressionado = true;
        }

        if (Input.GetMouseButtonUp(1) && estaBotaoDireitoPressionado)
        {
            StartCoroutine(CoroutineRolagem());
            estaBotaoDireitoPressionado = false;
            ultimoCliqueDireito = Time.time;
        }
    }

    IEnumerator CoroutineRolagem()
    {
        speed = rollSpeed;
        _isRolling = true;

        // Aguarde a duração desejada para a rolagem
        yield return new WaitForSeconds(0.20f);

        speed = initalSpeed;
        _isRolling = false;
    }



    #endregion
}
