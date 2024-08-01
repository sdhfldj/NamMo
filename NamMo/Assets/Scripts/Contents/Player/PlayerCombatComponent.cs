using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatComponent : MonoBehaviour
{
    private PlayerController _pc;
    public void SetPlayerController(PlayerController pc)
    {
        _pc = pc;
    }
    public void GetDamaged(/*TODO*/float damage, Vector3 attackPos)
    {
        if (_pc.GetASC().IsExsistTag(Define.GameplayTag.Player_State_Invincible))
        {
            // 무적상태일때 공격이 들어왔을 경우
            return;
        }
        if (_pc.GetASC().IsExsistTag(Define.GameplayTag.Player_Action_Block))
        {
            // 방어하고 있는 경우
            damage /= 2;
            StartCoroutine(CoInvincibleShortTime());
        }
        else
        {
            float force = 1;
            if (transform.position.x < attackPos.x) force = -1;
            (_pc.GetASC().GetAbility(Define.GameplayAbility.GA_Hurt) as GA_Hurt).SetKnockBackDirection(force);
            _pc.GetASC().TryActivateAbilityByTag(Define.GameplayAbility.GA_Hurt);
        }
        StartCoroutine(CoShowAttackedEffect());
        _pc.GetPlayerStat().ApplyDamage(damage);
    }
    IEnumerator CoInvincibleShortTime()
    {
        _pc.GetASC().AddTag(Define.GameplayTag.Player_State_Hurt);
        _pc.GetASC().AddTag(Define.GameplayTag.Player_State_Invincible);
        yield return new WaitForSeconds(0.5f);
        _pc.GetASC().RemoveTag(Define.GameplayTag.Player_State_Hurt);
        _pc.GetASC().RemoveTag(Define.GameplayTag.Player_State_Invincible);
    }
    IEnumerator CoShowAttackedEffect()
    {
        _pc.GetPlayerSprite().GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        _pc.GetPlayerSprite().GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
    }
}
