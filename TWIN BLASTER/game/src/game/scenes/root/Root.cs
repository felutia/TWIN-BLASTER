
/*!
 *  @file       Root.cs
 *  @brief      ルートシーン
 *  @author     Kazuya Maruyama
 *  @date       2014/10/08
 *  @since      1.0
 *
 *  Copyright (c) 2013 2014, Kazuya Maruyama. All rights reserved.
 */

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Input;

namespace game
{
    /// <summary>
    /// ルートシーンクラス
    /// DEBUG時のみ起動されるシーン
    /// </summary>
    public class CRoot
		: IScene
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
		public CRoot( )
		{
#if DEBUG
			m_SceneSelectID = 0;
#endif
		}
		
        /// <summary>
        /// 更新
        /// </summary>
		public override void Update( )
		{
#if DEBUG
			if( aqua.CGamePad.Trigger( GamePadButtons.Up ) )
			{
				--m_SceneSelectID;
				
				if( m_SceneSelectID < 0 )
					m_SceneSelectID = (int)SCENE_ID.NUM - 2;
				
				CSoundManager.GetInstance( ).Play( SOUND_ID.SELECT, 1.0f );
			}
			
			if( aqua.CGamePad.Trigger( GamePadButtons.Down ) )
			{
				m_SceneSelectID = ( m_SceneSelectID + 1 ) % ( (int)SCENE_ID.NUM - 1 );
				
				CSoundManager.GetInstance( ).Play( SOUND_ID.SELECT, 1.0f );
			}
			
			if( aqua.CGamePad.Trigger( GamePadButtons.Circle ) )
			{
				CSceneManager.GetInstance( ).ChangeScene( (SCENE_ID)( m_SceneSelectID + 1 ) );
				
				CSoundManager.GetInstance( ).Play( SOUND_ID.DECISION, 1.0f );
			}
			
			aqua.CDebugFont.GetInstance( ).Add( "[SCENE SELECT]",   new Vector2( 460.0f, 0.0f ) );
			aqua.CDebugFont.GetInstance( ).Add( "ROOT",				m_item_position[0] );
			aqua.CDebugFont.GetInstance( ).Add( "GCF_LOGO",			m_item_position[1] );
			aqua.CDebugFont.GetInstance( ).Add( "OJS_LOGO",			m_item_position[2] );
			aqua.CDebugFont.GetInstance( ).Add( "TITLE",			m_item_position[3] );
			aqua.CDebugFont.GetInstance( ).Add( "TUTORIAL",			m_item_position[4] );
			aqua.CDebugFont.GetInstance( ).Add( "CREDIT",			m_item_position[5] );
			aqua.CDebugFont.GetInstance( ).Add( "GAMEMAIN",			m_item_position[6] );
			aqua.CDebugFont.GetInstance( ).Add( "RESULT",			m_item_position[7] );
			
			Vector2 pos = new Vector2( 460.0f, m_SceneSelectID * 20.0f + 20.0f );
			
			aqua.CDebugFont.GetInstance( ).Add( ">", pos );
#endif
		}
		
        /// <summary>
        /// 解放
        /// </summary>
		public override void Dispose( )
		{
		}
		
#if DEBUG
        /// <summary>
        /// The m_item_position.
        /// </summary>
		private readonly Vector2[] m_item_position = new Vector2[]
		{
			new Vector2( 480.0f,  20.0f ),
			new Vector2( 480.0f,  40.0f ),
			new Vector2( 480.0f,  60.0f ),
			new Vector2( 480.0f,  80.0f ),
			new Vector2( 480.0f, 100.0f ),
			new Vector2( 480.0f, 120.0f ),
			new Vector2( 480.0f, 140.0f ),
			new Vector2( 480.0f, 160.0f ),
		};
		
        /// <summary>
        /// シーンID
        /// </summary>
		private int m_SceneSelectID;
#endif
    }
}
