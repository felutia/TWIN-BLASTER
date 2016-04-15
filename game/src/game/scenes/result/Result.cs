/*!
 *  @file       Result.cs
 *  @brief      リザルトシーン
 *  @author     Riki Ito
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Input;
using System.Collections.Generic;

namespace game
{
    /// <summary>
    /// リザルトシーンクラス
    /// </summary>
    public class CResult
		: IScene
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CResult( )
        {
        }
		
        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize( )
        {
            m_BGSprite   = new aqua.CSprite[m_sprite_num];
            
			// ロゴスプライト生成
			m_LogoSprite = CSpriteManager.Create( TEXTURE_ID.RESULT_IN );
			
			if( CStageLv.GetInstance( ).StageLv >= 0 && CStageLv.GetInstance( ).StageLv <= 5 )
			{
				m_AppSprite = CSpriteManager.Create( TEXTURE_ID.GOOD_IN );
			}
			else if( CStageLv.GetInstance( ).StageLv > 5 && CStageLv.GetInstance( ).StageLv <= 9 )
			{
				m_AppSprite = CSpriteManager.Create( TEXTURE_ID.GREAT_IN );
			}
			else if( CStageLv.GetInstance( ).StageLv >= 10 )
			{
				m_AppSprite = CSpriteManager.Create( TEXTURE_ID.PARFECT_IN );
			}
			
			for( int i = 0; i < m_sprite_num; i++ )
			{
				// 背景
				m_BGSprite[i] = CSpriteManager.Create( TEXTURE_ID.B_GROUND );
				
				// 位置設定
				m_BGSprite[i].Position = new Vector2( 1920.0f * (float)i, 0.0f );
			}
			
			m_LogoSprite.Position = new Vector2( 0.0f, 0.0f );
			m_AppSprite.Position = new Vector2( 300.0f, 400.0f );
			
			m_EffectTimer = new aqua.CFrameTimer[2];
			
			// 秒に設定
			m_EffectTimer[0] = new aqua.CFrameTimer( 20 );
			m_EffectTimer[1] = new aqua.CFrameTimer( 30 );
			
			m_Number = new CNumberResult( );
			m_Number.Initialize( new Vector2( 600.0f, 280.0f ), new Vector2( 2.0f, 2.0f ), 2, CStageLv.GetInstance().StageLv );
			
			m_EffectList = new List<CFFEffect>( );
			
			// サウンド初期化
			aqua.CBgm.Load( "data/sound/Result.mp3" );
			
			// スプライトソート
			CSpriteManager.Sort( );
			// サウンド再生
			aqua.CBgm.Play();
			aqua.CBgm.Volume = 0.5f;
        }
		
        /// <summary>
        /// 更新
        /// </summary>
        public override void Update( )
        {
			
            for( int i = 0; i < m_sprite_num; ++i )
            {
                m_BGSprite[i].Position.X -= 1.0f;
                
                if( m_BGSprite[i].Position.X < -1920.0f )
                    m_BGSprite[i].Position.X += 1920.0f * 2.0f;
            }
			
			// Psuh Any Key
			if( aqua.CGamePad.Trigger( GamePadButtons.Circle )	|| 
				aqua.CGamePad.Trigger( GamePadButtons.Cross )	|| 
				aqua.CGamePad.Trigger( GamePadButtons.Square )	||
				aqua.CGamePad.Trigger( GamePadButtons.Triangle)	||
				aqua.CGamePad.Trigger( GamePadButtons.Left )	||
				aqua.CGamePad.Trigger( GamePadButtons.Right )	||
				aqua.CGamePad.Trigger( GamePadButtons.Up )		||
				aqua.CGamePad.Trigger( GamePadButtons.Down )	||
				aqua.CGamePad.Trigger( GamePadButtons.Start ) )
			{
				CSceneManager.GetInstance( ).ChangeScene( SCENE_ID.TITLE );
				
				CSoundManager.GetInstance( ).Play( SOUND_ID.DECISION, 1.0f );
				
				// サウンド停止
				aqua.CBgm.Stop();
				
				return;
			}
			
			// エフェクトによる演出処理
			m_EffectTimer[0].Update( );
			m_EffectTimer[1].Update( );
			
			if( m_EffectTimer[0].IsEnd( ) == true || m_EffectTimer[1].IsEnd( ) == true )
			{
				CFFEffect e = new CFFEffect( );
				
				e.Initialize( );
				
				m_EffectList.Add( e );
			}
			
			if( m_EffectList.Count> 0 )
			{
				for( int i = 0; i < m_EffectList.Count;++i )
				{
					m_EffectList[i].Update( );
					
					if( m_EffectList[i].Active == false )
					{
						m_EffectList[i].Dispose( );
						
						m_EffectList.Remove( m_EffectList[i--] );
					}
				}
			}
			
			m_Number.Update( );
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
			
			m_LogoSprite.Dispose( );
			m_AppSprite.Dispose( );
			m_Number.Dispose( );
			
			for( int i = 0; i < m_EffectList.Count; ++i )
			{
				m_EffectList[i].Dispose( );
			}
			
			m_EffectList.Clear( );
			
			//CSpriteManager.AllReset( );
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
		private aqua.CSprite		m_LogoSprite;
		
		/// <summary>
		/// 評価スプライト
		/// </summary>
		private aqua.CSprite		m_AppSprite;
		
		/// <summary>
		/// エフェクト生成タイマー
		/// </summary>
		private aqua.CFrameTimer[]	m_EffectTimer;
		
		/// <summary>
		/// 数字表示
		/// </summary>
		private CNumberResult		m_Number;
		
		/// <summary>
		/// 花火エフェクト
		/// </summary>
		private List<CFFEffect>		m_EffectList;
	}
}
