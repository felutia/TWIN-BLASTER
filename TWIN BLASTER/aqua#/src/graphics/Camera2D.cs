
/*!
 *  @file       Camera2D.cs
 *  @brief      2Dカメラ
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
    /// 2Dカメラクラス
    /// </summary>
    public class CCamera2D
        : aqua.core.ICamera
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CCamera2D( )
        {
            // スクリーンの高さ取得
            float h = CGameFramework.ScreenHeight;
            
            // 視点設定
            eye = new Vector3( 0.0f, h, 0.0f );
            
            // 注視点設定
            at  = new Vector3( 0.0f, h, 1.0f );
            
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
            
            // プロジェクション行列
            m_ProjectionMatrix = Matrix4.Ortho( 0.0f, CGameFramework.ScreenWidth, 0.0f, CGameFramework.ScreenHeight, 0.0f, 1.0f );
        }
        
        /// <summary>
        /// 標準上方向
        /// </summary>
        private static Vector3    m_default_up    = new Vector3( 0.0f, -1.0f, 0.0f );
    }
}
