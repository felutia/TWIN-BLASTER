/*!
 *  @file       homing_bullet.cs
 *  @brief      ホーミング弾
 *  @author     Riki Ito
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
	public class CHomingBullet
		: IPlayerBullet
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CHomingBullet ( Vector2 pos, float direction, float speed, int power )
            : base( pos, direction, speed, power )
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize()
		{
			// スプライト生成
			m_Sprite = CSpriteManager.Create( TEXTURE_ID.P_BULLET );
			
			// 色設定
			m_Sprite.Color = CColorManager.GetColor( );
			m_Sprite.Color.A = 0.9f;
			
			// アルファブレンドを半加算に設定する
			m_Sprite.Alphablend = aqua.ALPHABLEND_TYPE.TRANSADD;
			
			// サイズ設定
			m_Size = new aqua.CSize(16.0f, 16.0f);

			// 中心を設定
			m_Sprite.Center = new Vector2( 8.0f, 8.0f );
			
			// 回転値を設定(反転)
			m_Sprite.Angle = FMath.Radians( m_Direction );
			
			// 位置設定
			m_Sprite.Position = m_Pos;
			
			// 弾の半径
			m_Radius = 8.0f;
			
			BulletAngle = 0.0f;
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public override void Update ()
		{
			IObject enemy = CGameManager.GetInstance( ).FindObject( OBJECT_ID.ENEMY );
			IObject boss  = CGameManager.GetInstance( ).FindObject( OBJECT_ID.BOSS );
			
			if( enemy == null && boss == null )
			{
				Vector2 pos = m_Sprite.Position;
				
				pos.X += FMath.Cos( FMath.Radians( m_Direction ) ) * m_Speed;
				pos.Y += FMath.Sin( FMath.Radians( m_Direction ) ) * m_Speed;
				
				m_Sprite.Position = pos;
				
				// 画面外判定
				CheckArea( );
			}
			
			if( enemy != null )
			{
				EnemyList.Add( (IEnemy)enemy );
				
				Vector2 pos = m_Sprite.Position;
				
				//ePos = new Vector2[EnemyList.Count];
				
				// 弾方向
				//BulletAngle = new float[EnemyList.Count];
				
				for( int i = 0; i < EnemyList.Count; ++i)
				{
					Distance.X = ( EnemyList[i].GetPos.X - EnemyList[i].Size.width )  - m_Pos.X;
					Distance.Y = ( EnemyList[i].GetPos.Y - EnemyList[i].Size.height ) - m_Pos.Y;
				}
				
				BulletAngle = FMath.Atan2( Distance.Y, Distance.X ) * 180 / FMath.PI;
				
				BulletMovement.X = FMath.Cos( FMath.Radians( BulletAngle )) * m_Speed;
				BulletMovement.Y = FMath.Sin( FMath.Radians( BulletAngle )) * m_Speed;
		
				pos += new Vector2( BulletMovement.X, BulletMovement.Y );
				
				m_Sprite.Position = pos;
				
				EnemyList.Clear();
			}
			
			if( boss != null )
			{
				IBoss Boss = (IBoss)CGameManager.GetInstance( ).FindObject( OBJECT_ID.BOSS );
				
				BossList.Add( (IBoss)boss );
				
				Vector2 pos = m_Sprite.Position;
				
				for( int i = 0; i < BossList.Count; ++i)
				{
					Distance.X = (Boss.Pos.X + BossList[i].Size.width)  - m_Pos.X;
					Distance.Y = (Boss.Pos.Y + BossList[i].Size.height) - 64 - m_Pos.Y;
				}
				
				BulletAngle = FMath.Atan2( Distance.Y, Distance.X ) * 180 / FMath.PI;
				
				BulletMovement.X = FMath.Cos( FMath.Radians( BulletAngle )) * m_Speed;
				BulletMovement.Y = FMath.Sin( FMath.Radians( BulletAngle )) * m_Speed;
			
				pos += new Vector2( BulletMovement.X, BulletMovement.Y );
				
				m_Sprite.Position = pos;
			}
			
			// 画面外判定
			CheckArea( );
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

