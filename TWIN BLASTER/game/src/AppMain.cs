
/*!
 *  @file       AppMain.cs
 *  @brief      テストプロジェクトエントリポイント
 *  @author     Kazuya Maruyama
 *  @date       2014/05/08
 *  @since      1.0
 *
 *  Copyright (c) 2013, Kazuya Maruyama. All rights reserved.
 */

namespace game
{
    /// <summary>
    /// アプリケーションメインクラス
    /// </summary>
    public class CAppMain
    {
        /// <summary>
        /// メインメソッド
        /// </summary>
        /// <param name='args'>
        /// コマンドライン引数
        /// </param>
        public static void Main( string[] args )
        {
            // AQUAライブラリセットアップ
            aqua.CGameFramework.Setup( new CGame( ) );
        }
    }
}
