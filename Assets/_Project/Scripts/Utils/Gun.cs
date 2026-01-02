using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 10f;
    [SerializeField] private float shootDistance = 5f;

   // [SerializeField] private Vector3 offset = new Vector3(0.5f, 0.2f, 0f); // Offset della Gun rispetto al Player DA CAPIRE SE METTERLO!!!!

    private float fireTimer;

    //  private void Start()
    //  {
    //      transform.localPosition = offset; // Posiziona l'arma con l'offset all'inizio   DA CAPIRE SE METTERLO!!!!
    //  }

    private void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= 1f / fireRate && EnemyInRange())
        {
            Shoot();
            fireTimer = 0f;
        }
    }

    private void Shoot()
    {
        Vector2 mousePos = Input.mousePosition; // posizione del mouse sullo schermo
        Vector2 playerPos = new Vector2(Screen.width / 2, Screen.height / 2); // centro dello schermo = player fisso
        Vector2 direction = (mousePos - playerPos).normalized;

        GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.SetDirection(direction);
        }
    }

    private bool EnemyInRange()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position,enemy.transform.position);

            if (distance <= shootDistance)
            {
                return true;
            }
        }
        
        return false;
    }
}
