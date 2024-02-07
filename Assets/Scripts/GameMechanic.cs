using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMechanic : MonoBehaviour {

	int boolToInt(bool variable)
	{
		if (variable) {
			return 1;
		} else {
			return 0;
		}
	}

	//Renderer rend;
	//public Material winnerMaterial;
	//public Transform[] tileArray;
	public float winnerX = 9f;
	public float timeToFinish = 4f;
	float timer = 0;
	int isWinner = 0;
	public GameObject LevelClearObj, UIObj;
	public GameObject Bgm;
	public GameObject Tiles;
	public GameObject TilesWin;
	public AudioClip Win;
	public AudioSource audioSource;
	public Scene scene;
	public string easy1, easy2, easy3, easy4;
	public float volume = 0.1f;
	[SerializeField] private Transform player;
	//public Material win;

	//To create block of winner only once
	bool isCreated = false;

	GameObject[,] mainTile = new GameObject[5,5];

	void Start () 
	{
		scene = SceneManager.GetActiveScene();

		/*{
			rend = GetComponent<Renderer>();
			rend.enabled = true;
		}*/

		int counter = 0;
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				mainTile[i, j] = GameObject.FindGameObjectWithTag("Tile").transform.GetChild(counter).gameObject;
				counter++;
			}
		}	
	}

	void Update () 
	{
		timer += Time.deltaTime;
	}

	/*public void changeWinningColor()
	{
		if (rend.sharedMaterial.name == "WinningTile")
		{
			rend.sharedMaterial = win;
		}
	}*/

	void FixedUpdate()
	{
		if (timer >= 0.5f)
		{
			//Checking all for win
			isWinner = 0;
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					if (mainTile [i, j].gameObject.GetComponent<CubeManager> ().isForWin() != mainTile [i, j].gameObject.GetComponent<CubeManager> ().Accept ()) {
						isWinner++;
					}
				}
			}

			//Is it win?
			if (isWinner == 0)
			{
				player.gameObject.GetComponent<player>().enabled = false;

				//CHANGE TO TILES WIN
				for (int i = 0; i < 5; i++) {
					for (int j = 0; j < 5; j++) {
						if (mainTile [i, j].gameObject.GetComponent<CubeManager> ().Accept () == 1)
						{
							//mainTile [i, j].gameObject.GetComponent<CubeManager> ().changeForColor (winnerMaterial);
						} else 
						{
							mainTile [i, j].gameObject.GetComponent<CubeManager> ().changeForColor (mainTile [i, j].gameObject.GetComponent<CubeManager> ().Blank);
						}
					}
				}
					
				//PlayerPrefs.SetInt (scene.name, 1);
				if (isCreated == false)
				{
					isCreated = true;
					UIObj.SetActive(false);
					Tiles.SetActive(false);
					TilesWin.SetActive(true);
					StartCoroutine(enableLevelClear());
				}

				Invoke ("backToMenu", timeToFinish);
			}

			timer = 0;
		}

		if (Input.GetKey (KeyCode.Escape))
		{
			SceneManager.LoadScene("MainMenu");
		}

	}

	IEnumerator enableLevelClear()
    {
		Bgm.SetActive(false);
		yield return new WaitForSeconds(1);
		audioSource.PlayOneShot(Win, volume);
		LevelClearObj.SetActive(true);
		
	}

	public void backToMenu()
	{
		if (scene.name == easy1)
		{
			SceneManager.LoadScene("easy2");
		}
		else if (scene.name == easy2)
		{
			SceneManager.LoadScene("easy3");
		}
		else if (scene.name == easy3)
		{
			SceneManager.LoadScene("easy4");
		}
		else //if (scene.name == easy4)
		{
			SceneManager.LoadScene("MainMenu");
		}
		//SceneManager.LoadScene ("GameMenu");
	}
	
}