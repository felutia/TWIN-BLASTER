
/*!
 *  @file       Size.cs
 *  @brief      サイズ
 *  @author     Kazuya Maruyama
 *  @date       2013-2014/05/11
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

namespace aqua
{
    /// <summary>
    /// サイズクラス
    /// </summary>
    public class CSize
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CSize( )
        {
            width   = 0.0f;
            height  = 0.0f;
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name='width'>
        /// 横幅
        /// </param>
        /// <param name='height'>
        /// 縦幅
        /// </param>
        public CSize( float width, float height )
        {
            this.width  = width;
            this.height = height;
        }
        
        /// <summary>
        /// 横幅
        /// </summary>
        public float width;
        
        /// <summary>
        /// 縦幅
        /// </summary>
        public float height;
    }
}

