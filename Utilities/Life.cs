using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum LifeStatus
{
    Alive,
    Dead,
    Invincible,
    Inactive
}

[System.Serializable]
public class Life
{
    [HideLabel]
    [HorizontalGroup("Split", 0.3f)] 
    public LifeStatus status;

    [HideLabel]
    [HorizontalGroup("Split", 0.35f)]
    [SuffixLabel("Life", Overlay = true)]
    public int lifePoints = 100;

    [HideLabel]
    [HorizontalGroup("Split", 0.35f)]
    [SuffixLabel("Max Life", Overlay = true)]
    public int maxLifePoints = 100;

    [System.Serializable]
    public class LifeStage
    {
        public int stageLifePoints = 50;
        [HideLabel]
        [FoldoutGroup("Stage Activate Event")]
        public FrameCoreEvent stageActivateEvent;
    }

    [FoldoutGroup("Life Events")]

    public List<LifeStage> lifeStages;

    [HideLabel]
    [FoldoutGroup("Life Events/Heal Event")]
    public FrameCoreEvent healEvent = new FrameCoreEvent { 
        eventName = "Heal"
    };

    [HideLabel]
    [FoldoutGroup("Life Events/Hurt Event")]
    public FrameCoreEvent hurtEvent = new FrameCoreEvent
    {
        eventName = "Hurt"
    };

    [HideLabel]
    [FoldoutGroup("Life Events/Death Event")]
    public FrameCoreEvent deathEvent = new FrameCoreEvent
    {
        eventName = "Death"
    };


    public void Damage(int amount)
    {
        if (status == LifeStatus.Dead || status == LifeStatus.Inactive || status == LifeStatus.Invincible)
        {
            return;
        };

        lifePoints -= amount;


        if (lifePoints <= 0)
        {
            Die();
            return;
        };

        hurtEvent.Activate();
    }

    public void Heal(int amount)
    {
        if (status == LifeStatus.Dead || status == LifeStatus.Inactive)
        {
            return;
        };

        lifePoints += amount;

        if (lifePoints > maxLifePoints)
        {
            lifePoints = maxLifePoints;
        };
    }


    public void Die()
    {
        status = LifeStatus.Dead;
        deathEvent.Activate();
    }


    public void ResetLife()
    {
        status = LifeStatus.Alive;
        lifePoints = maxLifePoints;
    }
}
