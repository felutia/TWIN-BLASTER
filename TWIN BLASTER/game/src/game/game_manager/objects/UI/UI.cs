/*!
 *  @file       UI.cs
 *  @brief      ユーザーインターフェース管理クラス
 *  @author     Riki Ito
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */

using System;

namespace game
{
	public class CUI:IObject
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CUI( )
			: base()
		{
			m_ObjectID = OBJECT_ID.UI;
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize( )
		{
			m_UIFrameSprite = CSpriteManager.Create(TEXTURE_ID.UI_FRAME);
			
			// 自機1の経験値&レベルUI初期化処理
			m_ExpGage1 = new CExpGage1( );
			m_ExpGage1.Initialize( );
			
			// 自機2の経験値&レベルUI初期化処理
			m_ExpGage2 = new CExpGage2( );
			m_ExpGage2.Initialize( );
			
			// 残機UI初期化処理
			m_RemainLife = new CRemainLife( );
			m_RemainLife.Initialize( );
			
			// ボム数UI初期化処理
			m_RemainBom = new CRemainBom( );
			m_RemainBom.Initialize( );
			
			// ステージレベル初期化処理
			CStageLv.GetInstance( ).Initialize( );
		}
		
		/// <summary>
		/// 更新
		/// </summary>/
		public override void Update( )
		{
			// 自機1の経験値&レベルUI更新処理
			m_ExpGage1.Update( );
			
			// 自機2の経験値&レベルUI更新処理
			m_ExpGage2.Update( );
			
			// 残機UI更新処理
			m_RemainLife.Update( );
			
			// ボム数UI更新処理
			m_RemainBom.Update( );
			
			// ステージレベル更新処理
			CStageLv.GetInstance( ).Update( );
		}
		
		/// <summary>
		/// 解放
		/// </summary>
		public override void Dispose( )
		{
			m_UIFrameSprite.Dispose( );
			
			// 自機1の経験値&レベルUI解放処理
			m_ExpGage1.Dipose( );
			
			// 自機2の経験値&レベルUI解放処理
			m_ExpGage2.Dipose( );
			
			// 残機UI解放処理
			m_RemainLife.Dispose( );
			
			// ボム数UI解放処理
			m_RemainBom.Dispose( );
			
			// ステージレベル解放処理
			CStageLv.GetInstance().Dispose( );
		}
		
		/// <summary>
		/// UIフレームスプライト
		/// </summary>
		private aqua.CSprite	m_UIFrameSprite;
		
		/// <summary>
		/// 経験値&レベル1UI
		/// </summary>
		private CExpGage1		m_ExpGage1;
		
		/// <summary>
		/// 経験値&レベル2UI
		/// </summary>
		private CExpGage2		m_ExpGage2;
		
		/// <summary>
		/// ボムカウントUI
		/// </summary>
		private CRemainBom		m_RemainBom;
		
		/// <summary>
		/// 残機カウントUI
		/// </summary>
		private CRemainLife		m_RemainLife;
	}
}

