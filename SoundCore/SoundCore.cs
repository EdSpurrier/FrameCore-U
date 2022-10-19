using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class PriorityLevelSetting
{
    [HorizontalGroup("Split", 0.5f)]
    [HideLabel]
    public LevelSetting levelSetting;

    [HorizontalGroup("Split", 0.5f)]
    [HideLabel]
    public int priority;
}



public class SoundCore : MonoBehaviour
{

    [Title("Sound Points")]
    [InlineButton("AddToPool")]
    public Transform soundPointPrefab;

    [Title("Active Sound Points")]
    public List<SoundPoint> soundPoints = new List<SoundPoint>();


    [Title("System Settings")]
    public List<PriorityLevelSetting> priorityLevels = new List<PriorityLevelSetting> {
        new PriorityLevelSetting
        {
            levelSetting = LevelSetting.VeryHigh,
            priority = 256
        },
        new PriorityLevelSetting
        {
            levelSetting = LevelSetting.High,
            priority = 200
        },
        new PriorityLevelSetting
        {
            levelSetting = LevelSetting.Medium,
            priority = 150
        },
        new PriorityLevelSetting
        {
            levelSetting = LevelSetting.Low,
            priority = 100
        },
        new PriorityLevelSetting
        {
            levelSetting = LevelSetting.VeryLow,
            priority = 50
        }
    };

    [Title("System")]
    [HideLabel]
    public DeBugger debug;

    bool prefabInPool = false;
    PoolCore pool;

    private void OnValidate()
    {

        if (!pool)
        {
            GameObject poolCore = GameObject.Find("PoolCore");
            pool = FindObjectOfType(typeof(PoolCore)) as PoolCore;
        };

        if (pool)
        {
            prefabInPool = pool.ObjectInPool(soundPointPrefab);
        }
        else {
            prefabInPool = false;
        };

    }




    private void AddToPool()
    {
        EditorInteractions.AddToPool(soundPointPrefab);
    }


    //  GENERATE SOUNDPOINT
    public void GenerateSoundPoint(SoundPointData soundPointData)
    {

        GameObject spawn = soundPointPrefab.SpawnObject(Vector3.zero, Quaternion.identity);

        SoundPoint soundPoint = spawn.GetComponent<SoundPoint>();
        soundPoint.Play(soundPointData);


        debug.Log("GenerateSoundPoint >> " + soundPointData.soundName);
    }


    public void RegisterSoundPoint(SoundPoint soundPoint)
    {
        if (!soundPoints.Contains(soundPoint))
        {
            soundPoints.Add(soundPoint);
        };
    }


    public void UnregisterSoundPoint(SoundPoint soundPoint)
    {
        if (soundPoints.Contains(soundPoint))
        {
            soundPoints.Remove(soundPoint);
        };
    }

    public void DestroyAllSoundPoints()
    {
        soundPoints.ForEach(soundPoint => {
            soundPoint.DestroySoundPoint();
        });
    }




    public int GetPriority(LevelSetting levelSetting)
    {
        return priorityLevels.FirstOrDefault(setting => (setting.levelSetting == levelSetting)).priority;
    }


}
