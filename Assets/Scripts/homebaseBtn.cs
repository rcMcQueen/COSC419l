using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class homebaseBtn : MonoBehaviour {

	public int btnID;
	public GameObject mainCanvas;
	public GameObject craftingCanvas;
	public GameObject storageCanvas;
	public GameObject workerCanvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
			onClick ();
	}

	public void onClick()
	{
		if(btnID == 0)//back to main
		{
			mainCanvas.SetActive (true);
			craftingCanvas.SetActive (false);
			storageCanvas.SetActive (false);
			workerCanvas.SetActive (false);
		}

		else if (btnID == 1)//worker btn
		{
			mainCanvas.SetActive (false);
			craftingCanvas.SetActive (false);
			storageCanvas.SetActive (false);
			workerCanvas.SetActive (true);
		}

		else if (btnID == 2)//storage
		{
			mainCanvas.SetActive (false);
			craftingCanvas.SetActive (false);
			storageCanvas.SetActive (true);
			workerCanvas.SetActive (false);
		}

		else if (btnID == 3)//craft
		{
			mainCanvas.SetActive (false);
			craftingCanvas.SetActive (true);
			storageCanvas.SetActive (false);
			workerCanvas.SetActive (false);
		}

		else if(btnID == -1)//world map
		{
			SceneManager.LoadScene ("overworldMap");
		}
	}
}
