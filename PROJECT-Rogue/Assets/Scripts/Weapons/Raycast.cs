using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : Ranged
{
    LineRenderer lineRenderer;
    protected override void Shoot() { whoAttacks.StartCoroutine(ShootRaycast()); }
    IEnumerator ShootRaycast()
    {
        lineRenderer = Shooting.instance.raycastPrefabs[0];
        RaycastHit2D hitInfo = Physics2D.Raycast(whoAttacks.firePoint.position, whoAttacks.firePoint.right);
        if (hitInfo)
        {
            FightingCharacter character = hitInfo.transform.GetComponent<FightingCharacter>();
            if (character != null)
            {
                character.TakeDamage(damage);
            }
            lineRenderer.SetPosition(0, whoAttacks.firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
            lineRenderer.transform.position = new Vector3(lineRenderer.transform.position.x, lineRenderer.transform.position.y, 0); // ?
        }
        else
        {
            lineRenderer.SetPosition(0, whoAttacks.firePoint.position);
            lineRenderer.SetPosition(1, whoAttacks.firePoint.position + whoAttacks.firePoint.right * 100);
            lineRenderer.transform.position = new Vector3(lineRenderer.transform.position.x, lineRenderer.transform.position.y, 0); // ?
        }
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.03f);
        lineRenderer.enabled = false;
    }
    public Raycast(float attackSpeed = 10, float damage = 20) : base(attackSpeed, damage) { }
}
