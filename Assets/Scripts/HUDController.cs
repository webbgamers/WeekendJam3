using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    Text moneyDisplay;
    PlacementController player;
    Button scarecrowButton;
    Button skeletonButton;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlacementController>();
        moneyDisplay = GameObject.FindGameObjectWithTag("Money").GetComponent<Text>();
        scarecrowButton = GameObject.FindGameObjectWithTag("ScarecrowButton").GetComponent<Button>();
        scarecrowButton.onClick.AddListener(player.ScarecrowSelect);
        skeletonButton = GameObject.FindGameObjectWithTag("SkeletonButton").GetComponent<Button>();
        skeletonButton.onClick.AddListener(player.SkeletonSelect);

    }

    // Update is called once per frame
    void Update()
    {
        moneyDisplay.text = player.money.ToString();
    }
}
