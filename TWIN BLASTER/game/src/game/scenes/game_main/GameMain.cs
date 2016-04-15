
/*!
 *  @file       GameMain.cs
 *  @brief      ゲームメインシーン
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
    /// ゲームメインシーンクラス
    /// </summary>
    public class CGameMain
        : IScene
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CGameMain( )
        {
        }
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize( )
        {
			// 初期化処理
			CGameManager.GetInstance( ).Initialize( );
			
			// スプライトソート
			CSpriteManager.Sort( );
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public override void Update( )
        {
			// 更新処理
			CGameManager.GetInstance( ).Update( );
        }
        
        /// <summary>
        /// 解放
        /// </summary>
        public override void Dispose( )
        {
			// 解放処理
			CGameManager.GetInstance( ).Dispose( );
			
			CSpriteManager.AllReset( );
        }
    }
}
