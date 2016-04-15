
/*!
 *  @file       Camera3D.cs
 *  @brief      3Dカメラ
 *  @author     Kazuya Maruyama
 *  @date       2013-2014/12/29
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using Sce.PlayStation.Core;

namespace aqua
{
    /// <summary>
    /// 3Dカメラクラス
    /// </summary>
    public class CCamera3D
        : aqua.core.ICamera
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CCamera3D( )
            : base( )
        {
            // 上方向設定
            up  = m_default_up;
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
        public CCamera3D( Vector3 eye, Vector3 at )
            : base( eye, at )
        {
            // 上方向設定
            up  = m_default_up;
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
        /// 視野角
        /// </param>
        public CCamera3D( Vector3 eye, Vector3 at, float angle )
            : base( eye, at, angle )
        {
            // 上方向設定
            up  = m_default_up;
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public override void Update( )
        {
            // ビュー行列作成
            m_ViewMatrix = Matrix4.LookAt( eye, at, up );
            
            // プロジェクション行列作成
            m_ProjectionMatrix = Matrix4.Perspective( FMath.Radians( angle ), aqua.CGameFramework.ScreenAspectRatio, near, far );
        }
        
        /// <summary>
        /// 標準上方向
        /// </summary>
        protected static Vector3    m_default_up    = new Vector3( 0.0f, 1.0f, 0.0f );
    }
}
