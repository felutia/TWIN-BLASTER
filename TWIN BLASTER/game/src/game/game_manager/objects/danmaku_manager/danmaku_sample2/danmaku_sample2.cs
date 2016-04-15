using System;
using Sce.PlayStation.Core;

namespace game
{
	public class CDanmakuSample2
		: IDanmaku
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CDanmakuSample2( DIFFICULTY diff, COLOR_ID color )
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
			if( m_Difficulty == DIFFICULTY.VERY_EASY)
			{
				m_AttackTimer[0] = new aqua.CFrameTimer( 12 );
				m_AttackTimer[1] = new aqua.CFrameTimer( 170 );
				m_AttackTimer[2] = new aqua.CFrameTimer( 170 );
			}
			else if( m_Difficulty == DIFFICULTY.EASY )
			{
				m_AttackTimer[0] = new aqua.CFrameTimer( 10 );
				m_AttackTimer[1] = new aqua.CFrameTimer( 150 );
				m_AttackTimer[2] = new aqua.CFrameTimer( 150 );
			}
			else if( m_Difficulty == DIFFICULTY.NORMAL )
			{
				m_AttackTimer[0] = new aqua.CFrameTimer( 6 );
				m_AttackTimer[1] = new aqua.CFrameTimer( 120 );
				m_AttackTimer[2] = new aqua.CFrameTimer( 120 );
			}
			else if( m_Difficulty == DIFFICULTY.HARD )
			{
				m_AttackTimer[0] = new aqua.CFrameTimer( 3 );
				m_AttackTimer[1] = new aqua.CFrameTimer( 90 );
				m_AttackTimer[2] = new aqua.CFrameTimer( 90 );
			}
			
			
			// 直線弾
			m_AttackTimer[3] = new aqua.CFrameTimer( 300 );
			
