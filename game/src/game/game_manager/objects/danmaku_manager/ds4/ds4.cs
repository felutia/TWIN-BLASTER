using System;
using Sce.PlayStation.Core;

namespace game
{
	public class CDs4
		: IDanmaku
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CDs4( DIFFICULTY diff, COLOR_ID color )
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

			
			m_AttackTimer[0] = new aqua.CFrameTimer( 15 );
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
				
				m_AttackTimer[1].Limit = 50;
				
				m_Angle[0] = 120.0f;
				ForNum = 3;
			}			
			else if( m_Difficulty == DIFFICULTY.EASY )
			{
				m_Angle[0] = 60.0f;
				ForNum = 6;
			}
			else if( m_Difficulty == DIFFICULTY.NORMAL )
			{
					m_Angle[0] = 40.0f;
				ForNum = 9;
			
			}
			else if( m_Difficulty == DIFFICULTY.HARD )
			{
					m_Angle[0] = 30.0f;
				ForNum = 30;
			
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
			
			if( m_AttackTimer[0].IsEnd())
			for(int i = 0 ; i < ForNum;++i )
			{
				CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, COLOR_ID.WHITE, false, new Vector2(0.0f, 0.0f ), 360.0f / ForNum * (float)i + 30.0f, 10.5f, 1 );
			}
			
			if(m_AttackTimer[1].IsEnd())
			for(int i = 0 ;i  < ForNum;++i )
			{
			CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color, false, new Vector2(0.0f, 0.0f ), 360.0f/ForNum * (float)i, 3.5f, 1 );
			}
			
		}
		
		
		System.Random m_Rand;
		int ForNum;
		
	}
}