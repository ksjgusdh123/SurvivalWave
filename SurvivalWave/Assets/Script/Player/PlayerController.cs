using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
public enum PlayerState
{
    Idle,
    Run,
    Jumping,
    Falling,
    Landing,
    Damaged,
    Die,
    Air
}

public enum TransitionState
{
    Move,
    Stop,
    JumpPressed,
    Falling,
    Landed,
    Damaged,
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] MainCamera mainCamera;
    public bool wasGrounded { get; private set; }
    public PlayerState state { get; private set; } = PlayerState.Idle;

    public float gravity = -9.8f;
    public float rotationSpeed = 10.0f;
    public float jumpHeight = 2f;
    public float speed { get; set; }
    public bool CanAir = true;

    PlayerInputHandler inputHandler;
    CharacterController characterController;

    Vector3 velocity;

    Dictionary<PlayerState, Dictionary<TransitionState, PlayerState>> stateTransitionDic = new Dictionary<PlayerState, Dictionary<TransitionState, PlayerState>>();

    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        characterController = GetComponent<CharacterController>();
        InitStateTransitionDic();
    }

    void Update()
    {
        Vector3 move = Vector3.zero;
        bool isGrounded = characterController.isGrounded;
        bool isJump = inputHandler.isJump;

        HandleJump(isGrounded, isJump);

        if (PlayerState.Falling != state)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        HandleMove(ref move);
        HandleMoveDirection(move);

        Vector3 finalMove = move * speed + velocity;
        characterController.Move(finalMove * Time.deltaTime);

        wasGrounded = isGrounded;

        if (isJump) inputHandler.ConsumeJump();
        HandleLook();
    }
    private void LateUpdate()
    {
    }
    void InitStateTransitionDic()
    {
        foreach (PlayerState ps in System.Enum.GetValues(typeof(PlayerState)))
        {
            stateTransitionDic[ps] = new Dictionary<TransitionState, PlayerState>();
        }

        stateTransitionDic[PlayerState.Idle][TransitionState.Move] = PlayerState.Run;
        stateTransitionDic[PlayerState.Idle][TransitionState.JumpPressed] = PlayerState.Jumping;
        //stateTransitionDic[PlayerState.Idle][TransitionState.Falling] = PlayerState.Falling;
        stateTransitionDic[PlayerState.Idle][TransitionState.Damaged] = PlayerState.Damaged;

        //stateTransitionDic[PlayerState.Run][TransitionState.Falling] = PlayerState.Falling;
        stateTransitionDic[PlayerState.Run][TransitionState.JumpPressed] = PlayerState.Jumping;
        stateTransitionDic[PlayerState.Run][TransitionState.Stop] = PlayerState.Idle;
        stateTransitionDic[PlayerState.Run][TransitionState.Damaged] = PlayerState.Damaged;

        stateTransitionDic[PlayerState.Jumping][TransitionState.Landed] = PlayerState.Landing;
        stateTransitionDic[PlayerState.Jumping][TransitionState.Falling] = PlayerState.Falling;

        stateTransitionDic[PlayerState.Falling][TransitionState.Landed] = PlayerState.Landing;

        stateTransitionDic[PlayerState.Landing][TransitionState.Stop] = PlayerState.Idle;

        stateTransitionDic[PlayerState.Damaged][TransitionState.Stop] = PlayerState.Idle;
    }

    void ChangeState(TransitionState transitionState)
    {
        if (stateTransitionDic.TryGetValue(state, out var transitionDic))
        {
            if (transitionDic.TryGetValue(transitionState, out var nextState))
            {
                state = nextState;
            }
        }
    }
    void HandleMove(ref Vector3 move)
    {
        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = mainCamera.transform.right;
        right.y = 0f;
        right.Normalize();

        move = (right * inputHandler.moveInput.x + forward * inputHandler.moveInput.y);
        move.Normalize();
    }
    void HandleMoveDirection(Vector3 move)
    {
        if (PlayerState.Landing != state && PlayerState.Damaged != state)
        {
            if (Vector3.zero != move)
            {
                Quaternion targetRot = Quaternion.LookRotation(move);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
                ChangeState(TransitionState.Move);
            }
            else
            {
                ChangeState(TransitionState.Stop);
            }
        }
    }
    void HandleLook()
    {
        Vector2 look = inputHandler.lookInput;
        mainCamera.UpdateCamera(look);
    }
    void HandleJump(bool isGrounded, bool isJump)
    {
        if (isGrounded)
        {
            if (velocity.y < 0f)
            {
                velocity.y = -2f;
            }

            if (isJump && PlayerState.Landing != state && PlayerState.Jumping != state && PlayerState.Damaged != state && CanAir)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                inputHandler.ConsumeJump();
                ChangeState(TransitionState.JumpPressed);
                SoundManager.GetInstance().PlaySFX(SFXType.Jump);
            }
            else if (!wasGrounded)
            {
                ChangeState(TransitionState.Landed);
            }
        }
        else
        {
            if (isJump && PlayerState.Falling == state)
            {
                velocity.y = -2f;
                inputHandler.ConsumeJump();
            }
            //ChangeState(TransitionState.Falling);
        }
    }

    public void Damaged()
    {
        ChangeState(TransitionState.Damaged);
        SoundManager.GetInstance().PlaySFX(SFXType.PlayerDamaged);
    }

    public void FinishLanded()
    {
        ChangeState(TransitionState.Stop);
    }
    public void FinishDamaged()
    {
        ChangeState(TransitionState.Stop);
    }
    public void EndReadyAir()
    {
        ChangeState(TransitionState.Falling);
        velocity.y = 0f;
    }

    public void KickOffAir()
    {
        velocity.y = -2f;
        inputHandler.ConsumeJump();
        CanAir = false;
    }
}
