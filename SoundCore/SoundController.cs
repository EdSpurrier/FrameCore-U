using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundController : MonoBehaviour
{
	[Title("Sound Data")]
	public List<SoundPointData> sounds;

    private void Start()
    {

		int soundId = 0;
		sounds.ForEach(sound => {
			SoundPointData soundPointData = sounds[soundId];
			if (soundPointData.playOnAwake)
            {
				Play(soundId);
				soundId++;
				Debug.Log("Playing Sound");
			};
		});

    }

	public void Play(int soundId)
	{
		//  IF TRUE => DEBOUNCE IS CURRENTLY ACTIVE
		if (sounds[soundId].debounce.CheckDebounce())
		{
			return;
		};

		Frame.core.sound.GenerateSoundPoint(sounds[soundId]);
	}

	public void Play(string soundName)
	{
		SoundPointData sound = sounds.First(soundData => (soundData.soundName == soundName));

		//  IF TRUE => DEBOUNCE IS CURRENTLY ACTIVE
		if (sound.debounce.CheckDebounce())
		{
			return;
		};

		Frame.core.sound.GenerateSoundPoint(sound);
	}




    private void OnValidate()
    {
		//	SET REFERENCE POSITION AS SELF TO BEGIN WITH
		sounds.ForEach(sound => {
			
			if (!sound.referencePosition)
			{
				sound.referencePosition = transform;
			};
		});
	}
}

