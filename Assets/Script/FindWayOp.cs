using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FindWayOp : ScriptableObject
{
	protected Dictionary<int2, WayNode> WayNodes = new Dictionary<int2, WayNode>();

	public virtual bool StartFindWay(Map map, out WayNode path)
	{
		path = null;
		return false;
	}

	public WayNode this[int2 pos] => WayNodes[pos];

	public virtual List<WayNode> GetSurround(Map map, WayNode center)
	{
		var result = new List<WayNode>();

		var centerPos = center.Current;
		var lPos = centerPos + new int2(-1, 0);
		var rPos = centerPos + new int2(1, 0);
		var uPos = centerPos + new int2(0, 1);
		var dPos = centerPos + new int2(0, -1);

		if (map.Avaliable(lPos))
		{
			result.Add(this[lPos]);
		}

		if (map.Avaliable(rPos))
		{
			result.Add(this[lPos]);
		}

		if (map.Avaliable(uPos))
		{
			result.Add(this[uPos]);
		}

		if (map.Avaliable(dPos))
		{
			result.Add(this[dPos]);
		}

		return result;
	}
}