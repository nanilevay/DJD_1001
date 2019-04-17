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
            PickRandomAngle();
            ReleaseLife();
        }

    }

    void ReleaseLife()
    {
        GameObject newLifePoint = Instantiate(lifePointPrefab, shootPoint.position, shootPoint.rotation);

        shootSpeed = Random.Range(700.0f, 850.0f);

        Rigidbody2D rb = newLifePoint.GetComponent<Rigidbody2D>();
        rb.velocity = shootSpeed * newLifePoint.transform.up;
    }

    void PickRandomAngle()
    {
        Vector3 generatedAngle = transform.rotation.eulerAngles;

        generatedAngle.z += Random.Range(-25, 25);
        if (generatedAngle.z > 180.0f) generatedAngle.z -= 360;

        generatedAngle.z = Mathf.Clamp(generatedAngle.z, -10, 10);
        transform.localRotation = Quaternion.Euler(generatedAngle);
    }
}
