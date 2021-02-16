//=========================================
//Author: 洪金敏
//Email: jonny.hong91@gmail.com
//Create Date: 2021-02-16 12:15:23
//Description: 
//=========================================

using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace AStar
{
	[CreateAssetMenu(fileName = "AStar", menuName = "FindWay/astar", order = 0)]
	public class AStarWay : FindWayOp
	{
		private Map _map;
		private int2 _startPos;
		private int2 _endPos;

		private List<AStarWayNode> _openList = new List<AStarWayNode>();
		private List<AStarWayNode> _closeList = new List<AStarWayNode>();

		public override bool StartFindWay(Map map, out WayNode path)
		{
			_openList.Clear();
			_closeList.Clear();

			_map = map;

			var startNode = map.FindWayData.Entrance;
			var endNode = map.FindWayData.Destination;
			_startPos = startNode.Pos;
			_endPos = endNode.Pos;
			//起点终点不可达
			if (!startNode.Walkable || !endNode.Walkable)
			{
				return base.StartFindWay(map, out path);
			}

			var node = new AStarWayNode(_startPos, new int2(-1, -1));
			node.CalcF(map);
			_openList.Add(node);

			while (_openList.Count > 0)
			{
				//获得F最小的点
				var minFNode = GetMinF();
				//从列表中移除
				_openList.Remove(minFNode);
				//加入关闭列表
				_closeList.Add(minFNode);

				var surroundPoints = GetSurround(map, minFNode);

				foreach (var surroundPoint in surroundPoints)
				{
					var surround = (AStarWayNode) surroundPoint;
					//在关闭列表中 丢弃
					if (!_closeList.Contains(surround) && !_openList.Contains(surround))
					{
						surround.CalcF(map);
						surround.Parent = minFNode.Current;
						if (surround.Current.Equals(map.FindWayData.Destination.Pos))
						{
							path = surround;
							return true;
						}
						else
						{
							_openList.Add(surround);
						}
					}
				}
			}

			return base.StartFindWay(map, out path);
		}

		private AStarWayNode GetMinF()
		{
			if (_openList.Count == 0)
			{
				return null;
			}

			if (_openList.Count == 1)
			{
				return _openList[0];
			}

			_openList.Sort((a, b) =>
			{
				if (a.F > b.F)
				{
					return 1;
				}
				else if (a.F < b.F)
				{
					return -1;
				}

				return 0;
			});

			return _openList[0];
		}

		public override List<WayNode> GetSurround(Map map, WayNode center)
		{
			var result = new List<WayNode>();

			var centerPos = center.Current;
			var lPos = centerPos + new int2(-1, 0);
			var rPos = centerPos + new int2(1, 0);
			var uPos = centerPos + new int2(0, 1);
			var dPos = centerPos + new int2(0, -1);

			if (map.Avaliable(lPos) && map.FindWayData[lPos].Walkable)
			{
				if (!WayNodes.ContainsKey(lPos))
				{
					WayNodes[lPos] = new AStarWayNode(lPos, new int2(-1, -1));
				}

				result.Add(this[lPos]);
			}

			if (map.Avaliable(rPos) && map.FindWayData[rPos].Walkable)
			{
				if (!WayNodes.ContainsKey(rPos))
				{
					WayNodes[rPos] = new AStarWayNode(rPos, new int2(-1, -1));
				}

				result.Add(this[rPos]);
			}

			if (map.Avaliable(uPos) && map.FindWayData[uPos].Walkable)
			{
				if (!WayNodes.ContainsKey(uPos))
				{
					WayNodes[uPos] = new AStarWayNode(uPos, new int2(-1, -1));
				}

				result.Add(this[uPos]);
			}

			if (map.Avaliable(dPos) && map.FindWayData[dPos].Walkable)
			{
				if (!WayNodes.ContainsKey(dPos))
				{
					WayNodes[dPos] = new AStarWayNode(dPos, new int2(-1, -1));
				}

				result.Add(this[dPos]);
			}

			return result;
		}
	}
}