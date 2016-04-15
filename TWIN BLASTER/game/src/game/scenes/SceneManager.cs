
/*!
 *  @file       SceneManager.cs
 *  @brief      シーンマネージャー
 *  @author     Kazuya Maruyama
 *  @date       2014/10/08
 *  @since      1.0
 *
 *  Copyright (c) 2013 2014, Kazuya Maruyama. All rights reserved.
 */

using Sce.PlayStation.Core;

namespace game
{
    /// <summary>
    /// シーンマネージャクラス
    /// </summary>
    public sealed class CSceneManager
    {
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CSceneManager GetInstance( )
        {
            if( m_Instance == null )
                m_Instance = new CSceneManager( );
            
            return m_Instance;
        }
        
        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize( )
        {
            // ダミーIDで初期化
            m_SceneID     = SCENE_ID.DUMMY;
#if MASTER
            m_NextSceneID = SCENE_ID.ROOT;
#else
            m_NextSceneID = SCENE_ID.GCF_LOGO;
#endif
            // はじめのシーン生成
            Change( );
            
            // スプライト生成
            m_Sprite = CSpriteManager.Create( TEXTURE_ID.WHITE );
            
            // プライオリティ設定
            m_Sprite.Priority = CTextureManager.GetInstance( ).GetPriority( TEXTURE_ID.WHITE );
            
            // 白色設定
            m_Sprite.Color = new Vector4( 1.0f, 1.0f, 1.0f, 1.0f );
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public void Update( )
        {
            switch( m_State )
            {
            case STATE.FADEIN:  FadeIn( );          break;
            case STATE.UPDATE:  SceneUpdate( );     break;
            case STATE.FADEOUT: FadeOut( );         break;
            case STATE.CHANGE:  Change( );          break;
            }
        }
        
        /// <summary>
        /// 解放
        /// </remarks>
        public void Dispose( )
        {
            // スプライト解放
            m_Sprite.Dispose( );
        }
        
        /// <summary>
        /// シーン変更
        /// </summary>
        /// <param name='id'>
        /// シーンID
        /// </param>
        public void ChangeScene( SCENE_ID id )
        {
            m_NextSceneID = id;
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CSceneManager( )
        {
        }
        
        /// <summary>
        /// フェードイン
        /// </summary>
        private void FadeIn( )
        {
            m_Sprite.Color.A -= m_fade_speed;
            
            if( m_Sprite.Color.A <= 0.0f )
            {
                m_Sprite.Color.A = 0.0f;
                
                m_State = STATE.UPDATE;
            }
        }
        
        /// <summary>
        /// シーン更新
        /// </summary>
        private void SceneUpdate( )
        {
            // シーン更新
            if( m_Scene != null ) m_Scene.Update( );
            
            // シーン変更がされた
            if( m_SceneID != m_NextSceneID )
                m_State = STATE.FADEOUT;
        }
        
        /// <summary>
        /// フェードアウト
        /// </summary>
        private void FadeOut( )
        {
            m_Sprite.Color.A += m_fade_speed;
            
            if( m_Sprite.Color.A >= 1.0f )
            {
                m_Sprite.Color.A = 1.0f;
                
                m_State = STATE.CHANGE;
            }
        }
        
        /// <summary>
        /// シーンを切り替える
        /// </summary>
		private void Change( )
		{
			// シーン解放
			if( m_Scene != null ) m_Scene.Dispose( );
			
			// シーンIDを切り替える
			m_SceneID = m_NextSceneID;
			
			// シーンを切り替える
			switch( m_SceneID )
			{
			case SCENE_ID.ROOT:         m_Scene = new CRoot( );     break;
			case SCENE_ID.GCF_LOGO:		m_Scene = new CgcfLogo( );	break;
			case SCENE_ID.OJS_LOGO:		m_Scene = new CojsLogo( );	break;
			case SCENE_ID.TITLE:
			{
				m_Scene = new CTitle( );
			
				// 黒色設定
				m_Sprite.Color = new Vector4( 0.0f, 0.0f, 0.0f, 1.0f );
			}
				break;
			case SCENE_ID.TUTORIAL:		m_Scene = new CTutorial( );	break;
			case SCENE_ID.CREDIT:		m_Scene = new CCredit( );	break;
			case SCENE_ID.GAMEMAIN:     m_Scene = new CGameMain( ); break;
			case SCENE_ID.RESULT:		m_Scene = new CResult();	break;
			}
			
			// シーン初期化
			m_Scene.Initialize( );
			
			// シーン更新
			m_Scene.Update( );
			
			// フェードイン
			m_State = STATE.FADEIN;
		}
        
        /// <summary>
        /// シーンマネージャの状態ID
        /// </summary>
        private enum STATE
        {
			FADEIN,     // フェードイン
			UPDATE,     // シーン更新
			FADEOUT,    // フェードアウト
			CHANGE      // シーンチェンジ
        }
        
        /// <summary>
        /// フェードスピード
        /// </summary>
        private const float             m_fade_speed = 0.0666f;
        
        /// <summary>
        /// インスタンス
        /// </summary>
        private static CSceneManager    m_Instance;
        
        /// <summary>
        /// 現在のシーン
        /// </summary>
        private IScene                  m_Scene;
        
        /// <summary>
        /// 現在のシーンID
        /// </summary>
        private SCENE_ID                m_SceneID;
        
        /// <summary>
        /// 次回のシーンID
        /// </summary>
        private SCENE_ID                m_NextSceneID;
        
        /// <summary>
        /// シーンの状態ID
        /// </summary>
        private STATE                   m_State;
        
        /// <summary>
        /// フェード用スプライト
        /// </summary>
        private aqua.CSprite            m_Sprite;
    }
}
