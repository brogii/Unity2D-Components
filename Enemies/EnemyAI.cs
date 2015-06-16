﻿using UnityEngine;
using System.Collections;

public class EnemyAI : CacheBehaviour {

    public GameObject[] targets;

    public float attackInterval = 1f;
    public float chanceOfAttack = 40f;
    public float attackWhenInRange = 20f;

    private ProjectileManager projectile;
    private Weapon weapon;
    private Transform target;

	void Start()
    {
        projectile = GetComponent<ProjectileManager>();
        weapon = GetComponentInChildren<Weapon>();
        target = GameObject.Find("Player").transform;
        StartCoroutine(LobCompTest());
	}

    void OnBecameVisible()
    {
        InvokeRepeating("LookAtTarget", 1f, .3f);
        // InvokeRepeating("AttackRandomly", 2f, attackInterval);
        // InvokeRepeating("RotateTowardsTarget", 1f, .01f);
    }

    void OnBecameInvisible()
    {
        CancelInvoke("LookAtTarget");
        // CancelInvoke("AttackRandomly");
        // CancelInvoke("RotateTowardsTarget");
    }

    void LookAtTarget()
    {
        int direction = (target.position.x > transform.position.x) ? RIGHT : LEFT;
        transform.localScale = new Vector3((float)direction, transform.localScale.y, transform.localScale.z);
    }

    void AttackRandomly()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= attackWhenInRange)
        {
            if (Random.Range(1, 101) <= chanceOfAttack)
                projectile.FireAtTarget(weapon, target);
        }
    }

    void RotateTowardsTarget()
    {
        Vector3 vel = GetForceFrom(transform.position,target.position);
        float angle = Mathf.Atan2(vel.y, vel.x)* Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    Vector2 GetForceFrom(Vector3 fromPos, Vector3 toPos)
    {
        float power = 1;
        return (new Vector2(toPos.x, toPos.y) - new Vector2(fromPos.x, fromPos.y))*power;
    }

    IEnumerator LobCompTest()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            projectile.FireAtTarget(weapon, targets[10].transform);

            // yield return new WaitForSeconds(1);
            // projectile.FireAtTarget(weapon, targets[0].transform);
            // yield return new WaitForSeconds(1);
            // projectile.FireAtTarget(weapon, targets[1].transform);
            // yield return new WaitForSeconds(1);
            // projectile.FireAtTarget(weapon, targets[2].transform);
            // yield return new WaitForSeconds(1);
            // projectile.FireAtTarget(weapon, targets[3].transform);
            // yield return new WaitForSeconds(1);
            // projectile.FireAtTarget(weapon, targets[4].transform);
            // yield return new WaitForSeconds(1);
            // projectile.FireAtTarget(weapon, targets[5].transform);
            // yield return new WaitForSeconds(1);
            // projectile.FireAtTarget(weapon, targets[6].transform);
            // yield return new WaitForSeconds(1);
            // projectile.FireAtTarget(weapon, targets[7].transform);
            // yield return new WaitForSeconds(1);
            // projectile.FireAtTarget(weapon, targets[8].transform);
            // yield return new WaitForSeconds(1);
            // projectile.FireAtTarget(weapon, targets[9].transform);
            // yield return new WaitForSeconds(1);
            // projectile.FireAtTarget(weapon, targets[10].transform);
            // yield return new WaitForSeconds(1);
            // projectile.FireAtTarget(weapon, targets[11].transform);
        }
    }

}
