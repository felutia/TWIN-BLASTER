using System;
using Sce.PlayStation.Core;

namespace game
{
	public class CDanmakuSample
		: IDanmaku
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CDanmakuSample ( DIFFICULTY diff, COLOR_ID color )
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
			if( m_Difficulty == DIFFICULTY.VERY_EASY )
			{
				m_AttackTimer[0] = new aqua.CFrameTimer( 120 );
				m_AttackTimer[1] = new aqua.CFrameTimer( 100 );
				m_AttackTimer[2] = new aqua.CFrameTimer( 170 );
				
				// 直線弾
				m_AttackTimer[3] = new aqua.CFrameTimer( 310 );
			}
			else if( m_Difficulty == DIFFICULTY.EASY )
			{
				m_AttackTimer[0] = new aqua.CFrameTimer( 100 );
				m_AttackTimer[1] = new aqua.CFrameTimer( 90 );
				m_AttackTimer[2] = new aqua.CFrameTimer( 150 );
				
				// 直線弾
				m_AttackTimer[3] = new aqua.CFrameTimer( 300 );
			}
			else if( m_Difficulty == DIFFICULTY.NORMAL )
			{
				m_AttackTimer[0] = new aqua.CFrameTimer( 80 );
				m_AttackTimer[1] = new aqua.CFrameTimer( 60 );
				m_AttackTimer[2] = new aqua.CFrameTimer( 120 );
				
				// 直線弾
				m_AttackTimer[3] = new aqua.CFrameTimer( 200 );
			}
			else if( m_Difficulty == DIFFICULTY.HARD )
			{
				m_AttackTimer[0] = new aqua.CFrameTimer( 60 );
				m_AttackTimer[1] = new aqua.CFrameTimer( 40 );
				m_AttackTimer[2] = new aqua.CFrameTimer( 100 );
				
				// 直線弾
				m_AttackTimer[3] = new aqua.CFrameTimer( 100 );
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
			if( m_Difficulty == DIFFICULTY.VERY_EASY)
			{
				// 一定間隔でミサイル発射
				if( m_AttackTimer[3].IsEnd() == true )
				{
					CreateBullet( ENEMY_BULLET_ID.MISSILE_BULLET, m_Color, false, new Vector2( 0.0f, 20.0f ), 0.0f, 5.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.MISSILE_BULLET, m_Color, false, new Vector2( 0.0f,-40.0f ), 0.0f, 5.0f, 1 );
				}
				
				/// <summary>
				/// 赤弾幕
				/// </summary>
				if( m_AttackTimer[0].IsEnd() == true )
				{
					for (int i = 0; i < 15; i++)
					{
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color, false, new Vector2( -8.0f, -8.0f ), m_Angle[0]+(360.0f/15*(float)i), 2.5f, 1 );
					}
					
					m_Angle[0] -= 2.0f;
				}
				if( m_AttackTimer[1].IsEnd( ) == true )
				{
					for (int i = 0; i < 8; i++)
					{
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color, false, new Vector2( -8.0f, -8.0f ), m_Angle[1]+(360.0f/8*(float)i), 3.0f, 1 ); 
					}
					
					m_Angle[1] += 1.0f;
				}
				if( m_AttackTimer[2].IsEnd( ) == true )
				{
					for (int i = 0; i < 8; i++)
					{
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color, false, new Vector2( -8.0f, -8.0f ), m_Angle[2]+(360.0f/8*(float)i), 5.0f, 1 ); 
					}
					
