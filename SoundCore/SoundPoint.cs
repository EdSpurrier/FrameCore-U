using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpatialSoundType
{
    Spatial3d,
    Static2d
}




[System.Serializable]
public class SoundPointData
{
    [HorizontalGroup("Sound Point Header")]
    [HideLabel]
    public string soundName = "Sound Name";
    [HorizontalGroup("Sound Point Header")]
    [HideLabel]
    public Debounce debounce;

    public List<AudioClip> clips;

    [FoldoutGroup("Sound Point Data")]

    [HorizontalGroup("Sound Point Data/SplitA", 0.5f)]


    [BoxGroup("Sound Point Data/SplitA/Play Settings")]
    [ToggleLeft]
    public bool playOnAwake = false;
    [BoxGroup("Sound Point Data/SplitA/Play Settings")]
    [ToggleLeft]
    public bool loop = false;

    [BoxGroup("Sound Point Data/SplitA/Priority")]
    [HideLabel]
    [SuffixLabel("Priority", Overlay = true)]
    public LevelSetting prioritySetting = LevelSetting.Medium;
    int priority = 256;

    // Having multiple properties in a column can be achived using multiple groups. Checkout the "Combining Group Attributes" example.
    [HorizontalGroup("Sound Point Data/Split", 0.5f)]
    [BoxGroup("Sound Point Data/Split/Spatial")]
    [HideLabel]
    public SpatialSoundType spatial3d = SpatialSoundType.Spatial3d;
    [BoxGroup("Sound Point Data/Split/Spatial")]
    [ShowIf("@this.spatial3d == SpatialSoundType.Spatial3d")]
    [HideLabel]
    [SuffixLabel("Ref Position", Overlay = true)]
    public Transform referencePosition;
    [BoxGroup("Sound Point Data/Split/Spatial")]
    [ShowIf("@this.spatial3d == SpatialSoundType.Spatial3d && this.referencePosition != null")]
    [ToggleLeft]
    public bool followReferencePosition;


    

    
    [BoxGroup("Sound Point Data/Split/Distance")]
    [HideLabel]
    [ShowIf("@this.spatial3d == SpatialSoundType.Spatial3d")]
    public AudioRolloffMode rolloffMode = AudioRolloffMode.Linear;

    [BoxGroup("Sound Point Data/Split/Distance")]
    [HideLabel]
    [ShowIf("@this.spatial3d == SpatialSoundType.Spatial3d")]
    public MaxMinFloat distance = new MaxMinFloat { 
        min = 1f,
        max = 7f
    };





    // Having multiple properties in a column can be achived using multiple groups. Checkout the "Combining Group Attributes" example.
    [HorizontalGroup("Sound Point Data/Split2", 0.5f)]


    [BoxGroup("Sound Point Data/Split2/Volume")]
    [ToggleLeft]
    public bool randomVolume = true;

    [BoxGroup("Sound Point Data/Split2/Volume")]
    [HideIf("randomVolume")]
    [SuffixLabel("Volume", Overlay = true)]
    [HideLabel]
    public float volume = 1f;

    [BoxGroup("Sound Point Data/Split2/Volume")]
    [ShowIf("randomVolume")]
    [HideLabel]
    public MaxMinFloat volumeRange = new MaxMinFloat
    {
        min = 0.8f,
        max = 1f,
    };




    [BoxGroup("Sound Point Data/Split2/Pitch")]
    [ToggleLeft] 
    public bool randomPitch = true;

    [BoxGroup("Sound Point Data/Split2/Pitch")]
    [HideIf("randomPitch")]
    [SuffixLabel("Pitch", Overlay = true)]
    [HideLabel]
    public float pitch = 1f;

    [BoxGroup("Sound Point Data/Split2/Pitch")]
    [ShowIf("randomPitch")]
    [HideLabel]
    public MaxMinFloat pitchRange = new MaxMinFloat
    {
        min = 0.9f,
        max = 1f,
    };


    
    
    
    public void PositionSoundPoint(AudioSource audioSource)
    {
        if (spatial3d == SpatialSoundType.Spatial3d)
        {
            audioSource.transform.position = referencePosition.position;
        };
    }


    public void Apply(AudioSource audioSource)
    {
        audioSource.priority = priority;
        audioSource.playOnAwake = playOnAwake;
        audioSource.loop = loop;
        audioSource.rolloffMode = rolloffMode;
        audioSource.minDistance = distance.min;
        audioSource.maxDistance = distance.max;

        if (spatial3d == SpatialSoundType.Spatial3d)
        {
            audioSource.spatialBlend = 1f;
        }
        else {
            audioSource.spatialBlend = 0f;
        };

        PositionSoundPoint(audioSource);
    }

    public AudioClip GetClip()
    {
        return clips[Random.Range(0, clips.Count)];
    }

    public float GetVolume()
    {
        if (randomVolume)
        {
            return Random.Range(volumeRange.min, volumeRange.max);
        } else {
            return volume;
        };
    }

    public float GetPitch()
    {
        if (randomPitch)
        {
            return Random.Range(pitchRange.min, pitchRange.max);
        }
        else
        {
            return pitch;
        };
    }

    public void Play(AudioSource audioSource)
    {
        Apply(audioSource);
        audioSource.clip = GetClip();
        audioSource.volume = GetVolume();
        audioSource.pitch = GetPitch();
        audioSource.Play();
    }

    public void Init()
    {
        priority = Frame.core.sound.GetPriority(prioritySetting);
    }
}

public class SoundPoint : MonoBehaviour
{
    
    [Title("Sound Point Data")]
    public SoundPointData soundPointData;

    [Title("Parts")]
    public AudioSource audioSource;



    private void Start()
    {
        soundPointData.Init();
    }




    public void Play(SoundPointData soundPointData)
    {

        this.soundPointData = soundPointData;

        this.soundPointData.Play(audioSource);

        Frame.core.sound.RegisterSoundPoint(this);

    }




    public void Activate()
    {
        UnMute();
    }


    public void Deactivate()
    {
        Mute();
    }



    //  SOUND CONTROLS
    public void Mute()
    {
        audioSource.mute = true;
    }

    public void UnMute()
    {
        audioSource.mute = false;
    }



    public void DestroySoundPoint()
    {
        Frame.core.sound.UnregisterSoundPoint(this);

        Destroy(gameObject);
    }



    void Update()
    {
        if (!audioSource.isPlaying)
        {
            DestroySoundPoint();
        }
        //  IF FOLLOW REFERENCE POSITION
        else if (soundPointData.followReferencePosition) {

            transform.position = soundPointData.referencePosition.position;

        };
    }
}
