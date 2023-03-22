using Euphrates;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Transform _transform;

    [Header("ATTRIBUTES")]
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _slideSpeed = 0.5f;
    [SerializeField] float _xLimit = 8f;
    [SerializeField] float _lerpAmount = 10f;

    [Space]
    [SerializeReference] TriggerChannelSO _gameStart;
    [SerializeReference] TriggerChannelSO _win;
    [SerializeReference] TriggerChannelSO _lose;

    //Vector3 _firstPos;
    //Vector3 _lastPost;

    [HideInInspector]
    public Animator playerAnimator;

    float _stopSpeed = 0f;
    float _stopSlide = 0f;
    float _resumeSpeed;
    float _resumeSlide;

    bool _movementEnabled = false;

    private void Awake() => _transform = transform;

    private void OnEnable()
    {
        _gameStart.AddListener(GameStarted);
        _win.AddListener(StopPlayer);
        _lose.AddListener(StopPlayer);
    }

    private void OnDisable()
    {
        _gameStart.RemoveListener(GameStarted);
        _win.RemoveListener(StopPlayer);
        _lose.RemoveListener(StopPlayer);
    }

    private void Start()
    {
        //Get the animator and save the speed variables for another use
        playerAnimator = GetComponent<Animator>();
        _resumeSpeed = _moveSpeed;
        _resumeSlide = _slideSpeed;

    }

    float _xTarget = 0f;

    Vector2 _lastTouch;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastTouch = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 mpos = Input.mousePosition;
            Vector2 mmove = mpos - _lastTouch;

            _xTarget += mmove.x * .05f;
            _xTarget = Mathf.Clamp(_xTarget, -_xLimit, _xLimit);

            _lastTouch = mpos;
            return;
        }
    }

    private void FixedUpdate()
    {
        if (!_movementEnabled)
            return;

        Movement();
    }
    private void Movement()
    {
        Vector3 target = _transform.position + Vector3.forward * _lerpAmount;
        target.x = _xTarget;

        Vector3 dir = (target - _transform.position).normalized;

        _transform.forward = dir;
        _transform.position += _moveSpeed * Time.fixedDeltaTime * _transform.forward;

        ////move in z 
        //transform.Translate(0, 0, _moveSpeed * Time.fixedDeltaTime);

        ////touch started
        //if (Input.GetMouseButtonDown(0))
        //{
        //    _firstPos = Input.mousePosition;
        //}
        ////while touching
        //else if (Input.GetMouseButton(0))
        //{
        //    //Get the last position that touched
        //    _lastPost = Input.mousePosition;

        //    //find the diffrence in x
        //    float xDiff = _lastPost.x - _firstPos.x;

        //    //Move in that direction
        //    transform.Translate(xDiff * Time.fixedDeltaTime * _slideSpeed, 0, 0);
        //}
        ////when released
        //if (Input.GetMouseButtonUp(0))
        //{
        //    //reset the start and finish vectors to zero otherwise player going to move constantly
        //    _firstPos = Vector3.zero;
        //    _lastPost = Vector3.zero;
        //}
    }

    public void StopPlayer()
    {
        //Equals players speed variables to zero
        _moveSpeed = _stopSpeed;
        _slideSpeed = _stopSlide;
        _movementEnabled = false;
    }

    public void ResumePlayer()
    {
        //Equals players speed variables to first values
        _moveSpeed = _resumeSpeed;
        _slideSpeed = _resumeSlide;
        _movementEnabled = true;
    }

    void GameStarted() => _movementEnabled = true;
}
