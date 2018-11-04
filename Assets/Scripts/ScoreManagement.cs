using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagement : MonoBehaviour {

    public Text scoreText;
    private GameObject tests, player;
    private GameObject[] healthbar;
    public int scoreAmount;
    private int maxHealth, currentHealth, currentImg;
    private string text = "Score: ";
    private Image[] healthImg = new Image[4];

    // Use this for initialization
    void Start () {
        currentHealth = 3; 
        maxHealth = 3;
        scoreAmount = 0;
        tests = GameObject.FindGameObjectWithTag("EditorOnly");
        player = GameObject.FindGameObjectWithTag("Player");
        healthbar = GameObject.FindGameObjectsWithTag("Healthbar");
        scoreText = tests.GetComponent<Text>();
        if(scoreText != null || tests != null)
        {
            scoreText.text = text + scoreAmount;
        }
        int add = 0;
        foreach (GameObject obj in healthbar)
        {
            healthImg[add++] = obj.GetComponent<Image>();
        }
    }

    public void increaseScore(int amount)
    {
        scoreAmount += amount;
        if(scoreText != null)
        {
            scoreText.text = text + scoreAmount;
        }
    }

    public void addHealth(int amount)
    {
        Debug.Log("Add Health");
        if((currentHealth + amount) < maxHealth)
        {
            currentHealth += amount;
        }
    }

    private void makeInvisible(int selectImg, bool invisible)
    {
        StartCoroutine(blinkHealth(healthImg[selectImg], .12f, 0.1f));
        healthImg[selectImg].enabled = invisible;
    }

    public void damageHealth(int amount)
    {
        if ((currentHealth - amount) == 0)
        {
            
            player.GetComponent<MovePlayer>().stopMovePlayer();
            currentHealth -= amount;
            makeInvisible(currentHealth, false);
            Application.Quit();
        }
        else{
            currentHealth -= amount;
            makeInvisible(currentHealth, false);
        }
        
    }

    IEnumerator blinkHealth(Image img, float duration, float blinkTime)
    {
        while(duration > 0f )
        {
            duration -= Time.deltaTime;
            img.enabled = !img.enabled;
            yield return new WaitForSeconds(blinkTime);
        }
        img.enabled = false;
    }
}
