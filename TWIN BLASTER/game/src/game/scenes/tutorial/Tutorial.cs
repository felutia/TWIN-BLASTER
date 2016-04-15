
/*!
 *  @file       tutorial.cs
 *  @brief      チュートリアルシーン
 *  @author     Riki Ito
 *  @date       2015/03/10
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Input;

namespace game
{
    /// <summary>
    /// チュートリアルシーンクラス
    /// </summary>
    public class CTutorial
        : IScene
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CTutorial( )
        {
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize ()
		{
			m_BGSprite  = new aqua.CSprite[m_sprite_num];
			m_TexSprite = new aqua.CSprite[m_sprite_num * 3];
			m_LRSprite = new aqua.CSprite[m_sprite_num];
			
			// 背景
			for( int i = 0; i < m_sprite_num; ++i )
			{
				m_BGSprite[i] = CSpriteManager.Create( TEXTURE_ID.B_GROUND );
				m_BGSprite[i].Position = new Vector2( 1920.0f * (float)i, 0.0f );
			}
			
			m_TexSprite[0] = CSpriteManager.Create( TEXTURE_ID.DESCRIPTION1 );
			m_TexSprite[1] = CSpriteManager.Create( TEXTURE_ID.DESCRIPTION2 );
			m_TexSprite[2] = CSpriteManager.Create( TEXTURE_ID.DESCRIPTION3 );
			m_TexSprite[3] = CSpriteManager.Create( TEXTURE_ID.DESCRIPTION4 );
			m_TexSprite[4] = CSpriteManager.Create( TEXTURE_ID.DESCRIPTION5 );
			m_TexSprite[5] = CSpriteManager.Create( TEXTURE_ID.DESCRIPTION6 );
			
			m_LRSprite[0] = CSpriteManager.Create( TEXTURE_ID.LEFT_BUTTON );
			m_LRSprite[1] = CSpriteManager.Create( TEXTURE_ID.RIGHT_BUTTON );
			
			// 配置
			for( int i = 0; i < m_sprite_num * 3; i ++ )
			{
				m_TexSprite[i].Position = new Vector2( 60.0f + i * 8.0f, 100.0f - i * 8.0f );
			}
			
			m_LRSprite[0].Position = new Vector2( 0.0f, 0.0f );
			m_LRSprite[1].Position = new Vector2( 860.0f, 0.0f );
			
			m_State = STATE.WAIT;
			
			// スプライトソート
			CSpriteManager.Sort();
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public override void Update( )
		{
			// スクロール
			for( int i = 0; i < m_sprite_num; ++i )
			{
				m_BGSprite[i].Position.X -= 1.0f;
				
				if( m_BGSprite[i].Position.X < -1920.0f )
					m_BGSprite[i].Position.X += 1920.0f * 2.0f;
			}
			
			// Lキーを押したら前の画像へ
			if( aqua.CGamePad.Trigger( GamePadButtons.L ) )
			{
				if( m_State == STATE.LEFT || m_State == STATE.RIGHT )
					return;
				
				m_LRSprite[0].Scaling = new Vector2( 0.8f, 0.8f );
				
				m_State = STATE.LEFT;
				
				CSoundManager.GetInstance( ).Play( SOUND_ID.SELECT, 1.0f );
			}
			else if( aqua.CGamePad.Released( GamePadButtons.L ) )
			{
				m_LRSprite[0].Scaling = new Vector2( 1.0f, 1.0f );
			}
			
			// Rキーを押したら次の画像へ
			if( aqua.CGamePad.Trigger( GamePadButtons.R ) )
			{
				if( m_State == STATE.LEFT || m_State == STATE.RIGHT )
					return;
				
				m_LRSprite[1].Scaling = new Vector2( 0.8f, 0.8f );
				
				m_State = STATE.RIGHT;
				
				CSoundManager.GetInstance( ).Play( SOUND_ID.SELECT, 1.0f );
			}
			else if( aqua.CGamePad.Released( GamePadButtons.R ) )
			{
				m_LRSprite[1].Scaling = new Vector2( 1.0f, 1.0f );
			}
			
			
			// 状態遷移
			switch( m_State )
			{
			case STATE.WAIT:
				{
					m_State = STATE.WAIT;
				}
				break;
			case STATE.LEFT:
				{
					if( m_SelectTexNum == 0 )
					{
						m_State = STATE.WAIT;
						
						return;
					}
					
					// 前へ
					m_TexSprite[m_SelectTexNum - 1].Position.X += 25.0f;
					
					if( m_TexSprite[m_SelectTexNum - 1].Position.X >= 60.0f + ( m_SelectTexNum - 1 ) * 8.0f )
					{
						m_TexSprite[m_SelectTexNum - 1].Position.X = 60.0f + ( m_SelectTexNum - 1 ) * 8.0f;
						
						--m_SelectTexNum;
						
						m_State = STATE.WAIT;
					}
				}
				break;
			case STATE.RIGHT:
				{
					// 次へ
					m_TexSprite[m_SelectTexNum].Position.X -= 25.0f;
					
					if( m_TexSprite[m_SelectTexNum].Position.X < -960.0f )
					{
						m_TexSprite[m_SelectTexNum].Position.X = -960.0f;
						
						++m_SelectTexNum;
						
						
					if( m_SelectTexNum > 5 )
					{
						CSaveDataManager.GetInstance( ).SaveData.first = true;
						CSaveDataManager.GetInstance( ).Write( );
						
						CSceneManager.GetInstance( ).ChangeScene( SCENE_ID.GAMEMAIN );
					}
						else
							m_State = STATE.WAIT;
					}
				}
				break;
			}
		}
		
		/// <summary>
		/// 解放
		/// </summary>
		public override void Dispose( )
		{
			for( int i = 0; i < m_sprite_num; ++i )
			{
				m_BGSprite[i].Dispose( );
				m_LRSprite[i].Dispose( );
			}
			
			for( int i = 0; i < m_sprite_num * 3; ++i )
			{
				m_TexSprite[i].Dispose( );
			}
		}
		
		/// <summary>
		/// 画像の状態
		/// </summary>
		enum STATE
		{
			WAIT,	// 待機
			LEFT,	// 左へ移行
			RIGHT,	// 右へ移行
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
		/// 説明画像
		/// </summary>
		private aqua.CSprite[]		m_TexSprite;
		
		/// <summary>
		/// LRボタン
		/// </summary>
		private aqua.CSprite[]		m_LRSprite;
		
		/// <summary>
		/// 表示中の画像番号
		/// </summary>
		private int					m_SelectTexNum = 0;
		
		/// <summary>
		/// 状態
		/// </summary>
		private STATE				m_State;
	}
}