					m_Angle[2] += 3.0f;
				}
			}
			if( m_Difficulty == DIFFICULTY.EASY )
			{
				// 一定間隔でミサイル発射
				if( m_AttackTimer[3].IsEnd() == true )
				{
					CreateBullet( ENEMY_BULLET_ID.MISSILE_BULLET, m_Color, false, new Vector2( 0.0f, 20.0f ), 0.0f, 5.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.MISSILE_BULLET, m_Color, false, new Vector2( 0.0f,-40.0f ), 0.0f, 5.0f, 1 );
				}
				
				/// <summary>
				/// 赤弾幕
				/// </summary>
				if( m_AttackTimer[0].IsEnd() == true )
				{
					for (int i = 0; i < 15; i++)
					{
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color, false, new Vector2( -8.0f, -8.0f ), m_Angle[0]+(360.0f/15*(float)i), 3.5f, 1 );
					}
					
					m_Angle[0] -= 2.0f;
				}
				if( m_AttackTimer[1].IsEnd( ) == true )
				{
					for (int i = 0; i < 8; i++)
					{
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color, false, new Vector2( -8.0f, -8.0f ), m_Angle[1]+(360.0f/8*(float)i), 4.0f, 1 ); 
					}
					
					m_Angle[1] += 1.0f;
				}
				if( m_AttackTimer[2].IsEnd( ) == true )
				{
					for (int i = 0; i < 8; i++)
					{
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color, false, new Vector2( -8.0f, -8.0f ), m_Angle[2]+(360.0f/8*(float)i), 6.0f, 1 ); 
					}
					
					m_Angle[2] += 3.0f;
				}
			}
			else if( m_Difficulty == DIFFICULTY.NORMAL )
			{
				// 一定間隔でミサイル発射
				if( m_AttackTimer[3].IsEnd() == true )
				{
					CreateBullet( ENEMY_BULLET_ID.MISSILE_BULLET, m_Color, false, new Vector2( 0.0f, 20.0f ), 0.0f, 8.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.MISSILE_BULLET, COLOR_ID.BLUE, false, new Vector2( 0.0f,-40.0f ), 0.0f, 8.0f, 1 );
				}
				
				/// <summary>
				/// 赤弾幕
				/// </summary>
				if( m_AttackTimer[0].IsEnd() == true )
				{
					for (int i = 0; i < 36; i++)
					{
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color, false, new Vector2( -8.0f, -8.0f ), m_Angle[0]+(360.0f/36*(float)i), 5.0f, 1 );
					}
					
					m_Angle[0] -= 2.0f;
				}
				if( m_AttackTimer[1].IsEnd( ) == true )
				{
					for (int i = 0; i < 15; i++)
					{
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color, false, new Vector2( -8.0f, -8.0f ), m_Angle[1]+(360.0f/15*(float)i), 6.0f, 1 );
					}
					
					m_Angle[1] += 1.0f;
				}
				if( m_AttackTimer[2].IsEnd( ) == true )
				{
					for (int i = 0; i < 16; i++)
					{
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color, false, new Vector2( -8.0f, -8.0f ), m_Angle[2]+(360.0f/16*(float)i), 8.0f, 1 ); 
					}
					
					m_Angle[2] += 3.0f;
				}
			}
			else if( m_Difficulty == DIFFICULTY.HARD )
			{
				// 一定間隔でミサイル発射
				if( m_AttackTimer[3].IsEnd() == true )
				{
					CreateBullet( ENEMY_BULLET_ID.MISSILE_BULLET, m_Color, false, new Vector2( 0.0f, 20.0f ), 0.0f, 8.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.MISSILE_BULLET, COLOR_ID.BLUE, false, new Vector2( 0.0f,-40.0f ), 0.0f, 8.0f, 1 );
				}
				
				/// <summary>
				/// 赤弾幕
				/// </summary>
				if( m_AttackTimer[0].IsEnd() == true )
				{
					for (int i = 0; i < 48; i++)
					{
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color, false, new Vector2( -8.0f, -8.0f ), m_Angle[0]+(360.0f/48*(float)i), 8.0f, 1 );
					}
					
					m_Angle[0] -= 2.0f;
				}
				if( m_AttackTimer[1].IsEnd( ) == true )
				{
					for (int i = 0; i < 24; i++)
					{
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color, false, new Vector2( -8.0f, -8.0f ), m_Angle[1]+(360.0f/24*(float)i), 6.0f, 1 ); 
					}
					
					m_Angle[1] += 1.0f;
				}
				if( m_AttackTimer[2].IsEnd( ) == true )
				{
					for (int i = 0; i < 28; i++)
					{
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color, false, new Vector2( -8.0f, -8.0f ), m_Angle[2]+(360.0f/28*(float)i), 10.0f, 1 ); 
					}
					
					m_Angle[2] += 3.0f;
				}
			}
		}
	}
}

