//=========================================
//Author: 洪金敏
//Email: jonny.hong91@gmail.com
//Create Date: 2021-02-11 23:49:49
//Description: 
//=========================================

using System.Collections.Generic;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class FindWayData
{
	private Dictionary<int2, Floor> _floorDic;
	private Dictionary<int2, Floor> _blockDic;
	private Floor _entrance;

	public Floor Entrance
	{
		get => _entrance;
		set
		{
			if (value != null)
			{
				value.SetFloor(FloorType.Entrance);
			}

			if (_entrance != null)
			{
				_entrance.SetFloor(FloorType.None);
			}

			_entrance = value;
		}
	}

	private Floor _destination;

	public Floor Destination
	{
		get => _destination;
		set
		{
			if (value != null)
			{
				value.SetFloor(FloorType.Destination);
			}

			if (_destination != null)
			{
				_destination.SetFloor(FloorType.None);
			}

			_destination = value;
		}
	}

	public FindWayData()
	{
		_floorDic = new Dictionary<int2, Floor>();
		_blockDic = new Dictionary<int2, Floor>();
	}

	public void AddFloorCache(Floor floor)
	{
		_floorDic.Add(floor.Pos, floor);
	}

	/// <summary>
	/// 随机生成阻挡
	/// </summary>
	public void RandomBlock()
	{
		_blockDic.Clear();
		foreach (var f in _floorDic.Values)
		{
			var rand = Random.Range(0, 100);
			if (rand > 60)
			{
				f.SetFloor(FloorType.Block);
				_blockDic.Add(f.Pos, f);
			}
			else
			{
				f.SetFloor(FloorType.None);
			}
		}
	}

	/// <summary>
	/// 重置所有 Block
	/// </summary>
	public void ResetFloor()
	{
		_blockDic.Clear();
		foreach (var f in _floorDic.Values)
		{
			f.SetFloor(FloorType.None);
		}
	}

	/// <summary>
	/// 清理
	/// </summary>
	public void Clear()
	{
		ResetFloor();
		Entrance = null;
		Destination = null;
	}
}