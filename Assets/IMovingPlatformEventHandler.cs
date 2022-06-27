using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IMovingPlatformEventHandler : IEventSystemHandler
{
    void OnPlatformMove(MovingPlatform platform);
    void OnPlatformLeave(MovingPlatform platform);
}
