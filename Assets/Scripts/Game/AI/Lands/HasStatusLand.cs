using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Lands")]
public class HasStatusLand : Conditional
{
    [SerializeField] private SharedLands Lands;
    [SerializeField] private Type type = Type.CanSowing;

    public override TaskStatus OnUpdate()
    {
        bool check = false;

        switch (type)
        {
            case Type.CanSowing:
                check = Lands.Value.CanSowing();
                break;
            case Type.CanWatering:
                check = Lands.Value.CanWatering();
                break;
            case Type.CanGaining:
                check = Lands.Value.CanGaining();
                break;
        }        
        return check ? TaskStatus.Success : TaskStatus.Failure;
    }

    [System.Serializable]
    private enum Type
    {
        CanSowing = 0,
        CanWatering,
        CanGaining
    }
}
