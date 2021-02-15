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
using Random = UnityEngine.Random;

public class Sample : MonoBehaviour
{
	[SerializeField]
	private Map Map;

	public EditorAction EditorAction { get; set; } = EditorAction.NONE;

	public FindWayOp FindWayOp;

	private void Start()
	{
		var offset = new Vector3((Map.Column - 1) / 2.0f, 0, (Map.Row - 1) / 2.0f);

		for (int c = 0; c < Map.Column; c++)
		{
			for (int r = 0; r < Map.Row; r++)
			{
				var h = Addressables.InstantiateAsync("Floor");
				var floor = h.WaitForCompletion();
				var f = floor.EnsureComponent<Floor>();
				f.name = $"{r}_{c}";
				f.Pos = new int2(c, r);
				f.transform.position = new Vector3(f.Pos.x, 0, f.Pos.y) - offset;

				var rand = Random.Range(0, 100);
				if (rand > 60)
				{
					f.SetFloor(FloorType.Block);
				}
				else
				{
					f.SetFloor(FloorType.None);
				}

				Map.AddFloorCache(f);
			}
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out var hit, float.MaxValue, LayerMask.GetMask("Floor")))
			{
				var hitFloor = hit.collider.GetComponent<Floor>();
				if (hitFloor != null && hitFloor.FloorType != FloorType.Block)
				{
					if (EditorAction == EditorAction.SET_DESTINATION)
					{
						Map.FindWayData.Destination = hitFloor;
					}
					else if (EditorAction == EditorAction.SET_ENTRANCE)
					{
						Map.FindWayData.Entrance = hitFloor;
					}
				}

				SetAction(EditorAction.NONE);
			}
		}
	}

	public void SetDestination()
	{
		SetAction(EditorAction.SET_DESTINATION);
	}

	public void SetEntrance()
	{
		SetAction(EditorAction.SET_ENTRANCE);
	}

	private void SetAction(EditorAction act)
	{
		EditorAction = act;
	}

	public void FindWay()
	{
		if (Map.FindWayData.OK)
		{
			if (FindWayOp.StartFindWay(Map, out var path))
			{
				ShowPath(path);
				Debug.Log("ok");
			}
			else
			{
				Debug.LogError("没找到路径!!!");
			}
		}
		else
		{
			Debug.LogError("出入口未设置...");
		}
	}

	private void ShowPath(WayNode path)
	{
		while (!path.Parent.Equals(Map.FindWayData.Entrance.Pos))
		{
			path = FindWayOp[path.Parent];
			var floor = Map.FindWayData[path.Current];
			floor.SetFloor(FloorType.Way);
		}
	}
}