//=========================================
//Author: 洪金敏
//Email: jonny.hong91@gmail.com
//Create Date: 2021-02-06 23:58:09
//Description: 
//=========================================

using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
	/// <summary>
	/// 行
	/// </summary>
	public int Row;

	/// <summary>
	/// 列
	/// </summary>
	[SerializeField]
	public int Column;

	private FindWayData _findWayData = new FindWayData();

	/// <summary>
	/// 本局数据
	/// </summary>
	public FindWayData FindWayData => _findWayData;

	public void AddFloorCache(Floor floor)
	{
		FindWayData.AddFloorCache(floor);
	}

	/// <summary>
	/// 随机生成阻挡
	/// </summary>
	public void RandomBlock()
	{
		FindWayData.RandomBlock();
	}

	/// <summary>
	/// 清理所有设置
	/// </summary>
	public void Clear()
	{
		FindWayData.Clear();
	}
}