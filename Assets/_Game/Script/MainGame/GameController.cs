using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
	private int diamond;
	public TextMeshProUGUI diamondText;

	void Start()
	{
		diamond = PlayerPrefs.GetInt("Diamond", 1000);
		UpdateDiamondText();
	}

	void Update()
	{
		// Update loop if needed
	}

	private void UpdateDiamondText()
	{
		diamondText.text = diamond.ToString();
	}

	public void AddDiamond(int amount)
	{
		diamond += amount;
		PlayerPrefs.SetInt("Diamond", diamond);
		UpdateDiamondText();
	}
}
