using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Action<char> OnInputAnyKey;

    private void Update()
    {
       ReceiveKeyInputEvent();
    }

    private void ReceiveKeyInputEvent()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnInputAnyKey?.Invoke('a');
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            OnInputAnyKey?.Invoke('b');
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            OnInputAnyKey?.Invoke('c');
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            OnInputAnyKey?.Invoke('d');
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            OnInputAnyKey?.Invoke('e');
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            OnInputAnyKey?.Invoke('f');
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            OnInputAnyKey?.Invoke('g');
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            OnInputAnyKey?.Invoke('h');
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            OnInputAnyKey?.Invoke('i');
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            OnInputAnyKey?.Invoke('j');
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            OnInputAnyKey?.Invoke('k');
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            OnInputAnyKey?.Invoke('l');
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            OnInputAnyKey?.Invoke('m');
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            OnInputAnyKey?.Invoke('n');
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            OnInputAnyKey?.Invoke('o');
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            OnInputAnyKey?.Invoke('p');
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            OnInputAnyKey?.Invoke('q');
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            OnInputAnyKey?.Invoke('r');
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            OnInputAnyKey?.Invoke('s');
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            OnInputAnyKey?.Invoke('t');
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            OnInputAnyKey?.Invoke('u');
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            OnInputAnyKey?.Invoke('v');
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            OnInputAnyKey?.Invoke('w');
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            OnInputAnyKey?.Invoke('x');
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            OnInputAnyKey?.Invoke('y');
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            OnInputAnyKey?.Invoke('z');
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            OnInputAnyKey?.Invoke('-');
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            OnInputAnyKey?.Invoke('\b');
        }
        else
        {
            OnInputAnyKey?.Invoke('\0');
        }
    }
}
