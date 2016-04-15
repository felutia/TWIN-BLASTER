/*!
 *  @file       stage_clear.cs
 *  @brief      ステージクリアロゴ
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
	public class CStageClearLogo
		: ILogo
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CStageClearLogo ( )
			: base()
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize( )
		{
			// クリア字幕スプライト設定
			m_Pos = new Vector2( 240.0f, 240.0f );
			m_Sprite = CSpriteManager.Create(TEXTURE_ID.STAGECLEAR);
			
			m_Sprite.Position = m_Pos;
			
			// アルファブレンドを半加算に設定する
			m_Sprite.Alphablend = aqua.ALPHABLEND_TYPE.TRANSADD;
			
			// フェード状態
			m_Fade = FADE.FADE_IN;
			
			m_FadeTimer = new aqua.CFrameTimer( 90 );
			
			// 休憩
			CGameManager.GetInstance( ).BreakFlag = true;
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public override void Update( )
		{
			switch(m_Fade)
			{
			case FADE.FADE_IN:
				{
					m_Sprite.Color.A += 0.2f;
				
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
					m_Sprite.Position.Y -= 1.0f;
				
					if( m_Sprite.Position.Y <= 230.0f )
					{
						m_Sprite.Position.Y = 230.0f;
						
						m_Fade = FADE.FADE_OUT;
					}
				}
				break;
			case FADE.FADE_OUT:
				{
					m_Sprite.Position.Y += 2.0f;
					m_Sprite.Color.A -= 0.033f;
				
					if( m_Sprite.Position.Y >= 250.0f )
					{
						m_Sprite.Position.Y = 250.0f;
						m_Sprite.Color.A = 0.0f;
						
						// 削除
						m_IsActive = false;
						
						// push Any Key
						new CPuchAnykey( ).Initialize( );
//						CLogoManager.GetInstance( ).Create( LOGO_ID.PUSH_ANY_KEY );
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

	}
}

