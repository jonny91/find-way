//=========================================
//Author: 洪金敏
//Email: jonny.hong91@gmail.com
//Create Date: 2021/02/06 23:53:39
//Description: 
//=========================================

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Sample : MonoBehaviour
{
	[SerializeField]
	private Map Map;

	private void Start()
	{
		for (int c = 0; c < Map.Column; c++)
		{
			for (int r = 0; r < Map.Row; r++)
			{
				var h = Addressables.InstantiateAsync("Floor");
				var floorPrefab = h.WaitForCompletion();
				var floor = Instantiate(floorPrefab, transform);
				var f = floor.EnsureComponent<Floor>();
				f.Pos = new int2(c, r);
				f.SetFloor(FloorType.Block);
			}
		}
	}
}