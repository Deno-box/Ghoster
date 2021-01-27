using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcentratedLineParticlesController : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particleSystem;
    [SerializeField]
    public float speed = 50;

    private ParticleSystem.Particle[] particles;

    private Vector3 tmp;

    void Start()
    {

    }

    void Update()
    {
        // 必要な場合のみ配列を作成しなおす
        int maxParticles = particleSystem.main.maxParticles;

        if (particles == null || particles.Length < maxParticles)
        {
            particles = new ParticleSystem.Particle[maxParticles];
        }

        // 現在のパーティクルを取得する
        int particleNum = particleSystem.GetParticles(particles);

        //パーティクルひとつひとつの処理
        for (int i = 0; i < particleNum; i++)
        {
            tmp = particles[i].position;
            // ひとつひとつのパーティクルを後ろに動かす
            tmp.z -= Time.deltaTime * speed;

            particles[i].position = tmp;
        }

        // 変更を適用する
        particleSystem.SetParticles(particles, particleNum);
    }
}
