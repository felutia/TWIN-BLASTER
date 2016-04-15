/*!
 *  @file       pause.cs
 *  @brief      ポーズ画面
 *  @author     Riki Ito
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */

using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Input;

namespace game
{
	public class CPause
	{
		/// <summary>
		/// インスタンスの取得
		/// </summary>
		/// <returns>
		/// インスタンス
		/// </returns>
		public static CPause GetInstance( )
		{
			if( m_Instance == null )
			    m_Instance = new CPause( );
			
			return m_Instance;
		}
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CPause( )
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize( )
		{
			// 背景
			m_BackSprite = CSpriteManager.Create( TEXTURE_ID.WHITE );
			
			// 黒色設定
			m_BackSprite.Color = new Vector4( 0.0f, 0.0f, 0.0f, 0.4f );
			m_BackSprite.Position = new Vector2( 0.0f, 0.0f );
			
			// ロゴ
			m_LogoSprite = CSpriteManager.Create( TEXTURE_ID.PAUSE_LOGO );
			m_LogoSprite.Position = new Vector2( 0.0f, 0.0f );
			
			// カーソル
			m_CursorSprite = CSpriteManager.Create( TEXTURE_ID.CURSOR );
			m_Pos[0] = new Vector2( 100.0f, 275.0f );
			m_Pos[1] = new Vector2( 100.0f, 410.0f );
			m_Select = SELECT_SCENE.RETURN;
			m_CursorSprite.Position = m_Pos[(int)m_Select];
			
			m_BackSprite.Visible = false;
			m_LogoSprite.Visible = false;
			m_CursorSprite.Visible = false;
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public void Update( )
		{
			if( aqua.CGamePad.Trigger( GamePadButtons.Up ) )
			{
				m_Select = SELECT_SCENE.RETURN;
				m_CursorSprite.Position = m_Pos[(int)m_Select];
				CSoundManager.GetInstance( ).Play( SOUND_ID.PAUSE_SELECT, 1.0f );
			}
			
			if( aqua.CGamePad.Trigger( GamePadButtons.Down ) )
			{
				m_Select = SELECT_SCENE.TITLE;
				m_CursorSprite.Position = m_Pos[(int)m_Select];
				CSoundManager.GetInstance( ).Play( SOUND_ID.PAUSE_SELECT, 1.0f );
			}
			
			// 選択
			if( aqua.CGamePad.Released( GamePadButtons.Circle ) )
			{
				CSoundManager.GetInstance( ).Play( SOUND_ID.DECISION, 1.0f );
				
				switch( m_Select )
				{
				case SELECT_SCENE.RETURN:	PauseEnd( );												return;
				case SELECT_SCENE.TITLE:	CSceneManager.GetInstance( ).ChangeScene(SCENE_ID.TITLE);	return;
				}
			}
			
			// ポーズ解除
			if( aqua.CGamePad.Trigger( GamePadButtons.Cross ) || aqua.CGamePad.Trigger( GamePadButtons.Start ) )
			{
				CSoundManager.GetInstance( ).Play( SOUND_ID.DECISION, 1.0f );
				PauseEnd( );				
				return;
			}
		}
		
		/// <summary>
		/// 解放
		/// </summary>
		public void Dispose( )
		{
			m_BackSprite.Dispose( );
			m_LogoSprite.Dispose( );
			m_CursorSprite.Dispose( );
			
			PauseEnd( );
		}
		
		/// <summary>
		/// ポーズ開始
		/// </summary>
		public void PauseStart( )
		{
			aqua.CBgm.Volume = 0.1f;
			
			m_BackSprite.Visible = true;
			m_LogoSprite.Visible = true;
			m_CursorSprite.Visible = true;
			
			m_Select = SELECT_SCENE.RETURN;
			m_CursorSprite.Position = m_Pos[(int)m_Select];
			CSoundManager.GetInstance( ).Play( SOUND_ID.DECISION, 1.0f );
		}
		
		/// <summary>
		/// ポーズ終了
		/// </summary>
		public void PauseEnd( )
		{
			aqua.CBgm.Volume = 0.5f;
			
			m_BackSprite.Visible = false;
			m_LogoSprite.Visible = false;
			m_CursorSprite.Visible = false;
			
			CGameManager.GetInstance( ).PauseFlag = false;
		}
		
		/// <summary>
		/// 選択肢
		/// </summary>
		private enum SELECT_SCENE
		{
			RETURN,	// ポーズ解除
			TITLE,	// タイトルへ移行
		}
		
		/// <summary>
        /// インスタンス
        /// </summary>
        private static CPause	m_Instance;
		
		/// <summary>
		/// 背景スプライト
		/// </summary>
		private aqua.CSprite	m_BackSprite;
		
		/// <summary>
		/// ポーズロゴスプライト
		/// </summary>
		private aqua.CSprite	m_LogoSprite;
		
		/// <summary>
		/// カーソルスプライト
		/// </summary>
		private aqua.CSprite	m_CursorSprite;
		
		/// <summary>
		/// セレクトシーンクラス
		/// </summary>
		private SELECT_SCENE	m_Select;
		
		/// <summary>
		/// カーソル位置
		/// </summary>
		private Vector2[]		m_Pos = new Vector2[2];
	}
}

