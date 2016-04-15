/*!
 *  @file       Lvup_Red_logo.cs
 *  @brief      自機1レベルアップロゴ
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
	public class CLvupRedLogo
		: ILogo
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CLvupRedLogo ( )
			: base()
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize( )
		{
			// レベルUp赤字幕スプライト設定
			m_Sprite = CSpriteManager.Create(TEXTURE_ID.LEVEL_UP_RED);
			m_Pos = new Vector2( 360.0f, 250.0f );
			m_Sprite.Position = m_Pos;
			
			// アルファブレンドを半加算に設定する
			m_Sprite.Alphablend = aqua.ALPHABLEND_TYPE.TRANSADD;
			
//			m_Sprite.Color = CColorManager.GetColor( );
//			m_Sprite.Color.A = 0.0f;
			
			// フェード状態
			m_Fade = FADE.FADE_IN;
			
			m_FadeTimer = new aqua.CFrameTimer( 5 );
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
					m_Sprite.Position.Y -= 2.0f;
				
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

