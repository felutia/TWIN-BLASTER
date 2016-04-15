/*!
 *  @file       resonat_effect.cs
 *  @brief      共鳴エフェクト
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
	public class CResonantEffect
		: IEffect
	{
		public CResonantEffect( COLOR_ID color, Vector2 pos )
            : base( color, pos )
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize ()
		{
			m_EffectID = EFFECT_ID.RESONANT_EFFECT;
			
			m_Sprite = CSpriteManager.Create( TEXTURE_ID.PLAYER_ICON );
			
			// 色設定
			if( m_Color == COLOR_ID.BLUE )
				m_Sprite.Color = new Vector4( 0.6f, 0.6f, 1.0f, 0.5f );
			else if( m_Color == COLOR_ID.RAINBOW )
            	m_Sprite.Color = CColorManager.GetColor( );
			else if ( m_Color == COLOR_ID.RED )
				m_Sprite.Color = new Vector4( 1.0f, 0.3f, 0.3f, 0.5f );
			else if( m_Color == COLOR_ID.WHITE )
				m_Sprite.Color = new Vector4( 1.0f, 1.0f, 1.0f, 0.5f );
			
			m_Sprite.Color.A = 0.0f;
			
			m_Sprite.Position = m_Pos;
			
			// サイズ設定
			m_Size = new aqua.CSize( 32.0f, 32.0f );
			
			// 向き設定
			m_Direction = 90;
			
			m_Sprite.Angle = FMath.Radians( m_Direction );
			
			m_ScalingTimer = new aqua.CFrameTimer( 10 );
		}
		
		/// <summary>
		///  更新
		/// </summary>
		public override void Update ()
		{
			CPlayer1 player1 = (CPlayer1)CGameManager.GetInstance( ).FindObject( OBJECT_ID.PLAYER1 );
			CPlayer2 player2 = (CPlayer2)CGameManager.GetInstance( ).FindObject( OBJECT_ID.PLAYER2 );
			
			if( player1 == null )
			{
				return;
			}
			
			if( player2 == null )
			{
				return;
			}
			
			if( player1.P1State == CPlayer1.STATE.DEAD )
				m_IsActive = false;
			
			if( player2.P2State == CPlayer2.STATE.DEAD )
				m_IsActive = false;
				
			if( m_Color == COLOR_ID.RED )
				m_Sprite.Position = player1.Position;
			else if( m_Color == COLOR_ID.BLUE )
				m_Sprite.Position = player2.Position;
			
			Vector2 p1v = player1.Position + player1.Center;
			Vector2 p2v = player2.Position + player2.Center;
			
			Vector2 pv = p1v - p2v;
			
			// 距離算出
			float len = pv.Length( );
			
			if( len < 64.0f )
			{
				m_Sprite.Color.A += 0.05f;
				
				m_Sprite.Scaling += m_AddScall;
				
				if( m_Sprite.Color.A >= 0.5f )
				{
					m_ScalingTimer.Update( );
				
					m_Sprite.Color.A = 0.5f;
					
					if( m_ScalingTimer.IsEnd( ) == true )
					{
						m_Sprite.Scaling = new Vector2( 1.7f, 1.5f );
						
						m_AddScall *= -1.0f;
					}
				}
			}
			else
			{
				m_Sprite.Color.A -= 0.1f;
				
				m_Sprite.Scaling -= m_AddScall * 2;
				
				if( m_Sprite.Color.A <= 0.0f )
				{
					m_Sprite.Color.A = 0.0f;
					
					m_Sprite.Scaling = new Vector2( 1.0f, 1.0f );
					
					m_IsActive = false;
				}
			}
		}
		
		/// <summary>
		/// 加算スケーリング値
		/// </summary>
		private Vector2				m_AddScall = new Vector2( 0.07f, 0.05f );
		
		/// <summary>
		/// 拡大縮小時間
		/// </summary>
		private aqua.CFrameTimer	m_ScalingTimer;
	}
}