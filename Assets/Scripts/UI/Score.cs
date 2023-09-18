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

   public static Score Instance;
   public void Start()
   {
      Instance = this;
      Collectable.OnCollectableGet += OnCollectableGet;
   }
   private void Update()
   {
      // Increase Score

      if (!GameManager.Instance.dead)
      {
         score += 1;
         scoreUI.text = score.ToString();  
      }
   }

   public void OnCollectableGet()
   {
      score += collectibleModifier;
   }
}
