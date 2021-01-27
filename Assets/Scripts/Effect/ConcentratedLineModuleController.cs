using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcentratedLineModuleController : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particleSystem = null;

    ParticleSystem.MainModule mainModule;
    ParticleSystem.EmissionModule emissionModule;
    ParticleSystem.ShapeModule shapeModule;

    // エフェクトの数調整用
    [SerializeField, Range(1, 100)]
    float rate = 1.0f;
    // エフェクトの出現から消えるまでのスピード調整用
    [SerializeField, Range(0, 2)]
    float speed = 1.0f;
    // エフェクトの奥行きのランダム調整用
    [SerializeField, Range(0, 1)]
    float rand = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // メインモジュール
        mainModule = particleSystem.main;
        SetSimulationSpeed(speed);
        // エミッションモジュール
        emissionModule = particleSystem.emission;
        SetRateOverTime(rate);
        // シェイプモジュール
        shapeModule = particleSystem.shape;
        SetRandomDirection(rand);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // エフェクトの数
    public void SetRateOverTime(float rateNum)
    {
        emissionModule.rateOverTime = rateNum;
    }

    // シミュレーションスピード
    public void SetSimulationSpeed(float speedNum)
    {
        mainModule.simulationSpeed = speedNum;
    }

    // ランダムの大きさ
    public void SetRandomDirection(float randNum)
    {
        shapeModule.randomDirectionAmount = randNum;
    }
}
