using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
	[Serializable]
	public struct prefabsSet {
		public GameObject prefab;
		public GameObject parent;
	}
	
	[SerializeField]
	private prefabsSet[] prefabs;
	
	[Serializable]
	public struct mapItem {
		public int idPrefab;
		public Vector2 position;
		public float rotation;
	}
	
	[Serializable]
	public struct level {
		public mapItem[] items;
	}
	
	[SerializeField]
	private level[] levels;
	
	void Awake() {
		// ID of the level to load
		int levelToLoad = 0;
		// Level object 
		level level = levels[levelToLoad];
		
		foreach (mapItem it in level.items) {
			//Debug.Log(it.idPrefab);
			Instantiate(prefabs[it.idPrefab].prefab, new Vector3(it.position.x - 500, 0, it.position.y), Quaternion.Euler(0, it.rotation, 0), prefabs[it.idPrefab].parent.transform);
		}
	}

}
