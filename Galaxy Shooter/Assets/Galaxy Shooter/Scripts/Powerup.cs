using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _fallSpeed = 3.0f;
    [SerializeField]
    private PowerType _type;
    [SerializeField]
    private AudioClip _clip;
    private void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _fallSpeed);

        // when off the screen on the bottom
        if (transform.position.y <= -7f)
        {
            Destroy(transform.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                ActivatePowerup(player);
                AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
                Destroy(this.gameObject);
            }
        }
    }

    private void ActivatePowerup(Player player)
    {
        switch (_type)
        {
            case PowerType.TripleShot:
                player.ActivateTripleShotPowerup(5f);
                break;
            case PowerType.Speed:
                player.ActivateSpeedPowerup(10f);
                break;
            case PowerType.Shield:
                player.ActivateShieldPowerup(10f);
                break;
        }
    }

    private enum PowerType
    {
        Speed,
        Shield,
        TripleShot
    }
}
