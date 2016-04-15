
/*!
 *  @file       Rect.cs
 *  @brief      矩形
 *  @author     Kazuya Maruyama
 *  @date       2014/01/02
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

namespace aqua
{
    /// <summary>
    /// 矩形クラス
    /// </summary>
    public class CRect
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CRect( )
        {
            left   = 0.0f;
            top    = 0.0f;
            right  = 0.0f;
            bottom = 0.0f;
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name='left'>
        /// 左端
        /// </param>
        /// <param name='top'>
        /// 上端
        /// </param>
        /// <param name='right'>
        /// 右端
        /// </param>
        /// <param name='bottom'>
        /// 下端
        /// </param>
        public CRect( float left, float top, float right, float bottom )
        {
            this.left   = left;
            this.top    = top;
            this.right  = right;
            this.bottom = bottom;
        }
        
        /// <summary>
        /// 左端
        /// </summary>
        public float left;
        
        /// <summary>
        /// 上端
        /// </summary>
        public float top;
        
        /// <summary>
        /// 右端
        /// </summary>
        public float right;
        
        /// <summary>
        /// 下端
        /// </summary>
        public float bottom;
        
        /// <summary>
        /// メンバ初期化用
        /// </summary>
        public static readonly CRect Zero = new CRect( );
    }
}
