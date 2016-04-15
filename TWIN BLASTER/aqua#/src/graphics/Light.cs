
/*!
 *  @file       Light.cs
 *  @brief      ライト
 *  @author     Kazuya Maruyama
 *  @date       2014/01/10
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using Sce.PlayStation.Core;

namespace aqua
{
    /// <summary>
    /// ライトクラス
    /// </summary>
    public class CLight
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CLight( )
        {
            direction       = new Vector3( 0.0f, -1.0f, 0.0f );
            diffuse_color   = CColor.White;
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name='Direction'>
        /// ライトの向き
        /// </param>
        /// <param name='diffuse_color'>
        /// ライトのディフューズカラー
        /// </param>
        /// </param>
        public CLight( Vector3 direction, Vector4 diffuse_color )
        {
            this.direction      = direction;
            this.diffuse_color  = diffuse_color;
        }
        
        /// <summary>
        /// ライトの向き
        /// </summary>
        public Vector3      direction;
        
        /// <summary>
        /// ライトのディフューズカラー
        /// アルファ値は適用されてないので注意
        /// </summary>
        public Vector4      diffuse_color;
    }
}
