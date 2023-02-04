using UnityEngine;

public class Destructable : MonoBehaviour
{
    public ParticleSystem particleSystem;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            particleSystem.Play();
            Object.Destroy(this.gameObject);
        }
    }
}
