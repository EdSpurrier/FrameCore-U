using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ListType
{
    GameObjects,
    Transforms,
    GameObjectsWithFloats,
    GameObjectsWithHitPointAndFloat,
}


[System.Serializable]
public class GameObjectWithHitPointAndFloat
{
    public GameObject gameObject;
    public float floatValue = 0f;
    public Quaternion rotation;
    public Vector3 position;
}

[System.Serializable]
public class GameObjectWithFloat
{
    public GameObject gameObject;
    public float floatValue = 0f;
}

[System.Serializable]
public class Vector3WithBool
{
    public Vector3 point = Vector3.zero;
    public bool status = true;
}



[HideMonoScript]
public class ListOfObjects : MonoBehaviour
{
    [HideLabel]
    [HorizontalGroup("Line 1", 0.5f)]
    [SuffixLabel("List Name", Overlay = true)]
    public string listName = "List Name";

    [HideLabel]
    [HorizontalGroup("Line 1", 0.5f)]
    public ListType listType;

    public bool allowRepeatingObjects = false;

    [ShowIf("listType", ListType.GameObjects)]
    public List<GameObject> gameObjects;

    [ShowIf("listType", ListType.Transforms)]
    public List<Transform> transforms;

    [ShowIf("listType", ListType.GameObjectsWithFloats)]
    public List<GameObjectWithFloat> gameObjectsWithFloats;

    [ShowIf("listType", ListType.GameObjectsWithHitPointAndFloat)]
    public List<GameObjectWithHitPointAndFloat> gameObjectsWithHitPointAndFloat;

    public void AddGameObject(GameObject gameObject)
    {
        if (!gameObjects.Contains(gameObject))
        {
            gameObjects.Add(gameObject);
        };
    }

    public void RemoveGameObject(GameObject gameObject)
    {
        if (gameObjects.Contains(gameObject))
        {
            gameObjects.Remove(gameObject);
        };
    }


    public void AddGameObjectWithHitPointAndFloat(GameObject gameObjectValue, float newFloatValue, Vector3 position, Quaternion rotation)
    {
        if (!gameObjectsWithHitPointAndFloat.Any(item => item.gameObject == gameObjectValue) || allowRepeatingObjects)
        {
            gameObjectsWithHitPointAndFloat.Add(new GameObjectWithHitPointAndFloat
            { 
                gameObject = gameObjectValue, 
                floatValue = newFloatValue,
                position = position,
                rotation = rotation
            });
        };
    }

    public void ClearGameObjectWithHitPointAndFloat()
    {
        gameObjectsWithHitPointAndFloat.Clear();
    }


    public void AddGameObjectWithFloat(GameObject gameObjectValue, float newFloatValue)
    {
        if (!gameObjectsWithFloats.Any(item => item.gameObject == gameObjectValue) || allowRepeatingObjects)
        {
            gameObjectsWithFloats.Add(new GameObjectWithFloat { gameObject = gameObjectValue, floatValue = newFloatValue });
        };
    }

    public void ClearGameObjectWithFloat()
    {
        gameObjectsWithFloats.Clear();
    }
}
