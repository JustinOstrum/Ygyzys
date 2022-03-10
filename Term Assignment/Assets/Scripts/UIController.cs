using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text CollectibleText;

    public GameObject[] Lives;

    int currentLivesIndex = 2;
    
    void Update()
    {
        if(GameStateManager.Instance != null)
        {
            CollectibleText.text = GameStateManager.Instance.Collectibles.ToString("D3");
        }

        int curLives = GameStateManager.Instance.Lives;

        if (curLives - 1 != currentLivesIndex && curLives < 3)
        {
            Lives[currentLivesIndex].SetActive(false);

            if(currentLivesIndex > 0)
               currentLivesIndex--;
        }
    }
}
