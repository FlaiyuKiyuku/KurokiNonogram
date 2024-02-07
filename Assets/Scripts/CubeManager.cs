using UnityEngine;
using System.Collections;

public class CubeManager : MonoBehaviour {

	float timer = 0;
	public bool forWin = false;
	bool isChoosed = false;
	public int isAccepted = 0; 
	public bool canUseShift = true;
	public PlayerHealth ph;
	public GameObject Emote;
	float after = 1.0f;
	public AudioSource wrongsound, Paint, Mark;

	/*
	 * 0 - Blank
	 * 1 - Accepted
	 * 2 - NonAccepted
	 * 3 - Wrong
	*/

	Renderer rend;
	RaycastHit hit;
	public Material Accepted;
	public Material NotAccepted;
	public Material Blank;
	public Material Wrong;

	void Start () 
	{
		rend = GetComponent<Renderer> ();
		rend.enabled = true;
	}

	void Update()
	{
		timer += Time.deltaTime;
		if (timer >= 0.5f)
		{
			if (Input.GetKey(KeyCode.Z) && isChoosed)
			{
				changeForAcceptedColor();
				timer = 0;
			}
			else if (Input.GetKey(KeyCode.X) && isChoosed && canUseShift)
			{
				changeForNotAcceptedColor();
				timer = 0;
			}
		}

	}
	public void changeForAcceptedColor()
	{
		if (forWin == true) //(rend.sharedMaterial.name == "Tile" || rend.sharedMaterial.name == "TileNotAccepted")
		{
			Paint.Play();
			rend.sharedMaterial = Accepted;
			isAccepted = 1;
		}

		else if (forWin == false)
		{
			if (rend.sharedMaterial.name == "Tile" || rend.sharedMaterial.name == "TileAccepted" || rend.sharedMaterial.name == "TileNotAccepted")
			{
				if (ph.life > 1)
				{ 
					wrongsound.Play();
				}
				ph.LoseHealth();
				rend.sharedMaterial = Wrong;
				isAccepted = 3;
				Emote.SetActive(true);
				Invoke("Disable", after);
				
			}
		}
	} 
		/*else if (rend.sharedMaterial.name == "TileAccepted")
		{
			rend.sharedMaterial = Blank;
			isAccepted = 0;
		}*/
	
	
	public void changeForNotAcceptedColor()
	{
		Mark.Play();
		if (rend.sharedMaterial.name == "Tile") {
			rend.sharedMaterial = NotAccepted;
			isAccepted = 2;
		} else if (rend.sharedMaterial.name == "TileNotAccepted") {
			rend.sharedMaterial = Blank;
			isAccepted = 0;
		}
	}

	public void changeForColor(Material material)
	{
		rend.sharedMaterial = material;
	}

	void OnCollisionEnter(Collision obj)
	{
		isChoosed = true;
	}

	void OnCollisionExit(Collision obj)
	{
		isChoosed = false;
	}

    void Disable()
    {
		Emote.SetActive(false);
    }

    public int Accept()
	{
		if (isAccepted == 0 || isAccepted == 2 || isAccepted == 3)
		{
			return 0;
		} else {
			return 1;
		}
	}

	public int isForWin()
	{
		if (forWin) {
			return 1;
		} else {
			return 0;
		}
	}
}
