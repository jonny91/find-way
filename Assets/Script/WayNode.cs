//=========================================
//Author: 洪金敏
//Email: jonny.hong91@gmail.com
//Create Date: 2021-02-15 17:58:37
//Description: 
//=========================================

using Unity.Mathematics;

public class WayNode
{
	public int2 Parent;
	public int2 Current;

	public WayNode(int2 current, int2 parent)
	{
		Current = current;
		Parent = parent;
	}
}