using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float _movementSpeed = 4f;
    [SerializeField]
    private GameObject _explosisionAnimationPrefab;
    [SerializeField]
    private AudioClip _clip;

    private UIManager _uiManager;
    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        // move down
        transform.Translate(Vector3.down * _movementSpeed * Time.deltaTime);

        // when off the screen on the bottom
        if (transform.position.y <= -7f)
        {
            float randomX = Random.Range(-7.9f, 7.9f);
            //respawn back on top with random x position within screen bounds.
            transform.position = new Vector3(randomX, 7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.TakeDamage();
            }

            Instantiate(_explosisionAnimationPrefab, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Laser")
        {
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
            Destroy(other.gameObject);
            Instantiate(_explosisionAnimationPrefab, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            _uiManager.UpdateScore();
            Destroy(this.gameObject);
        }
    }
}
