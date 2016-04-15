
/*!
 *  @file       Material.cs
 *  @brief      マテリアル
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
    /// マテリアルクラス
    /// </summary>
    public class CMaterial
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CMaterial( )
        {
            diffuse_color = CColor.White;
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name='diffuse_color'>
        /// ライトのディフューズカラー
        /// </param>
        public CMaterial( Vector4 diffuse_color )
        {
            this.diffuse_color = diffuse_color;
        }
        
        /// <summary>
        /// ライトのディフューズカラー
        /// </summary>
        public Vector4  diffuse_color;
    }
}
