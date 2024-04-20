using UnityEngine;

public class FlightlessZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Flapper flapper))
            flapper.SetFlapPossibility(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Flapper flapper))
            flapper.SetFlapPossibility(true);
    }
}