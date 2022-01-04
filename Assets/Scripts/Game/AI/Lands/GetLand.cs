using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Lands")]
public class GetLand : Action
{
    [SerializeField] private SharedLands Lands;
    [SerializeField] private SharedLand Land;
    [SerializeField] private Type type = Type.CanSowing;

    public override TaskStatus OnUpdate()
    {
        Land land = null;

        switch (type)
        {
            case Type.CanSowing:
                land = Lands.Value.GetLandCanSowing(this.transform);
                break;
            case Type.CanWatering:
                land = Lands.Value.GetLandCanWatering(this.transform);
                break;
            case Type.CanGaining:
                land = Lands.Value.GetLandCanGaining(this.transform);
                break;
            case Type.Random:
                land = Lands.Value.GetLandRandom();
                break;
        }

        if (land)
        {
            Land.Value = land;
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }

    [System.Serializable]
    private enum Type
    {
        CanSowing = 0,
        CanWatering,
        CanGaining,

        Random
    }
}
