using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : Ranged
{
    LineRenderer lineRenderer;
    public override void Shoot()
    {
        whoAttacks.StartCoroutine(ShootRaycast());
    }

    IEnumerator ShootRaycast()
    {
        //lineRenderer = ((Player)whoAttacks).lineRenderer;
        lineRenderer = Shooting.instance.raycastPrefabs[0];
        RaycastHit2D hitInfo = Physics2D.Raycast(whoAttacks.firePoint.position, whoAttacks.firePoint.right);
        if (hitInfo)
        {
            FightingCharacter character = hitInfo.transform.GetComponent<FightingCharacter>();
            if (character != null)
            {
                if (character is Player)
                    ((Player)character).TakeDamage(damage);
                else
                    character.TakeDamage(damage);
            }
            Debug.Log("RayCast hit");
            lineRenderer.SetPosition(0, whoAttacks.firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
            lineRenderer.transform.position = new Vector3(lineRenderer.transform.position.x, lineRenderer.transform.position.y, 0);
        }
        else
        {
            Debug.Log("RayCast no hit");
            lineRenderer.SetPosition(0, whoAttacks.firePoint.position);
            lineRenderer.SetPosition(1, whoAttacks.firePoint.position + whoAttacks.firePoint.right * 100);
            lineRenderer.transform.position = new Vector3(lineRenderer.transform.position.x, lineRenderer.transform.position.y, 0);
        }

        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);

        lineRenderer.enabled = false;
    }

    public Raycast(float attackSpeed = 3, float damage = 10) : base(attackSpeed, damage)
    {

    }
}
