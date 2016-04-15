
/*!
 *  @file       Camera.cs
 *  @brief      カメラベース
 *  @author     Kazuya Maruyama
 *  @date       2013-2014/12/29
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using Sce.PlayStation.Core;

namespace aqua.core
{
    /// <summary>
    /// カメラベースクラス
    /// </summary>
    public abstract class ICamera
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ICamera( )
        {
            eye                 = Vector3.Zero;
            at                  = Vector3.Zero;
            up                  = Vector3.Zero;
            angle               = m_default_angle;
            near                = m_default_near;
            far                 = m_default_far;
            m_ViewMatrix        = Matrix4.Identity;
            m_ProjectionMatrix  = Matrix4.Identity;
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name='eye'>
        /// 視点
        /// </param>
        /// <param name='at'>
        /// 注視点
        /// </param>
        public ICamera( Vector3 eye, Vector3 at )
        {
            this.eye            = eye;
            this.at             = at;
            up                  = Vector3.Zero;
            angle               = m_default_angle;
            near                = m_default_near;
            far                 = m_default_far;
            m_ViewMatrix        = Matrix4.Identity;
            m_ProjectionMatrix  = Matrix4.Identity;
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name='eye'>
        /// 視点
        /// </param>
        /// <param name='at'>
        /// 注視点
        /// </param>
        /// <param name='angle'>
        /// 視野角(ラジアン値)
        /// </param>
        public ICamera( Vector3 eye, Vector3 at, float angle )
        {
            this.eye            = eye;
            this.at             = at;
            up                  = Vector3.Zero;
            this.angle          = angle;
            near                = m_default_near;
            far                 = m_default_far;
            m_ViewMatrix        = Matrix4.Identity;
            m_ProjectionMatrix  = Matrix4.Identity;
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public abstract void Update( );
        
        /// <summary>
        /// ビュー行列取得
        /// </summary>
        /// <value>
        /// ビュー行列
        /// </value>
        public Matrix4 ViewMatrix
        {
            get { return m_ViewMatrix; }
        }
        
        /// <summary>
        /// プロジェクション行列取得
        /// </summary>
        /// <value>
        /// プロジェクション行列
        /// </value>
        public Matrix4 ProjectionMatrix
        {
            get { return m_ProjectionMatrix; }
        }
        
        /// <summary>
        /// 視点
        /// </summary>
        public Vector3          eye;
        
        /// <summary>
        /// 注視点
        /// </summary>
        public Vector3          at;
            
        /// <summary>
        /// 上方向
        /// </summary>
        public Vector3          up;
        
        /// <summary>
        /// 視野角(ラジアン値)
        /// </summary>
        public float            angle;
        
        /// <summary>
        /// 視野範囲最小値
        /// </summary>
        public float            near;
        
        /// <summary>
        /// 視野範囲最大値
        /// </summary>
        public float            far;
        
        /// <summary>
        /// 標準視野角
        /// </summary>
        protected static float      m_default_angle = 45.0f;
        
        /// <summary>
        /// 標準視野範囲最少値
        /// </summary>
        protected static float      m_default_near  = 0.1f;
        
        /// <summary>
        /// 標準視野範囲最大値
        /// </summary>
        protected static float      m_default_far   = 1000000.0f;
        
        /// <summary>
        /// ビュー行列
        /// </summary>
        protected Matrix4           m_ViewMatrix;
        
        /// <summary>
        /// プロジェクション行列
        /// </summary>
        protected Matrix4           m_ProjectionMatrix;
    }
}
