
/*!
 *  @file       DrawObject2DManager.cs
 *  @brief      2D描画用オブジェクト管理
 *  @author     Kazuya Maruyama
 *  @date       2014/01/06
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using System;
using System.Collections.Generic;
using Sce.PlayStation.Core.Graphics;

namespace aqua.core
{
    /// <summary>
    /// 2D描画オブジェクト管理クラス
    /// </summary>
    public sealed class CDrawObject2DManager
    {
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CDrawObject2DManager GetInstance( )
        {
            if( m_Instance == null )
                m_Instance = new CDrawObject2DManager( );
            
            return m_Instance;
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CDrawObject2DManager( )
        {
        }
        
        /// <summary>
        /// インスタンス
        /// </summary>
        private static CDrawObject2DManager     m_Instance  = null;
    }
}
