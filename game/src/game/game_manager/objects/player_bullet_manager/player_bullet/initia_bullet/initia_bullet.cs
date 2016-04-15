/*!
 *  @file       initia.cs
 *  @brief      初期弾
 *  @author     Riki Ito
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */

using System;
using Sce.PlayStation.Core;

namespace game
{
	public class CInitiaBullet
		: IPlayerBullet
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CInitiaBullet ( Vector2 pos, float direction, float speed, int power )
            : base( pos, direction, speed, power )
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize( )
		{
            // スプライト生成
            m_Sprite = CSpriteManager.Create( TEXTURE_ID.P_BULLET );
			
			// 色設定
            m_Sprite.Color = CColorManager.GetColor( );
			m_Sprite.Color.A = 1.0f;
            
            // アルファブレンドを半加算に設定する
            m_Sprite.Alphablend = aqua.ALPHABLEND_TYPE.TRANSADD;
			
			// サイズ設定
			m_Size = new aqua.CSize( 16.0f, 16.0f );

			// 中心を設定
			m_Sprite.Center = new Vector2( 8.0f, 8.0f );
			
			// 回転値を設定(反転)
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
			Vector2 pos = m_Sprite.Position;
			
			pos.X += FMath.Cos( FMath.Radians( m_Direction ) ) * m_Speed;
			pos.Y += FMath.Sin( FMath.Radians( m_Direction ) ) * m_Speed;
			
			m_Sprite.Position = pos;
			
			// 画面外判定
			CheckArea( );
		}
	}
}

