using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using System;
using UnityEngine.PlayerLoop;
using JetBrains.Annotations;
using static BirdMovement;

public class BirdMovement : MonoBehaviour
{
    public class Speed
    {
        public float max_speed;
        public float min_speed;
        public float max_value;
        public float min_value;
        [HideInInspector] public float speed;
        public float acceleration;

        protected GameObject gameobject;
        protected ValueMonitoring vm;
        protected CharacterController cc;

        public void Init(GameObject gameobject_, ValueMonitoring vm_, CharacterController cc_)
        {
            gameobject = gameobject_;
            vm = vm_;
            cc = cc_;
        }

        public virtual void Update(float delta)
        {

        }
        public virtual void Move(float delta)
        {

        }

        public void UpdateSpeedByAcc(float delta)
        {
            speed += acceleration * delta;
        }
        public virtual void UpdateSpeedByBalue(float value)
        {
            float ratio = (value - min_value) / (max_value - min_value);
            speed = Mathf.Lerp(min_speed, max_speed, ratio);
        }
        public virtual void CutOff()
        {
            speed = Mathf.Clamp(speed, min_speed, max_speed);
        }
    }
    [Serializable]
    public class Glide : Speed
    {
        public override void Update(float delta)
        {
            UpdateSpeedByAcc(delta);
            CutOff();
        }
        public override void Move(float delta)
        {
            gameobject.transform.InverseTransformVector(speed * delta * vm.forward);
            cc.Move(speed * delta * vm.forward);
            //gameobject.transform.Translate(speed * delta * vm.forward, Space.World);
        }
    }
    [Serializable]
    public class Turn : Speed
    {
        public override void Update(float delta)
        {
            UpdateSpeedByBalue(vm.m_turn);
            CutOff();
        }
        public override void Move(float delta)
        {
            gameobject.transform.Rotate(Vector3.up, speed * delta);
        }
        public override void UpdateSpeedByBalue(float value)
        {
            float ratio = (Mathf.Abs(value) - min_value) / (max_value - min_value);
            speed = Mathf.Lerp(min_speed, max_speed, ratio) * (value > 0 ? 1 : -1);
        }
        public override void CutOff()
        {
            float speed_ = Mathf.Clamp(Mathf.Abs(speed), min_speed, max_speed);
            speed = speed_ * (speed>0 ? 1:-1);
        }
    }
    [Serializable]
    public class Flap : Speed
    {
        private float flap = 0;
        public override void Update(float delta)
        {
            flap = flap * 0.8f + vm.m_flap_intensity * 0.2f;
            UpdateSpeedByBalue(flap);
            CutOff();
        }
        public override void Move(float delta)
        {
            cc.Move(speed * delta * Vector3.up);
            //gameobject.transform.Translate(speed * delta * Vector3.up);
        }
    }
    [Serializable]
    public class Descent : Speed
    {
        public override void Update(float delta)
        {
            UpdateSpeedByBalue(vm.m_pitch);
            CutOff();
        }
        public override void Move(float delta)
        {
            cc.Move(-1 * speed * delta * Vector3.up);
            //gameobject.transform.Translate(-1 * speed * delta * Vector3.up);
        }
    }
    [Serializable]
    public class Deaccelation : Speed
    {
        public override void Update(float delta)
        {
            UpdateSpeedByBalue(-vm.m_pitch);
            CutOff();
        }
    }

    public BirdFSM m_FSM;
    public ValueMonitoring m_VM;

    public Glide glide;
    public Turn turn;
    public Flap flap;
    public Descent descent;
    public Deaccelation deaccelation;

    public XROrigin xrorigin;

    private CharacterController cc;
    private void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
        glide.Init(gameObject, m_VM, cc);
        turn.Init(gameObject, m_VM, cc);
        flap.Init(gameObject, m_VM, cc);
        descent.Init(gameObject, m_VM, cc);
        deaccelation.Init(gameObject, m_VM, cc);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_FSM.isActivated)
        {
            switch (m_FSM.state)
            {
                case BirdFSM.State.Glide:
                    glide.Update(Time.deltaTime);
                    break;
                case BirdFSM.State.Flap:
                    flap.Update(Time.deltaTime);
                    flap.Move(Time.deltaTime);
                    break;
                case BirdFSM.State.Descent:
                    descent.Update(Time.deltaTime);
                    descent.Move(Time.deltaTime);
                    break;
                case BirdFSM.State.Decelation:
                    deaccelation.Update(Time.deltaTime);
                    glide.speed -= deaccelation.speed * Time.deltaTime;
                    glide.CutOff();
                    break;
                case BirdFSM.State.Turn:
                    turn.Update(Time.deltaTime);
                    turn.Move(Time.deltaTime);
                    break;
            }

            glide.Move(Time.deltaTime);
        }
    }
}
