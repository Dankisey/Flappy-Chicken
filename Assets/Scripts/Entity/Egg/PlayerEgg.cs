using UnityEngine;

public class PlayerEgg : Egg
{
    protected override void OnEntityCollided(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            enemy.GetComponent<Health>().TakeHit();
    }
}
