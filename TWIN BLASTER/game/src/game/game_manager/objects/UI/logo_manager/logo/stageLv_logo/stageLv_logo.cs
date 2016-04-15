/*!
 *  @file       stageLv_logo.cs
 *  @brief      ステージレベルロゴ(流れタイプ)
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
	public class CStageLvLogo
		: ILogo
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CStageLvLogo ( )
			: base()
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize( )
		{
			// ステージレベル字幕スプライト設定
			m_Sprite = CSpriteManager.Create(TEXTURE_ID.LEVEL_LOGO);
			m_Pos = new Vector2( -300.0f, 250.0f );
			
			m_Sprite.Position = m_Pos;
			
			// アルファブレンドを半加算に設定する
			m_Sprite.Alphablend = aqua.ALPHABLEND_TYPE.TRANSADD;
			
			// フェード状態
			m_Fade = FADE.FADE_IN;
			
			m_FadeTimer = new aqua.CFrameTimer( 45 );
			
			m_Number = new CNumber();
			
			m_Number.Initialize( m_Pos + new Vector2( 230.0f, 10.0f ), new Vector2( 1.0f, 1.0f ), 2, CStageLv.GetInstance().StageLv );
			
			m_NPosition = m_Number.Position;
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
					m_Sprite.Position.X += 15.0f;
					m_NPosition.X += 15.0f;

					if( m_Sprite.Position.X >= 330.0f )
					{
						// SE
						CSoundManager.GetInstance( ).Play( SOUND_ID.STAGE_LVUP, 1.0f );
						
						m_Sprite.Position.X = 330.0f;
						m_NPosition.X = 560.0f;
					
						m_Fade = FADE.POP;
					}
				}
				break;
			case FADE.POP:
				{
					m_FadeTimer.Update( );
				
					if( m_FadeTimer.IsEnd( ) == true )
					{	
						m_Fade = FADE.FADE_OUT;
					}
				}
				break;
			case FADE.FADE_OUT:
				{
					m_Sprite.Position.X += 15.0f;
					m_NPosition.X += 15.0f;
				
					if( m_Sprite.Position.X >= 1000.0f )
					{
						// 削除
						m_IsActive = false;
						
						// 休憩終了
						CGameManager.GetInstance( ).BreakFlag = false;
					}
				}
				break;
			}
			
			m_Number.Position = m_NPosition;
		}
		
		/// <summary>
		/// 解放
		/// </summary>
		public override void Dispose( )
		{
			m_Sprite.Dispose( );
			
			m_Number.Active = false;
		}
	}
}

