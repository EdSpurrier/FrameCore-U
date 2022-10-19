#if DATABOX_PLAYMAKER
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using Databox;

namespace Databox.PlayMaker
{
	[ActionCategory("Databox")]
	[HutongGames.PlayMaker.Tooltip("Upload the database file to the configured cloud url")] 
	public class UploadToCloud : FsmStateAction
	{
			
		[ObjectType(typeof(DataboxObject))]
		public FsmObject databoxObject;
						
		public override void Awake()
		{
			if (databoxObject.Value != null)
			{
				var _db = databoxObject.Value as DataboxObject;
				_db.OnDatabaseCloudUploaded += OnCloudUploadReady;
				_db.OnDatabaseCloudUploadFailed += OnCloudUploadFailed;
			}
		}
						
		public override void OnEnter()
		{
			base.OnEnter();
							
			if (databoxObject.Value != null)
			{
				var _db = databoxObject.Value as DataboxObject;
				_db.UploadToCloud();
			}
			else
			{
				Debug.LogError("No databox object defined. Please assign a Databox object.");
			}
		}
						
		private void OnCloudUploadReady()
		{
			Finish();
		}
				
		private void OnCloudUploadFailed()
		{
			Debug.LogError("Upload failed. Please check server url.");
		}
	}
}
#endif