#if DATABOX_PLAYMAKER
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using Databox;

namespace Databox.PlayMaker
{
	[ActionCategory("Databox")]
	[HutongGames.PlayMaker.Tooltip("Download the database file from the configured cloud url")] 
	public class DownloadFromCloud : FsmStateAction
	{
		
		[ObjectType(typeof(DataboxObject))]
		public FsmObject databoxObject;
				
		public override void Awake()
		{
			if (databoxObject.Value != null)
			{
				var _db = databoxObject.Value as DataboxObject;
				_db.OnDatabaseCloudDownloaded += OnCloudDownloadReady;
				_db.OnDatabaseCloudDownloadFailed += OnCloudDownloadFailed;
			}
		}
				
		public override void OnEnter()
		{
			base.OnEnter();
				
			if (databoxObject.Value != null)
			{
				var _db = databoxObject.Value as DataboxObject;
				_db.DownloadFromCloud();
			}
			else
			{
				Debug.LogError("No databox object defined. Please assign a Databox object.");
			}
		}
				
		private void OnCloudDownloadReady()
		{
			Finish();
		}
		
		private void OnCloudDownloadFailed()
		{
			Debug.LogError("Download failed. Please check server url.");
		}
	}
}
#endif