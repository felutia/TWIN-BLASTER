
/*!
 *  @file       FrameSync.cs
 *  @brief      フレーム制御
 *  @author     Kazuya Maruyama
 *  @date       2013-2014/01/03
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using System.Diagnostics;

namespace aqua
{
    /// <summary>
    /// フレーム制御クラス
    /// </summary>
    public sealed class CFrameSync
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CFrameSync( )
        {
            // 標準フレームレート設定
            m_FrameRate = m_default_frame_rate;
            
            // ストップウォッチクラス生成
            m_Stopwatch = new Stopwatch( );
            
            // 測定開始
            m_Stopwatch.Start( );
            
            // 時間取得
            m_PrevTick = m_Stopwatch.ElapsedTicks;
        }
        
        /// <summary>
        /// フレーム制御
        /// 戻り値がtrueのとき更新可能
        /// </summary>
        public bool Sync( )
        {
            // 現在の時間取得
            long tick = m_Stopwatch.ElapsedTicks;
            
            float step_time = ( tick - m_PrevTick ) / (float)Stopwatch.Frequency;
            
            // 更新可能時間になった
            if( step_time >= ( 1.0f / m_FrameRate ) )
            {
                // 現在の時間を保存
                m_PrevTick = tick;
                
                return true;
            }
            
            return false;
        }
        
        /// <summary>
        /// フレームレートの取得と設定
        /// </summary>
        /// <value>
        /// フレームレート
        /// </value>
        public int FrameRate
        {
            get { return m_FrameRate; }
            set
            {
                // 最大最少制限
                if( value < 10 )
                    value = 10;
                else if( value > 60 )
                    value = 60;
                
                m_FrameRate = value;
            }
        }
        
        /// <summary>
        /// 標準フレームレート
        /// 30FPS
        /// </summary>
        private const int   m_default_frame_rate    = 30;
        
        /// <summary>
        /// 計測用ストップウォッチクラス
        /// </summary>
        private Stopwatch   m_Stopwatch             = null;
        
        /// <summary>
        /// フレームレート
        /// </summary>
        private int         m_FrameRate             = m_default_frame_rate;
        
        /// <summary>
        /// 前の時間
        /// </summary>
        private long        m_PrevTick              = 0;
    }
}
