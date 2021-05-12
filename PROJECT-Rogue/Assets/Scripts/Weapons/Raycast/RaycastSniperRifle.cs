using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSniperRifle : Raycast
{
    protected override IEnumerator ShootRaycast()
    {
        LineRenderer lineRenderer = Shooting.instance.raycastPrefabs.Find(x => x.tag.Contains("|RaySniper|"));
        RaycastHit2D hitInfo = Physics2D.Raycast(whoAttacks.firePoint.position, whoAttacks.firePoint.right);
        var obj = Object.Instantiate(lineRenderer);
        if (hitInfo && (hitInfo.distance < whoAttacks.Range * rangeModifier))
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

    public RaycastSniperRifle(float attackSpeed = 1, int damage = 40) : base(attackSpeed, damage) { }

    public RaycastSniperRifle(WeaponStats weaponStats) : base(weaponStats)
    {

    }
}
