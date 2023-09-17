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
   }
   private void Update()
   {
      // Increase Score
      score += 1;
      scoreUI.text = score.ToString();
   }
   private void OnCollisionEnter(Collision other)
   {
      // OnCollectableGet
   }
}
