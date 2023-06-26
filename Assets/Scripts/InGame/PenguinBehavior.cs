using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Trail
{
    public string timeStamp;
    public float x;
    public float y;

    public Trail(string _timeStamp, float _x, float _y)
    {
        timeStamp = _timeStamp;
        x = _x;
        y = _y;
    }
}

namespace penguin
{
    public class PenguinBehavior : MonoBehaviour
    {
        private int trailFrame = 0;
        public static List<Trail> penguinTrail = new List<Trail>();

        // 現在のステータスを管理するクラス
        [SerializeField] private InGameStatusManager statusManager;

        // ペンギンが動く時のアニメーター
        [SerializeField] private Animator penguinMoveAnimator;
        
        // ペンギンにアタッチされたRigidBody2D。コースアウト判定は2次元で十分なため、RigidBody2Dを使用。
        [SerializeField] private Rigidbody2D penguinRigidBody;

        // SE再生・停止クラス
        [SerializeField] private InGameAudio audio;

        // OSC入力を受け取る際に用いる。isReceiveOSCInputがtrueのとき、OSC入力でキャラクターを動かす。
        [SerializeField] private OSCInputManager osc;
        [SerializeField] public static bool isReceiveOSCInput;

        // ペンギンの現在の移動速度。
        private Vector3 speed;
        // ペンギンの速度上限。
        private float penguinMaximumSpeed;
        // ペンギンの加速度。
        private float penguinAcceleration;
        // 氷の摩擦係数。
        private float friction;

        // ペンギンの基本感度。大きいほど入力に対する移動量が大きい。
        private float sensitivity;

        // trueの場合、ペンギンの動きをAddForceメソッドで制御する。falseの場合は、スクリプトから制御する。
        [SerializeField] private bool usePhysics = false;

        // 加速フラグ。速度を変更するために使用する。
        private bool speedUp;
        
        // Start is called before the first frame update
        void Start()
        {
            trailFrame = 0;
            penguinTrail = new List<Trail>();

            penguinMaximumSpeed = ParameterManager.maximumSpeed;
            penguinAcceleration = ParameterManager.acceleration;
            friction = ParameterManager.friction;
            sensitivity = ParameterManager.sensitivity;
        }

        void Update()
        {
            bool isInGame = (statusManager.CurrentStatus == InGameStatus.InGameNormal ||
                             statusManager.CurrentStatus == InGameStatus.HurryUp);
            if (isInGame)
            {
                DateTime currentTime = DateTime.Now;
                string year = currentTime.Year.ToString();
                string month = currentTime.Month.ToString();
                string day = currentTime.Day.ToString();
                string hour = currentTime.Hour.ToString();
                string minute = currentTime.Minute.ToString();
                string second = currentTime.Second.ToString();

                penguinTrail.Add(new Trail(year + "年" + month + "月" + day + "日" + hour + "時" + minute + "分" + second + "秒" + trailFrame + "f", transform.position.x, transform.position.y));
                trailFrame++;

                float horizon;
                float vertical;

                if (isReceiveOSCInput)
                {
                    horizon = osc.speed.x;
                    vertical = osc.speed.y;
                }
                else
                {
                    horizon = Input.GetAxis("Horizontal");
                    vertical = Input.GetAxis("Vertical");
                }

                if (usePhysics)
                {
                    // 移動入力があった際の処理
                    if (vertical != 0.0f || horizon != 0.0f)
                    {
                        PhysicsMove(vertical, horizon);
                        Rotate(vertical, horizon);
                    }
                    else { penguinMoveAnimator.SetBool("IsMoving", false); }
                }
                else
                {
                    Move(vertical, horizon);

                    // 移動入力があった際の処理
                    if (vertical != 0.0f || horizon != 0.0f) { Rotate(vertical, horizon); }
                    if (vertical == 0.0f && horizon == 0.0f) { penguinMoveAnimator.SetBool("IsMoving", false); }
                }

                // 加速入力があった際の処理
                if (!isReceiveOSCInput && (Input.GetButtonDown("Submit") || Input.GetKeyDown(KeyCode.Space))) { SpeedUp(); }
                else if (isReceiveOSCInput && osc.acceleration == 1) { SpeedUp(); }
            }
        }

        private void PhysicsMove(float vertical, float horizon)
        {
            // play animation
            penguinMoveAnimator.SetBool("IsMoving", true);

            // ペンギンに加える力
            Vector3 force;
            
            // プレイヤーオブジェクトに力を加える
            if (!speedUp) { force = new Vector3(horizon, vertical, 0) * sensitivity; }
            else { force = new Vector3(horizon, vertical, 0) * sensitivity * 3; }
            penguinRigidBody.AddForce(force);
        }

        private void Move(float vertical, float horizon)
        {
            // play animation
            penguinMoveAnimator.SetBool("IsMoving", true);

            // プレイヤーオブジェクトを加速度運動させる
            if (speedUp) { penguinAcceleration = ParameterManager.acceleration * 3.0f; }
            else { penguinAcceleration = ParameterManager.acceleration; }

            // 速度を、加速度をもとに計算
            if (vertical < 0.0f)
            {
                // 左に動く
                if (speed.y - penguinAcceleration > -penguinMaximumSpeed * Mathf.Abs(vertical)) { speed.y -= penguinAcceleration; }
                else { speed.y += penguinAcceleration; }
            }
            else if (vertical > 0.0f)
            {
                // 右に動く
                if (speed.y + penguinAcceleration < penguinMaximumSpeed * Mathf.Abs(vertical)) { speed.y += penguinAcceleration; }
                else { speed.y -= penguinAcceleration; }
            }
            else { speed.y *= friction; }

            if (horizon > 0.0f)
            {
                // 前に進む
                if (speed.x + penguinAcceleration < penguinMaximumSpeed * Mathf.Abs(horizon)) { speed.x += penguinAcceleration; }
                else { speed.x -= penguinAcceleration; }
            }
            else if (horizon < 0.0f)
            {
                // 後ろに進む
                if (speed.x - penguinAcceleration > -penguinMaximumSpeed * Mathf.Abs(horizon)) { speed.x -= penguinAcceleration; }
                else { speed.x += penguinAcceleration; }
            }
            else { speed.x *= friction; }

            transform.Translate(speed * Time.deltaTime, Space.World);
        }

        private void Rotate(float vertical, float horizon)
        {
            // プレイヤーオブジェクトが向く方向を更新する
            float angle = Mathf.Atan(vertical / horizon) * Mathf.Rad2Deg;
            
            if (horizon >= 0) { transform.rotation = Quaternion.Euler(0, 0, angle - 90.0f); }
            else { transform.rotation = Quaternion.Euler(0, 0, angle + 90.0f); }
        }

        private void SpeedUp()
        {
            speedUp = true;
            StartCoroutine("PlayAccelerationAnimation");
            audio.acceleration.Play();
        }

        private IEnumerator PlayAccelerationAnimation() 
        {
            penguinMoveAnimator.SetBool("IsAcceleration", true);
            yield return new WaitForSeconds (0.7f);
            speedUp = false;
            penguinMoveAnimator.SetBool("IsAcceleration", false);
        }

        public IEnumerator Stop(float stopTime)
        {
            yield return new WaitForSeconds (stopTime);
            penguinRigidBody.velocity = Vector3.zero;
            penguinRigidBody.angularVelocity = 0;
            enabled = false;
        }
    }
}