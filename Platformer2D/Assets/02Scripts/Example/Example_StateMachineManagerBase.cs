using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_StateMachineManagerBase : MonoBehaviour
{
    //protected Dictionary<dynamic, StateMachineBase> _machines = new Dictionary<dynamic, StateMachineBase>();

    //protected void AddStateMachine(dynamic state)
    //{
    //    if (_machines.ContainsKey(state))
    //        return;

    //    string typeName = "StateMachine" + state.ToString();
    //    Type type = Type.GetType(typeName);
    //    if (type != null)
    //    {
    //        ConstructorInfo constructorInfo =
    //            type.GetConstructor(new Type[]
    //            {
    //                //typeof(State),
    //                typeof(StateMachineManager),
    //                typeof(AnimationManager)
    //            });

    //        StateMachineBase machine =
    //            constructorInfo.Invoke(new object[]
    //            {
    //                state,
    //                this,
    //                //_animationManager
    //            }) as StateMachineBase;

    //        _machines.Add(state, machine);
    //        //if (machine.shortKey != KeyCode.None)
    //        //    _states.Add(machine.shortKey, state);

    //        Debug.Log($"{state}가 등록됨");
    //    }
    //    else
    //    {
    //        Debug.LogWarning($"{state}의 머신을 찾을 수 없음");
    //    }
    //}
}
