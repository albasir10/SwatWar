using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponOption : MonoBehaviour
{
    public GameObject prefabBullet;
    List<BulletOption> bulletInactive;
    List<BulletOption> bulletActive;
    public int maxBullet = 40;
    public int damageBullet = 10;
    bool isShooting;

    void Start()
    {
        bulletInactive = new();
        bulletActive = new();
        for (int i = 0; i < maxBullet; i++) 
        {

            GameObject bullet = Instantiate(prefabBullet, new Vector3(-999, -999, -999), new Quaternion(0, 0, 0, 0));
            BulletOption bulletOption = bullet.GetComponent<BulletOption>();
            bulletInactive.Add(bulletOption);
            bulletOption.weaponOption = this;


        }

    }

    public void Shoot(Unit unit, Transform startPoint)
    {
        if (isShooting)
            return;
        isShooting = true;
        BulletOption ball = bulletInactive[0];
        bulletActive.Add(ball);
        bulletInactive.Remove(ball);
        ball.transform.SetPositionAndRotation(startPoint.position, startPoint.rotation);
        ball.GetComponent<Rigidbody>().isKinematic = false;

        ball.GetComponent<Rigidbody>().AddForce(ball.transform.forward * ball.speedBullet, ForceMode.Impulse);
        StartCoroutine(AnimationShoot(unit));
        StartCoroutine(CoroutineToInactiveBall(ball));
    }

    IEnumerator AnimationShoot(Unit unit)
    {
        unit.animator.SetBool("Shoot", isShooting);
        while (unit.animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1)
        {
            yield return new WaitForSeconds(0.1f);
        }
        unit.animator.SetBool("Shoot", false);
        isShooting = false;
    }


    IEnumerator CoroutineToInactiveBall(BulletOption ball)
    {
        yield return new WaitForSeconds(ball.timeToInactiveBullet);
        if (bulletActive.IndexOf(ball) != -1)
        {
            InactiveBall(ball);
        }
    }

    public void InactiveBall(BulletOption ball)
    {
        bulletActive.Remove(ball);
        bulletInactive.Add(ball);
        ball.transform.SetPositionAndRotation(new Vector3(-999, -999, -999), new Quaternion(0, 0, 0, 0));
        ball.GetComponent<Rigidbody>().isKinematic = true;
    }


}
