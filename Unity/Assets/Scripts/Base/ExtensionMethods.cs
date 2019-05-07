using System;
using System.Collections.Generic;

public static class ExtensionMethods
{
	public static int WeightedRandom(this List<int> weights, System.Random rand)
	{
		int sum = 0;
		weights.ForEach(delegate(int weight)
		{
			sum += weight;
		});
		int num = rand.Next(0, sum);
		for (int i = 0; i < weights.Count; i++)
		{
			num -= weights[i];
			if (num <= 0 && weights[i] > 0)
			{
				return i;
			}
		}
		return 0;
	}


}
