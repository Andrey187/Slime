using TMPro;
using UnityEngine;

//TODO Refactor Needed
public class Gate : MonoBehaviour
{
   [SerializeField] private int scoreAmount = 1;
   private TextMeshPro _prefabText;
   [SerializeField] private bool isBad;
   private Color vegeColor;
   
   private void Start()
   {
      vegeColor = this.gameObject.GetComponent<MeshRenderer>().material.color;
      if (transform.GetChild(0) != null)
      {
         _prefabText = transform.GetChild(0).GetComponent<TextMeshPro>();
         if (!isBad)
         {
            _prefabText.text = "+" + scoreAmount.ToString();
         }
         else
         {
            _prefabText.text = "-" + scoreAmount.ToString();
         }

      }
   }
   
   private void OnTriggerEnter(Collider other)
   {
      EventManager.SendGateActivate(scoreAmount, isBad, vegeColor);
      Destroy(this.gameObject);
   }

}
