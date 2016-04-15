
/*!
 *  @file       SpriteManager.cs
 *  @brief      スプライト管理
 *  @author     Kazuya Maruyama
 *  @date       2014/10/08
 *  @since      1.0
 *
 *  Copyright (c) 2013 2014, Kazuya Maruyama. All rights reserved.
 */

using System;
using System.Collections.Generic;

namespace game
{
    /// <summary>
    /// スプライト管理クラス
    /// </summary>
    public sealed class CSpriteManager
    {
        /// <summary>
        /// スプライトの生成
        /// </summary>
        /// <param name='id'>
        /// テクスチャID
        /// </param>
        public static aqua.CSprite Create( TEXTURE_ID id )
        {
            CTextureManager t = CTextureManager.GetInstance( );
            
            return aqua.core.CSpriteManager.GetInstance( ).Create( t.GetTexture( id ), t.GetPriority( id ) );
        }
        
        /// <summary>
        /// スプライトソート
        /// </summary>
        public static void Sort( )
        {
            aqua.core.CSpriteManager.GetInstance( ).SortFlag = true;
        }
		
		/// <summary>
		/// 全スプライトの解放
		/// </summary>
		public static void AllReset( )
		{
//			aqua.core.CSpriteManager.GetInstance( ).SpriteReset( );
		}
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CSpriteManager( )
        {
        }
    }
}
