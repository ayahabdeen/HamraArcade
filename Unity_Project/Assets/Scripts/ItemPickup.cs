using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType
    {
        //Hamra
        ClockBomb,
        CrowdSpeedDecrease,
        PoliceManFreeze,
        //Aragoz
        KissBomb,
        BaloonSpeedDecrease,
        TickleFreeze,
        //General
        SpeedIncrease, 
        GeneralBombs,
    }

    public ItemType type;

    private void HamraPickup(GameObject player)
    {
        switch (type)
        {
            case ItemType.ClockBomb:
                player.GetComponent<HamraController>().AddClock(); // reduce health of aragoz
                Destroy(gameObject);
                break;
            case ItemType.CrowdSpeedDecrease:
                player.GetComponent<HamraController>().SpeedDecrease(); // decreese speed of aragoz
                Destroy(gameObject);
                break;
            case ItemType.PoliceManFreeze:
                player.GetComponent<HamraController>().AddBomb(); // freezes aragoz 
                Destroy(gameObject);
                break;

            case ItemType.KissBomb:
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>()); // ignore collision
                break;
            case ItemType.BaloonSpeedDecrease:
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>()); // ignore collision
                break;
            case ItemType.TickleFreeze:
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>()); // ignore collision
                break;

            case ItemType.SpeedIncrease:
                player.GetComponent<HamraController>().SpeedIncrease(); // increase hamra speed 
                Destroy(gameObject);
                break;
            case ItemType.GeneralBombs:
                player.GetComponent<HamraController>().AddBomb(); // refill distructive bombs 
                Destroy(gameObject);
                break;

        }

        
    }

    private void AragozPickup(GameObject player)
    {
        Debug.Log("aragoz");
        switch (type)
        {
            case ItemType.ClockBomb:
                
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>()); // ignore collision
                break;
            case ItemType.CrowdSpeedDecrease:
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>()); // ignore collision
                break;
            case ItemType.PoliceManFreeze:
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>()); // ignore collision 
                break;

            case ItemType.KissBomb:
                Debug.Log("here");
                player.GetComponent<AragozController>().AddKiss(); // reduces health of hamra 
                Destroy(gameObject);
                break;
            case ItemType.BaloonSpeedDecrease:
                player.GetComponent<AragozController>().SpeedDecrease(); // decreases speed of hamra 
                Destroy(gameObject);
                break;
            case ItemType.TickleFreeze:
                player.GetComponent<AragozController>().AddBomb(); // freezes hamra 
                Destroy(gameObject);
                break;

            case ItemType.SpeedIncrease:
                player.GetComponent<AragozController>().SpeedIncrease(); // increase aragoz speed 
                Destroy(gameObject);
                break;
            case ItemType.GeneralBombs:
                player.GetComponent<AragozController>().AddBomb(); // refill distructive bombs 
                Destroy(gameObject);
                break;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("aragoz")) {

            AragozPickup(other.gameObject);
        }
        else if (other.CompareTag("Hamra"))
        {
            HamraPickup(other.gameObject);
        }
    }

}
