using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSingleton
{
	private SnakeSingleton()
	{
		
	}

	~SnakeSingleton()
	{
		
	}

	private static SnakeSingleton ins = null;

	public static SnakeSingleton GetInstance()
	{
		SnakeSingleton lclins = ins;

		if (lclins == null) 
		{
			lclins = new SnakeSingleton ();
			ins = lclins;
		} 
		else 
		{
			lclins = ins;
		}

		return lclins;
	}

	public snakeGameManager gm;
}
