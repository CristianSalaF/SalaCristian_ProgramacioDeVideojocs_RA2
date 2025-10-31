using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanonRotation : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    public Vector3 _maxRotation;
    public Vector3 _minRotation;
    private float _offset = -51.6f;
    public GameObject ShootPoint;
    public GameObject Bullet;
    public float ProjectileSpeed = 0;
    public float MaxSpeed;
    public float MinSpeed;
    public GameObject PotencyBar;
    private float _initialScaleX;
    private Vector2 _distanceBetweenMouseAndPlayer;
    private bool isRaising = false;
    [SerializeField] private float _multiplier = 10f;
    private InputSystem_Actions _ia;

    Vector2 mousePos;
    private void Awake()
    {
        _ia = new InputSystem_Actions();
        _ia.Player.SetCallbacks(this);
        _initialScaleX = PotencyBar.transform.localScale.x;
    }

    private void OnEnable()
    {
        _ia.Player.Enable();
    }

    private void OnDisable()
    {
        _ia.Player.Disable();
    }

    void Update()
    {
        // Vector2 mousePos; //obtenir el valor del click del cursor (Fer amb new input system)
        _distanceBetweenMouseAndPlayer = mousePos - new Vector2(this.transform.position.x, this.transform.position.y); //obtenir el vector distància entre el canó i el cursor
        var ang = (Mathf.Atan2(_distanceBetweenMouseAndPlayer.y, _distanceBetweenMouseAndPlayer.x) * 180f / Mathf.PI + _offset);
        transform.rotation = Quaternion.Euler(0,0,ang); //en quin dels tres eixos va l'angle? al Z, que es el que va del davant cap al fons

        if (isRaising)
        {
            ProjectileSpeed = Time.deltaTime * _multiplier + ProjectileSpeed; //acotar entre dos valors (mirar variables)
            CalculateBarScale();
        }
        
        CalculateBarScale();
    }
    public void CalculateBarScale()
    {
        PotencyBar.transform.localScale = new Vector3(Mathf.Lerp(0, _initialScaleX, ProjectileSpeed / MaxSpeed),
            transform.localScale.y,
            transform.localScale.z);
    }
    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isRaising = true;
        }
        if (context.canceled)
        {
            var projectile = Instantiate(Bullet, transform.position, Quaternion.identity); //canviar la posició on s'instancia
            projectile.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            ProjectileSpeed = 0f;
            isRaising = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Debug.Log("Mouse pos X: " + mousePos.x + " mouse Y is: " + mousePos.y);
        mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
