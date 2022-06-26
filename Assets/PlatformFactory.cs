using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A factory class used to produce (special) platforms in a stage.
/// </summary>
public class PlatformFactory : MonoBehaviour
{
    public GameObject movingPlatformPrefab;

    public MovingPlatform CreateMovingPlatform(List<Vector2> points, float timeBetweenPoints, MovingPlatform.CycleType cycleType = MovingPlatform.CycleType.None, MovingPlatform.MovementType movementType = MovingPlatform.MovementType.Linear)
    {
        GameObject generatedObject = GameObject.Instantiate(movingPlatformPrefab);
        MovingPlatform platform = generatedObject.GetComponent<MovingPlatform>();
        platform.Initialize(points, timeBetweenPoints, cycleType, movementType);
        return platform;
    }
}
