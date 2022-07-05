using UnityEngine;

public class Bunker : MonoBehaviour
{
    private int hp = 3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Phantom")) 
        {
            this.gameObject.SetActive(false);
            hp =  -1;

            if (hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
