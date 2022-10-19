using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action_Spawn
{

    [HorizontalGroup("Split", 0.35f)]
    [HideLabel]
    public Transform prefab;

    [InlineButton("AddToPool")]

    [EnumPaging]
    [EnumToggleButtons]
    [HorizontalGroup("Split", 0.65f)]
    [HideLabel]
    public SpawnType spawnPositionType;

    public enum SpawnType
    {
        Preset,
        ReferenceTransform,
    }

    [FoldoutGroup("Spawn")]
    public Transform parent;


    [FoldoutGroup("Spawn")]
    [ShowIf("spawnPositionType", SpawnType.Preset)]
    public Vector3 position;
    [FoldoutGroup("Spawn")]
    [ShowIf("spawnPositionType", SpawnType.Preset)]
    public Vector3 rotation;


    [FoldoutGroup("Spawn")]
    [ShowIf("spawnPositionType", SpawnType.ReferenceTransform)]
    public Transform spawnPointReference;
    [FoldoutGroup("Spawn")]
    [ShowIf("spawnPositionType", SpawnType.ReferenceTransform)]
    public Transform spawnRotationReference;

    
    [Title("System")]
    [FoldoutGroup("Spawn")]
    public DeBugger debug;

    private void ActivateSpawnAction()
    {
        if (EditorInteractions.InPlayerButton())
        {
            Activate();
        };
    }


    private void AddToPool()
    {
        EditorInteractions.AddToPool(prefab);
    }

    private void ActivateScaleAction()
    {
        if (EditorInteractions.InPlayerButton())
        {
            Activate();
        };
    }

    public void Activate()
    {
        Quaternion spawnRotation = Quaternion.Euler(rotation);

        if (spawnPositionType == SpawnType.ReferenceTransform)
        {
            position = spawnPointReference.position;
            
            if (spawnRotationReference)
            {
                spawnRotation = spawnRotationReference.rotation;
            };
        };

        GameObject spawn = prefab.SpawnObject(position, spawnRotation);

        spawn.transform.parent = parent;

        debug.Log("Spawned Object >> " + spawn.name);
    }


}
