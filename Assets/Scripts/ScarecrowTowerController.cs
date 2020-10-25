using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowTowerController : MonoBehaviour
{
    GameObject closestEnemy = null;
    public float shotCooldown = 0.5f;
    public float range = 3f;
    float nextShotTime = 0f;
    public float shotSpread = 20f;
    public GameObject projectile;
    public AudioClip shootSound;
    AudioSource soundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        soundPlayer = GameObject.FindGameObjectWithTag("SoundPlayer").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float closestEnemyDistance = range;
        closestEnemy = null;
        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemyArray) {
            if (Vector3.Distance(enemy.transform.position, transform.position) < closestEnemyDistance) {
                closestEnemyDistance = Vector3.Distance(enemy.transform.position, transform.position);
                closestEnemy = enemy;
            }
        }
        if (closestEnemy != null) {
            LookAt2D(closestEnemy.transform.position);
            if (nextShotTime <= Time.time) {
                nextShotTime = Time.time + shotCooldown;
                soundPlayer.PlayOneShot(shootSound);
                Vector3 spawnPosition = transform.position;
                float spawnAngleOffset = Random.Range(-(shotSpread/2), shotSpread/2);
                float spawnAngle = transform.eulerAngles.z + spawnAngleOffset;
                GameObject newProjectile = (GameObject)Instantiate(projectile, spawnPosition, Quaternion.Euler(0, 0, spawnAngle));
            }
        }
    }

    // 2D Lookat
    void LookAt2D(Vector3 targetPoint) {
        targetPoint.x -= transform.position.x;
        targetPoint.y -= transform.position.y;
        float angle = Mathf.Atan2(targetPoint.y, targetPoint.x) * Mathf.Rad2Deg;
        angle -= 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
