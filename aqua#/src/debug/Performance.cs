
/*!
 *  @file       PerformanceCounter.cs
 *  @brief      パフォーマンス測定
 *  @author     Kazuya Maruyama
 *  @date       2014/01/02
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using System;
using System.Diagnostics;
using System.Collections.Generic;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core;

namespace aqua
{
    public class CPerformanceData
    {
#if DEBUG
        /// <summary>
        /// 測定時間
        /// </summary>
        public long     time;
        
        /// <summary>
        /// ラベル名
        /// </summary>
        public string   label_name;
#endif
    }
    
    /// <summary>
    /// パフォーマンス測定クラス
    /// </summary>
    public class CPerformance
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CPerformance( )
        {
#if DEBUG
            m_Stopwatch = new Stopwatch( );
            
            m_Stopwatch.Start( );
            
            m_TimeList  = new List<CPerformanceData>( );
            
            visible   = true;
#endif
        }
        
        /// <summary>
        /// 解放
        /// </summary>
        public void Dispose( )
        {
#if DEBUG
            m_TimeList.Clear( );
#endif
        }
        
        /// <summary>
        /// 測定開始
        /// </summary>
        public void Begin( )
        {
#if DEBUG
            m_TimeList.Clear( );
            
            CPerformanceData d = new CPerformanceData( );
            
            d.time = m_Stopwatch.ElapsedTicks;
            
            m_TimeList.Add( d );
#endif
        }
        
        /// <summary>
        /// 測定終了
        /// </summary>
        public void End( )
        {
#if DEBUG
            if( visible == false )      return;
            if( m_TimeList.Count <= 0 ) return;
            
            string text = string.Format( "[FPS]{0:N0}\n", m_Fps );
            
            text += "[Process Performance]\n";
            
            long used_memory = System.GC.GetTotalMemory( false );
            
            text += string.Format( "{0, -20}={1:N0}byte\n", "Used Managed Memory", used_memory );
            
            float sum = 0.0f;
            
            float per = (float)Stopwatch.Frequency / (float)CGameFramework.FrameRate;
            
            for( int i = 0; i < m_TimeList.Count - 1; ++i )
            {
                float p = (m_TimeList[i + 1].time - m_TimeList[i].time) / per * 100.0f;
                
                sum += p;
                
                text += string.Format( "{0, -20}={1,6:N}%\n", m_TimeList[i].label_name, p );
            }
            
            text += string.Format( "{0, -20}={1,6:N}%\n", "Empty", 100.0f - sum );
            
            CDebugFont.WriteLine( text, Vector2.Zero );
#endif
        }
        
        /// <summary>
        /// 計測ラベル追加
        /// </summary>
        /// <param name='label_name'>
        /// ラベル名
        /// </param>
        public void AddLabel( string label_name )
        {
#if DEBUG
            m_TimeList[m_TimeList.Count - 1].label_name = label_name;
            
            CPerformanceData d = new CPerformanceData( );
            
            d.time = m_Stopwatch.ElapsedTicks;
            
            m_TimeList.Add( d );
#endif
        }
        
        /// <summary>
        /// FPS算出
        /// </summary>
        public void CalcFPS( )
        {
#if DEBUG
            ++m_FrameCount;
            
            long tick = m_Stopwatch.ElapsedTicks;
            
            if( tick - m_PrevTick >= Stopwatch.Frequency )
            {
                m_Fps = (float)m_FrameCount * Stopwatch.Frequency / ( tick - m_PrevTick );
                
                m_FrameCount = 0;
                
                m_PrevTick = m_Stopwatch.ElapsedTicks;
            }
#endif
        }
        
#if DEBUG
        /// <summary>
        /// 表示フラグ
        /// </summary>
        public bool                     visible         = false;
        
        /// <summary>
        /// ストップウォッチクラス
        /// </summary>
        private Stopwatch               m_Stopwatch     = null;
        
        /// <summary>
        /// ラップタイムリスト
        /// </summary>
        private List<CPerformanceData>  m_TimeList;
        
        /// <summary>
        /// 前の時間
        /// </summary>
        private long                    m_PrevTick      = 0;
        
        /// <summary>
        /// FPS
        /// </summary>
        private float                   m_Fps           = 0;
        
        /// <summary>
        /// フレームカウント
        /// </summary>
        private int                     m_FrameCount    = 0;
#endif
    }
}
