using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastRifle : Raycast
{
    
    protected override IEnumerator ShootRaycast()
    {
        LineRenderer lineRenderer = Shooting.instance.raycastPrefabs.Find(x => x.tag.Contains("|RayRiffle|"));
        RaycastHit2D hitInfo = Physics2D.Raycast(whoAttacks.firePoint.position, whoAttacks.firePoint.right);
        var obj = Object.Instantiate(lineRenderer);
        if (hitInfo && (hitInfo.distance < whoAttacks.Range * 1.5f))
        {
            FightingCharacter character = hitInfo.transform.GetComponent<FightingCharacter>();
            if (character != null)
            {
                character.TakeDamage(damage * whoAttacks.Damage);
            }           
            obj.SetPosition(0, whoAttacks.firePoint.position);
            obj.SetPosition(1, hitInfo.point);
        }
        else
        {
            obj.SetPosition(0, whoAttacks.firePoint.position);
            obj.SetPosition(1, whoAttacks.firePoint.position + whoAttacks.firePoint.right * (whoAttacks.Range * 1.3f));
        }
        obj.enabled = true;
        yield return new WaitForSeconds(0.03f);
        Object.Destroy(obj.gameObject);
    }
}
