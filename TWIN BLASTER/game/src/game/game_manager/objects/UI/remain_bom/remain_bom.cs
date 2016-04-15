/*!
 *  @file       remain_bom.cs
 *  @brief      ボス数UI
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
	public class CRemainBom
	{
		public CRemainBom( )
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize()
		{
			m_IconSprite = CSpriteManager.Create(TEXTURE_ID.BOM_ICON);
			
			m_IconSprite.Position = new Vector2( 10.0f, 80.0f );
			
			m_Number = new CNumber();
			
			m_Number.Initialize( new Vector2( 50.0f, 60.0f ), new Vector2( 1.0f, 1.0f ), 2, CUnit.GetInstance().Bom );
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public void Update( )
		{
			m_Number.Number = CUnit.GetInstance().Bom;
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
		/// 数字表示
		/// </summary>
		private CNumber				m_Number;
	}
}

