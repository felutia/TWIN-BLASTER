/*!
 *  @file       charge_effect.cs
 *  @brief      チャージエフェクト
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
	public class CChargeEffect
		: IEffect
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CChargeEffect( COLOR_ID color, Vector2 pos )
            : base( color, pos )
		{
		}
		
		/// <summary>
		///  初期化 
		/// /// </summary>
		public override void Initialize()
		{
			m_EffectID = EFFECT_ID.CHARGE_EFFECT;
			
			// スプライト生成
			m_Sprite = CSpriteManager.Create( TEXTURE_ID.C_EFFECT );
			
			// 色設定
			if( m_Color == COLOR_ID.BLUE )
				m_Sprite.Color = new Vector4( 0.6f, 0.6f, 1.0f, 0.7f );
			else if( m_Color == COLOR_ID.RAINBOW )
				m_Sprite.Color = CColorManager.GetColor( );
			else if ( m_Color == COLOR_ID.RED )
				m_Sprite.Color = new Vector4( 1.0f, 0.3f, 0.3f, 0.7f );
			else if( m_Color == COLOR_ID.WHITE )
				m_Sprite.Color = new Vector4( 1.0f, 1.0f, 1.0f, 0.7f );
			
			// アルファブレンドを半加算に設定する
			m_Sprite.Alphablend = aqua.ALPHABLEND_TYPE.TRANSADD;
			
			// サイズ設定
			m_Size = new aqua.CSize( 48.0f, 48.0f );
			
			// 中心値設定
			m_Sprite.Center = new Vector2( 24.0f, 24.0f );
			
			// 位置設定
			m_Sprite.Position = m_Pos;
			
			m_AnimaFrame = 0;
			
			// アニメーションタイマー設定
			m_AnimaTimer = new aqua.CFrameTimer( 6 );
			
			// アニメーション用RECT設定
			aqua.CRect rect = new aqua.CRect( 0.0f, 0.0f, 0.0f, 0.0f );
			
			rect.left	= ( m_AnimaFrame % 10 ) * m_Size.width;
			rect.top	= 0;
			rect.right	= rect.left + m_Size.width;
			rect.bottom	= rect.top + m_Size.height;
			
			m_Sprite.Rect = rect;
			
			CPlayer1 player1 = (CPlayer1)CGameManager.GetInstance( ).FindObject( OBJECT_ID.PLAYER1 );
			
			if( player1 == null )
				return;
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public override void Update ()
		{
			m_AnimaTimer.Update();
			
			CPlayer2 player2 = (CPlayer2)CGameManager.GetInstance( ).FindObject( OBJECT_ID.PLAYER2 );
			CPlayer1 player1 = (CPlayer1)CGameManager.GetInstance( ).FindObject( OBJECT_ID.PLAYER1 );
			
			if( m_Color == COLOR_ID.RED )
			{
				if( player1 == null )
				{
					m_IsActive = false;
					
					return;
				}
				
				m_Sprite.Position = player1.Position + player1.Center + new Vector2( 5.0f, -23.0f );
			}
			else if( m_Color == COLOR_ID.BLUE )
			{
				if( player2 == null )
				{
					m_IsActive = false;
					
					return;
				}
				
				m_Sprite.Position = player2.Position + player2.Center + new Vector2( 5.0f, -23.0f );
			}
			
			if( m_AnimaTimer.IsEnd( ) )
			{
				++m_AnimaFrame;
				
				// アニメーション終了
				if( m_AnimaFrame > 9 )
				{
					// オブジェクト削除
					m_IsActive = false;
					
					// レーザー生成
					CEffectManager.GetInstance( ).Create( EFFECT_ID.LASER_EFFECT, OBJECT_ID.EFFECT, m_Color, m_Pos );
				}
				
				// アニメーション用RECT設定
				aqua.CRect rect = new aqua.CRect( 0.0f, 0.0f, 0.0f, 0.0f );
				
				rect.left	= ( m_AnimaFrame % 10 ) * m_Size.width;
				rect.top	= 0;
				rect.right	= rect.left + m_Size.width;
				rect.bottom	= rect.top + m_Size.height;
				
				m_Sprite.Rect = rect;
			}
		}
	}
}

