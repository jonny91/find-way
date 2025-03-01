﻿//=========================================
//Author: 洪金敏
//Email: jonny.hong91@gmail.com
//Create Date: 2021/02/06 23:53:33
//Description: 
//=========================================

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Floor : MonoBehaviour
{
	[SerializeField]
	private GameObject None;

	[SerializeField]
	private GameObject Block;

	[SerializeField]
	private GameObject Entrance;

	[SerializeField]
	private GameObject Destination;

	[SerializeField]
	private GameObject Way;

	public FloorType FloorType { get; set; }

	/// <summary>
	/// 地板的位置
	/// </summary>
	public int2 Pos;

	/// <summary>
	/// 设置地板类型
	/// </summary>
	/// <param name="type"></param>
	public void SetFloor(FloorType type)
	{
		FloorType = type;
		None.SetActive(false);
		Block.SetActive(false);
		Entrance.SetActive(false);
		Destination.SetActive(false);
		Way.SetActive(false);
		switch (type)
		{
			case FloorType.None:
				None.SetActive(true);
				break;
			case FloorType.Block:
				Block.SetActive(true);
				break;
			case FloorType.Entrance:
				Entrance.SetActive(true);
				break;
			case FloorType.Destination:
				Destination.SetActive(true);
				break;
			case FloorType.Way:
				Way.SetActive(true);
				break;
		}
	}

	/// <summary>
	/// 可以行走
	/// </summary>
	public bool Walkable => FloorType != FloorType.Block;
}