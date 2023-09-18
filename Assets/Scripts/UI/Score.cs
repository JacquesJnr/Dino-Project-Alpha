using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
   public TMP_Text scoreUI;
   public int score;
   public int collectibleModifier;
   public int normalModifier;
   public int killModifier;
   public float scoreIncreaseTime;
   private float timer;

   public static Score Instance;

   public void Start()
   {
      score = 0;
      timer = 0;
      Instance = this;
      
      PlayerHealth.OnEnemyKilled += OnEnemyKilled;
   }

    private void OnDestroy()
    {
        PlayerHealth.OnEnemyKilled -= OnEnemyKilled;
    }

    private void Update()
   {
      // Increase Score
      if (!GameManager.Instance.dead)
      {
         if (timer < scoreIncreaseTime)
         {
            timer += Time.deltaTime;
         }
         else
         {
            timer = 0;
            score += normalModifier;
            scoreUI.text = score.ToString();
         }
      }
   }

   public void OnCollectableGet()
   {
      score += collectibleModifier;
   }

   public void OnEnemyKilled()
   {
      score += killModifier;
   }
}
