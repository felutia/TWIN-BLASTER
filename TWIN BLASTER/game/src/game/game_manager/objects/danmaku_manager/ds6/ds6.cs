
//謎



using System;
using Sce.PlayStation.Core;

namespace game
{
	public class CDs6
		: IDanmaku
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CDs6( DIFFICULTY diff, COLOR_ID color )
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

			
			m_AttackTimer[1] = new aqua.CFrameTimer( 23 );
			m_AttackTimer[2] = new aqua.CFrameTimer( 150 );
				
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
				
							m_AttackTimer[0] = new aqua.CFrameTimer( 40 );

				Speed = 1.5f;
			
			}
			
			
			else if( m_Difficulty == DIFFICULTY.EASY )
			{
				
							m_AttackTimer[0] = new aqua.CFrameTimer( 20 );

				Speed = 2.5f;
			
			}
			
			else if( m_Difficulty == DIFFICULTY.NORMAL )
			{
							m_AttackTimer[0] = new aqua.CFrameTimer( 5 );

				Speed = 3.5f;
			
			}
			else if( m_Difficulty == DIFFICULTY.HARD )
			{
			m_AttackTimer[0] = new aqua.CFrameTimer( 5 );
			
				Speed = 3.5f;
			
			}

		}
		
		/// <summary>
		/// 更新 
		/// </summary>
		public override void Update ()
		{
			base.Update();
		
			m_AttackTimer[0].Update();
			

			
			
			if(m_AttackTimer[0].IsEnd())
			{
							m_Angle[0] += 10.0f;
				CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,false , new Vector2(0.0f, 0.0f ),FMath.Sin(m_Angle[0]) *45.0f, Speed, 1 );
			
			
			
			
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, COLOR_ID.WHITE,false , new Vector2(0.0f, 0.0f ),FMath.Sin(m_Angle[0]) *35.0f, Speed, 1 );
				

			
			
			
			
				if( m_Difficulty == DIFFICULTY.HARD )
				{
			
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, COLOR_ID.WHITE,false , new Vector2(0.0f, 0.0f ),FMath.Sin(m_Angle[0]) *60.0f, Speed, 1 );
					//CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,false , new Vector2(0.0f, 0.0f ),FMath.Sin(m_Angle[0]) *14.0f, 2.3f, 1 );
			
			
			
				}

		
			}
			
			
		}
		
		System.Random m_Rand;
		float Speed;
		
	}
}