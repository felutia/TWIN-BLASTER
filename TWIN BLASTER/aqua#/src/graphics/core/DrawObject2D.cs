
/*!
 *  @file       DrawObject2D.cs
 *  @brief      2D描画用オブジェクト
 *  @author     Kazuya Maruyama
 *  @date       2014/01/02
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using Sce.PlayStation.Core;

namespace aqua.core
{
    /// <summary>
    /// 2D描画オブジェクトベースクラス
    /// </summary>
    public abstract class IDrawObject2D
        : IDrawObject
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IDrawObject2D( )
            : base( )
        {
            Position    = Vector2.Zero;
            Color       = Vector4.One;
            Scaling     = Vector2.One;
            Center      = Vector2.Zero;
            Angle       = 0.0f;
        }
        
        /// <summary>
        /// トランスフォーム行列更新
        /// </summary>
        protected void UpdateTransform( )
        {
            // 位置
            var trans_matrix  = Matrix4.Translation( Position + Center );
            
            // 回転
            var rot_matrix    = Matrix4.RotationZ( Angle );
            
            // 拡大率
            var scale_matrix  = Matrix4.Scale( Scaling );

            // 回転中心
            var center_matrix = Matrix4.Translation( -Center );
            
            // 合成した行列
            m_Transform = trans_matrix * rot_matrix * scale_matrix * center_matrix;
        }
        
        /// <summary>
        /// 表示位置
        /// </summary>
        public Vector2  Position;
        
        /// <summary>
        /// 色
        /// </summary>
        public Vector4  Color;
        
        /// <summary>
        /// 拡大率
        /// </summary>
        public Vector2  Scaling;
        
        /// <summary>
        /// 拡大・回転中心座標
        /// </summary>
        public Vector2  Center;
        
        /// <summary>
        /// 回転値(ラジアン値)
        /// </summary>
        public float    Angle;
    }
}
