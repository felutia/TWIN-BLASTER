/*!
 *  @file       exp_gage1.cs
 *  @brief      自機1経験値ゲージ
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
	public class CExpGage1
	{
		public CExpGage1( )
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize()
		{
			// スプライト設定
			m_GFrameSpirte = CSpriteManager.Create(TEXTURE_ID.P1_GAGE_FARME);
			m_GageSpirte   = CSpriteManager.Create(TEXTURE_ID.P1_GAGE);
			m_MFrameSpirte = CSpriteManager.Create(TEXTURE_ID.P1_LV_FRAME);
			m_MemorySpirte = CSpriteManager.Create(TEXTURE_ID.P1_LV);
			m_MaxSpirte = CSpriteManager.Create(TEXTURE_ID.MAX_LOGO);
			m_MaxSpirte.Color.A = 0.0f;
			
			// レクト設定
			aqua.CRect rectG = new aqua.CRect( 0.0f, 0.0f, 360.0f, 36.0f );
			aqua.CRect rectM = new aqua.CRect( 0.0f, 0.0f,  89.0f, 36.0f );
			
			rectG.left   = 0.0f;
			rectG.top    = 0.0f;
			rectG.right  = 360.0f;
			rectG.bottom = 36.0f;
			
			rectM.left   = 0.0f;
			rectM.top    = 0.0f;
			rectM.right  = 89.0f;
			rectM.bottom = 36.0f;
			
			// レクト設定
			m_GageSpirte.Rect = rectG;
			m_MemorySpirte.Rect = rectM;
			
			Vector2 Gpos = new Vector2( 160.0f, 20.0f );
			Vector2 Mpos = new Vector2( 530.0f, 20.0f );
			
			// 位置設定
			m_GFrameSpirte.Position = Gpos;
			m_GageSpirte.Position   = Gpos;
			m_MFrameSpirte.Position = Mpos;
			m_MemorySpirte.Position = Mpos;
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public void Update()
		{
			CPlayer1 player1 = (CPlayer1)CGameManager.GetInstance().FindObject(OBJECT_ID.PLAYER1);
			
			if( player1 != null )
			{
				// 自機1経験値設定
				int p1Exp = player1.Player1Exp;
				int p1NeedExp = player1.NeedExp;
				aqua.CRect rectG = new aqua.CRect( 0.0f, 0.0f, (((float)p1Exp / (float)p1NeedExp) * 360), 36.0f );
			
				// 自機1レベル設定
				int p1Lv = player1.Player1Lv;
				aqua.CRect rectM = new aqua.CRect( 0.0f, 0.0f, (float)(p1Lv * 9), 36.0f );
					
				if( p1Lv >= 10 )
				{
					rectG = new aqua.CRect( 0.0f, 0.0f, 360.0f, 36.0f );
					
					p1Lv = 10;
					
					CreateMAX( );
				}
				
				m_GageSpirte.Rect = rectG;
				m_MemorySpirte.Rect = rectM;
			}
			else 
			{
				aqua.CRect rectG = new aqua.CRect( 0.0f, 0.0f, 360.0f, 36.0f );
				aqua.CRect rectM = new aqua.CRect( 0.0f, 0.0f, 89.0f, 36.0f );
				
				m_GageSpirte.Rect = rectG;
				m_MemorySpirte.Rect = rectM;
			}
		}
		
		/// <summary>
		/// MAX字幕生成
		/// </summary>
		public void CreateMAX()
		{
			m_MaxSpirte.Color.A = 1.0f;
			m_MaxSpirte.Position = new Vector2( 528.0f, 18.0f );
			
			// カラー設定
			m_MaxSpirte.Color = CColorManager.GetColor( );
			
			// アルファブレンドを半加算に設定する
			m_MaxSpirte.Alphablend = aqua.ALPHABLEND_TYPE.TRANSADD;
		}
		
		/// <summary>
		/// 解放
		/// </summary>
		public void Dipose()
		{
			m_GFrameSpirte.Dispose( );
			m_GageSpirte.Dispose( );
			m_MFrameSpirte.Dispose( );
			m_MemorySpirte.Dispose( );
			m_MaxSpirte.Dispose( );
		}
		
		/// <summary>
		/// 経験値ゲージ
		/// </summary>
		private aqua.CSprite		m_GageSpirte;
		
		/// <summary>
		/// 経験値フレーム
		/// </summary>
		private aqua.CSprite		m_GFrameSpirte;
		
		/// <summary>
		/// 自機レベルメモリ
		/// </summary>
		private aqua.CSprite		m_MemorySpirte;
		
		/// <summary>
		/// 自機レベルフレーム
		/// </summary>
		private aqua.CSprite		m_MFrameSpirte;
		
		/// <summary>
		/// MAX字幕
		/// </summary>
		private aqua.CSprite		m_MaxSpirte;
	}
}
