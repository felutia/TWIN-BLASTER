/*!
 *  @file       remain_	life.cs
 *  @brief      残機数UI
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
	public class CRemainLife
	{
		public CRemainLife( )
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize()
		{
			m_IconSprite = CSpriteManager.Create(TEXTURE_ID.PLAYER_ICON);
			
			m_IconSprite.Position = new Vector2( 10.0f, 20.0f );
			
			m_IconSprite.Color = new Vector4( 1.0f, 1.0f, 1.0f, 1.0f );
			
			m_Number = new CNumber();
			
			m_Number.Initialize( new Vector2( 50.0f, 0.0f ), new Vector2( 1.0f, 1.0f ), 2, CUnit.GetInstance().Life );
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public void Update( )
		{
			m_Number.Number = CUnit.GetInstance().Life;
		}
		
		/// <summary>
		/// 解放
		/// </summary>
		public void Dispose()
		{
			m_IconSprite.Dispose();
			
			m_Number.Active = false;
		}
		
		/// <summary>
		/// プレイヤーアイコン
		/// </summary>
		private aqua.CSprite		m_IconSprite;
		
		/// <summary>
		/// 数字
		/// </summary>
		private CNumber				m_Number;
	}
}

