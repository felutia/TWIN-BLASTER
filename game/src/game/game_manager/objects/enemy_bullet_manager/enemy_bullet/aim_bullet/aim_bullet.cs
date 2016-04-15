/*!
 *  @file       aim_bullet.cs
 *  @brief      エイム弾
 *  @author     Itiki Amano
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */
 
using System;
using System.Collections.Generic;
using Sce.PlayStation.Core;

namespace game
{
	public class CAimBullet
		: IEnemyBullet
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CAimBullet ( COLOR_ID color, bool reflec, Vector2 pos, float direction, float speed, int power )
			: base( color, reflec, pos, direction, speed, power )
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize()
		{
			// スプライト生成
			m_Sprite = CSpriteManager.Create( TEXTURE_ID.E_BULLET);
			
			// 色設定
			if( m_Color == COLOR_ID.BLUE )
				m_Sprite.Color = new Vector4( 0.6f, 0.6f, 1.0f, 1.0f );
			else if( m_Color == COLOR_ID.RAINBOW )
            	m_Sprite.Color = CColorManager.GetColor( );
			else if ( m_Color == COLOR_ID.RED )
				m_Sprite.Color = new Vector4( 1.0f, 0.4f, 0.4f, 1.0f );
			else if( m_Color == COLOR_ID.WHITE )
				m_Sprite.Color = new Vector4( 1.0f, 1.0f, 1.0f, 1.0f );
			
			// アルファブレンドを半加算に設定する
			m_Sprite.Alphablend = aqua.ALPHABLEND_TYPE.TRANSADD;
			
			// サイズ設定
			m_Size = new aqua.CSize( 16.0f, 16.0f );
			
			// 中心を設定
			m_Sprite.Center = new Vector2( 8.0f, 8.0f );
			
			// 回転値を設定
			m_Sprite.Angle = FMath.Radians( m_Direction );
			
			// 位置設定
			m_Sprite.Position = m_Pos;
			
			// 弾の半径
			m_Radius = 8.0f;
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public override void Update ()
		{
			IObject p1 = CGameManager.GetInstance( ).FindObject( OBJECT_ID.PLAYER1 );
			IObject p2 = CGameManager.GetInstance( ).FindObject( OBJECT_ID.PLAYER2 );
			
			if( p1 == null && p2 == null )
			{
				Vector2 pos = m_Sprite.Position;
				
				pos.X += FMath.Cos( FMath.Radians( m_Direction ) ) * m_Speed;
				pos.Y += FMath.Sin( FMath.Radians( m_Direction ) ) * m_Speed;
				
				m_Sprite.Position = pos;
			}
			
			if( p1 != null )
			{
				CPlayer1 P1 = (CPlayer1)CGameManager.GetInstance( ).FindObject( OBJECT_ID.PLAYER1 );
				
				Vector2 pos = m_Sprite.Position;
				
				for( int i = 0; i < BossList.Count; ++i)
				{
					Distance.X = P1.Center.X - m_Pos.X;
					Distance.Y = P1.Center.Y - m_Pos.Y;
				}
				
				BulletAngle = FMath.Atan2( Distance.Y, Distance.X ) * 180 / FMath.PI;
				
				BulletMovement.X = FMath.Cos( FMath.Radians( BulletAngle )) * m_Speed;
				BulletMovement.Y = FMath.Sin( FMath.Radians( BulletAngle )) * m_Speed;
			
				pos += new Vector2( BulletMovement.X, BulletMovement.Y );
				
				m_Sprite.Position = pos;
			}
			
			if( p2 != null )
			{
				CPlayer2 P2 = (CPlayer2)CGameManager.GetInstance( ).FindObject( OBJECT_ID.PLAYER2 );
				
				Vector2 pos = m_Sprite.Position;
				
				for( int i = 0; i < BossList.Count; ++i)
				{
					Distance.X = P2.Center.X - m_Pos.X;
					Distance.Y = P2.Center.Y - m_Pos.Y;
				}
				
				BulletAngle = FMath.Atan2( Distance.Y, Distance.X ) * 180 / FMath.PI;
				
				BulletMovement.X = FMath.Cos( FMath.Radians( BulletAngle )) * m_Speed;
				BulletMovement.Y = FMath.Sin( FMath.Radians( BulletAngle )) * m_Speed;
			
				pos += new Vector2( BulletMovement.X, BulletMovement.Y );
				
				m_Sprite.Position = pos;
			}
			
			// 画面外判定
			CheckArea( m_ReflecFlag );
		}
		
		/// <summary>
		/// 距離
		/// </summary>
		private Vector2 Distance;
		
		/// <summary>
		/// 弾の射出角度
		/// </summary>
		private float BulletAngle;
		
		/// <summary>
		/// 弾の移動量
		/// </summary>
		private Vector2 BulletMovement;
		
		private List<IEnemy> EnemyList = new List<IEnemy>( );
		
		private List<IBoss>  BossList  = new List<IBoss>( );
	}
}

