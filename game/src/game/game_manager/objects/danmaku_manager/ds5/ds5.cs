/*!
 *  @file       ds5.cs
 *  @brief      ボス弾幕5
 *  @author     Masaya Ichiki
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */
 
//波形反射



using System;
using Sce.PlayStation.Core;

namespace game
{
	public class CDs5
		: IDanmaku
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CDs5( DIFFICULTY diff, COLOR_ID color )
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

			
			m_AttackTimer[0] = new aqua.CFrameTimer( 0 );
			m_AttackTimer[1] = new aqua.CFrameTimer( 23 );
			m_AttackTimer[2] = new aqua.CFrameTimer( 150 );
				
				// 直線弾
			m_AttackTimer[3] = new aqua.CFrameTimer( 300 );
			
						
			// 射出角度
			m_Angle = new float[m_attack_time_num];
			m_Angle[0] = 0.0f;
			m_Angle[1] = 15.0f;
			m_Angle[2] = 5.0f;
			
			
			ForNum = 0 ;
			
			
			if( m_Difficulty == DIFFICULTY.VERY_EASY )
			{
				
				ForNum = 10;
				
			}

			else if( m_Difficulty == DIFFICULTY.EASY )
			{
				
				ForNum = 30;
				
			}
			
			else if( m_Difficulty == DIFFICULTY.NORMAL )
			{
				ForNum = 40;
				
			}
			else if( m_Difficulty == DIFFICULTY.HARD )
			{
				ForNum = 70;
			
			}

		}
		
		/// <summary>
		/// 更新 
		/// </summary>
		public override void Update ()
		{
			base.Update();
		
			m_AttackTimer[0].Update();
			
			m_Angle[0] += 1.0f;
			
			if( m_AttackTimer[0].IsEnd())
			{
				for(int i = 0 ; i < ForNum;++i )
				{
	
					
					
					if(i % 5 ==0)
					{
						
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, COLOR_ID.WHITE,true , new Vector2(0.0f, 0.0f ), 360.0f / ForNum* i, 2.0f, 1 );
					
						
						
					}
					else
					{
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,true , new Vector2(0.0f, 0.0f ), 360.0f / ForNum* i, 2.0f, 1 );
						
						
						
					}
					
					
					
				}
		m_AttackTimer[0].Limit = 300;				
			}
			
			
		}
		
		System.Random m_Rand;
		int ForNum;
		
	}
}