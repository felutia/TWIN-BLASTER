
/*!
 *  @file       SceneFactory.cs
 *  @brief      シーンファクトリー
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
    /// シーンID
    /// </summary>
    public enum eSceneID
    {
          Dummy     // ダミーID
        , Title     // タイトルシーン
        , GameMain  // ゲームメインシーン
    }
    
    /// <summary>
    /// シーンファクトリークラス
    /// </summary>
    public sealed class CSceneFactory
    {
        public static IScene Create( eSceneID scene_id )
        {
            switch( scene_id )
            {
            case eSceneID.Title:    return new CTitle( );
            case eSceneID.GameMain: return new CGameMain( );
            }
            
            return null;
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CSceneFactory( )
        {
        }
    }
}
