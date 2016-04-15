
/*!
 *  @file       Motion.cs
 *  @brief      ジャイロ入力
 *  @author     Kazuya Maruyama
 *  @date       2013-2014/05/30
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Input;

namespace aqua
{
    /// <summary>
    /// ジャイロ判定クラス
    /// </summary>
    public static class CMotion
    {
        /// <summary>
        /// 更新
        /// </summary>
        public static void Update( )
        {
            try
            {
                // モーションデータ取得
                m_MotionData = Motion.GetData( 0 );
            }
            catch( Exception )
            {
                // モーションデータの取得が行われていない
                // もしくはモーションデータの取得設定がされていない
                // デフォルト値を返す
//                m_MotionData = new MotionData( );
            }
        }
        
        /// <summary>
        /// 加速度データ取得
        /// </summary>
        /// <value>
        /// 加速度データ
        /// </value>
        public static Vector3 Acceleration
        {
            get { return m_MotionData.Acceleration; }
        }
        
        /// <summary>
        /// 角加速データ取得
        /// </summary>
        /// <value>
        /// 角加速データ
        /// </value>
        public static Vector3 AngularVelocity
        {
            get { return m_MotionData.AngularVelocity; }
        }
        
        /// <summary>
        /// モーションデータ
        /// </summary>
        private static MotionData m_MotionData;
    }
}
