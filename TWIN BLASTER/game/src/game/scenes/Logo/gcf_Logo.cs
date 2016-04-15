/*!
 *  @file       gcf_Logo.cs
 *  @brief      gcfロゴシーン
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
	public class CgcfLogo
        : IScene
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CgcfLogo( )
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize( )
		{
			// ロゴスプライト設定
			m_Sprite = CSpriteManager.Create(TEXTURE_ID.GCF_LOGO);
			m_Pos = new Vector2( 0.0f, 0.0f );
			m_Sprite.Position = m_Pos;
			
			m_FadeTimer = new aqua.CFrameTimer( 90 );
			
			// スプライトソート
			CSpriteManager.Sort( );
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public override void Update( )
		{
			m_FadeTimer.Update( );
			
			if( m_FadeTimer.IsEnd( ) == true )
			{
				CSceneManager.GetInstance( ).ChangeScene( SCENE_ID.OJS_LOGO );
			}
		}

		/// <summary>
		/// 解放
		/// </summary>
		public override void Dispose( )
		{
			m_Sprite.Dispose( );
		}
		
		/// <summary>
		/// 字幕スプライト
		/// </summary>
		private aqua.CSprite		m_Sprite;
		
		/// <summary>
		/// フェードインフェードアウトポップ時間用タイマー
		/// </summary>
		private aqua.CFrameTimer	m_FadeTimer;
		
		/// <summary>
		/// 位置
		/// </summary>
		private Vector2				m_Pos;
	}
}

