using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent<int, bool, Color> OnGateActivate = new UnityEvent<int, bool, Color>();
    public static void SendGateActivate(int scoreAmount, bool isBad, Color vegeColor)
    {
       OnGateActivate.Invoke(scoreAmount, isBad, vegeColor);
    }
}
