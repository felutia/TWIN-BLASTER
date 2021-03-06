/*!
 *  @file       ds3.cs
 *  @brief      ボス弾幕3
 *  @author     Masaya Ichiki
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */
 
using System;
using Sce.PlayStation.Core;

namespace game
{
	public class CDs3
		: IDanmaku
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CDs3 ( DIFFICULTY diff, COLOR_ID color )
			: base( diff, color )
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize ()
		{
			// 攻撃タイマー設定(仮)
			m_AttackTimer = new aqua.CFrameTimer[m_attack_time_num];
			
			m_BulletNum = 360;
			
			
			
			m_Rand = new System.Random();
			
				m_AttackTimer[0] = new aqua.CFrameTimer( 10 );
				m_AttackTimer[1] = new aqua.CFrameTimer( 13 );
				m_AttackTimer[2] = new aqua.CFrameTimer( 150 );
				
				// 直線弾
				m_AttackTimer[3] = new aqua.CFrameTimer( 300 );
			
			if(m_Difficulty == DIFFICULTY.VERY_EASY)
			{
					ForNum = 2;
				m_AttackTimer[1] = new aqua.CFrameTimer( 50 );
				
				
			}
			
			else if( m_Difficulty == DIFFICULTY.EASY )
			{
				ForNum = 2;
			}
			else if( m_Difficulty == DIFFICULTY.NORMAL )
			{
				ForNum = 4;
				
			}
			else if( m_Difficulty == DIFFICULTY.HARD )
			{
				ForNum =6;
				
			}
			
			// 射出角度
			m_Angle = new float[m_attack_time_num];
			m_Angle[0] = 0.0f;
			m_Angle[1] = 15.0f;
			m_Angle[2] = 5.0f;
		}
		
		/// <summary>
		/// 更新 
		/// </summary>
		public override void Update ()
		{
			base.Update();
			
			m_Angle[0] +=1.0f;
			
			m_AttackTimer[0].Update();
			m_AttackTimer[1].Update();
			

			if(m_AttackTimer[0].IsEnd())
			{
			
			CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, COLOR_ID.WHITE, false, new Vector2( 0.0f,0.0f ), 20.0f, 5.0f, 1 );
			CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, COLOR_ID.WHITE, false, new Vector2( 0.0f, 0.0f  ), -20.0f, 5.0f, 1 );
//			CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, COLOR_ID.WHITE, false, new Vector2( 0.0f, 0.0f ), 90.0f, 5.0f, 1 );
//			CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, COLOR_ID.WHITE, false, new Vector2( 0.0f, 0.0f  ), -90.0f, 5.0f, 1 );
//			CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, COLOR_ID.WHITE, false, new Vector2( 0.0f, 0.0f  ), -45.0f  , 5.0f, 1 );
//			CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, COLOR_ID.WHITE, false, new Vector2( 0.0f, 0.0f ), 45.0f, 5.0f, 1 );
			
			}
			
		

			
			if(m_AttackTimer[1].IsEnd())
			{
				for(int i= 0 ; i < ForNum; ++i)
				{
					float ran = (float)aqua.CRandom.GetInstance().GetValue(-19,19);

					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color, false, new Vector2( 0.0f, 0.0f ), ran, 3.0f, 1 );
				}
				
				
			}
			
			
			
			
			
			
			
		}
		
		
		System.Random m_Rand;
	int ForNum;
		
	}
}

