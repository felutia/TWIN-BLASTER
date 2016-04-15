/*!
 *  @file       Title.cs
 *  @brief      タイトルシーン
 *  @author     Riki Ito
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Input;

namespace game
{
	/// <summary>
	/// タイトルシーンクラス
	/// </summary>
	public class CTitle
		: IScene
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CTitle( )
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize ()
		{
			m_BGSprite   = new aqua.CSprite[m_sprite_num];
			m_LogoSprite = new aqua.CSprite[m_sprite_num * 4];
			
			// 背景
			for( int i = 0; i < m_sprite_num; ++i )
			{
				m_BGSprite[i] = CSpriteManager.Create( TEXTURE_ID.B_GROUND );
				m_BGSprite[i].Position = new Vector2( 1920.0f * (float)i, 0.0f );
			}
			
			// ロゴスプライト生成
			// スタート
			m_LogoSprite[0] = CSpriteManager.Create( TEXTURE_ID.START_WHITE_LOGO );
			m_LogoSprite[1] = CSpriteManager.Create( TEXTURE_ID.START_LOGO );
			m_LogoSprite[0].Color = CColorManager.GetColor( );
			
			// チュートリアル
			m_LogoSprite[2] = CSpriteManager.Create( TEXTURE_ID.TUTORIAL_WHITE_LOGO );
			m_LogoSprite[3] = CSpriteManager.Create( TEXTURE_ID.TUTORIAL_LOGO );
			m_LogoSprite[2].Color = CColorManager.GetColor( );
			
			// クレジット
			m_LogoSprite[4] = CSpriteManager.Create( TEXTURE_ID.CREDIT_WHITE_LOGO );
			m_LogoSprite[5] = CSpriteManager.Create( TEXTURE_ID.CREDIT_LOGO );
			for( int i = 4; i < m_sprite_num * 3; ++i )
			m_LogoSprite[4].Color = CColorManager.GetColor( );
			
			// タイトル
			m_LogoSprite[6] = CSpriteManager.Create( TEXTURE_ID.TITLE_WHITE_LOGO );
			m_LogoSprite[7] = CSpriteManager.Create( TEXTURE_ID.TITLE_LOGO );
			m_LogoSprite[6].Color = CColorManager.GetColor( );
			
			m_Pos[0] = new Vector2( 300.0f, 300.0f );
			m_Pos[1] = new Vector2( 300.0f, 380.0f );
			m_Pos[2] = new Vector2( 300.0f, 450.0f );
			m_SelectID = 0;
			
			m_LogoSprite[0].Position = m_Pos[0] + new Vector2( 108.0f, 0.0f );
			m_LogoSprite[1].Position = m_Pos[0] + new Vector2( 108.0f, 0.0f );
			m_LogoSprite[2].Position = m_Pos[1] + new Vector2( 100.0f, 3.0f );
			m_LogoSprite[3].Position = m_Pos[1] + new Vector2( 100.0f, 3.0f );
			m_LogoSprite[4].Position = m_Pos[2] + new Vector2( 118.0f, 10.0f );
			m_LogoSprite[5].Position = m_Pos[2] + new Vector2( 118.0f, 10.0f );
			
			// スプライトソート
			CSpriteManager.Sort( );
			
			// サウンド初期化
			aqua.CBgm.Load("data/sound/Title.mp3");
			
			// サウンド再生
			aqua.CBgm.Play( );
		}
		
        /// <summary>
        /// 更新
        /// </summary>
		public override void Update( )
		{
			// 色設定
			m_LogoSprite[0].Color = CColorManager.GetColor( );
			m_LogoSprite[2].Color = m_LogoSprite[0].Color;
			m_LogoSprite[4].Color = CColorManager.GetColor( );
			m_LogoSprite[6].Color = m_LogoSprite[4].Color;
			
			// スクロール
			for( int i = 0; i < m_sprite_num; ++i )
			{
				m_BGSprite[i].Position.X -= 1.0f;
				
				if( m_BGSprite[i].Position.X < -1920.0f )
					m_BGSprite[i].Position.X += 1920.0f * 2.0f;
			}
			
			// 操作
			if( aqua.CGamePad.Trigger( GamePadButtons.Up ) )
			{
				if( m_SelectID == 0 )
				{
					CSoundManager.GetInstance( ).Play( SOUND_ID.PAUSE_SELECT, 1.0f );
					
					return;
				}
				
				m_SelectID -= 1;
				CSoundManager.GetInstance( ).Play( SOUND_ID.PAUSE_SELECT, 1.0f );
				
				m_LogoSprite[(m_SelectID + 1) * 2].Scaling = new Vector2( 1.0f, 1.0f );
				m_LogoSprite[(m_SelectID + 1) * 2 + 1].Scaling = new Vector2( 1.0f, 1.0f );
				m_AddScal = 0.02f;
			}
			if( aqua.CGamePad.Trigger( GamePadButtons.Down ) )
			{
				if( m_SelectID == 2 )
				{
					CSoundManager.GetInstance( ).Play( SOUND_ID.PAUSE_SELECT, 1.0f );
					
					return;
				}
				
				m_SelectID += 1;
				CSoundManager.GetInstance( ).Play( SOUND_ID.PAUSE_SELECT, 1.0f );
				
				m_LogoSprite[(m_SelectID - 1) * 2].Scaling = new Vector2( 1.0f, 1.0f );
				m_LogoSprite[(m_SelectID - 1) * 2 + 1].Scaling = new Vector2( 1.0f, 1.0f );
				m_AddScal = 0.02f;
			}
			// 選択
			if( aqua.CGamePad.Trigger( GamePadButtons.Circle ) )
			{
				CSoundManager.GetInstance( ).Play( SOUND_ID.DECISION, 1.0f );
				
				switch( (SELECT_SCENE)m_SelectID )
				{
					case SELECT_SCENE.GAMEMAIN:
					{
						if( CSaveDataManager.GetInstance( ).SaveData.first == false )
							CSceneManager.GetInstance( ).ChangeScene( SCENE_ID.TUTORIAL );
						else
							CSceneManager.GetInstance( ).ChangeScene( SCENE_ID.GAMEMAIN );
					}
					return;
					case SELECT_SCENE.TUTORIAL:	CSceneManager.GetInstance( ).ChangeScene(SCENE_ID.TUTORIAL);	return;
					case SELECT_SCENE.CREDIT:	CSceneManager.GetInstance( ).ChangeScene(SCENE_ID.CREDIT);		return;
				}
			}
			
			// 選択中の項目を拡大縮小する　
			m_LogoSprite[m_SelectID * 2].Scaling += m_AddScal;
			m_LogoSprite[m_SelectID * 2 + 1].Scaling += m_AddScal;
			
			if( m_LogoSprite[m_SelectID * 2].Scaling.X >= 1.3f )
				m_AddScal *= -1.0f;
			if( m_LogoSprite[m_SelectID * 2].Scaling.X <= 1.0f )
				m_AddScal *= -1.0f;
		}
		
        /// <summary>
        /// 解放
        /// </summary>
		public override void Dispose( )
		{
			for( int i = 0; i < m_sprite_num; ++i )
			{
				m_BGSprite[i].Dispose( );
			}
			
			for( int i = 0; i < m_sprite_num * 4; ++i )
			{
				m_LogoSprite[i].Dispose( );
			}
			
			CSpriteManager.AllReset( );
		}
		
		/// <summary>
		/// 選択肢
		/// </summary>
		private enum SELECT_SCENE
		{
			GAMEMAIN,	// ゲームメインへ移行
			TUTORIAL,	// チュートリアル
			CREDIT,		// クレジット
		}
		
        /// <summary>
        /// スプライト数
        /// </summary>
		private const int			m_sprite_num = 2;
		
		/// <summary>
		/// 背景スプライト
		/// </summary>
		private aqua.CSprite[]		m_BGSprite;
		
		/// <summary>
		/// ロゴ用スプライト
		/// </summary>
		private aqua.CSprite[]		m_LogoSprite;
		
		/// <summary>
		/// セレクトシーンクラス
		/// </summary>
		private int					m_SelectID;
		
		/// <summary>
		/// カーソル位置
		/// </summary>
		private Vector2[]			m_Pos = new Vector2[3];
		
		/// <summary>
		/// 加算スケーリング値
		/// </summary>
		private float				m_AddScal = 0.02f;
	}
}