using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGameObjectToRuntimeSet : MonoBehaviour
{
    public GameObjectRuntimeSet gameObjectRuntimeSet;

    private void OnEnable()
    {
        gameObjectRuntimeSet.AddToList(this.gameObject);
    }

    private void OnDisable()
    {
        gameObjectRuntimeSet.RemoveFromList(this.gameObject);
    }
}
