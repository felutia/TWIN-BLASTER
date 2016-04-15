/*!
 *  @file       dead_effect.cs
 *  @brief      死亡エフェクト
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
	public class CDeadEffect
		: IEffect
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CDeadEffect ( COLOR_ID color, Vector2 pos )
            : base( color, pos )
		{
		}
		
		/// <summary>
		///  初期化 
		/// /// </summary>
		public override void Initialize()
		{
			m_EffectID = EFFECT_ID.DEAD_EFFECT;
			
            // スプライト生成
            m_Sprite = CSpriteManager.Create( TEXTURE_ID.F_EFFECT );
			
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
			m_Size = new aqua.CSize( 120.0f, 120.0f );
			
			// 中心値設定
			m_Sprite.Center = new Vector2( 60.0f, 60.0f );
			
			// 位置設定
			m_Sprite.Position = m_Pos - m_Sprite.Center;
			
			m_AnimaFrame = 0;
			
			// アニメーションタイマー設定
			m_AnimaTimer = new aqua.CFrameTimer( 2 );
			
			// アニメーション用RECT設定
			aqua.CRect rect = new aqua.CRect( 0.0f, 0.0f, 0.0f, 0.0f );
			
			rect.left	= ( m_AnimaFrame % 12 ) * 120.0f;
			rect.top	= 0;
			rect.right	= rect.left + 120.0f;
			rect.bottom	= rect.top + 120.0f;
			
			m_Sprite.Rect = rect;
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public override void Update ()
		{
			m_AnimaTimer.Update();
			
			if( m_AnimaTimer.IsEnd( ))
			{
				++m_AnimaFrame;
				
				// アニメーション終了
				if( m_AnimaFrame > 11 )
				{
					// オブジェクト削除
					m_IsActive = false;
				}
				
				// アニメーション用RECT設定
				aqua.CRect rect = new aqua.CRect( 0.0f, 0.0f, 0.0f, 0.0f );
				
				rect.left	= ( m_AnimaFrame % 12 ) * 120.0f;
				rect.top	= 0;
				rect.right	= rect.left + 120.0f;
				rect.bottom	= rect.top + 120.0f;
				
				m_Sprite.Rect = rect;
			}
		}
	}
}

