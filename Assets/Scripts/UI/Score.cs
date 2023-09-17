using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
   public TMP_Text score;

   private void Update()
   {
      score.text = GameManager.Instance.score.ToString();
   }
}
