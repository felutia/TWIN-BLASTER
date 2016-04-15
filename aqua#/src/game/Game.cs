
/*!
 *  @file       Game.cs
 *  @brief      ゲーム登録用インターフェイス
 *  @author     Kazuya Maruyama
 *  @date       2014/01/02
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

namespace aqua
{
    /// <summary>
    /// ゲームベースクラス
    /// ライブラリ使用時に登録するゲームクラス用ベースクラス
    /// 継承して使用する
    /// </summary>
    public abstract class IGame
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        protected IGame( )
        {
        }
        
        /// <summary>
        /// 初期化
        /// 純粋仮想関数
        /// </summary>
        public abstract void Initialize( );
        
        /// <summary>
        /// 更新
        /// 純粋仮想関数
        /// </summary>
        public abstract void Update( );
        
        /// <summary>
        /// 解放
        /// 純粋仮想関数
        /// </summary>
        public abstract void Dispose( );
    }
}
