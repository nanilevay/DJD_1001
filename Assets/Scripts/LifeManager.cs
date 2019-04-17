using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private GameObject lifePointPrefab;
    [SerializeField] private float      shootSpeed;
    [SerializeField] private Transform  shootPoint;
    private float finalSpeed;
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
        finalSpeed = Random.Range(shootSpeed - 100, shootSpeed + 1);

        GameObject newLifePoint = Instantiate(lifePointPrefab,
            shootPoint.position, shootPoint.rotation);

        Rigidbody2D rb = newLifePoint.GetComponent<Rigidbody2D>();
        rb.velocity = finalSpeed * newLifePoint.transform.up +
            finalSpeed / Random.Range(3,6) * Vector3.right * Random.Range(-1,2);
    }

}
