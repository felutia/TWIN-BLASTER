/*!
 *  @file       ds8.cs
 *  @brief      ボス弾幕8
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
	public class CDs8
		: IDanmaku
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CDs8( DIFFICULTY diff, COLOR_ID color )
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
			

			
			m_AttackTimer[1] = new aqua.CFrameTimer( 120 );
			m_AttackTimer[2] = new aqua.CFrameTimer( 120 );
				
				// 直線弾
			m_AttackTimer[3] = new aqua.CFrameTimer( 300 );
			
						
			// 射出角度
			m_Angle = new float[m_attack_time_num];
			m_Angle[0] = 10.0f;
			m_Angle[1] = 0.0f;
			m_Angle[2] = 5.0f;
			
			
			Dir = new float[7];
			Dir[0] = 90.0f;
			Dir[1] = 120.0f;
			Dir[2] = 150.0f;
			Dir[3] = 180.0f;
			Dir[4] = 210.0f;
			Dir[5] = 240.0f;
			Dir[6] = 270.0f;
			
			
			reFlag = true;
			
			if( m_Difficulty == DIFFICULTY.VERY_EASY )
			{
				
			m_AttackTimer[0] = new aqua.CFrameTimer( 60 );
				
			}
			else if( m_Difficulty == DIFFICULTY.EASY )
			{
				
			m_AttackTimer[0] = new aqua.CFrameTimer( 20 );
				
			}
			
			else if( m_Difficulty == DIFFICULTY.NORMAL )
			{
			m_AttackTimer[0] = new aqua.CFrameTimer( 15 );
				
			}
			else if( m_Difficulty == DIFFICULTY.HARD )
			{
			m_AttackTimer[0] = new aqua.CFrameTimer( 10 );
			reFlag = true;
			}
		}
		
		/// <summary>
		/// 更新 
		/// </summary>
		public override void Update ()
		{
			base.Update();
				
			m_AttackTimer[0].Update();	
		m_AttackTimer[1].Update();
			
			
			if(m_AttackTimer[0].IsEnd())
			{
				
				for(int i =0 ; i <7;++i)
				{
					
					
					if( i ==3 )
						CreateBullet( ENEMY_BULLET_ID.RAIN_BULLET, COLOR_ID.WHITE,reFlag, new Vector2(0.0f, 0.0f ), Dir[i], 5.5f, 1 );
					
					else	
						CreateBullet( ENEMY_BULLET_ID.RAIN_BULLET, m_Color,reFlag, new Vector2(0.0f, 0.0f ), Dir[i], 5.5f, 1 );
					
					
				Dir[i] += m_Angle[0];
				
				if(Dir[i] >90.0f + 30.0f * i + 30.0f)
					m_Angle[0] *= -1.0f;
		
				if(Dir[i] <90.0f + 30.0f * i - 30.0f)
					m_Angle[0]*=-1.0f;
				}				
			}
		
			
			
			
		}
	
		

		bool reFlag;
		

		
		float[] Dir;
	}
}