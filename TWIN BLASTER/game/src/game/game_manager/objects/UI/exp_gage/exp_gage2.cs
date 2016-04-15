/*!
 *  @file       exp_gage2.cs
 *  @brief      自機2経験値ゲージ
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
	public class CExpGage2
	{
		public CExpGage2( )
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize()
		{
			// スプライト設定
			m_GFrameSpirte = CSpriteManager.Create(TEXTURE_ID.P2_GAGE_FARME);
			m_GageSpirte   = CSpriteManager.Create(TEXTURE_ID.P2_GAGE);
			m_MFrameSpirte = CSpriteManager.Create(TEXTURE_ID.P2_LV_FRAME);
			m_MemorySpirte = CSpriteManager.Create(TEXTURE_ID.P2_LV);
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
			
			Vector2 Gpos = new Vector2( 160.0f, 70.0f );
			Vector2 Mpos = new Vector2( 530.0f, 70.0f );
			
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
			CPlayer2 player2 = (CPlayer2)CGameManager.GetInstance().FindObject(OBJECT_ID.PLAYER2);
			
			if( player2 != null )
			{
				// 自機2経験値設定
				int p2Exp = player2.Player2Exp;
				int p2NeedExp = player2.NeedExp;
				aqua.CRect rectG = new aqua.CRect( 0.0f, 0.0f, (((float)p2Exp / (float)p2NeedExp) * 360), 36.0f );
				
				// 自機2レベル設定
				int p2Lv = player2.Player2Lv;
				aqua.CRect rectM = new aqua.CRect( 0.0f, 0.0f, (float)(p2Lv * 9), 36.0f );
				
				if( p2Lv >= 10 )
				{
					rectG = new aqua.CRect( 0.0f, 0.0f, 360.0f, 36.0f );
					
					p2Lv = 10;
					
					CreateMAX( );
				}
				
				m_GageSpirte.Rect = rectG;
				m_MemorySpirte.Rect = rectM;
			}
			else
			{
				aqua.CRect rectG = new aqua.CRect( 0.0f, 0.0f, 360.0f, 36.0f );
				aqua.CRect rectM = new aqua.CRect( 0.0f, 0.0f,  89.0f, 36.0f );
				
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
			m_MaxSpirte.Position = new Vector2( 528.0f, 68.0f );
			
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
