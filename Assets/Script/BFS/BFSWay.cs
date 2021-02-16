//=========================================
//Author: 洪金敏
//Email: jonny.hong91@gmail.com
//Create Date: 2021-02-12 17:35:08
//Description: 
//=========================================

using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace BFS
{
	[CreateAssetMenu(fileName = "BFS", menuName = "FindWay/bfs", order = 0)]
	public class BFSWay : FindWayOp
	{
		private Map _map;
		private int2 _endPos;

		public override bool StartFindWay(Map map, out WayNode path)
		{
			_map = map;
			var startNode = map.FindWayData.Entrance;
			var endNode = map.FindWayData.Destination;
			_endPos = endNode.Pos;
			//起点终点不可达
			if (!startNode.Walkable || !endNode.Walkable)
			{
				return base.StartFindWay(map, out path);
			}

			//检查队列
			var queue = new Queue<Floor>();
			//已经检查过的点
			var checkedDic = new Dictionary<Floor, bool>();
			queue.Enqueue(startNode);

			while (queue.Count > 0)
			{
				var tmp = queue.Dequeue();
				var tmpPos = tmp.Pos;
				var lPos = tmpPos + new int2(-1, 0);
				var rPos = tmpPos + new int2(1, 0);
				var uPos = tmpPos + new int2(0, 1);
				var dPos = tmpPos + new int2(0, -1);
				var lFloor = _map.FindWayData[lPos];
				var rFloor = _map.FindWayData[rPos];
				var uFloor = _map.FindWayData[uPos];
				var dFloor = _map.FindWayData[dPos];
				if (lFloor && !checkedDic.ContainsKey(lFloor))
				{
					var w = CheckPosNode(tmp, lFloor);
					if (w.Item1 != null)
					{
						WayNodes[lPos] = w.Item1;
						queue.Enqueue(lFloor);
						if (w.Item2)
						{
							path = w.Item1;
							return true;
						}
					}

					checkedDic.Add(lFloor, true);
				}

				if (rFloor && !checkedDic.ContainsKey(rFloor))
				{
					var w = CheckPosNode(tmp, rFloor);
					if (w.Item1 != null)
					{
						WayNodes[rPos] = w.Item1;
						queue.Enqueue(rFloor);
						if (w.Item2)
						{
							path = w.Item1;
							return true;
						}
					}

					checkedDic.Add(rFloor, true);
				}

				if (uFloor && !checkedDic.ContainsKey(uFloor))
				{
					var w = CheckPosNode(tmp, uFloor);
					if (w.Item1 != null)
					{
						WayNodes[uPos] = w.Item1;
						queue.Enqueue(uFloor);
						if (w.Item2)
						{
							path = w.Item1;
							return true;
						}
					}

					checkedDic.Add(uFloor, true);
				}

				if (dFloor && !checkedDic.ContainsKey(dFloor))
				{
					var w = CheckPosNode(tmp, dFloor);
					if (w.Item1 != null)
					{
						WayNodes[dPos] = w.Item1;
						queue.Enqueue(dFloor);
						if (w.Item2)
						{
							path = w.Item1;
							return true;
						}
					}

					checkedDic.Add(dFloor, true);
				}
			}

			return base.StartFindWay(map, out path);
		}

		private (WayNode, bool) CheckPosNode(Floor parentFloor, Floor checkedFloor)
		{
			if (_map.Avaliable(checkedFloor.Pos))
			{
				if (checkedFloor.Walkable)
				{
					var wayNode = new WayNode(checkedFloor.Pos, parentFloor.Pos);
					var isReach = checkedFloor.Pos.Equals(_endPos);
					return (wayNode, isReach);
				}
			}

			return (null, false);
		}
	}
}