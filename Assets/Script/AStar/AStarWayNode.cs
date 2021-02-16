//=========================================
//Author: 洪金敏
//Email: jonny.hong91@gmail.com
//Create Date: 2021-02-16 13:48:17
//Description: 
//=========================================

using Unity.Mathematics;
using UnityEngine;

namespace AStar
{
	public class AStarWayNode : WayNode
	{
		public int F => G + H;
		public int G { get; set; }
		public int H { get; set; }

		public AStarWayNode(int2 current, int2 parent) : base(current, parent)
		{
		}

		/// <summary>
		/// 计算G  H
		/// </summary>
		/// <param name="map"></param>
		public void CalcF(Map map)
		{
			G = Mathf.Abs(map.FindWayData.Entrance.Pos.x - Current.x) +
			    Mathf.Abs(map.FindWayData.Entrance.Pos.y - Current.y);

			H = Mathf.Abs(map.FindWayData.Destination.Pos.x - Current.x) +
			    Mathf.Abs(map.FindWayData.Destination.Pos.y - Current.y);
		}
	}
}