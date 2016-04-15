/*!
 *  @file       push_Any_key.cs
 *  @brief      push_Any_key
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
	public class CPuchAnykey
		: ILogo
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CPuchAnykey()
			:base( )
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize( )
		{
			// AnyKey字幕スプライト設定
			m_Sprite = CSpriteManager.Create(TEXTURE_ID.PUSH_ANY_KEY);
			m_Pos = new Vector2( 0.0f, 0.0f );
			m_Sprite.Position = m_Pos;
			
			// アルファブレンドを半加算に設定する
			m_Sprite.Alphablend = aqua.ALPHABLEND_TYPE.TRANSADD;
			
			m_Sprite.Color.A = 0.0f;
			
			// フェード状態
			m_Fade = FADE.FADE_IN;
			
			m_FadeTimer = new aqua.CFrameTimer( 30 );
			
			// 休憩
			CGameManager.GetInstance( ).BreakFlag = true;
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public override void Update( )
		{
			switch( m_Fade )
			{
			case FADE.FADE_IN:
				{
					m_Sprite.Color.A += m_AddAlpha;
					
					if( m_Sprite.Color.A >= 1.0f )
					{
						m_Sprite.Color.A = 1.0f;
					}
					
					m_FadeTimer.Update( );
				
					if( m_FadeTimer.IsEnd( ) == true )
					{
						m_Sprite.Color.A = 1.0f;
					
						m_Fade = FADE.POP;
					}
				}
				break;
			case FADE.POP:
				{
					// 半透明にして点滅させる
					m_Sprite.Color.A -= m_AddAlpha;
					
					if( m_Sprite.Color.A <= 0.0f )
						m_AddAlpha *= -1.0f;
					
					if( m_Sprite.Color.A > 1.0f )
						m_AddAlpha *= -1.0f;
					
					// Psuh Any Key(ボムに使用しているボタンは離したとき)
					if( aqua.CGamePad.Trigger( GamePadButtons.Circle )	||
						aqua.CGamePad.Trigger( GamePadButtons.Cross )	||
						aqua.CGamePad.Trigger( GamePadButtons.Square )	||
						aqua.CGamePad.Trigger( GamePadButtons.Triangle)	||
						aqua.CGamePad.Trigger( GamePadButtons.R )		||
						aqua.CGamePad.Trigger( GamePadButtons.L )		||
						aqua.CGamePad.Trigger( GamePadButtons.Left )	||
						aqua.CGamePad.Trigger( GamePadButtons.Right )	||
						aqua.CGamePad.Trigger( GamePadButtons.Up )		||
						aqua.CGamePad.Trigger( GamePadButtons.Down ) )
					{
						m_Fade = FADE.FADE_OUT;
					
						// ステージロゴ
						new CStageLvLogo( ).Initialize( );
//						CLogoManager.GetInstance( ).Create( LOGO_ID.STAGE_LV );
					}
				}
				break;
			case FADE.FADE_OUT:
				{
					m_AddAlpha = 0.06f;
					
					m_Sprite.Color.A -= m_AddAlpha;
				
					if( m_Sprite.Color.A <= 0.0f )
					{
						m_Sprite.Color.A = 0.0f;
						
						// 削除
						m_IsActive = false;
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
			m_Sprite.Dispose( );
		}

		/// <summary>
		/// 加算アルファ値
		/// </summary>
		private float					m_AddAlpha = 0.06f;
	}
}