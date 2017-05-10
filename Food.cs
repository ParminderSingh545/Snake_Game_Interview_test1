using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour 
{
	public snakeGameManager gml;

	// Use this for initialization
	void Start () 
	{
		gml = SnakeSingleton.GetInstance ().gm;
	}

	void OnTriggerEnter2D(Collider2D col)
	{	
		gml.Spawn (gml.element, gml.gameObj, gml.snake, gml.direction);
		gml.SpawnFood (gml.food, gml.gameObj);
		gml.score = gml.score + 1;
		gml.score_text.text = gml.score.ToString ();
		Destroy (gameObject);
	}
}
