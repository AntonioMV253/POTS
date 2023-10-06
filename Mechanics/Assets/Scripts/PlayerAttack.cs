//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.InputSystem;

//public class PlayerAttack : MonoBehaviour
//{
//    [SerializeField] GameObject arrowPrefab;
//    [SerializeField] SpriteRenderer arrowGFX;
//    [SerializeField] Slider bowPowerSlider;
//    [SerializeField] Transform bow;
//    [SerializeField] Transform arrowSpawnPoint;

//    [Range(0, 10)]
//    [SerializeField] float bowPower;

//    [Range(0, 3)]
//    [SerializeField] float maxBowCharge;

//    [Range(0, 3)]
//    [SerializeField] float minBowChargeToFire = 0.5f; // Valor mínimo requerido para disparar

//    float bowCharge;
//    bool canFire = true;

//    private InputManager inputManager;

//    private void Start()
//    {
//        bowPowerSlider.value = 0f;
//        bowPowerSlider.maxValue = maxBowCharge;

//        inputManager = FindObjectOfType<InputManager>();
//    }

//    private void Update()
//    {
//        if (inputManager.IsActionButtonHold && canFire)
//        {
//            ChargeBow();
//        }
//        else if (inputManager.IsSelectionButtonHold && canFire && bowCharge >= minBowChargeToFire)
//        {
//            FireBow();
//        }
//        else
//        {
//            if (bowCharge > 0f)
//            {
//                bowCharge -= 1f * Time.deltaTime;
//            }
//            else
//            {
//                bowCharge = 0f;
//                canFire = true;
//            }

//            bowPowerSlider.value = bowCharge;
//        }
//    }

//    void ChargeBow()
//    {
//        arrowGFX.enabled = true;

//        bowCharge += Time.deltaTime;

//        bowPowerSlider.value = bowCharge;

//        if (bowCharge > maxBowCharge)
//        {
//            bowPowerSlider.value = maxBowCharge;
//        }
//    }

//    void FireBow()
//    {
//        if (bowCharge > maxBowCharge) bowCharge = maxBowCharge;

//        float arrowSpeed = bowCharge + bowPower;
//        float arrowDamage = bowCharge * bowPower;

//        Quaternion playerRotation = bow.rotation;

//        Quaternion rot = playerRotation * Quaternion.Euler(new Vector3(0f, 0f, -90f));

//        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, rot);
//        Arrow arrowScript = arrow.GetComponent<Arrow>();
//        arrowScript.ArrowVelocity = arrowSpeed;
//        arrowScript.ArrowDamage = arrowDamage;

//        canFire = false;
//        arrowGFX.enabled = false;
//    }
//}
