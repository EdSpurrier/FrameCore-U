#if DATABOX_PLAYMAKER
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using Databox;

namespace Databox.PlayMaker
{
	[ActionCategory("Databox")]
	[HutongGames.PlayMaker.Tooltip("Get data from Databox object and store it to a PlayMaker variable.")]
	[HelpUrl("http://databox.doorfortyfour.com/documentation/playmaker/actions/getdata")]     
	public class GetData : FsmStateAction
	{
		
		[ObjectType(typeof(DataboxObject))]
		[RequiredField]  
		[UIHint(UIHint.Variable)]
		public FsmObject databoxObject;
		
		public Data data = new Data();
		
		public enum DataType
		{
			Float,
			Int,
			Bool,
			String,
			Color,
			Quaternion,
			Vector2,
			Vector3,
			GameObject,
			Material,
			Texture,
			Sprite,
			AudioClip
		}
		
		public DataType dataType;
		
		public bool usePlayMakerIDFields;
		// use playmaker variable fields instead of predefined popup selection
		public FsmString table;
		public FsmString entry;
		public FsmString value;
		
		public FsmFloat storeResultFloat;
		public FsmInt storeResultInt;
		public FsmBool storeResultBool;
		public FsmColor storeResultColor;
		public FsmString storeResultString;
		public FsmVector2 storeResultVector2;
		public FsmVector3 storeResultVector3;
		public FsmQuaternion storeResultQuaternion;
		public FsmGameObject storeResultGameObject;
		public FsmMaterial storeResultMaterial;
		public FsmTexture storeResultTexture;
		public FsmObject storeResultSprite;
		public FsmObject storeResultAudioClip;
		
	
		
		public override void OnEnter()
		{
			base.OnEnter();
			
			var _db = databoxObject.Value as DataboxObject;
			
			if (data.useOwnerInstanceID)
			{
				data.selectedEntry = Owner.gameObject.GetInstanceID().ToString();
			}
			
			var _selectedTable = data.selectedTable;
			var _selectedEntry = data.selectedEntry;
			var _selectedValue = data.selectedValue;
			
			if (usePlayMakerIDFields)
			{
				_selectedTable = table.Value;
				_selectedEntry = entry.Value;
				_selectedValue = value.Value;
			}
		
			switch (dataType)
			{
				case DataType.Int:
					var _valueInt = _db.GetData<IntType>(_selectedTable, _selectedEntry, _selectedValue);
					storeResultInt.Value = _valueInt.Value;
					break;
				case DataType.Float:
					var _valueFloat = _db.GetData<FloatType>(_selectedTable, _selectedEntry, _selectedValue);
					storeResultFloat.Value = _valueFloat.Value;
					break;
				case DataType.Bool:
					var _valueBool = _db.GetData<BoolType>(_selectedTable, _selectedEntry, _selectedValue);
					storeResultBool.Value = _valueBool.Value;
					break;
				case DataType.String:
					var _valueString = _db.GetData<StringType>(_selectedTable, _selectedEntry, _selectedValue);
					storeResultString.Value = _valueString.Value;
					break;
				case DataType.Color:
					var _valueColor = _db.GetData<ColorType>(_selectedTable, _selectedEntry, _selectedValue);
					storeResultColor.Value = _valueColor.Value;
					break;
				case DataType.Quaternion:
					var _valueQuaternion = _db.GetData<QuaternionType>(_selectedTable, _selectedEntry, _selectedValue);
					storeResultQuaternion.Value = _valueQuaternion.Value;
					break;
				case DataType.Vector2:
					var _valueVector2 = _db.GetData<Vector2Type>(_selectedTable, _selectedEntry, _selectedValue);
					storeResultVector2.Value = _valueVector2.Value;
					break;
				case DataType.Vector3:
					var _valueVector3 = _db.GetData<Vector3Type>(_selectedTable, _selectedEntry, _selectedValue);
					storeResultVector3.Value = _valueVector3.Value;
					break;
				case DataType.GameObject:
					var _valueGameObject = _db.GetData<ResourceType>(_selectedTable, _selectedEntry, _selectedValue).Load() as GameObject;
					storeResultGameObject.Value = _valueGameObject;
					break;
				case DataType.Material:
					var _valueMaterial = _db.GetData<ResourceType>(_selectedTable, _selectedEntry, _selectedValue).Load() as Material;
					storeResultMaterial.Value = _valueMaterial;
					break;
				case DataType.Texture:
					var _valueTexture = _db.GetData<ResourceType>(_selectedTable, _selectedEntry, _selectedValue).Load() as Texture;
					storeResultTexture.Value = _valueTexture;
					break;
				case DataType.Sprite:
					var _valueSprite = _db.GetData<ResourceType>(_selectedTable, _selectedEntry, _selectedValue).Load() as Sprite;
					storeResultSprite.Value = _valueSprite;
					break;
				case DataType.AudioClip:
					var _valueAudioClip = _db.GetData<ResourceType>(_selectedTable, _selectedEntry, _selectedValue).Load() as AudioClip;
					storeResultAudioClip.Value = _valueAudioClip;
					break;
			}
		
			
			Finish();
		}
	}
}
#endif