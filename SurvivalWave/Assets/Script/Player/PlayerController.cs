using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    Idle,
    Run,
    Jumping,
    Falling,
    Landing
}

public enum TransitionState
{
    Move,
    Stop,
    JumpPressed,
    Falling,
    Landed
}

public class PlayerController : MonoBehaviour
{
    public bool wasGrounded { get; private set; }

    public PlayerState state { get; private set; } = PlayerState.Idle;

    public float gravity = -9.8f;
    public float rotationSpeed = 10.0f;
    public float speed = 3.0f;
    public float jumpHeight = 2f;

    PlayerInputHandler inputHandler;
    CharacterController characterController;

    Vector3 velocity;

    Dictionary<PlayerState, Dictionary<TransitionState ,PlayerState>> stateTransitionDic = new Dictionary<PlayerState, Dictionary<TransitionState, PlayerState>>();

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

        if (PlayerState.Landing != state)
        {
            move = new Vector3(inputHandler.moveInput.x, 0, inputHandler.moveInput.y);
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

        if(isGrounded)
        {
            if(velocity.y < 0f)
            {
                velocity.y = -2f;
            }

            if(isJump)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                inputHandler.ConsumeJump();
                ChangeState(TransitionState.JumpPressed);
            }
            else if (!wasGrounded)
            {
                ChangeState(TransitionState.Landed);
            }
        }

        velocity.y += gravity * Time.deltaTime;

        Vector3 finalMove = move * speed + velocity;
        characterController.Move(finalMove * Time.deltaTime);

        wasGrounded = isGrounded;
    }

    void InitStateTransitionDic()
    {
        foreach (PlayerState ps in System.Enum.GetValues(typeof(PlayerState)))
        {
            stateTransitionDic[ps] = new Dictionary<TransitionState, PlayerState>();
        }

        stateTransitionDic[PlayerState.Idle][TransitionState.Move] = PlayerState.Run;
        stateTransitionDic[PlayerState.Idle][TransitionState.JumpPressed] = PlayerState.Jumping;
        stateTransitionDic[PlayerState.Idle][TransitionState.Falling] = PlayerState.Falling;

        stateTransitionDic[PlayerState.Run][TransitionState.Falling] = PlayerState.Falling;
        stateTransitionDic[PlayerState.Run][TransitionState.JumpPressed] = PlayerState.Jumping;
        stateTransitionDic[PlayerState.Run][TransitionState.Stop] = PlayerState.Idle;

        stateTransitionDic[PlayerState.Jumping][TransitionState.Landed] = PlayerState.Landing;

        stateTransitionDic[PlayerState.Falling][TransitionState.Landed] = PlayerState.Landing;

        stateTransitionDic[PlayerState.Landing][TransitionState.Stop] = PlayerState.Idle;
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

    public void FinishLanded()
    {
        ChangeState(TransitionState.Stop);
    }
}
