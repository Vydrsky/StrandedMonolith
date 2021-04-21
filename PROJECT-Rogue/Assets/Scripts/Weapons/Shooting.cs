using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public static Shooting instance;

    [SerializeField] public List<GameObject> bulletPrefabs = new List<GameObject>();

    private void Start()
    {
        instance = this;
    }


    //public void ProjectileRiffleShot(FightingCharacter whoAttacks)
    //{
    //    var obj = Instantiate(bulletPrefabs.Find(x => x.tag.Contains("|ProjRiffle|")), whoAttacks.firePoint.position, whoAttacks.firePoint.rotation);
    //    obj.GetComponent<Bullet>().SetAttacker(whoAttacks);
    //}


    //public void ProjectileShotgunShot(FightingCharacter whoAttacks)
    //{
    //    float spread = 1 / 2.0f;
    //    GameObject bullet = bulletPrefabs.Find(x => x.tag.Contains("|ProjShotgun|"));
    //    Vector3 rotationVector;
    //    for (int i = -5; i <= 5; i++)
    //    {
    //        rotationVector = whoAttacks.transform.rotation.eulerAngles;
    //        rotationVector.z += i / spread;
    //        var obj = Instantiate(bullet, whoAttacks.firePoint.position, Quaternion.Euler(rotationVector));
    //        obj.GetComponent<Bullet>().SetAttacker(whoAttacks);
    //    }
    //}
}