			// 射出角度
			m_Angle = new float[m_attack_time_num];
			m_Angle[0] = 90.0f;
			m_Angle[1] = 93.0f;
			m_Angle[2] = 5.0f;
		}
		
		/// <summary>
		/// 更新 
		/// </summary>
		public override void Update()
		{
			base.Update();
			if( m_Difficulty == DIFFICULTY.VERY_EASY )
			{
				// 一定間隔でミサイル発射
				if( m_AttackTimer[3].IsEnd() == true )
				{
					CreateBullet( ENEMY_BULLET_ID.MISSILE_BULLET, COLOR_ID.RED,  false, new Vector2( 0.0f, 20.0f ), 0.0f, 5.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.MISSILE_BULLET, m_Color, false, new Vector2( 0.0f,-40.0f ), 0.0f, 5.0f, 1 );
				}
				
				/// <summary>
				/// 赤弾幕
				/// </summary>
				if( m_AttackTimer[0].IsEnd( ) == true )
				{
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[0],  3.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[0],  3.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[0],  4.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[1],  2.5f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[1] + 5.0f, 3.5f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, COLOR_ID.WHITE, false, new Vector2( -8.0f, -8.0f ), m_Angle[1] - 6.0f, 3.5f, 1 );
					
					m_Angle[0] -= 20.0f;
					m_Angle[1] -= 30.0f;
				}
				
				if( m_AttackTimer[1].IsEnd( ) == true )
				{
					IObject p1 = CGameManager.GetInstance().FindObject(OBJECT_ID.PLAYER1);
					IObject p2 = CGameManager.GetInstance().FindObject(OBJECT_ID.PLAYER2);
					IObject boss = CGameManager.GetInstance().FindObject(OBJECT_ID.BOSS);
					
					if( boss != null )
					{
						if( p1 != null )
						{
							CPlayer1 player1 = (CPlayer1)CGameManager.GetInstance().FindObject( OBJECT_ID.PLAYER1 );
							IBoss bossLv1 = (IBoss)CGameManager.GetInstance().FindObject(OBJECT_ID.BOSS);
							
							Vector2 bp  = bossLv1.Pos + bossLv1.Sprite.Center;
							Vector2 p1p = player1.Position + player1.Center;
							
							Vector2 v1  = bp - p1p;
							Vector2 Nv1 = new Vector2(0.0f, 0.0f);
							
							// 正規化
							v1.Normalize( out Nv1 );
							
							// 逆正接を求める( X,Y )
							float eAngle = FMath.Atan2( Nv1.X, Nv1.Y );
							float angle1 = eAngle * ( 180.0f / FMath.PI );
							
							if( angle1 < 0.0f )
								angle1 += 360.0f;
							
							angle1 -= 90.0f;
							
							// 弾生成
							CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , m_Color,  false, new Vector2( -8.0f, -8.0f ), -angle1, 7.0f, 1 );
							CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , COLOR_ID.WHITE, false, new Vector2( -8.0f, -8.0f ), -angle1 + 2.0f, 7.0f, 1 );
							CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , m_Color,  false, new Vector2( -8.0f, -8.0f ), -angle1 - 2.0f, 7.0f, 1 );
						}
						
						if ( p2 != null )
						{
							CPlayer2 player2 = (CPlayer2)CGameManager.GetInstance().FindObject( OBJECT_ID.PLAYER2 );
							IBoss bossLv1 = (IBoss)CGameManager.GetInstance().FindObject(OBJECT_ID.BOSS);
							
							Vector2 bp  = bossLv1.Pos + bossLv1.Sprite.Center;
							Vector2 p2p = player2.Position + player2.Center;
							
							Vector2 v2  = bp - p2p;
							Vector2 Nv2 = new Vector2(0.0f, 0.0f);
							
							// 正規化
							v2.Normalize( out Nv2 );
							
							// 逆正接を求める
							float eAngle = FMath.Atan2( Nv2.X, Nv2.Y );
							float angle2 = eAngle * ( 180.0f / FMath.PI );
							
							if( angle2 < 0.0f )
								angle2 += 360.0f;
							
							angle2 -= 90.0f;
							
							// 散弾生成
							CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , m_Color,  false, new Vector2( -8.0f, -8.0f ), -angle2, 7.0f, 1 );
							CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , COLOR_ID.WHITE, false, new Vector2( -8.0f, -8.0f ), -angle2 + 2.0f, 7.0f, 1 );
							CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , m_Color,  false, new Vector2( -8.0f, -8.0f ), -angle2 - 2.0f, 7.0f, 1 );
						}
					}
				}
			}
			else if( m_Difficulty == DIFFICULTY.EASY )
			{
				// 一定間隔でミサイル発射
				if( m_AttackTimer[3].IsEnd() == true )
				{
					CreateBullet( ENEMY_BULLET_ID.MISSILE_BULLET, COLOR_ID.RED,  false, new Vector2( 0.0f, 20.0f ), 0.0f, 5.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.MISSILE_BULLET, m_Color, false, new Vector2( 0.0f,-40.0f ), 0.0f, 5.0f, 1 );
				}
				
				/// <summary>
				/// 赤弾幕
				/// </summary>
				if( m_AttackTimer[0].IsEnd( ) == true )
				{
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[0],  4.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[0],  4.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[0],  5.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[1],  3.5f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[1] + 6.0f, 3.5f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, COLOR_ID.WHITE, false, new Vector2( -8.0f, -8.0f ), m_Angle[1] - 6.0f, 3.5f, 1 );
					
					m_Angle[0] -= 20.0f;
					m_Angle[1] -= 30.0f;
				}
				
				if( m_AttackTimer[1].IsEnd( ) == true )
				{
					IObject p1 = CGameManager.GetInstance().FindObject(OBJECT_ID.PLAYER1);
					IObject p2 = CGameManager.GetInstance().FindObject(OBJECT_ID.PLAYER2);
					IObject boss = CGameManager.GetInstance().FindObject(OBJECT_ID.BOSS);
					
					if( boss != null )
					{
						if( p1 != null )
						{
							CPlayer1 player1 = (CPlayer1)CGameManager.GetInstance().FindObject( OBJECT_ID.PLAYER1 );
							IBoss bossLv1 = (IBoss)CGameManager.GetInstance().FindObject(OBJECT_ID.BOSS);
							
							Vector2 bp  = bossLv1.Pos + bossLv1.Sprite.Center;
							Vector2 p1p = player1.Position + player1.Center;
							
							Vector2 v1  = bp - p1p;
							Vector2 Nv1 = new Vector2(0.0f, 0.0f);
							
							// 正規化
							v1.Normalize( out Nv1 );
							
							// 逆正接を求める( X,Y )
							float eAngle = FMath.Atan2( Nv1.X, Nv1.Y );
							float angle1 = eAngle * ( 180.0f / FMath.PI );
							
							if( angle1 < 0.0f )
								angle1 += 360.0f;
							
							angle1 -= 90.0f;
							
							// 弾生成
							CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , m_Color,  false, new Vector2( -8.0f, -8.0f ), -angle1, 9.0f, 1 );
							CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , COLOR_ID.WHITE, false, new Vector2( -8.0f, -8.0f ), -angle1 + 2.0f, 9.0f, 1 );
							CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , m_Color,  false, new Vector2( -8.0f, -8.0f ), -angle1 - 2.0f, 9.0f, 1 );
						}
						
						if ( p2 != null )
						{
							CPlayer2 player2 = (CPlayer2)CGameManager.GetInstance().FindObject( OBJECT_ID.PLAYER2 );
							IBoss bossLv1 = (IBoss)CGameManager.GetInstance().FindObject(OBJECT_ID.BOSS);
							
							Vector2 bp  = bossLv1.Pos + bossLv1.Sprite.Center;
							Vector2 p2p = player2.Position + player2.Center;
							
							Vector2 v2  = bp - p2p;
							Vector2 Nv2 = new Vector2(0.0f, 0.0f);
							
							// 正規化
							v2.Normalize( out Nv2 );
							
							// 逆正接を求める
							float eAngle = FMath.Atan2( Nv2.X, Nv2.Y );
							float angle2 = eAngle * ( 180.0f / FMath.PI );
							
							if( angle2 < 0.0f )
								angle2 += 360.0f;
							
							angle2 -= 90.0f;
							
							// 散弾生成
							CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , m_Color,  false, new Vector2( -8.0f, -8.0f ), -angle2, 9.0f, 1 );
							CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , COLOR_ID.WHITE, false, new Vector2( -8.0f, -8.0f ), -angle2 + 2.0f, 9.0f, 1 );
							CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , m_Color,  false, new Vector2( -8.0f, -8.0f ), -angle2 - 2.0f, 9.0f, 1 );
						}
					}
				}
			}
			else if( m_Difficulty == DIFFICULTY.NORMAL )
			{
				// 一定間隔でミサイル発射
				if( m_AttackTimer[3].IsEnd() == true )
				{
					CreateBullet( ENEMY_BULLET_ID.MISSILE_BULLET, COLOR_ID.RED,  false, new Vector2( 0.0f, 20.0f ), 0.0f, 8.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.MISSILE_BULLET, m_Color, false, new Vector2( 0.0f,-40.0f ), 0.0f, 8.0f, 1 );
				}
				
				/// <summary>
				/// 赤弾幕
				/// </summary>
				if( m_AttackTimer[0].IsEnd( ) == true )
				{
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[0],  6.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[0],  6.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[0],  7.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[1],  5.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[1] + 6.0f, 5.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, COLOR_ID.WHITE, false, new Vector2( -8.0f, -8.0f ), m_Angle[1] - 6.0f, 5.0f, 1 );
					
					m_Angle[0] -= 20.0f;
					m_Angle[1] -= 30.0f;
				}
				
				if( m_AttackTimer[1].IsEnd( ) == true )
				{
					IObject p1 = CGameManager.GetInstance().FindObject(OBJECT_ID.PLAYER1);
					IObject p2 = CGameManager.GetInstance().FindObject(OBJECT_ID.PLAYER2);
					
					if( p1 != null )
					{
						CPlayer1 player1 = (CPlayer1)CGameManager.GetInstance().FindObject( OBJECT_ID.PLAYER1 );
						IBoss bossLv1 = (IBoss)CGameManager.GetInstance().FindObject(OBJECT_ID.BOSS);
						
						Vector2 bp  = bossLv1.Pos + bossLv1.Sprite.Center;
						Vector2 p1p = player1.Position + player1.Center;
						
						Vector2 v1  = bp - p1p;
						Vector2 Nv1 = new Vector2(0.0f, 0.0f);
						
						// 正規化
						v1.Normalize( out Nv1 );
						
						// 逆正接を求める( X,Y )
						float eAngle = FMath.Atan2( Nv1.X, Nv1.Y );
						float angle1 = eAngle * (180.0f / FMath.PI );
						
						if( angle1 < 0.0f )
							angle1 += 360.0f;
						
						angle1 -= 90.0f;
						
						// 弾生成
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , m_Color,  false, new Vector2( -8.0f, -8.0f ), -angle1, 12.0f, 1 );
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , COLOR_ID.WHITE, false, new Vector2( -8.0f, -8.0f ), -angle1 + 2.0f, 12.0f, 1 );
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , m_Color,  false, new Vector2( -8.0f, -8.0f ), -angle1 - 2.0f, 12.0f, 1 );
					}
					
					if ( p2 != null )
					{
						CPlayer2 player2 = (CPlayer2)CGameManager.GetInstance().FindObject( OBJECT_ID.PLAYER2 );
						IBoss bossLv1 = (IBoss)CGameManager.GetInstance().FindObject(OBJECT_ID.BOSS);
						
						Vector2 bp  = bossLv1.Pos + bossLv1.Sprite.Center;
						Vector2 p2p = player2.Position + player2.Center;
						
						Vector2 v2  = bp - p2p;
						Vector2 Nv2 = new Vector2(0.0f, 0.0f);
						
						// 正規化
						v2.Normalize( out Nv2 );
						
						// 逆正接を求める
						float eAngle = FMath.Atan2( Nv2.X, Nv2.Y );
						float angle2 = eAngle * (180.0f / FMath.PI );
						
						if( angle2 < 0.0f )
							angle2 += 360.0f;
						
						angle2 -= 90.0f;
						
						// 散弾生成
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , m_Color,  false, new Vector2( -8.0f, -8.0f ), -angle2, 12.0f, 1 );
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , COLOR_ID.WHITE, false, new Vector2( -8.0f, -8.0f ), -angle2 + 2.0f, 12.0f, 1 );
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , m_Color,  false, new Vector2( -8.0f, -8.0f ), -angle2 - 2.0f, 12.0f, 1 );
					}
				}
			}
			else if( m_Difficulty == DIFFICULTY.HARD )
			{
				// 一定間隔でミサイル発射
				if( m_AttackTimer[3].IsEnd() == true )
				{
					CreateBullet( ENEMY_BULLET_ID.MISSILE_BULLET, COLOR_ID.RED,  false, new Vector2( 0.0f, 20.0f ), 0.0f, 10.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.MISSILE_BULLET, m_Color, false, new Vector2( 0.0f,-40.0f ), 0.0f, 10.0f, 1 );
				}
				
				/// <summary>
				/// 赤弾幕
				/// </summary>
				if( m_AttackTimer[0].IsEnd( ) == true )
				{
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[0],  8.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[0],  8.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[0],  9.0f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[1],  6.5f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, m_Color,  false, new Vector2( -8.0f, -8.0f ), m_Angle[1] + 6.0f, 6.5f, 1 );
					CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET, COLOR_ID.WHITE, false, new Vector2( -8.0f, -8.0f ), m_Angle[1] - 6.0f, 6.5f, 1 );
					
					m_Angle[0] -= 20.0f;
					m_Angle[1] -= 30.0f;
				}
				
				if( m_AttackTimer[1].IsEnd( ) == true )
				{
					IObject p1 = CGameManager.GetInstance().FindObject(OBJECT_ID.PLAYER1);
					IObject p2 = CGameManager.GetInstance().FindObject(OBJECT_ID.PLAYER2);
					
					if( p1 != null )
					{
						CPlayer1 player1 = (CPlayer1)CGameManager.GetInstance().FindObject( OBJECT_ID.PLAYER1 );
						IBoss bossLv1 = (IBoss)CGameManager.GetInstance().FindObject(OBJECT_ID.BOSS);
						
						Vector2 bp  = bossLv1.Pos + bossLv1.Sprite.Center;
						Vector2 p1p = player1.Position + player1.Center;
						
						Vector2 v1  = bp - p1p;
						Vector2 Nv1 = new Vector2(0.0f, 0.0f);
						
						// 正規化
						v1.Normalize( out Nv1 );
						
						// 逆正接を求める( X,Y )
						float eAngle = FMath.Atan2( Nv1.X, Nv1.Y );
						float angle1 = eAngle * (180.0f / FMath.PI );
						
						if( angle1 < 0.0f )
							angle1 += 360.0f;
						
						angle1 -= 90.0f;
						
						// 弾生成
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , m_Color,  false, new Vector2( -8.0f, -8.0f ), -angle1, 15.0f, 1 );
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , COLOR_ID.WHITE, false, new Vector2( -8.0f, -8.0f ), -angle1 + 2.0f, 15.0f, 1 );
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , m_Color,  false, new Vector2( -8.0f, -8.0f ), -angle1 - 2.0f, 15.0f, 1 );
					}
					
					if ( p2 != null )
					{
						CPlayer2 player2 = (CPlayer2)CGameManager.GetInstance().FindObject( OBJECT_ID.PLAYER2 );
						IBoss bossLv1 = (IBoss)CGameManager.GetInstance().FindObject(OBJECT_ID.BOSS);
						
						Vector2 bp  = bossLv1.Pos + bossLv1.Sprite.Center;
						Vector2 p2p = player2.Position + player2.Center;
						
						Vector2 v2  = bp - p2p;
						Vector2 Nv2 = new Vector2(0.0f, 0.0f);
						
						// 正規化
						v2.Normalize( out Nv2 );
						
						// 逆正接を求める
						float eAngle = FMath.Atan2( Nv2.X, Nv2.Y );
						float angle2 = eAngle * (180.0f / FMath.PI );
						
						if( angle2 < 0.0f )
							angle2 += 360.0f;
						
						angle2 -= 90.0f;
						
						// 散弾生成
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , m_Color,  false, new Vector2( -8.0f, -8.0f ), -angle2, 15.0f, 1 );
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , COLOR_ID.WHITE, false, new Vector2( -8.0f, -8.0f ), -angle2 + 2.0f, 15.0f, 1 );
						CreateBullet( ENEMY_BULLET_ID.NORMAL_BULLET , m_Color,  false, new Vector2( -8.0f, -8.0f ), -angle2 - 2.0f, 15.0f, 1 );
					}
				}
			}
		}
	}
}

