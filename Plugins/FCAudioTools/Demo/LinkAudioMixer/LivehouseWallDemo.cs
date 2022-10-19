using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

namespace FutureCartographer.FCAudioTools.Demo
{
	public class LivehouseWallDemo : MonoBehaviour
	{
		public Transform human;

		public Transform rotateLights;
		public float rotateLightsSpeed;
		public float rotateHumanSpeed;

		public AudioMixer mixer;
		public AudioMixerSnapshot snapshotInside;
		public AudioMixerSnapshot snapshotOutside;

		public GUISkin skin;

		private float transitionWeight;

		void Start()
		{
			snapshotInside.TransitionTo(0);
			transitionWeight = 1;
		}

		void Update()
		{
			rotateLights.Rotate(new Vector3(0, rotateLightsSpeed * Time.deltaTime, 0));
			human.Rotate(new Vector3(0, rotateHumanSpeed * Time.deltaTime, 0));

			if (Input.GetMouseButtonDown(0) == true)
			{
				var mp = Input.mousePosition;
				var vpray = Camera.main.ScreenPointToRay(mp);

				RaycastHit hit;
				if (Physics.Raycast(vpray, out hit) == true)
				{
					var tw = hit.collider.gameObject.GetComponent<TransitionWeight>();
					if (tw != null)
					{
						var mxx = new AudioMixerSnapshot[2] { snapshotInside, snapshotOutside };
						var mfw = new float[2] { tw.weight, 1.0f - tw.weight };

						mixer.TransitionToSnapshots(mxx, mfw, 0.3f);

						transitionWeight = tw.weight;
					}

					human.position = new Vector3(hit.point.x, 0.1f, hit.point.z);
				}
			}
		}

		void OnGUI()
		{
			GUI.color = Color.white;
			GUI.contentColor = Color.white;

			GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

			GUILayout.Label("Move to the clicked location. Change to the suitable weight.");

			GUILayout.Label("Audio Mixer Snapshot weight , inside <-> outside : " + transitionWeight);

			GUILayout.EndArea();
		}
	}
}
