using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BulletPattern : MonoBehaviour
{
    GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.instance;
    }

    //single fire
    public void SingleShot(float angle, string type, string key, out Bullet shape) 
    {
        GameObject bullet = BulletManager.instance.GetBullet(type);
        bullet.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0f, 0f, angle));
        bullet.SetActive(true);
        //add whatever to shoot
        bullet.GetComponent<Bullet>().AddChar(key);
        //add additional speed base on number of points
        shape = bullet.GetComponent<Bullet>();
    }

    public void BasedAOE(Pattern_Variables settings) 
    {
        //angle step = angle / n -1...
        float step = settings.innerAngle / ((settings.innerNum > 1 && settings.innerAngle % 360f != 0) ?
            settings.innerNum - 1 : settings.innerNum);
        //could have use i*innernum + j but count is lazy way out...
        for (int i = 0; i < settings.totalNum; i++)
        {
            for (int j = 0; j < settings.innerNum; j++)
            {
                SingleShot(j * step + (i * settings.totalAngle) + settings.angle, settings.type, settings.key, out Bullet point);
                //change speed
                point.AddExSpeed(Mathf.Abs(Mathf.Sin(settings.pointNum * (point.transform.rotation.eulerAngles.z - settings.angle) * Mathf.PI / settings.innerAngle)));
            }
        }
        settings.angle += settings.rotation;
        settings.angle %= 360;
    }
    //Coroutine
    public IEnumerator ReverseRotation(Pattern_Variables settings, float timetakes)
    {
        float newRotation = settings.rotation * -1;
        float startRot = settings.rotation;
        float time = 0;
        float count = 0;
        while(count < 1) 
        {
            settings.rotation = Mathf.Lerp(startRot, newRotation, count);
            time += Time.deltaTime;
            count = time / timetakes;
            yield return new WaitForFixedUpdate();
        }
        settings.rotation = Mathf.Round(settings.rotation);
        settings.angle = Mathf.Round(settings.angle);
        yield return null;
    }
    //target player
    public void TargetPlayer(Pattern_Variables settings) 
    {
        // innerNum is the number of shots
        // type is bullet type
        // key is characteristic of bullet (i.e: wave, acceleration, shoot straight)
        //total angle is the restriction angle
        Vector3 local = manager.player.transform.position - transform.position;
        float angle = Mathf.Atan2(local.y, local.x) * (180f / Mathf.PI);
        if (settings.innerNum % 2 == 1)
        {
            SingleShot(angle, settings.type, settings.key, out Bullet point);
            point.AddExSpeed(Mathf.Abs(Mathf.Sin(settings.pointNum * (point.transform.rotation.eulerAngles.z - settings.angle) * Mathf.PI / settings.innerAngle)));

        }
        for(int i = 0; i < (int) (settings.innerNum * 0.5); i++) 
        {
            SingleShot(angle + ((i + 1) * (settings.totalAngle / settings.innerNum)), settings.type, settings.key, out _);
            SingleShot(angle - ((i + 1) * (settings.totalAngle / settings.innerNum)), settings.type, settings.key, out _);
        }
    }
}
