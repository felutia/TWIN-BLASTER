/*!
 *  @file       repel_effect.cs
 *  @brief      吸収エフェクト
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
	public class CRepelEffect
		: IEffect
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CRepelEffect ( COLOR_ID color, Vector2 pos )
            : base( color, pos )
		{
		}
		
		/// <summary>
		/// 初期化 
		/// </summary>
		public override void Initialize()
		{
			m_EffectID = EFFECT_ID.REPEL_EFFECT;
			
            // スプライト生成
            m_Sprite = CSpriteManager.Create( TEXTURE_ID.R_EFFECT );
			
            // アルファブレンドを半加算に設定する
            m_Sprite.Alphablend = aqua.ALPHABLEND_TYPE.TRANSADD;
			
			// 色設定
            if( m_Color == COLOR_ID.BLUE )
				m_Sprite.Color = new Vector4( 0.6f, 0.6f, 1.0f, 0.7f );
			else if( m_Color == COLOR_ID.RAINBOW )
            	m_Sprite.Color = CColorManager.GetColor( );
			else if( m_Color == COLOR_ID.RED )
				m_Sprite.Color = new Vector4( 1.0f, 0.3f, 0.3f, 0.7f );
			else if( m_Color == COLOR_ID.WHITE )
				m_Sprite.Color = new Vector4( 1.0f, 1.0f, 1.0f, 0.7f );
			
			m_Sprite.Color.A = 0.3f;
            
			// サイズ設定
			m_Size = new aqua.CSize( 64.0f, 64.0f );
			
			// 中心値設定
			m_Sprite.Center = new Vector2( 32.0f, 32.0f );
			
			// 位置設定
			m_Sprite.Position = m_Pos;
			
			m_AnimaFrame = 0;
			
			// アニメーションタイマー設定
			m_AnimaTimer = new aqua.CFrameTimer( 3 );
			
			// アニメーション用RECT設定
			aqua.CRect rect = new aqua.CRect( 0.0f, 0.0f, 0.0f, 0.0f );
			
			rect.left	= (m_AnimaFrame % 8 ) * 64.0f;
			rect.top	= 0;
			rect.right	= rect.left + 64.0f;
			rect.bottom	= rect.top + 64.0f;
			
			m_Sprite.Rect = rect;
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public override void Update ()
		{
			CPlayer1 player1 = (CPlayer1)CGameManager.GetInstance().FindObject(OBJECT_ID.PLAYER1);
			CPlayer2 player2 = (CPlayer2)CGameManager.GetInstance().FindObject(OBJECT_ID.PLAYER2);
			
			if( player1 != null )
			{
				if( m_Color == COLOR_ID.RED )
				{
					m_Sprite.Position = player1.Position - ( player1.Center + new Vector2( 5.0f, 0.0f ) );
				}
			}
			
			if( player2 != null )
			{
				if( m_Color == COLOR_ID.BLUE )
				{
					m_Sprite.Position = player2.Position - ( player2.Center + new Vector2( 5.0f, 0.0f ) );
				}
			}
			
			m_AnimaTimer.Update();
			
			if( m_AnimaTimer.IsEnd())
			{
				++m_AnimaFrame;
				
				// アニメーション終了
				if( m_AnimaFrame > 7 )
				{
					// オブジェクト削除
					m_IsActive = false;
				}
				
				// アニメーション用RECT設定
				aqua.CRect rect = new aqua.CRect( 0.0f, 0.0f, 0.0f, 0.0f );
				
				rect.left	= (m_AnimaFrame % 8 ) * 64.0f;
				rect.top	= 0;
				rect.right	= rect.left + 64.0f;
				rect.bottom	= rect.top + 64.0f;
				
				m_Sprite.Rect = rect;
			}
		}
	}
}

