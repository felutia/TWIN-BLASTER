/*!
 *  @file       ds9.cs
 *  @brief      ボス弾幕9
 *  @author     Masaya Ichiki
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */
 
//謎



using System;
using Sce.PlayStation.Core;

namespace game
{
	public class CDs9
		: IDanmaku
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CDs9( DIFFICULTY diff, COLOR_ID color )
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
			
			
			m_AttackTimer[0] = new aqua.CFrameTimer( 30 );
			m_AttackTimer[1] = new aqua.CFrameTimer( 400 );
			m_AttackTimer[2] = new aqua.CFrameTimer( 100 );
				
				// 直線弾
			m_AttackTimer[3] = new aqua.CFrameTimer( 300 );
			
						
			// 射出角度
			m_Angle = new float[m_attack_time_num];
			m_Angle[0] = 0.0f;
			m_Angle[1] = 15.0f;
			m_Angle[2] = 5.0f;
			
			
			Speed = 2.5f;
			if( m_Difficulty == DIFFICULTY.VERY_EASY )
			{
				
				m_AttackTimer[0] = new aqua.CFrameTimer( 60 );

				Speed = 5.5f;
			
			}
			else if( m_Difficulty == DIFFICULTY.EASY )
			{
				
				m_AttackTimer[0] = new aqua.CFrameTimer( 40 );

				Speed = 5.5f;
			
			}
			
			else if( m_Difficulty == DIFFICULTY.NORMAL )
			{
				Speed = 2.5f;
			
			}
			else if( m_Difficulty == DIFFICULTY.HARD )
			{
			
				Speed = 2.5f;
			
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
				CreateBullet(ENEMY_BULLET_ID.NORMAL_BULLET,COLOR_ID.WHITE,false,new Vector2(0.0f,0.0f) ,5.0f,Speed,1);		
			
				CreateBullet(ENEMY_BULLET_ID.NORMAL_BULLET,COLOR_ID.WHITE,false,new Vector2(0.0f,0.0f) ,-5.0f,Speed,1);		
			if(m_Difficulty!=DIFFICULTY.EASY||m_Difficulty !=DIFFICULTY.VERY_EASY)
				{
					CreateBullet(ENEMY_BULLET_ID.NORMAL_BULLET,COLOR_ID.WHITE,false,new Vector2(0.0f,0.0f) ,20.0f,Speed,1);		
				
					CreateBullet(ENEMY_BULLET_ID.NORMAL_BULLET,COLOR_ID.WHITE,false,new Vector2(0.0f,0.0f) ,-20.0f,Speed,1);		
				}
			}
			
			
			
			
			if(m_Difficulty == DIFFICULTY.NORMAL)
			if(m_AttackTimer[1].IsEnd())
			{
				for(int i = 0 ; i < 40 ;++i )
				{
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,false, new Vector2(0.0f, 0.0f ), 360.0f /40.0f * (float)i, 2.5f, 1 );
				
				
				}
				
				
				
			}
			if(m_Difficulty == DIFFICULTY.HARD)
			if(m_AttackTimer[2].IsEnd())
			{
				for(int i = 0 ; i < 360 / 5;++i )
				{
				CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,false, new Vector2(0.0f, 0.0f ), i*5+1, 2.5f, 1 );
				CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,false , new Vector2(0.0f, 0.0f ), i*5+2,3.5f, 1 );
				CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,false , new Vector2(0.0f, 0.0f ), i*5+3 ,4.5f, 1 );
				CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,false, new Vector2(0.0f, 0.0f ), i*5+4 , 5.5f, 1 );
				CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,false, new Vector2(0.0f, 0.0f ), i*5+5 , 6.5f, 1 );
				
				}
				
				
				
			}
			
			
			
		}
		
		float Speed;
		
	}
}