using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class snakeGameManager : MonoBehaviour 
{
	public GameObject element;
	public GameObject food;
	public GameObject gameObj;
	public List<GameObject> snake;
	public bool canMove = false;
	public int direction = 0;
	public int ldirection = 0;
	public float snakeSpeed = 50;
	public float GameSpeed = 0.1f;
	public int score = 0;
	public Text score_text;

	// Use this for initialization
	void Start () 
	{
		SnakeSingleton.GetInstance ().gm = this;
		Spawn (element,gameObj,snake,direction);
		SpawnFood (food,gameObj);
		canMove = true;
		StartCoroutine (GameLoop(GameSpeed));
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey ("up"))
			direction = 0;
		else
		if (Input.GetKey("down"))
			direction = 1;
		else
		if (Input.GetKey("left"))
			direction = 2;
		else
		if (Input.GetKey("right"))
			direction = 3;
		if (ldirection == 0 && direction == 1) 
		{
			direction = 0;
		}
		else
		if(ldirection == 1 && direction == 0)
		{
			direction = 1;
		}
		else
		if(ldirection == 2 && direction == 3)
		{
			direction = 2;
		}
		else
		if(ldirection == 3 && direction == 2)
		{
			direction = 3;
		}
	}

	public IEnumerator GameLoop(float gameSpeed)
	{
		if (canMove) 
		{
			Move (direction,snake,snakeSpeed);
		}
		ldirection = direction;
		yield return new WaitForSeconds (gameSpeed);

		StartCoroutine (GameLoop(gameSpeed));
	}

	public GameObject Spawn(GameObject obj)
	{
		return Instantiate (obj,obj.GetComponent<RectTransform>().localPosition,Quaternion.identity) as GameObject;
	}

	public void Spawn(GameObject obj,GameObject parent,List<GameObject> _snake,int dir)
	{
		int dx = 0;
		int dy = 0;

		if (dir == 0)
			dy = -50;
		else 
		if (dir == 1)
			dy = 50;
		else 
		if (dir == 2)
			dx = -50;
		else 
		if (dir == 3)
			dx = 50;
		
		GameObject go = null;

		go =  Instantiate (obj,new Vector3(obj.GetComponent<RectTransform>().localPosition.x,obj.GetComponent<RectTransform>().localPosition.y,obj.GetComponent<RectTransform>().localPosition.z),Quaternion.identity) as GameObject;
		if(_snake.Count > 1)
			go.GetComponent<RectTransform> ().localPosition = new Vector3 (_snake [_snake.Count - 1].GetComponent<RectTransform> ().localPosition.x + dx, _snake [_snake.Count - 1].GetComponent<RectTransform> ().localPosition.y + dy, _snake [_snake.Count - 1].GetComponent<RectTransform> ().localPosition.z);
		go.transform.SetParent (parent.transform,false);
		_snake.Add (go);
	}
		
	public void Move(int dir,List<GameObject> _snake,float speedDiff)
	{
		int x_s = 1920;
		int y_s = 1080;

		List<Vector3> initialLoc = new List<Vector3> ();

		for(int i = 0; i < _snake.Count - 1; i++)
		{
			initialLoc.Add (_snake[i].GetComponent<RectTransform>().localPosition);
		}

		if (dir == 0) 
		{
			// up
			_snake[0].GetComponent<RectTransform>().localPosition = new Vector3(_snake[0].GetComponent<RectTransform>().localPosition.x,_snake[0].GetComponent<RectTransform>().localPosition.y+speedDiff,_snake[0].GetComponent<RectTransform>().localPosition.z);
		} 
		else 
		if (dir == 1) 
		{
			// down	
			_snake[0].GetComponent<RectTransform>().localPosition = new Vector3(_snake[0].GetComponent<RectTransform>().localPosition.x,_snake[0].GetComponent<RectTransform>().localPosition.y-speedDiff,_snake[0].GetComponent<RectTransform>().localPosition.z);
		} 
		else 
		if (dir == 2) 
		{
			// left
			_snake[0].GetComponent<RectTransform>().localPosition = new Vector3(_snake[0].GetComponent<RectTransform>().localPosition.x-speedDiff,_snake[0].GetComponent<RectTransform>().localPosition.y,_snake[0].GetComponent<RectTransform>().localPosition.z);
		} 
		else 
		if (dir == 3)
		{
			//	right
			_snake[0].GetComponent<RectTransform>().localPosition = new Vector3(_snake[0].GetComponent<RectTransform>().localPosition.x+speedDiff,_snake[0].GetComponent<RectTransform>().localPosition.y,_snake[0].GetComponent<RectTransform>().localPosition.z);
		}
		
		if (_snake [0].GetComponent<RectTransform> ().localPosition.y > y_s / 2) 
		{
			_snake[0].GetComponent<RectTransform>().localPosition = new Vector3(_snake[0].GetComponent<RectTransform>().localPosition.x,-(y_s / 2),_snake[0].GetComponent<RectTransform>().localPosition.z);
		}
		else
		if (_snake [0].GetComponent<RectTransform> ().localPosition.y < -(y_s / 2)) 
		{
			_snake[0].GetComponent<RectTransform>().localPosition = new Vector3(_snake[0].GetComponent<RectTransform>().localPosition.x,(y_s / 2),_snake[0].GetComponent<RectTransform>().localPosition.z);
		}
		else
		if (_snake [0].GetComponent<RectTransform> ().localPosition.x > x_s / 2) 
		{
			_snake[0].GetComponent<RectTransform>().localPosition = new Vector3(-(x_s / 2),_snake[0].GetComponent<RectTransform>().localPosition.y,_snake[0].GetComponent<RectTransform>().localPosition.z);
		}
		else
		if (_snake [0].GetComponent<RectTransform> ().localPosition.x < -(x_s / 2)) 
		{
			_snake[0].GetComponent<RectTransform>().localPosition = new Vector3((x_s / 2),_snake[0].GetComponent<RectTransform>().localPosition.y,_snake[0].GetComponent<RectTransform>().localPosition.z);
		}
		
		for(int i = 1; i < _snake.Count; i++)
		{
			_snake [i].GetComponent<RectTransform> ().localPosition = initialLoc [i - 1];
		}
	}

	public void SpawnFood(GameObject prefabFood,GameObject parent)
	{
		int x_s = 1920;
		int y_s = 1080;

		int xRand = Random.Range (0,x_s/2);
		int yRand = Random.Range (0,y_s/2);

		GameObject go = Spawn(prefabFood);
		go.transform.SetParent (parent.transform, false);
		go.GetComponent<RectTransform> ().transform.localPosition = new Vector3 (xRand,yRand,go.GetComponent<RectTransform>().localPosition.z);
	}
}
