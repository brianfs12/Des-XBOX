using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2_StateMachine : MonoBehaviour
{
    public MonoBehaviour idle;
    public MonoBehaviour attack;
    public MonoBehaviour back;
    public MonoBehaviour initialState;

    MonoBehaviour actualState;

    void Start()
    {
        ActivateState(initialState);
    }

    public void ActivateState(MonoBehaviour _newState)
    {
        if (actualState != null)
        {
            actualState.enabled = false;
        }

        actualState = _newState;
        actualState.enabled = true;
    }

}
