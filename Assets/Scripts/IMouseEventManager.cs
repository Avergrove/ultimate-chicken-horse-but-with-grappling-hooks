using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IMouseEventManager : IEventSystemHandler
{
    void OnMouseMoved(Vector2 mousePos);
}
