using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private GameObject lifePointPrefab;
    private float                       shootSpeed;
    [SerializeField] private Transform  shootPoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ReleaseLife();
        }

    }

    void ReleaseLife()
    {
        GameObject newLifePoint = Instantiate(lifePointPrefab, 
            shootPoint.position, Quaternion.Euler(0.0f, 90.0f, 0.0f));

        shootSpeed = Random.Range(700.0f, 850.0f);

        Rigidbody2D rb = newLifePoint.GetComponent<Rigidbody2D>();
        rb.velocity = (shootSpeed * newLifePoint.transform.up) +
           (2.0f * shootSpeed * newLifePoint.transform.right);
    }
}
