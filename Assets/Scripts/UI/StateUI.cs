using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StateUI : MonoBehaviour
{
   public TMP_Text stateText;

   private void Update()
   {
      stateText.text = $"Player State: {StateMachine.Instance.GetState().ToString()}";
   }
}
