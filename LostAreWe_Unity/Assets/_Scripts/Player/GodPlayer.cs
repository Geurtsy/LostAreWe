using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GodPlayer : MonoBehaviour, IHealth {

    [SerializeField] private HealthManager _healthManager;
    private IMove _moveMech;

    [Header("Stamina")]
    [SerializeField] private float _maxStamina;
    [SerializeField] private float _staminaCoolDownEnd;
    [SerializeField] private float _staminaIncreaseRate;
    [SerializeField] private float _staminaDecayRate;
    private bool _staminaCooling;
    private float _stamina;

    public string HorizontalInputName { get; set; }
    public string VerticalInputName { get; set; }
    public string LookHorizontalInputName { get; set; }
    public string LookVerticalInputName { get; set; }
    public string AttackBtnName { get; set; }
    public string StartBtnName { get; set; }
    public string SpecialBtnName { get; set; }

    // Gets + sets "Stamina". It only allows stamina to be set to a value less than or equal to-
    // "_maxStamina".
    public float Stamina
    {
        get { return _stamina; }
        set { _stamina = value > _maxStamina ? _maxStamina : value; }
    }

    // Gets "_maxStamina".
    // Removes ability to set "_maxStamina".
    public float MaxStamina
    { 
        get { return _maxStamina; }
        set { }
    }

    // Returns the a percentage of "_stamina".
    // Adds the percentage of "value" to _stamina with an end result greater than or equal to-
    // "_maxStamina".
    public float StaminaPercentage
    {
        get { return (_stamina / _maxStamina) * 100.0f; }
        set { _stamina = value > 100 ? _maxStamina : _stamina + value / 100 * _maxStamina; }
    }

    // Score.
    private int _score;

    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }

    public void AddScore(int score)
    {
        _score += score;
    }

    // A class which controls the health of this player.
    public HealthManager HealthManager
    {
        get { return _healthManager; }
        set { }
    }

    // Attack.
    [SerializeField] private float _attackCooldown;
    private IAttack _attackMech;
    private bool _attackReady = true;

    private AudioSource _as;

    private void Start()
    {
        // References.
        _as = GetComponent<AudioSource>();
        _moveMech = GetComponent<IMove>();
        _attackMech = GetComponent<IAttack>();

        // Setup.
        _stamina = _maxStamina;
        _healthManager._as = _as;

    }

    private void Update()
    {
        // If "Run" button is pressed, change "CurrentSpeedState" to RUN, else, WALK.
        _moveMech.CurrentSpeedState = Input.GetButton(SpecialBtnName) ? Movement.SpeedState.RUN : Movement.SpeedState.WALK;

        // If player has no stamina left, change "CurrentSpeedState" to STILL.
        // Else, leave as is.
        _moveMech.CurrentSpeedState = _stamina <= 0 ? Movement.SpeedState.STILL : _moveMech.CurrentSpeedState;

        // Initiates cooling if applicable.
        // Ends cooling if applicable.
        if(_stamina <= 0 && !_staminaCooling)
        {
            _moveMech.CurrentSpeedState = Movement.SpeedState.STILL;
            _staminaCooling = true;
        }
        else if(_stamina > _staminaCoolDownEnd)
        {
            _staminaCooling = false;
        }

        if(_staminaCooling && _stamina < _staminaCoolDownEnd)
        {
            _moveMech.CurrentSpeedState = Movement.SpeedState.STILL;
        }

        // Inputs the player's input into the movement mechanic.
        if (Input.GetAxis(HorizontalInputName) != 0 || Input.GetAxis(VerticalInputName) != 0)
            _moveMech.Move(new Vector2(Input.GetAxis(HorizontalInputName), Input.GetAxis(VerticalInputName)));

        if(Input.GetAxis(LookHorizontalInputName) > 0.5 || Input.GetAxis(LookHorizontalInputName) < -0.5 || Input.GetAxis(LookVerticalInputName) > 0.5 || Input.GetAxis(LookVerticalInputName) < -0.5)
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, Mathf.Atan2(Input.GetAxis(LookHorizontalInputName), Input.GetAxis(LookVerticalInputName)) * Mathf.Rad2Deg + 90));

        Stamina += Time.deltaTime * _staminaIncreaseRate;
        Stamina -= _moveMech.CurrentSpeedState == Movement.SpeedState.RUN ? Time.deltaTime * _staminaDecayRate : 0.0f;

        // Attacking.
        if(Input.GetAxis(AttackBtnName) == 1 && _attackReady)
        {
            Debug.Log(AttackBtnName);
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        _attackMech.Attack();
        _attackReady = false;

        yield return new WaitForSeconds(_attackCooldown);

        while(Input.GetAxis(AttackBtnName) == 1)
        {
            _attackReady = false;
            yield return null;
        }

        _attackReady = true;

        yield return null;
    }
}
