using UnityEngine;

public class EnemyEgg : Egg
{
    protected override void OnEntityCollided(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            player.GetComponent<Health>().TakeHit();
    }
}