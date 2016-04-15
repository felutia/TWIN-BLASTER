/*!
 *  @file       FlowerFireEffect.cs
 *  @brief      花火エフェクト
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
	public class CFFEffect
	{
		public CFFEffect( )
		{
		}
		
		/// <summary>
		///  初期化 
		/// /// </summary>
		public void Initialize(  )
		{
			// スプライト生成
			m_Sprite = CSpriteManager.Create( TEXTURE_ID.F_EFFECT );
			
			// 色設定
			m_Sprite.Color = CColorManager.GetColor( );
			
			// アルファブレンドを半加算に設定する
			m_Sprite.Alphablend = aqua.ALPHABLEND_TYPE.TRANSADD;
			
			// サイズ設定
			m_Size = new aqua.CSize( 120.0f, 120.0f );
			
			// 中心値設定
			m_Sprite.Center = new Vector2( 60.0f, 60.0f );
			
			// X座標とY座標を設定
			int x = aqua.CRandom.GetInstance( ).GetValue( 120, 840 );
			int y = aqua.CRandom.GetInstance( ).GetValue( 120, 424 );
		
			// 位置設定
			m_Sprite.Position = new Vector2( x, y );
			
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
			
			m_IsActive = true;
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public void Update ()
		{
			m_AnimaTimer.Update( );
			
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
			
			CColorManager.GetColor( );
			
			m_Sprite.Scaling += new Vector2( 0.01f, 0.01f );
		}
		
		/// <summary>
		/// 解放
		/// </summary>
		public void Dispose( )
		{
			m_Sprite.Dispose( );
		}
		
		public bool Active
		{
			get { return m_IsActive; }
			set { m_IsActive = value; }
		}
		
		/// <summary>
        /// スプライトクラス
        /// </summary>
        private aqua.CSprite		m_Sprite;
        
        /// <summary>
        /// エフェクトのサイズ
        /// </summary>
        private aqua.CSize    		m_Size;
	
		/// <summary>
		/// アニメーションタイマー
		/// </summary>
		private aqua.CFrameTimer	m_AnimaTimer;
		
		/// <summary>
		/// アニメーションフレーム数
		/// </summary>
		private int					m_AnimaFrame;
		
		/// <summary>
		/// 位置
		/// </summary>
		//private Vector2			m_Pos;
		
		private bool				m_IsActive;
	}
}

