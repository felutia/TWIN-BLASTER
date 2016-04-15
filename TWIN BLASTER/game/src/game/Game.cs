
/*!
 *  @file       Game.cs
 *  @brief      ゲーム管理
 *  @author     Kazuya Maruyama
 *  @date       2014/10/08
 *  @since      1.0
 *
 *  Copyright (c) 2013, Kazuya Maruyama. All rights reserved.
 */

using Sce.PlayStation.Core;
using aqua;

namespace game
{
    /// <summary>
    /// ゲームクラス
    /// </summary>
    public sealed class CGame
        : aqua.IGame
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CGame( )
        {
        }
        
        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize( )
        {
            // テクスチャ読み込み
            CTextureManager.GetInstance( ).Load( );
            
            // サウンド読み込み
            CSoundManager.GetInstance( ).Load( );
            
            // シーン初期化
            CSceneManager.GetInstance( ).Initialize( );
            
            // セーブデータ読み込み
            CSaveDataManager.GetInstance( ).Read( );
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public override void Update( )
        {
            // シーン更新
            CSceneManager.GetInstance( ).Update( );
        }
        
        /// <summary>
        /// 解放
        /// </summary>
        public override void Dispose( )
        {
            // セーブデータ書き出し
            CSaveDataManager.GetInstance( ).Write( );
            
            // シーン解放
            CSceneManager.GetInstance( ).Dispose( );
            
            // テクスチャ解放
            CTextureManager.GetInstance( ).Unload( );
            
            // サウンド解放
            CSoundManager.GetInstance( ).Unload( );
        }
    }
}
