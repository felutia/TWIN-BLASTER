/*!
 *  @file       enemy_blue_lv2.cs
 *  @brief      エネミー青Lv2
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
	public class CEnemyBlue_Lv2:IEnemy
	{
		public CEnemyBlue_Lv2 ( Vector2 pos, float speed, int power, int life, INSTAGE_ID instage_id, OUTSTAGE_ID outstage_id)
			:base( pos, speed, power, life, instage_id, outstage_id)
		{
			m_EnemyID = ENEMY_ID.ENEMY_BLUE;
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize ()
		{
			// スプライト設定
			m_Sprite = CSpriteManager.Create(TEXTURE_ID.BOSS2);
			
			// サイズ設定
			m_Size = new aqua.CSize( 64.0f, 64.0f );
			
			// 逆様
			m_Sprite.Angle = FMath.Radians( 0.0f );
			
			// 中心値設定
			m_Sprite.Center = new Vector2( 32.0f, 32.0f );
			
			// 位置設定
			m_Sprite.Position = m_Pos;
			
			// 攻撃間隔設定
			m_attack_time = 30;
			
			// 攻撃タイマー設定(仮)
			m_AttackTimer = new aqua.CFrameTimer( m_attack_time );
			
			// 攻撃回数
			m_attack_num = 0;
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
				if( StageLv > 0)
				{
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , COLOR_ID.BLUE, false, new Vector2( -20.0f, -15.0f　),   0.0f, 6.0f, 1 );
					
					if( StageLv > 2)
					{
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , COLOR_ID.BLUE, false, new Vector2( -20.0f, -15.0f　),  30.0f, 6.0f, 1 );
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , COLOR_ID.BLUE, false, new Vector2( -20.0f, -15.0f　), -30.0f, 6.0f, 1 );
						
						if( StageLv > 5 )
						{
							CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , COLOR_ID.BLUE, false, new Vector2( -20.0f, -15.0f　),  15.0f, 6.0f, 1 );
							CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , COLOR_ID.BLUE, false, new Vector2( -20.0f, -15.0f　), -15.0f, 6.0f, 1 );
							
							if( StageLv > 8)
							{ 
								for( int i = -1; i <= 1; i++ )
								{
									CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , COLOR_ID.BLUE, false, new Vector2( 20.0f, -8.0f  + 40 * i ),   0.0f, 6.0f, 1 );
								}
							}
						}
					}
				}

				
				++m_attack_num;
				
				if( m_attack_num == 5 )
				{
					// 移動状態へ移行
					m_State = STATE.OUTSTAGE;
					
					m_attack_num = 0;
				}
			}
		}
	}
}

