using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastRifle : Raycast
{
    
    protected override IEnumerator ShootRaycast()
    {
        LayerMask newMask = (1 << LayerMask.NameToLayer("Hole"));
        newMask |= (1 << LayerMask.NameToLayer("Ignore Raycast"));
        newMask = ~newMask;

        LineRenderer lineRenderer = Shooting.instance.raycastPrefabs.Find(x => x.tag.Contains("|RayRiffle|"));
        RaycastHit2D hitInfo = Physics2D.Raycast(whoAttacks.firePoint.position, whoAttacks.firePoint.right, whoAttacks.Range * rangeModifier, newMask);
        var obj = Object.Instantiate(lineRenderer);
        if (hitInfo) //&& (hitInfo.distance < whoAttacks.Range * rangeModifier)
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
            obj.SetPosition(1, whoAttacks.firePoint.position + whoAttacks.firePoint.right * (whoAttacks.Range * rangeModifier));
        }
        obj.enabled = true;
        yield return new WaitForSeconds(0.03f);
        Object.Destroy(obj.gameObject);
    }

    public RaycastRifle(WeaponStats weaponStats) : base(weaponStats)
    {

    }
}
