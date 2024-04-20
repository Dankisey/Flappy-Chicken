using UnityEngine;

public class PlayerEggThrower : EggThrower
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            TryThrow();
    }
}
