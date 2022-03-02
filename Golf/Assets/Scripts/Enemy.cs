﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyClass {
    bool isDead = false;
    CapsuleCollider collider;
    Animator animator;
    ParticleSystem particles;

    protected virtual void Start()
    {
        aliveCount++;
        health = 1;
        speed = 3f;

        playerPos = GameObject.Find("Player").transform.position;
        collider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
        particles = GetComponent<ParticleSystem>();
    }

    protected virtual void Update()
    {
        if(health <= 0) {
            totalKilledCount++;
            GameEvents.current.ProgressChange(totalKilledCount);
            if (increase) {
                killedCount++;
                if (killedCount <= 5) {
                    GameEvents.current.KillsChange(killedCount);
                }
            }
            health = 10000;
            isDead = true;
            gameObject.tag = "Dead";
            GameEvents.current.Reward();
            collider.enabled = false;
            animator.enabled = false;
            particles.Stop(true,UnityEngine.ParticleSystemStopBehavior.StopEmittingAndClear);
            Destroy(gameObject,5);
        }

        if(!isDead) {
            transform.LookAt(playerPos);
            transform.Translate(0,0,speed * Time.deltaTime);
        }
    }

    protected override void OnCollisionEnter(Collision collision) {
        base.OnCollisionEnter(collision);
    }
}
