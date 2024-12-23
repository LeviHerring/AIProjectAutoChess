using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public abstract class StateManager<T>
{
    public abstract void Execute(T parentClass);
    public abstract void Enter(T parentClass);
    public abstract void Exit(T parentClass);

}
