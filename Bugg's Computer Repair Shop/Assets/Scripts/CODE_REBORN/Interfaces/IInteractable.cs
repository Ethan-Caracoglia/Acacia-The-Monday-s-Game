using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public abstract void TryMouseInput(InteractionState state);
    public abstract void UpdateMousePosition(Vector3 MousePos);

}
