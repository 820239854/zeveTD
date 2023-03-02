using UnityEngine;

public class BasicTower : Tower
{
    public Transform pivot;
    public Transform barrel;
    public GameObject bulletPrefab;
    protected override void shoot()
    {
        base.shoot();
        GameObject newBullet = Instantiate(bulletPrefab,barrel.position,pivot.rotation);
    }
}