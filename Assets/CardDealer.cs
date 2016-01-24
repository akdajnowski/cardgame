using UnityEngine;

public class CardDealer : MonoBehaviour
{
    public GameObject hand;
    private HandBehaviour handBehaviour;

    // Use this for initialization
    void Start()
    {
        if (hand == null)
        {
            Debug.LogError("Hand is not set");
        }

        handBehaviour = hand.GetComponent<HandBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DealCard()
    {
        handBehaviour.DealCard();
    }
}
