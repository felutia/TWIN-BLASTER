/*!
 *  @file       ds7.cs
 *  @brief      ボス弾幕7
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
	public class CDs7
		: IDanmaku
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CDs7( DIFFICULTY diff, COLOR_ID color )
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
			
			Atkint = 180;
			
			reflecFlag =false;
			
			if( m_Difficulty == DIFFICULTY.HARD )
			{
				Atkint = 240;
				
				reflecFlag = true;
				
			}
			
			
			m_AttackTimer[0] = new aqua.CFrameTimer( Atkint );
			m_AttackTimer[1] = new aqua.CFrameTimer( Atkint * 3 );
			m_AttackTimer[2] = new aqua.CFrameTimer( Atkint );
			
			
			
			
				// 直線弾
			m_AttackTimer[3] = new aqua.CFrameTimer( 300 );
			
						
			// 射出角度
			m_Angle = new float[m_attack_time_num];
			m_Angle[0] = 0.0f;
			m_Angle[1] = 15.0f;
			m_Angle[2] = 5.0f;
			
			
				
			if( m_Difficulty == DIFFICULTY.VERY_EASY )
			{
				
				AtkSpeed = 3.0f;
				ForNum = 25;
			
			
			}
			else if( m_Difficulty == DIFFICULTY.EASY )
			{
				
				AtkSpeed =3.0f;
				ForNum = 50;
				
			}
			
			else if( m_Difficulty == DIFFICULTY.NORMAL )
			{
				AtkSpeed  = 2.0f;
	
				ForNum = 70;
						
			}
			else if( m_Difficulty == DIFFICULTY.HARD )
			{
				AtkSpeed = 1.3f;
			
				ForNum = 120;
			}

			
			
			reFlag = false;
		}
		
		/// <summary>
		/// 更新 
		/// </summary>
		public override void Update ()
		{
			base.Update();
		
			m_AttackTimer[0].Update();
			m_AttackTimer[1].Update();
			
			
			m_Angle[0] += 1.0f;
			
			if( m_AttackTimer[0].IsEnd())
			for(int i = 0 ; i < ForNum / 5;++i )
			{
				if(reFlag == false)
				{
				CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,reflecFlag, new Vector2(0.0f, 0.0f ), 360.0f / ForNum *i*5+1, 2.5f*AtkSpeed, 1 );
				CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,reflecFlag , new Vector2(0.0f, 0.0f ),360.0f / ForNum * i*5+2,2.7f*AtkSpeed, 1 );
				CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,reflecFlag , new Vector2(0.0f, 0.0f ), 360.0f / ForNum *i*5+3 ,2.9f*AtkSpeed, 1 );
				CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,reflecFlag, new Vector2(0.0f, 0.0f ), 360.0f / ForNum *i*5+4 , 3.1f*AtkSpeed, 1 );
				CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, COLOR_ID.WHITE,reflecFlag, new Vector2(0.0f, 0.0f ), 360.0f / ForNum *i*5+5 , 3.3f*AtkSpeed, 1 );
				}
				else
				{
				CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, COLOR_ID.WHITE,reflecFlag, new Vector2(0.0f, 0.0f ), 360.0f / ForNum *i*5+1, 3.3f*AtkSpeed, 1 );
				CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET,m_Color,reflecFlag , new Vector2(0.0f, 0.0f ), 360.0f / ForNum *i*5+2,3.1f*AtkSpeed, 1 );
				CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,reflecFlag , new Vector2(0.0f, 0.0f ), 360.0f / ForNum *i*5+3 ,2.9f*AtkSpeed, 1 );
				CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,reflecFlag , new Vector2(0.0f, 0.0f ), 360.0f / ForNum *i*5+4 , 2.7f*AtkSpeed, 1 );
				CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,reflecFlag , new Vector2(0.0f, 0.0f ),360.0f / ForNum * i*5+5 , 2.5f*AtkSpeed, 1 );	
				}
				
				
			}
		
			if(m_AttackTimer[1].IsEnd())
			reFlag = !reFlag;
			
			
			
			
			
			
		}
		

		bool reFlag;
		
		bool reflecFlag;
		
		
		float AtkSpeed;
		
		uint Atkint;
		
		
		int ForNum;
		
	}
}