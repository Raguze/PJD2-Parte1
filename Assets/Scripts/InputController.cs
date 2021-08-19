using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vector2Event : UnityEvent<Vector2> {}
public class VoidEvent : UnityEvent {}

public class InputController : MonoBehaviour
{
    public Vector2Event OnDirection { get; protected set;}
    public VoidEvent OnJumpButtonDown { get; protected set;}
    public VoidEvent OnJumpButtonUp { get; protected set;}

    public Vector2 Direction { get; protected set; }

    private bool initialized;

    public void Init()
    {
        if(initialized)
        {
            return;
        }

        if(OnDirection == null)
        {
            OnDirection = new Vector2Event();
        }

        if(OnJumpButtonDown == null)
        {
            OnJumpButtonDown = new VoidEvent();
        }

        if(OnJumpButtonUp == null)
        {
            OnJumpButtonUp = new VoidEvent();
        }

        initialized = true;
    }

    private void Awake() 
    {
        Init();
    }

    void Update()
    {
        Direction = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        OnDirection.Invoke(Direction);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpButtonDown.Invoke();
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            OnJumpButtonUp.Invoke();
        }
    }
}
