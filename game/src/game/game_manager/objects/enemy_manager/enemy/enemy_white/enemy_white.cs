/*!
 *  @file       enemy_white.cs
 *  @brief      エネミー白
 *  @author     Itiki Amano
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */

using System;
using Sce.PlayStation.Core;

namespace game
{
	public class CEnemyRainbow:IEnemy
	{
		public CEnemyRainbow ( Vector2 pos, float speed, int power, int life,  INSTAGE_ID instage_id, OUTSTAGE_ID outstage_id )
			:base( pos, speed, power, life, instage_id, outstage_id)
		{
			m_EnemyID = ENEMY_ID.ENEMY_WHITE;
		}
		
		/// <summary>
		/// 通常攻撃
		/// </summary>
		public override void AttackNormal()
		{
			// 攻撃用タイマー更新
			m_AttackTimer.Update();
				
			if( m_AttackTimer.IsEnd() == true )
			{
				CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , COLOR_ID.WHITE, false, new Vector2( -20.0f, -8.0f　), 0.0f, 6.0f, 1 );
				
				++m_attack_num;
				
				if( m_attack_num == 5 )
				{
					// 移動状態へ移行
					m_State = IEnemy.STATE.OUTSTAGE;
					
					m_attack_num = 0;
				}
			}
		}
		/*
		/// <summary>
        /// 弾の作成
        /// </summary>
        /// <param name='type'>
        /// Type.
        /// </param>
        /// <param name='pos'>
        /// Position.
        /// </param>
        /// <param name='angle'>
        /// Angle.
        /// </param>
        /// <param name='speed'>
        /// Speed.
        /// </param>
        /// <param name='power'>
        /// Power.
        /// </param>
        private void CreateBullet( ENEMY_BULLET_ID id, COLOR_ID color, bool reflec, Vector2 pos, float direction, float speed, int power )
        {
            CEnemyBulletManager.GetInstance( ).Create( id, OBJECT_ID.ENEMY_BULLET, color, reflec, m_Pos + m_Sprite.Center + pos, direction, speed, power );
        }
        */
	}
}