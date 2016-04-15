
/*!
 *  @file       DrawObject3D.cs
 *  @brief      3D描画用オブジェクト
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
    /// 3D描画オブジェクトベースクラス
    /// </summary>
    public abstract class IDrawObject3D
        : IDrawObject
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IDrawObject3D( )
            : base( )
        {
            Position    = Vector3.Zero;
            Scaling     = Vector3.One;
            Axis        = m_default_axis;
            Angle       = 0.0f;
            Color       = Vector4.One;
        }
        
        /// <summary>
        /// トランスフォーム行列更新
        /// </summary>
        protected void UpdateTransform( )
        {
            // 位置
            var trans_matrix  = Matrix4.Translation( Position );
            
            // 回転
            var rot_matrix    = Matrix4.RotationAxis( Axis, Angle );
            
            // 拡大率
            var scale_matrix  = Matrix4.Scale( Scaling );
            
            // 合成した行列
            m_Transform = trans_matrix * rot_matrix * scale_matrix;
        }
        
        /// <summary>
        /// 描画オブジェクトリストに追加
        /// </summary>
        protected void Add( )
        {
            core.CDrawObject3DManager.GetInstance( ).Add( this );
        }
        
        /// <summary>
        /// 描画オブジェクトリストから削除
        /// </summary>
        protected void Remove( )
        {
            core.CDrawObject3DManager.GetInstance( ).Remove( this );
        }
        
        /// <summary>
        /// 標準回転軸
        /// </summary>
        private readonly Vector3    m_default_axis = new Vector3( 0.0f, 1.0f, 0.0f );
        
        /// <summary>
        /// 表示位置
        /// </summary>
        public Vector3              Position;
        
        /// <summary>
        /// 拡大率
        /// </summary>
        public Vector3              Scaling;
        
        /// <summary>
        /// 回転軸
        /// </summary>
        public Vector3              Axis;
        
        /// <summary>
        /// 回転値(ラジアン値)
        /// </summary>
        public float                Angle;
        
        /// <summary>
        /// 色
        /// </summary>
        public Vector4              Color;
    }
}
